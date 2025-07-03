// 文件: WeiboCrawlerHelper.cs (已修正分隔符问题)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

public class WeiboCrawlerHelper
{
    // --- 配置区域保持不变，请确保路径正确 ---
    private const string PythonExePath = @"D:\Anaconda\envs\Mediacrawler\python.exe";
    private const string PythonProjectDir = @"E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler\";
    private const string CsvOutputDir = @"E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler\data\weibo\";

    // ... 其他代码保持不变 ...
    private readonly string _configFilePath;
    private readonly string _mainScriptPath;

    public WeiboCrawlerHelper()
    {
        if (!File.Exists(PythonExePath)) throw new FileNotFoundException($"找不到 Python 解释器。当前配置路径: '{PythonExePath}'");
        if (!Directory.Exists(PythonProjectDir)) throw new DirectoryNotFoundException($"找不到 Python 项目目录。当前配置路径: '{PythonProjectDir}'");
        _configFilePath = Path.Combine(PythonProjectDir, "config", "base_config.py");
        _mainScriptPath = Path.Combine(PythonProjectDir, "main.py");
        if (!File.Exists(_configFilePath)) throw new FileNotFoundException($"找不到配置文件 'config/base_config.py'。");
        if (!File.Exists(_mainScriptPath)) throw new FileNotFoundException($"找不到主脚本 'main.py'。");
    }

    public async Task<List<WeiboPost>> RunCrawlerAsync(string keywords, int maxNotesCount)
    {
        ModifyConfigFile(keywords, maxNotesCount);
        string output = await ExecutePythonScriptAsync();
        Debug.WriteLine("Python Script Output:\n" + output);
        string latestCsvFile = FindLatestCsvFile();
        if (string.IsNullOrEmpty(latestCsvFile)) throw new FileNotFoundException($"爬虫执行完毕，但在目录 '{CsvOutputDir}' 中找不到任何 CSV 结果文件。");
        return ReadCsvData(latestCsvFile);
    }

    private void ModifyConfigFile(string keywords, int maxNotesCount)
    {
        string content = File.ReadAllText(_configFilePath, Encoding.UTF8);
        content = Regex.Replace(content, @"^(\s*KEYWORDS\s*=\s*).*", $"$1\"{keywords}\"", RegexOptions.Multiline);
        content = Regex.Replace(content, @"^(\s*CRAWLER_MAX_NOTES_COUNT\s*=\s*).*", $"$1{maxNotesCount}", RegexOptions.Multiline);
        File.WriteAllText(_configFilePath, content, Encoding.UTF8);
    }

    private Task<string> ExecutePythonScriptAsync()
    {
        var tcs = new TaskCompletionSource<string>();
        var process = new Process { StartInfo = new ProcessStartInfo { FileName = PythonExePath, Arguments = $"\"{_mainScriptPath}\" --platform wb --lt phone --type search", WorkingDirectory = PythonProjectDir, UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true, StandardOutputEncoding = Encoding.UTF8, StandardErrorEncoding = Encoding.UTF8 }, EnableRaisingEvents = true };
        var outputBuilder = new StringBuilder();
        process.OutputDataReceived += (s, e) => { if (e.Data != null) outputBuilder.AppendLine(e.Data); };
        process.ErrorDataReceived += (s, e) => { if (e.Data != null) outputBuilder.AppendLine("ERROR: " + e.Data); };
        process.Exited += (s, e) => { tcs.SetResult(outputBuilder.ToString()); process.Dispose(); };
        try { process.Start(); process.BeginOutputReadLine(); process.BeginErrorReadLine(); } catch (Exception ex) { tcs.SetException(ex); }
        return tcs.Task;
    }

    private string FindLatestCsvFile()
    {
        var directory = new DirectoryInfo(CsvOutputDir);
        if (!directory.Exists) return null;
        return directory.GetFiles("*.csv").OrderByDescending(f => f.CreationTime).FirstOrDefault()?.FullName;
    }

    // [已修正] 修改这个方法中的 Delimiter
    private List<WeiboPost> ReadCsvData(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",", // ✅ 使用逗号作为分隔符
            HeaderValidated = null,
            MissingFieldFound = null,
        };
        using (var reader = new StreamReader(filePath, Encoding.UTF8))
        using (var csv = new CsvReader(reader, config))
        {
            return csv.GetRecords<WeiboPost>().ToList();
        }
    }
}
