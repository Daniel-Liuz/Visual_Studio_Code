#
# 文件名: analyze_data.py (V3 - 最终调试版)
# 描述: 增加了对空词列表的检查，使程序更健壮。
#

import sys
import pandas as pd
import jieba
from wordcloud import WordCloud
from snownlp import SnowNLP
import os
import json
import traceback

# ==============================================================================
#  配置区域：使用我们测试成功的系统字体路径
# ==============================================================================
STOP_WORDS_FILE = r'E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler\docs\hit_stopwords.txt'

# 【重要】继续使用Windows系统自带的稳定字体
FONT_PATH = r'C:\Windows\Fonts\msyh.ttc'

POSITIVE_THRESHOLD = 0.6
NEGATIVE_THRESHOLD = 0.4
# ==============================================================================

jieba.setLogLevel(jieba.logging.INFO)


def log_progress(message):
    print(f"DEBUG: {message}", flush=True)


def report_error_and_exit(message, include_traceback=False):
    error_info = {"error": message}
    if include_traceback:
        error_info["traceback"] = traceback.format_exc()
    print(f"ANALYSIS_ERROR:{json.dumps(error_info, ensure_ascii=False)}", flush=True)
    sys.exit(1)


def analyze_sentiment(text):
    if pd.isna(text) or not str(text).strip():
        return "中性"
    try:
        score = SnowNLP(str(text)).sentiments
        if score >= POSITIVE_THRESHOLD:
            return "积极"
        elif score <= NEGATIVE_THRESHOLD:
            return "消极"
        else:
            return "中性"
    except Exception:
        return "中性"


def run_analysis(csv_file_path):
    try:
        log_progress("脚本启动，开始分析流程。")
        log_progress(f"接收到的文件路径参数: {csv_file_path}")

        if not os.path.exists(csv_file_path):
            report_error_and_exit(f"文件未找到: {csv_file_path}")

        log_progress("文件存在，准备使用pandas读取CSV。")
        df = pd.read_csv(csv_file_path, encoding='utf-8')
        log_progress(f"CSV读取成功，文件包含 {len(df)} 行。")

        # 性别分析
        log_progress("开始进行性别分析...")
        if 'sex' in df.columns:
            gender_counts = df['sex'].fillna('保密').value_counts().to_dict()
            log_progress("性别分析完成。")
        else:
            gender_counts = {"error": "'sex' 列未在CSV中找到。"}
            log_progress("警告: 未找到 'sex' 列。")

        # 情感分析
        log_progress("开始进行情感分析...")
        if 'content' in df.columns:
            log_progress("正在对 'content' 列应用情感分析函数...")
            df['sentiment'] = df['content'].apply(analyze_sentiment)
            log_progress("情感分析函数应用完成，开始统计结果...")
            sentiment_counts = df['sentiment'].value_counts().to_dict()
            log_progress("情感分析完成。")
        else:
            sentiment_counts = {"error": "'content' 列未在CSV中找到。"}
            log_progress("警告: 未找到 'content' 列。")

        # 词云生成
        wordcloud_image_path = ""
        log_progress("开始生成词云...")
        if 'content' in df.columns:
            text_for_wordcloud = " ".join(df['content'].dropna().astype(str))
            log_progress("已合并所有文本内容。")

            if not os.path.exists(STOP_WORDS_FILE):
                log_progress(f"警告: 停用词文件未找到于 {STOP_WORDS_FILE}")
                stopwords = set()
            else:
                with open(STOP_WORDS_FILE, 'r', encoding='utf-8') as f:
                    stopwords = {line.strip() for line in f}
                log_progress("停用词加载完成。")

            words = jieba.cut(text_for_wordcloud)
            filtered_words = [word for word in words if word.strip() and word not in stopwords and len(word) > 1]
            log_progress("分词和过滤完成。")

            # <<< 新增的最后一道保险 >>>
            if not filtered_words:
                log_progress("警告: 没有可用于生成词云的有效词语，将跳过词云生成。")
            elif not os.path.exists(FONT_PATH):
                # 再次检查字体路径，以防万一
                report_error_and_exit(f"关键错误: 字体文件未找到于 {FONT_PATH}")
            else:
                log_progress("准备初始化 WordCloud 对象...")
                wc = WordCloud(
                    font_path=FONT_PATH, width=800, height=400,
                    background_color='white', max_words=150, colormap='viridis'
                ).generate(" ".join(filtered_words))
                log_progress("词云对象生成成功。")

                wordcloud_image_path = os.path.join(os.path.dirname(csv_file_path), 'wordcloud_result.png')
                wc.to_file(wordcloud_image_path)
                log_progress(f"词云图片已保存至: {wordcloud_image_path}")

        # 准备最终输出
        final_results = {
            "gender_distribution": gender_counts,
            "sentiment_analysis": sentiment_counts,
            "wordcloud_image_path": os.path.abspath(
                wordcloud_image_path) if wordcloud_image_path and "Error" not in wordcloud_image_path else ""
        }

        log_progress("所有分析完成，正在打包最终结果...")
        print(f"ANALYSIS_RESULT:{json.dumps(final_results, ensure_ascii=False)}", flush=True)

    except Exception as e:
        report_error_and_exit(f"脚本在执行过程中发生意外的Python错误: {e}", include_traceback=True)


if __name__ == "__main__":
    if len(sys.argv) < 2:
        report_error_and_exit("未向 analyze_data.py 提供CSV文件路径。")

    input_csv_path = sys.argv[1]
    run_analysis(input_csv_path)

