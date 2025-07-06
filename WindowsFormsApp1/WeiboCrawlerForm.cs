// 文件: WeiboCrawlerForm.cs (已实现列宽调整和顺序重排)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; // 确保包含此行，如果需要设置颜色
using System.IO;
using System.Linq; // 确保包含此行，用于 LINQ 查询 (如 .Any() )
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace WeiboCrawlerApp
{
    public partial class WeiboCrawlerForm : Form
    {
        public WeiboCrawlerForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            numMaxCount.Minimum = 1;
            numMaxCount.Maximum = 1000;
            numMaxCount.Value = 20;
            lblStatus.Text = "准备就绪";
        }

        private async void btnStartCrawler_Click(object sender, EventArgs e)
        {
            string keywords = txtKeywords.Text.Trim();
            int maxCount = (int)numMaxCount.Value;

            if (string.IsNullOrEmpty(keywords))
            {
                MessageBox.Show("请输入要搜索的关键词！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 禁用按钮，显示等待光标和进度条
            btnStartCrawler.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "准备启动 Python 爬虫...";
            dgvResults.DataSource = null; // 清空旧的表格数据
            progressBar.Style = ProgressBarStyle.Marquee;

            // 在开始新任务前，清空饼图显示，防止旧数据残留
            // 即使 DisplayGenderDistributionChart 内部会清空，这里先清空一次更保险，提供即时反馈
            DisplayGenderDistributionChart(new List<WeiboPost>()); // 传入空列表以清空图表并显示无数据提示

            try
            {
                var crawler = new WeiboCrawlerHelper();
                lblStatus.Text = $"正在执行爬取，关键词: '{keywords}'... 请耐心等待。";
                // 强制刷新UI，让用户看到状态更新
                Application.DoEvents();

                // 执行爬取任务，并获取截断后的数据
                List<WeiboPost> posts = await crawler.RunCrawlerAsync(keywords, maxCount);

                // 恢复进度条样式
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = progressBar.Maximum; // 设置进度条为满，表示完成

                // 将爬取到的数据绑定到 DataGridView
                dgvResults.DataSource = posts;
                // 调用方法美化 DataGridView 列
                CustomizeDataGridViewColumns();

                // 【新增】调用方法显示性别分布饼图
                DisplayGenderDistributionChart(posts);

                // 更新状态标签并显示完成消息
                lblStatus.Text = $"任务完成！成功加载了 {posts.Count} 条数据。";
                MessageBox.Show($"任务完成！成功加载了 {posts.Count} 条数据。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException fex) // 捕获文件或目录相关的特定错误
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                lblStatus.Text = "配置或文件错误！";
                MessageBox.Show($"文件或目录配置错误：\n\n{fex.Message}\n\n请检查 WeiboCrawlerHelper.cs 中的路径配置。", "配置错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine("文件/目录异常: " + fex.ToString());
            }
            catch (Exception ex) // 捕获其他通用错误
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                lblStatus.Text = "发生错误！";
                MessageBox.Show($"爬取过程中发生错误：\n\n{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine("爬虫执行异常: " + ex.ToString());
            }
            finally
            {
                // 无论成功或失败，都恢复按钮状态和光标，并重置进度条
                btnStartCrawler.Enabled = true;
                this.Cursor = Cursors.Default;
                progressBar.Value = 0; // 重置进度条
            }
        }
        private void DisplayGenderDistributionChart(List<WeiboPost> posts)
        {
            // 1. 清除旧的图表数据，确保每次刷新都是新的
            chartGenderDistribution.Series.Clear(); // 清除所有数据系列
            chartGenderDistribution.Titles.Clear();  // 清除所有标题
            chartGenderDistribution.Legends.Clear(); // 清除所有图例（如果需要自定义图例）

            // 如果没有数据，显示“无数据”提示
            if (posts == null || !posts.Any())
            {
                chartGenderDistribution.Titles.Add("无数据可用于性别分布分析");
                return;
            }

            // 2. 计算男女及未知比例
            int maleCount = 0;
            int femaleCount = 0;
            int unknownCount = 0;

            foreach (var post in posts)
            {
                // 确保 Gender 属性不为空，并转换为小写进行比较，增加健壮性
                string gender = post.Gender?.ToLower();
                if (gender == "f") // 女性
                {
                    femaleCount++;
                }
                else if (gender == "m") // 男性
                {
                    maleCount++;
                }
                else // 未知或其他值
                {
                    unknownCount++;
                }
            }

            // 3. 配置饼图系列
            Series series = new Series("GenderDistribution") // 创建一个新的数据系列
            {
                ChartType = SeriesChartType.Pie, // 设置图表类型为饼图
                IsValueShownAsLabel = true,      // 在饼图上显示数据点的数值
                LabelFormat = "{0} ({P})",       // 标签格式：值 (百分比)
                LegendText = "#VALX: #PERCENT"   // 图例文本格式：类别名: 百分比
            };

            // 4. 添加数据点到系列
            if (maleCount > 0)
            {
                series.Points.Add(new DataPoint(0, maleCount) { LegendText = "男性", Label = "男性" });
            }
            if (femaleCount > 0)
            {
                series.Points.Add(new DataPoint(0, femaleCount) { LegendText = "女性", Label = "女性" });
            }
            if (unknownCount > 0)
            {
                series.Points.Add(new DataPoint(0, unknownCount) { LegendText = "未知/其他", Label = "未知/其他" });
            }

            // 如果没有任何有效的性别数据（例如，所有计数都为0），添加一个提示点
            if (maleCount == 0 && femaleCount == 0 && unknownCount == 0)
            {
                chartGenderDistribution.Titles.Add("没有可用于性别分析的数据");
                return; // 无需添加系列
            }


            // 5. 将系列添加到图表控件
            chartGenderDistribution.Series.Add(series);

            // 6. 添加图表标题
            chartGenderDistribution.Titles.Add("微博用户性别分布");
            // 7. 配置图例（可选，但通常有助于理解饼图）
            // 确保至少有一个默认图例存在
            if (chartGenderDistribution.Legends.Count == 0)
            {
                chartGenderDistribution.Legends.Add(new Legend("DefaultLegend"));
            }
            chartGenderDistribution.Legends["DefaultLegend"].Docking = Docking.Bottom; // 将图例放在底部

            // 刷新图表
            chartGenderDistribution.Invalidate();
        }
        /// <summary>
        /// 自定义 DataGridView 的列显示、顺序和宽度。
        /// </summary>
        private void CustomizeDataGridViewColumns()
        {
            if (dgvResults.DataSource == null) return;

            // 步骤1：先全部隐藏
            foreach (DataGridViewColumn col in dgvResults.Columns)
            {
                col.Visible = false;
            }

            // 步骤2：设置我们想看的列的属性
            // 我们将按照 Nickname -> Gender -> Content 的顺序来设置它们的 DisplayIndex

            if (dgvResults.Columns.Contains("Nickname"))
            {
                var col = dgvResults.Columns["Nickname"];
                col.Visible = true;
                col.HeaderText = "用户昵称";
                col.DisplayIndex = 0; // 【新功能】设置为第一列
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // 让其宽度自适应内容
            }

            if (dgvResults.Columns.Contains("Gender"))
            {
                var col = dgvResults.Columns["Gender"];
                col.Visible = true;
                col.HeaderText = "性别";
                col.DisplayIndex = 1; // 【新功能】设置为第二列
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // 让其宽度自适应内容
            }

            if (dgvResults.Columns.Contains("Content"))
            {
                var col = dgvResults.Columns["Content"];
                col.Visible = true;
                col.HeaderText = "微博内容";
                col.DisplayIndex = 2; // 【新功能】设置为第三列

                // 【核心】将内容列的宽度模式设置为 Fill
                // 这会让它自动填充表格中剩余的所有可用空间
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void txtKeywords_TextChanged(object sender, EventArgs e)
        {

        }

        private void chartGenderDistribution_Click(object sender, EventArgs e)
        {

        }
    }
}
