using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;       // 确保添加了
using System.Net.Http.Json;  // 确保添加了

// >>>>> 这里应该放置你的数据模型类 <<<<<


// >>>>> 数据模型类放置区域结束 <<<<<


namespace WindowsFormsApp1 // <-- 你的命名空间声明应该在这里开始
{
    // 请求体数据模型
    

    public partial class NLP : Form
    {
        // >>>>> 这里放置 HttpClient 和 PythonApiUrl 声明 <<<<<
        private static readonly HttpClient client = new HttpClient();
        private static readonly string PythonApiUrl = "http://localhost:5000/extract_relations";
        // >>>>> 声明结束 <<<<<

        public NLP()
        {
            InitializeComponent();
        }

        // >>>>> 这里放置 btnExtract_Click 事件处理函数 <<<<<
        private async void btnExtract_Click(object sender, EventArgs e)
        {
            lbxRelations.Items.Clear(); // 清空 ListBox 中之前的显示内容
            string inputText = txtInputText.Text; // 从 txtInputText 获取用户输入的文本

            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("请输入要分析的文本。", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnExtract.Enabled = false; // 禁用按钮防止重复点击
            lbxRelations.Items.Add("正在提取人名和关系，请稍候..."); // 在ListBox中显示提示

            try
            {
                var requestBody = new RelationExtractionRequest { Text = inputText };

                HttpResponseMessage response = await client.PostAsJsonAsync(PythonApiUrl, requestBody);

                // 为了调试，读取原始JSON响应内容
                string rawJsonContent = await response.Content.ReadAsStringAsync();
                // 在Visual Studio的“输出”窗口可以看到这行打印信息
                Console.WriteLine($"--- C# 收到原始 JSON 响应 ---\n{rawJsonContent}\n--- 原始 JSON 响应结束 ---\n");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"请求失败，HTTP状态码: {response.StatusCode}\n错误详情: {rawJsonContent}", "API请求错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lbxRelations.Items.Add($"请求失败: {response.StatusCode} - {rawJsonContent}");
                    return;
                }

                // 尝试将JSON反序列化为C#对象
                RelationExtractionResponse apiResponse = null;
                try
                {
                    // 使用 System.Text.Json 进行反序列化。
                    // 即使你在数据模型中使用了[JsonPropertyName]，加上PropertyNameCaseInsensitive = true 也是一个好习惯，可以增加代码的健壮性。
                    apiResponse = System.Text.Json.JsonSerializer.Deserialize<RelationExtractionResponse>(rawJsonContent,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch (System.Text.Json.JsonException jsonEx)
                {
                    // 捕获JSON解析错误，这通常意味着C#数据模型和Python返回的JSON结构不匹配
                    lbxRelations.Items.Clear();
                    lbxRelations.Items.Add($"JSON解析错误: {jsonEx.Message}");
                    lbxRelations.Items.Add($"原始JSON内容(前500字符): {rawJsonContent.Substring(0, Math.Min(rawJsonContent.Length, 500))}...");
                    MessageBox.Show($"JSON解析错误: {jsonEx.Message}\n\n请检查 ApiDataModels.cs 文件中的类定义是否与后端返回的JSON结构完全匹配。\n原始JSON:\n{rawJsonContent}", "数据解析错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // 停止执行
                }

                lbxRelations.Items.Clear(); // 清空 "正在提取..." 的提示

                if (apiResponse != null)
                {
                    // --- 核心逻辑更新 ---
                    // 首先显示识别到的人物
                    if (apiResponse.ExtractedPeople != null && apiResponse.ExtractedPeople.Count > 0)
                    {
                        lbxRelations.Items.Add("--- 识别到的人物 ---");
                        foreach (var person in apiResponse.ExtractedPeople)
                        {
                            lbxRelations.Items.Add($"- {person}");
                        }
                    }
                    else
                    {
                        lbxRelations.Items.Add("未识别到任何人物。");
                    }

                    // 然后显示提取到的关系
                    if (apiResponse.ExtractedRelations != null && apiResponse.ExtractedRelations.Count > 0)
                    {
                        lbxRelations.Items.Add(""); // 添加一个空行，让显示更清晰
                        lbxRelations.Items.Add("--- 人物关系提取结果 ---");
                        foreach (var relation in apiResponse.ExtractedRelations)
                        {
                            lbxRelations.Items.Add($"- 人物A: {relation.Source}, 关系: {relation.Relation}, 人物B: {relation.Target}");
                        }
                    }
                    else
                    {
                        lbxRelations.Items.Add(""); // 添加一个空行
                        lbxRelations.Items.Add("未提取到任何人物关系。");
                    }

                    // 如果后端报告了非“success”状态，也显示出来
                    if (apiResponse.Status != "success" && !string.IsNullOrEmpty(apiResponse.Error))
                    {
                        lbxRelations.Items.Add($"后端报告错误: {apiResponse.Error}");
                        MessageBox.Show($"后端报告错误: {apiResponse.Error}", "后端错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    lbxRelations.Items.Add("未能从后端获取有效响应（反序列化结果为空）。");
                }
            }
            catch (HttpRequestException ex)
            {
                lbxRelations.Items.Clear();
                lbxRelations.Items.Add($"网络请求错误: {ex.Message}");
                lbxRelations.Items.Add("请确保Python后端服务正在运行在 http://localhost:5000，并且网络连接正常。");
                MessageBox.Show($"网络请求错误: {ex.Message}\n请确保Python后端服务正在运行在 http://localhost:5000", "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // 捕获所有其他未知错误
            {
                lbxRelations.Items.Clear();
                lbxRelations.Items.Add($"发生未知错误: {ex.Message}");
                MessageBox.Show($"发生未知错误: {ex.Message}", "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExtract.Enabled = true; // 无论成功失败，最后都重新启用按钮
            }
        }
        // >>>>> 事件处理函数结束 <<<<<
    }
}
