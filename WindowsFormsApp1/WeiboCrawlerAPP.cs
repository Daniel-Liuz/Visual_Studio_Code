// 文件: WeiboCrawlerForm.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WeiboCrawlerApp // 确保这里的命名空间和你的项目名一致
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
            // 在这里设置控件的初始状态
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

            btnStartCrawler.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "准备启动 Python 爬虫...";
            dgvResults.DataSource = null;
            progressBar.Style = ProgressBarStyle.Marquee;

            try
            {
                var crawler = new WeiboCrawlerHelper();

                lblStatus.Text = $"正在执行爬取，关键词: '{keywords}'... 请耐心等待。";
                Application.DoEvents();

                List<WeiboPost> posts = await crawler.RunCrawlerAsync(keywords, maxCount);

                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = progressBar.Maximum;
                dgvResults.DataSource = posts;
                dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                if (dgvResults.Columns.Contains("Content"))
                {
                    dgvResults.Columns["Content"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                lblStatus.Text = $"任务完成！成功加载了 {posts.Count} 条数据。";
                MessageBox.Show($"任务完成！成功加载了 {posts.Count} 条数据。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                lblStatus.Text = "发生错误！";
                MessageBox.Show($"爬取过程中发生错误：\n\n{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine("爬虫执行异常: " + ex.ToString());
            }
            finally
            {
                btnStartCrawler.Enabled = true;
                this.Cursor = Cursors.Default;
                progressBar.Value = 0;
            }
        }
    }
}
