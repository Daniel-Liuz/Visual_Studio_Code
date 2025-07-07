// BiliAnalysisService.cs (已为 Form1 修正)

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

// 确保这个命名空间与你的项目一致，例如 WindowsFormsApp1 或 BiliCommentAnalysis
namespace BiliCommentAnalysis
{
    public class BiliAnalysisService
    {
        // ... 配置区保持不变 ...
        private const string PythonProjectPath = @"E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler";
        private const string ConfigFilePath = @"E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler\config\base_config.py";
        private const string AnalyzeScriptPath = @"E:\Visual Studio programs\python_backend\analyze_data.py";
        private const string PythonExecutablePath = @"D:\Anaconda\envs\Mediacrawler\python.exe";

        public Action<string> OnProgressUpdate;

        // ======================= 第1处关键修正 =======================
        // 声明一个私有字段来存储主窗体的引用，类型为 Form1
        private readonly Form1 _form;

        // ======================= 第2处关键修正 =======================
        // 构造函数，接收一个 Form1 类型的参数
        public BiliAnalysisService(Form1 mainForm)
        {
            _form = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
        }

        // 主执行流程
        public async Task<AnalysisResult> PerformFullAnalysisAsync(string keyword)
        {
            // 步骤 0: 清理上一次的数据
            OnProgressUpdate?.Invoke("0/6: 正在准备工作空间...");
            // 构造出需要被清空的 "data/bilibili" 文件夹的完整路径
            string dataDirToClear = Path.Combine(PythonProjectPath, "data", "bilibili");
            await ClearDirectoryAsync(dataDirToClear);
            // 1. 更新配置文件
            OnProgressUpdate?.Invoke("1/6: 更新配置文件...");
            await UpdateKeywordsInConfigAsync(keyword);

            // 2. 运行爬虫
            OnProgressUpdate?.Invoke("2/6: 正在运行爬虫 (若弹出二维码窗口请扫码登录)...");
            await RunPythonScriptAsync("main.py", "--platform bili --lt qrcode --type search", PythonProjectPath);

            // 3. 查找数据文件
            OnProgressUpdate?.Invoke("3/6: 正在查找数据文件...");
            string csvFilePath = FindLatestCsvFile();
            if (string.IsNullOrEmpty(csvFilePath))
            {
                throw new FileNotFoundException("爬虫执行完毕，但未找到生成的CSV数据文件。");
            }

            // 4. 加载评论并更新UI表格
            OnProgressUpdate?.Invoke("4/6: 正在加载评论列表并更新表格...");
            var comments = await LoadCommentsFromCsvAsync(csvFilePath);
            UpdateCommentsDataGridView(comments);

            // 5. 运行分析脚本
            OnProgressUpdate?.Invoke("5/6: 正在分析数据...");
            AnalysisResult analysisResult = await RunAnalysisScriptAndGetResultAsync(csvFilePath);
            if (analysisResult == null)
            {
                throw new Exception("数据分析脚本执行失败或未返回有效结果。");
            }

            analysisResult.Comments = comments;

            OnProgressUpdate?.Invoke("分析完成！");
            return analysisResult;
        }

        // 更新关键词
        private async Task UpdateKeywordsInConfigAsync(string newKeyword)
        {
            string content = await Task.Run(() => File.ReadAllText(ConfigFilePath, Encoding.UTF8));
            string pattern = @"(KEYWORDS\s*=\s*)"".*?""";
            string replacement = $"$1\"{newKeyword}\"";
            string newContent = Regex.Replace(content, pattern, replacement);
            await Task.Run(() => File.WriteAllText(ConfigFilePath, newContent, Encoding.UTF8));
        }

        // 查找最新的CSV文件
        private string FindLatestCsvFile()
        {
            string dataDir = Path.Combine(PythonProjectPath, "data", "bilibili");
            if (!Directory.Exists(dataDir)) return null;

            var directoryInfo = new DirectoryInfo(dataDir);

            var filePattern = $"*_search_comments_{DateTime.Now:yyyy-MM-dd}.csv";
            var latestFile = directoryInfo.GetFiles(filePattern)
                                          .OrderByDescending(f => f.LastWriteTime)
                                          .FirstOrDefault();
            if (latestFile != null) return latestFile.FullName;

            filePattern = $"*_search_comments_{DateTime.Now.AddDays(-1):yyyy-MM-dd}.csv";
            latestFile = directoryInfo.GetFiles(filePattern)
                                      .OrderByDescending(f => f.LastWriteTime)
                                      .FirstOrDefault();
            return latestFile?.FullName;
        }

        // 运行分析脚本
        private async Task<AnalysisResult> RunAnalysisScriptAndGetResultAsync(string csvFilePath)
        {
            string output = await RunPythonScriptAsync(AnalyzeScriptPath, $"\"{csvFilePath}\"", AppDomain.CurrentDomain.BaseDirectory);
            string jsonLine = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                    .FirstOrDefault(line => line.Trim().StartsWith("ANALYSIS_RESULT:"));

            if (jsonLine != null)
            {
                string json = jsonLine.Substring("ANALYSIS_RESULT:".Length);
                return JsonConvert.DeserializeObject<AnalysisResult>(json);
            }
            return null;
        }

        // 从CSV加载评论
        private async Task<List<Comment>> LoadCommentsFromCsvAsync(string filePath)
        {
            var comments = new List<Comment>();
            var lines = await Task.Run(() => File.ReadAllLines(filePath, Encoding.UTF8));

            foreach (var line in lines.Skip(1))
            {
                var columns = line.Split(',');
                if (columns.Length >= 8)
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

        // 更新UI表格
        public void UpdateCommentsDataGridView(List<Comment> comments)
        {
            // 现在 _form.xxx 的调用是完全合法的，因为 _form 就是 Form1
            _form.statusLabel.Text = "正在将评论数据绑定到表格...";
            _form.commentsDataGridView.DataSource = comments;
            _form.commentsDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            _form.statusLabel.Text = "评论列表加载完成。";
        }

        // 运行Python脚本的通用方法
        private Task<string> RunPythonScriptAsync(string scriptName, string arguments, string workingDirectory)
        {
            var tcs = new TaskCompletionSource<string>();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = PythonExecutablePath,
                    Arguments = $"\"{scriptName}\" {arguments}",
                    WorkingDirectory = workingDirectory,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8,
                },
                EnableRaisingEvents = true
            };

            process.StartInfo.EnvironmentVariables["PYTHONIOENCODING"] = "UTF-8";

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

                if (process.ExitCode == 0 && !standardOutput.Contains("ANALYSIS_ERROR:"))
                {
                    tcs.SetResult(standardOutput);
                }
                else
                {
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
        private async Task ClearDirectoryAsync(string directoryPath)
        {
            // 安全检查 #1: 确认目录存在，如果不存在，就没必要继续了。
            if (!Directory.Exists(directoryPath))
            {
                OnProgressUpdate?.Invoke($"信息：目录 '{Path.GetFileName(directoryPath)}' 不存在，无需清理。");
                return;
            }

            // 安全检查 #2: 防止灾难！确保路径看起来是正确的，而不是像 "C:\" 这样的根目录。
            // 我们检查路径是否包含我们项目的特定文件夹名。
            if (!directoryPath.Contains("MediaCrawler") || !directoryPath.Contains("data"))
            {
                // 如果路径看起来不对，我们立即抛出异常，停止一切操作。
                throw new InvalidOperationException(
                    $"安全检查失败！目标删除路径 '{directoryPath}' 看起来不正确，操作已取消。");
            }

            OnProgressUpdate?.Invoke($"正在清空目录: {Path.GetFileName(directoryPath)}...");

            // 使用 Task.Run 将所有同步的文件操作放到后台线程，防止UI冻结
            await Task.Run(() =>
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

                // 遍历并删除所有文件
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                // 遍历并删除所有子文件夹（以及子文件夹里的所有内容）
                foreach (DirectoryInfo subDir in directoryInfo.GetDirectories())
                {
                    subDir.Delete(true); // 参数 "true" 表示递归删除
                }
            });

            OnProgressUpdate?.Invoke("旧数据清理完成。");
        }
    }

    // ... 数据模型类保持不变 ...
    public class AnalysisResult
    {
        [JsonProperty("gender_distribution")]
        public Dictionary<string, int> GenderDistribution { get; set; }

        [JsonProperty("sentiment_analysis")]
        public Dictionary<string, int> SentimentAnalysis { get; set; }

        [JsonProperty("wordcloud_image_path")]
        public string WordcloudImagePath { get; set; }

        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public string Nickname { get; set; }
        public string Sex { get; set; }
        public string Content { get; set; }
    }
}
