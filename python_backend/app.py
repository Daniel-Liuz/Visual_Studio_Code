from flask import Flask, request, jsonify
from transformers import AutoTokenizer, AutoModelForCausalLM, pipeline
import torch
import os
import re

app = Flask(__name__)

# --- 模型加载配置 ---
MODEL_PATH = "E:/DesignThinking/model/7B" # 你的当前路径

model = None
tokenizer = None
text_generator = None

def load_model():
    """
    加载模型和 tokenizer。
    """
    global model, tokenizer, text_generator
    if model is not None and tokenizer is not None:
        print("模型已加载，跳过重复加载。")
        return

    print(f"正在加载模型: {MODEL_PATH}")
    try:
        if not os.path.exists(MODEL_PATH):
            raise FileNotFoundError(f"模型路径不存在: {MODEL_PATH}")

        tokenizer = AutoTokenizer.from_pretrained(MODEL_PATH)
        model = AutoModelForCausalLM.from_pretrained(
            MODEL_PATH,
            device_map="auto",
            torch_dtype=torch.float16,
            trust_remote_code=True
        )
        model.eval()

        text_generator = pipeline(
            "text-generation",
            model=model,
            tokenizer=tokenizer,
            torch_dtype=torch.float16 if torch.cuda.is_available() else torch.float32,
            # 移除任何可能导致警告的额外生成参数，如 temperature, top_p
            # 如果你有自定义的 generation_config.json 文件，也可能需要检查
            # 这里的目的是让pipeline使用模型的默认或最兼容的参数
        )
        print("模型加载成功！")
    except Exception as e:
        print(f"\n加载模型失败: {e}")
        print(f"请检查模型路径 '{MODEL_PATH}' 是否正确，文件是否完整，以及硬件（特别是显存）是否足够加载模型（即使是 float16）。")
        model = None
        tokenizer = None
        text_generator = None

# --- 关系提取核心逻辑 ---
def extract_relations_from_text(input_text: str) -> dict:
    """
    使用DeepSeek模型从文本中提取人物关系和所有识别到的人名。
    此函数包含提示词工程和模型输出解析逻辑。
    """
    if text_generator is None:
        print("模型未加载，无法进行推理。")
        return {"people": [], "relations": []}

    # --- 提示词工程 (Prompt Engineering) ---
    max_tokens = tokenizer.model_max_length if tokenizer.model_max_length is not None else 4096
    encoded_input = tokenizer.encode(input_text, add_special_tokens=False)
    # 留出足够的tokens给生成内容（包括可能的<think>和答案）
    if len(encoded_input) > max_tokens - 500: # 留出500 tokens给生成内容和prompt
        print(f"警告：输入文本过长 ({len(encoded_input)} tokens)，已截断至约 {max_tokens - 500} tokens。")
        input_text = tokenizer.decode(encoded_input[:max_tokens - 500])


    messages = [
        {"role": "system", "content": "你是一个专业的人物关系提取助手。你需要从文本中识别所有提到的人名，然后判断这些人之间是否存在明确的关系。"},
        {"role": "user", "content": f"请从以下文本中提取所有人物名称及其之间存在的明确关系。\n\n文本：{input_text}\n\n请先列出所有识别到的人名，用逗号分隔。然后，在单独一行，以 '人物A - 关系 - 人物B' 的格式输出所有明确的关系，每条关系用分号分隔。如果有多条关系，请用分号分隔。如果文本中没有人名或没有明确关系，请分别输出'无人物'或'无关系'。\n\n示例：\n人名：张三, 李四, 王五\n人物关系：张三 - 朋友 - 李四; 李四 - 同事 - 王五\n\n你的输出："}
    ]

    try:
        formatted_prompt = tokenizer.apply_chat_template(messages, tokenize=False, add_generation_prompt=True)
        print(f"\n--- 发送给模型的完整提示词（formatted_prompt）---\n{formatted_prompt}\n--- 提示词结束 ---\n")

    except Exception as e:
        print(f"tokenizer.apply_chat_template出错 ({e})，回退到普通prompt。")
        formatted_prompt = (
            f"从以下文本中提取所有人物名称及其之间存在的明确关系。首先列出所有识别到的人名，用逗号分隔。然后，在单独一行，以 '人物A - 关系 - 人物B' 的格式输出所有明确的关系，每条关系用分号分隔。如果没有人名，请回答 '无人物'。如果没有明确关系，请回答 '无关系'。\n\n文本：{input_text}\n\n人名：")
        print(f"\n--- 发送给模型的后备提示词（formatted_prompt）---\n{formatted_prompt}\n--- 提示词结束 ---\n")


    # --- 模型推理 ---
    try:
        outputs = text_generator(
            formatted_prompt,
            max_new_tokens=700, # 进一步增加max_new_tokens，确保模型有足够空间生成完整的回答，包括思考过程
            do_sample=False,
            num_return_sequences=1,
        )

        generated_text = outputs[0]['generated_text']
        print(f"\n--- 模型原始生成文本（generated_text）---\n{generated_text}\n--- 模型原始生成文本结束 ---\n")


        # --- 模型输出的后处理与解析 ---
        actual_llm_response = ""
        # 优化response_text的提取：寻找最后一个 '<｜Assistant｜>' 或 '你的输出：' 后的内容
        # 优先查找 <｜Assistant｜> 后的内容
        assistant_marker = "<｜Assistant｜>"
        # combined_marker = "<｜Assistant｜><think>" # 也可以尝试精确匹配这个
        output_prompt_marker = "你的输出："


        if output_prompt_marker in generated_text:
             # 如果找到了“你的输出：”，直接从它之后开始取
             actual_llm_response = generated_text.split(output_prompt_marker, 1)[-1].strip()
        elif assistant_marker in generated_text:
            # 如果没有“你的输出：”，但有 <｜Assistant｜>，则从它之后开始取
            # 这里可能会包含 <think> 和 <eot>，需要在后续处理中清除
            parts = generated_text.split(assistant_marker, 1)
            if len(parts) > 1:
                actual_llm_response = parts[1].strip()
        else:
            # 最坏情况：没有找到任何明确的标记，直接去除原始formatted_prompt
            # 这可能仍然不够鲁棒，但作为后备
            if generated_text.startswith(formatted_prompt):
                actual_llm_response = generated_text[len(formatted_prompt):].strip()
            else:
                actual_llm_response = generated_text.strip()


        # 清理可能的特殊标记，如 <think> 和 <eot>
        actual_llm_response = re.sub(r'<\|begin of sentence\|>|<\|end of sentence\|>|<think>|<eot>', '', actual_llm_response).strip()
        # 清理模型可能在回答前重复的"人名："等
        if actual_llm_response.startswith("人名：人名："):
            actual_llm_response = actual_llm_response[len("人名："):].strip()


        print(f"\n--- 经过提取和清理后的实际LLM响应文本（actual_llm_response）---\n{actual_llm_response}\n--- 实际LLM响应文本结束 ---\n")


        # 初始化结果
        extracted_people = []
        extracted_relations = []

        # 尝试解析人名和关系
        # 我们现在期望 `actual_llm_response` 已经是模型纯粹的回答部分
        # 再次尝试用更健壮的正则表达式匹配来提取
        people_line_match = re.search(r'人名：(.*?)(?:\n|$)', actual_llm_response, re.DOTALL)
        relations_line_match = re.search(r'关系：(.*?)(?:\n|$)', actual_llm_response, re.DOTALL) # 修改为匹配“关系：”


        people_raw = people_line_match.group(1).strip() if people_line_match else ""
        relations_raw = relations_line_match.group(1).strip() if relations_line_match else ""

        print(f"解析到的人名原始行（处理后）: '{people_raw}'")
        print(f"解析到的关系原始行（处理后）: '{relations_raw}'")

        # 解析人名
        if people_raw and "无人物" not in people_raw and people_raw != "无关系": # 增加对“无关系”的排除
            extracted_people = [p.strip() for p in people_raw.split(',') if p.strip()]
        else:
            print("模型表示未提取到人名或人名输出为空/无效。")

        # 解析关系
        if relations_raw and "无关系" not in relations_raw and relations_raw != "无人物": # 增加对“无人物”的排除
            pattern = re.compile(r'([\w\s\u4e00-\u9fa5]+)\s*-\s*([\w\s\u4e00-\u9fa5]+)\s*-\s*([\w\s\u4e00-\u9fa5]+)')
            relation_segments = relations_raw.split(';')
            for segment in relation_segments:
                match = pattern.search(segment.strip())
                if match:
                    source = match.group(1).strip()
                    relation_type = match.group(2).strip()
                    target = match.group(3).strip()
                    if source and relation_type and target:
                        extracted_relations.append({
                            "source": source,
                            "relation": relation_type,
                            "target": target
                        })
        else:
            print("模型表示未提取到关系或关系输出为空/无效。")

        print(f"最终提取人名列表: {extracted_people}")
        print(f"最终提取关系列表: {extracted_relations}")

        return {"people": extracted_people, "relations": extracted_relations}

    except Exception as e:
        print(f"推理或解析过程中发生错误: {e}")
        return {"people": [], "relations": []}


# --- Flask API 路由定义 ---
@app.route('/extract_relations', methods=['POST'])
def handle_extract_relations():
    """
    处理来自C#前端的HTTP POST请求，执行关系提取。
    """
    if model is None or tokenizer is None or text_generator is None:
        return jsonify({"error": "模型未加载成功，请检查后端日志。", "status": "model_not_ready"}), 503

    data = request.get_json()
    if not data or 'text' not in data:
        return jsonify({"error": "请求体中缺少 'text' 字段。"}), 400

    input_text = data['text']
    print(f"收到请求，文本长度：{len(input_text)}字符。")

    try:
        result = extract_relations_from_text(input_text)
        return jsonify({
            "original_text": input_text,
            "extracted_people": result.get("people", []),
            "extracted_relations": result.get("relations", []),
            "status": "success"
        }), 200
    except Exception as e:
        print(f"处理请求时发生错误: {e}")
        return jsonify({"error": f"后端处理请求失败: {str(e)}", "status": "processing_error"}), 500

# --- 应用启动入口 ---
if __name__ == '__main__':
    load_model()
    app.run(host='0.0.0.0', port=5000, debug=True)

