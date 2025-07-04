// 文件: WeiboCrawlerForm.cs (已实现列宽调整和顺序重排)
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

                // 每次都调用这个方法来美化表格
                CustomizeDataGridViewColumns();

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
    }
}
