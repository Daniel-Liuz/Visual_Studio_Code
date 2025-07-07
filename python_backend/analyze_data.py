#
# 文件名: analyze_data.py
# 描述: 接收一个爬虫生成的CSV文件路径，进行性别统计、情感分析和词云生成，
#       然后将所有分析结果以JSON格式输出，供C#程序调用。
#

import sys
import pandas as pd
import jieba
from wordcloud import WordCloud
from snownlp import SnowNLP
import os
import json

# ==============================================================================
#  配置区域：根据您的文件实际位置进行修改
# ==============================================================================

# 【重要】已根据您提供的新路径进行修改
# 使用 r'' 语法来确保Windows路径的反斜杠被正确识别
STOP_WORDS_FILE = r'E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler\docs\hit_stopwords.txt'

# 【重要】请确保您的字体文件也在此路径下
FONT_PATH = r'E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler\docs\STZHONGS.TTF'

# SnowNLP 情感分析阈值
POSITIVE_THRESHOLD = 0.6
NEGATIVE_THRESHOLD = 0.4

# ==============================================================================

# 设置jieba日志级别，避免打印过多信息
jieba.setLogLevel(jieba.logging.INFO)


def analyze_sentiment(text):
    """使用 SnowNLP 分析单条文本的情感"""
    # 如果文本为空或无效，则直接返回中性
    if pd.isna(text) or not str(text).strip():
        return "中性"
    try:
        # 创建SnowNLP对象并获取情感分数
        score = SnowNLP(str(text)).sentiments
        if score >= POSITIVE_THRESHOLD:
            return "积极"
        elif score <= NEGATIVE_THRESHOLD:
            return "消极"
        else:
            return "中性"
    except Exception:
        # 如果分析过程中出现任何异常，也归为中性
        return "中性"


def run_analysis(csv_file_path):
    """主分析函数，执行所有分析任务"""
    if not os.path.exists(csv_file_path):
        # 如果文件不存在，输出错误信息并退出
        print(json.dumps({"error": f"CSV file not found at: {csv_file_path}"}))
        sys.exit(1)

    # 使用 pandas 读取 CSV 文件
    try:
        df = pd.read_csv(csv_file_path, encoding='utf-8')
    except Exception as e:
        print(json.dumps({"error": f"Failed to read CSV file: {e}"}))
        sys.exit(1)

    # --- 1. 性别分析 ---
    # 检查 'sex' 列是否存在
    if 'sex' in df.columns:
        # 使用 value_counts() 统计每个性别的数量，并转换为字典
        # fillna('保密') 将所有空值填充为'保密'
        gender_counts = df['sex'].fillna('保密').value_counts().to_dict()
    else:
        gender_counts = {"error": "'sex' column not found in CSV."}

    # --- 2. 情感分析 ---
    # 检查 'content' 列是否存在
    if 'content' in df.columns:
        df['sentiment'] = df['content'].apply(analyze_sentiment)
        sentiment_counts = df['sentiment'].value_counts().to_dict()
    else:
        sentiment_counts = {"error": "'content' column not found in CSV."}

    # --- 3. 词云生成 ---
    wordcloud_image_path = ""
    if 'content' in df.columns:
        # 将所有非空的 'content' 列合并为一个长字符串
        text_for_wordcloud = " ".join(df['content'].dropna().astype(str))

        # 加载停用词
        stopwords = set()
        if os.path.exists(STOP_WORDS_FILE):
            with open(STOP_WORDS_FILE, 'r', encoding='utf-8') as f:
                stopwords = {line.strip() for line in f}

        # 使用 jieba 分词
        words = jieba.cut(text_for_wordcloud)

        # 过滤掉停用词、空字符串和单个字符的词
        filtered_words = [word for word in words if word.strip() and word not in stopwords and len(word) > 1]

        if filtered_words and os.path.exists(FONT_PATH):
            try:
                wc = WordCloud(
                    font_path=FONT_PATH,
                    width=800,
                    height=400,
                    background_color='white',
                    max_words=150,  # 增加词云图的词量
                    colormap='viridis'
                ).generate(" ".join(filtered_words))

                # 将词云图片保存在原CSV文件旁边，方便C#查找
                wordcloud_image_path = os.path.join(os.path.dirname(csv_file_path), 'wordcloud_result.png')
                wc.to_file(wordcloud_image_path)
            except Exception as e:
                # 如果生成词云失败（例如字体问题），记录错误但程序不中断
                wordcloud_image_path = f"Error creating wordcloud: {e}"

    # --- 4. 准备向 C# 输出的结果 ---
    # 将所有结果打包成一个字典
    final_results = {
        "gender_distribution": gender_counts,
        "sentiment_analysis": sentiment_counts,
        # 使用 os.path.abspath() 获取绝对路径，确保C#能准确找到图片
        "wordcloud_image_path": os.path.abspath(
            wordcloud_image_path) if wordcloud_image_path and "Error" not in wordcloud_image_path else ""
    }

    # 使用 JSON 格式打印到标准输出，这是与 C# 通信的关键。
    # 添加一个唯一的前缀 "ANALYSIS_RESULT:"，方便 C# 在输出流中精确捕获这一行。
    print(f"ANALYSIS_RESULT:{json.dumps(final_results, ensure_ascii=False)}")


if __name__ == "__main__":
    # 脚本的入口点
    # 期望从命令行接收第一个参数作为CSV文件路径
    if len(sys.argv) < 2:
        print(json.dumps({"error": "No CSV file path provided to analyze_data.py."}))
        sys.exit(1)

    # 获取C#传过来的CSV文件路径
    input_csv_path = sys.argv[1]
    run_analysis(input_csv_path)

