using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq; // 确保包含 Linq 命名空间以便使用 Take() 方法
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

public class WeiboCrawlerHelper
{
    // --- 配置区域保持不变 ---
    private const string PythonExePath = @"D:\Anaconda\envs\Mediacrawler\python.exe";
    private const string PythonProjectDir = @"E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler\";
    private const string CsvOutputDir = @"E:\Project_Collection\2023-2024项目合集\市场调研大赛\爬虫\Mediacrawler\MediaCrawler\data\weibo\";

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
        // 1. 在每次爬取前清理旧的 CSV 文件，确保数据干净
        ClearPreviousCsvFiles();

        // 2. 修改 Python 配置文件中的关键词和最大笔记数量
        ModifyConfigFile(keywords, maxNotesCount);

        // 3. 执行 Python 脚本
        string output = await ExecutePythonScriptAsync();
        Debug.WriteLine("Python Script Output:\n" + output);

        // 4. 查找最新的 CSV 结果文件
        string latestCsvFile = FindLatestCsvFile();
        if (string.IsNullOrEmpty(latestCsvFile))
        {
            throw new FileNotFoundException($"爬虫执行完毕，但在目录 '{CsvOutputDir}' 中找不到任何 CSV 结果文件。");
        }

        // 5. 读取 CSV 数据
        List<WeiboPost> allPosts = ReadCsvData(latestCsvFile);

        // 【核心修改】6. 根据 maxNotesCount 截断数据
        // 如果实际爬取到的数据量大于用户设定的上限，则只取前 maxNotesCount 条
        if (allPosts.Count > maxNotesCount)
        {
            Debug.WriteLine($"实际爬取到 {allPosts.Count} 条数据，将截断至用户设定的 {maxNotesCount} 条。");
            return allPosts.Take(maxNotesCount).ToList();
        }
        else
        {
            Debug.WriteLine($"实际爬取到 {allPosts.Count} 条数据，未超过用户设定的 {maxNotesCount} 条。");
            return allPosts;
        }
    }

    /// <summary>
    /// 清理指定输出目录下所有旧的 .csv 文件。
    /// </summary>
    private void ClearPreviousCsvFiles()
    {
        if (!Directory.Exists(CsvOutputDir))
        {
            Debug.WriteLine($"CSV 输出目录 '{CsvOutputDir}' 不存在，无需清理。");
            return;
        }

        try
        {
            DirectoryInfo directory = new DirectoryInfo(CsvOutputDir);
            foreach (FileInfo file in directory.GetFiles("*.csv"))
            {
                file.Delete(); // 删除文件
                Debug.WriteLine($"已删除旧的 CSV 文件: {file.FullName}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"清理旧的 CSV 文件时发生错误: {ex.Message}");
        }
    }

    /// <summary>
    /// 修改 Python 配置文件中指定的关键词和最大笔记数量。
    /// </summary>
    private void ModifyConfigFile(string keywords, int maxNotesCount)
    {
        string[] allLines = File.ReadAllLines(_configFilePath, Encoding.UTF8);
        List<string> newLines = new List<string>();

        Regex keywordsPrefixRegex = new Regex(@"^(\s*KEYWORDS\s*=\s*)", RegexOptions.Singleline);
        Regex maxCountPrefixRegex = new Regex(@"^(\s*CRAWLER_MAX_NOTES_COUNT\s*=\s*)", RegexOptions.Singleline);

        foreach (string line in allLines)
        {
            Match keywordsMatch = keywordsPrefixRegex.Match(line);
            if (keywordsMatch.Success)
            {
                string prefix = keywordsMatch.Groups[1].Value;
                newLines.Add($"{prefix}\"{keywords}\"");
                continue;
            }

            Match maxCountMatch = maxCountPrefixRegex.Match(line);
            if (maxCountMatch.Success)
            {
                string prefix = maxCountMatch.Groups[1].Value;
                newLines.Add($"{prefix}{maxNotesCount}");
                continue;
            }

            newLines.Add(line);
        }

        File.WriteAllText(_configFilePath, string.Join(Environment.NewLine, newLines), Encoding.UTF8);
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

    private List<WeiboPost> ReadCsvData(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
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
