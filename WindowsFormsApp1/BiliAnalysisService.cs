// BiliAnalysisService.cs

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BiliCommentAnalysis
{
    // 这个类负责处理所有与Python脚本的交互
    public class BiliAnalysisService
    {
        // ================== 配置区：你的Python项目路径 ==================
        private const string PythonProjectPath = @"E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler";
        private const string ConfigFilePath = @"E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler\config\base_config.py";
        private const string AnalyzeScriptPath = @"E:\Visual Studio programs\python_backend\analyze_data.py";
        // =================================================================

        // Action<string> 是一个委托，可以让我们从这个类向UI层实时报告进度
        public Action<string> OnProgressUpdate;

        // 主执行流程
        public async Task<AnalysisResult> PerformFullAnalysisAsync(string keyword)
        {
            // 1. 修改 Python 配置文件
            OnProgressUpdate?.Invoke("1/5: 更新配置文件...");
            await UpdateKeywordsInConfigAsync(keyword);

            // 2. 运行爬虫脚本 (这可能需要一些时间)
            OnProgressUpdate?.Invoke("2/5: 正在运行爬虫 (请在弹出的二维码窗口登录)...");
            // 注意：这里需要确保你的Python环境能被系统找到
            await RunPythonScriptAsync("main.py", "--platform bili --lt qrcode --type search", PythonProjectPath);

            // 3. 找到生成的CSV文件
            OnProgressUpdate?.Invoke("3/5: 查找数据文件...");
            string csvFilePath = FindLatestCsvFile();
            if (string.IsNullOrEmpty(csvFilePath))
            {
                throw new FileNotFoundException("爬虫执行完毕，但未找到生成的CSV数据文件。请检查爬虫是否成功。");
            }

            // 4. 运行分析脚本并获取结果
            OnProgressUpdate?.Invoke("4/5: 正在分析数据...");
            AnalysisResult analysisResult = await RunAnalysisScriptAndGetResultAsync(csvFilePath);
            if (analysisResult == null)
            {
                throw new Exception("数据分析脚本执行失败或未返回有效结果。");
            }

            // 5. 加载CSV中的评论数据
            OnProgressUpdate?.Invoke("5/5: 正在加载评论列表...");
            analysisResult.Comments = await LoadCommentsFromCsvAsync(csvFilePath);

            OnProgressUpdate?.Invoke("分析完成！");
            return analysisResult;
        }

        // 功能1: 异步更新配置文件中的关键词
        private async Task UpdateKeywordsInConfigAsync(string newKeyword)
        {
            // 使用 Task.Run 将同步的文件读取操作放到后台线程
            string content = await Task.Run(() => File.ReadAllText(ConfigFilePath, Encoding.UTF8));

            string pattern = @"(KEYWORDS\s*=\s*)"".*?""";
            string replacement = $"$1\"{newKeyword}\"";
            string newContent = Regex.Replace(content, pattern, replacement);

            // 使用 Task.Run 将同步的文件写入操作放到后台线程
            await Task.Run(() => File.WriteAllText(ConfigFilePath, newContent, Encoding.UTF8));
        }

        // 查找爬虫生成的最新CSV文件
        private string FindLatestCsvFile()
        {
            string dataDir = Path.Combine(PythonProjectPath, "data", "bilibili");
            if (!Directory.Exists(dataDir)) return null;

            // 根据你的文件命名规则 1_search_comments_YYYY-MM-DD.csv 查找
            // 为了避免时区问题，我们检查今天和昨天的文件
            string todayString = DateTime.Now.ToString("yyyy-MM-dd");
            var todayFile = Directory.GetFiles(dataDir, $"1_search_comments_{todayString}.csv").FirstOrDefault();
            if (todayFile != null) return todayFile;

            string yesterdayString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            var yesterdayFile = Directory.GetFiles(dataDir, $"1_search_comments_{yesterdayString}.csv").FirstOrDefault();
            return yesterdayFile;
        }

        // 运行分析脚本并解析其JSON输出
        private async Task<AnalysisResult> RunAnalysisScriptAndGetResultAsync(string csvFilePath)
        {
            // 将分析脚本和其参数（CSV文件路径）传递给Python解释器
            string output = await RunPythonScriptAsync(AnalyzeScriptPath, $"\"{csvFilePath}\"", AppDomain.CurrentDomain.BaseDirectory);

            // 从Python脚本的所有输出中，找到我们约定的 "ANALYSIS_RESULT:" 开头的那一行
            string jsonLine = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                    .FirstOrDefault(line => line.Trim().StartsWith("ANALYSIS_RESULT:"));

            if (jsonLine != null)
            {
                // 提取JSON部分并反序列化为C#对象
                string json = jsonLine.Substring("ANALYSIS_RESULT:".Length);
                return JsonConvert.DeserializeObject<AnalysisResult>(json);
            }
            return null;
        }

        // 从CSV文件加载评论数据
        private async Task<List<Comment>> LoadCommentsFromCsvAsync(string filePath)
        {
            var comments = new List<Comment>();
            // 使用 Task.Run 将同步的文件读取操作放到后台线程
            var lines = await Task.Run(() => File.ReadAllLines(filePath, Encoding.UTF8));

            // 跳过表头，从第二行开始处理
            foreach (var line in lines.Skip(1))
            {
                // 这是一个基础的CSV解析，如果你的评论内容中包含逗号，会导致解析错误。
                // 若要提高健壮性，应使用专门的CSV库如 CsvHelper。
                var columns = line.Split(',');
                if (columns.Length >= 8) // 确保列数足够
                {
                    comments.Add(new Comment
                    {
                        Nickname = columns[6],
                        Sex = columns[7],
                        Content = columns[4]
                    });
                }
            }
            return comments;
        }

        // 运行Python脚本的通用异步方法
        private Task<string> RunPythonScriptAsync(string scriptName, string arguments, string workingDirectory)
        {
            var tcs = new TaskCompletionSource<string>();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {


                    FileName = @"D:\Anaconda\envs\Mediacrawler\python.exe", // 确保 "python" 在你的系统环境变量 PATH 中
                    Arguments = $"\"{scriptName}\" {arguments}",
                    WorkingDirectory = workingDirectory,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true, // 对于main.py，你可能需要设为 false 来看二维码
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8,
                },
                EnableRaisingEvents = true
            };
            // <<< 新增的关键代码：强制Python进程使用UTF-8编码 >>>
            process.StartInfo.EnvironmentVariables["PYTHONIOENCODING"] = "UTF-8";
            // 对于爬虫脚本，我们需要看到浏览器/二维码窗口，所以不隐藏它
            if (scriptName.Contains("main.py"))
            {
                process.StartInfo.CreateNoWindow = false;
            }

            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            process.OutputDataReceived += (s, e) => { if (e.Data != null) outputBuilder.AppendLine(e.Data); };
            process.ErrorDataReceived += (s, e) => { if (e.Data != null) errorBuilder.AppendLine(e.Data); };

            process.Exited += (s, e) =>
            {
                string errorOutput = errorBuilder.ToString();
                string standardOutput = outputBuilder.ToString();

                // <<< 关键修改在这里 >>>
                // 即使成功退出(ExitCode == 0)，我们也要检查输出中是否包含我们自定义的错误标识
                if (process.ExitCode == 0 && !standardOutput.Contains("ANALYSIS_ERROR:"))
                {
                    tcs.SetResult(standardOutput);
                }
                else
                {
                    // 将所有捕获到的信息（标准输出、标准错误）都打包到异常里
                    string fullErrorMessage = $"Python 脚本 '{scriptName}' 执行失败 (代码: {process.ExitCode}).\n\n" +
                                              $"--- 错误流 (Standard Error) ---\n{errorOutput}\n\n" +
                                              $"--- 输出流 (Standard Output) ---\n{standardOutput}";

                    tcs.SetException(new Exception(fullErrorMessage));
                }
                process.Dispose();
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return tcs.Task;
        }
    }


    // ================== 数据模型类 ==================

    // 用于反序列化Python分析结果的JSON
    public class AnalysisResult
    {
        [JsonProperty("gender_distribution")]
        public Dictionary<string, int> GenderDistribution { get; set; }

        [JsonProperty("sentiment_analysis")]
        public Dictionary<string, int> SentimentAnalysis { get; set; }

        [JsonProperty("wordcloud_image_path")]
        public string WordcloudImagePath { get; set; }

        // 我们额外添加一个属性来存放从CSV加载的评论
        public List<Comment> Comments { get; set; }
    }

    // 用于在DataGridView中展示的评论对象
    public class Comment
    {
        public string Nickname { get; set; }
        public string Sex { get; set; }
        public string Content { get; set; }
    }
}