
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BiliCommentAnalysis
{
    public partial class Form1 : Form
    {
        private readonly BiliAnalysisService _analysisService;

        public Form1()
        {
            InitializeComponent();
            _analysisService = new BiliAnalysisService(this);

            // 将Service的进度更新委托指向我们的UI更新方法
            _analysisService.OnProgressUpdate = (message) =>
            {
                // 确保UI更新在主线程上执行
                statusLabel.Text = message;
                statusStrip1.Refresh(); // 强制刷新状态栏
            };
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            // 清理上一次的结果
            ClearResults();

            // 禁用按钮，防止重复点击
            startButton.Enabled = false;
            string keyword = KeywordTextBox.Text;

            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("请输入关键词！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                startButton.Enabled = true;
                return;
            }

            try
            {
                // **核心调用**：异步执行整个分析流程
                AnalysisResult result = await _analysisService.PerformFullAnalysisAsync(keyword);

                // **UI更新**：将获取到的数据显示在界面上

                // 功能2: 根据用户设定的数量，截取并显示评论
                int displayCount = (int)displayCountNumeric.Value;
                commentsDataGridView.DataSource = result.Comments.Take(displayCount).ToList();

                // 功能3: 绘制性别分布图
                PopulatePieChart(genderChart, "性别分布", result.GenderDistribution);

                // 功能4: 绘制情感分析图 (使用柱状图更清晰)
                PopulateBarChart(sentimentChart, "情感分析", result.SentimentAnalysis);

                // 功能5: 显示词云图片
                if (!string.IsNullOrEmpty(result.WordcloudImagePath) && File.Exists(result.WordcloudImagePath))
                {
                    // 使用内存流加载图片，避免文件被锁定
                    using (var stream = new MemoryStream(File.ReadAllBytes(result.WordcloudImagePath)))
                    {
                        wordCloudPictureBox.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    statusLabel.Text = "词云图未找到。";
                }
            }
            catch (Exception ex)
            {
                // 如果过程中出现任何错误，弹出消息框显示
                MessageBox.Show($"操作失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "操作失败。";
            }
            finally
            {
                // 无论成功还是失败，最后都恢复按钮可用
                startButton.Enabled = true;
            }
        }

        // --- UI 更新辅助方法 ---

        private void ClearResults()
        {
            commentsDataGridView.DataSource = null;
            genderChart.Series.Clear();
            sentimentChart.Series.Clear();
            if (wordCloudPictureBox.Image != null)
            {
                wordCloudPictureBox.Image.Dispose();
                wordCloudPictureBox.Image = null;
            }
            statusLabel.Text = "准备就绪";
        }

        private void PopulatePieChart(Chart chart, string title, System.Collections.Generic.Dictionary<string, int> data)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);

            var series = new Series
            {
                Name = "Series1",
                IsVisibleInLegend = true,
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true // 在饼图上直接显示数值
            };

            foreach (var entry in data)
            {
                series.Points.AddXY(entry.Key, entry.Value);
                // 给每个扇区设置图例文字
                series.Points.Last().LegendText = $"{entry.Key} ({entry.Value})";
            }
            chart.Series.Add(series);
        }

        private void PopulateBarChart(Chart chart, string title, System.Collections.Generic.Dictionary<string, int> data)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);
            chart.ChartAreas[0].AxisX.Interval = 1; // 确保每个标签都显示

            var series = new Series
            {
                Name = "Series1",
                IsVisibleInLegend = false, // 柱状图通常不需要图例
                ChartType = SeriesChartType.Column
            };

            foreach (var entry in data)
            {
                series.Points.AddXY(entry.Key, entry.Value);
            }
            chart.Series.Add(series);
        }
    }
}

