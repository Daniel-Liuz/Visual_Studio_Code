namespace WeiboCrawlerApp
{
    partial class WeiboCrawlerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numMaxCount = new System.Windows.Forms.NumericUpDown();
            this.btnStartCrawler = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.chartGenderDistribution = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartGenderDistribution)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "关键词";
            // 
            // txtKeywords
            // 
            this.txtKeywords.Location = new System.Drawing.Point(144, 24);
            this.txtKeywords.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(269, 28);
            this.txtKeywords.TabIndex = 1;
            this.txtKeywords.TextChanged += new System.EventHandler(this.txtKeywords_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "爬取数量";
            // 
            // numMaxCount
            // 
            this.numMaxCount.Location = new System.Drawing.Point(144, 80);
            this.numMaxCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numMaxCount.Name = "numMaxCount";
            this.numMaxCount.Size = new System.Drawing.Size(135, 28);
            this.numMaxCount.TabIndex = 3;
            // 
            // btnStartCrawler
            // 
            this.btnStartCrawler.Location = new System.Drawing.Point(645, 24);
            this.btnStartCrawler.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStartCrawler.Name = "btnStartCrawler";
            this.btnStartCrawler.Size = new System.Drawing.Size(101, 39);
            this.btnStartCrawler.TabIndex = 4;
            this.btnStartCrawler.Text = "开始爬取";
            this.btnStartCrawler.UseVisualStyleBackColor = true;
            this.btnStartCrawler.Click += new System.EventHandler(this.btnStartCrawler_Click);
            // 
            // dgvResults
            // 
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(27, 118);
            this.dgvResults.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.RowTemplate.Height = 27;
            this.dgvResults.Size = new System.Drawing.Size(829, 314);
            this.dgvResults.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 686);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(900, 31);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(82, 24);
            this.lblStatus.Text = "准备就绪";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(225, 23);
            // 
            // chartGenderDistribution
            // 
            chartArea1.Name = "ChartArea1";
            this.chartGenderDistribution.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartGenderDistribution.Legends.Add(legend1);
            this.chartGenderDistribution.Location = new System.Drawing.Point(27, 439);
            this.chartGenderDistribution.Name = "chartGenderDistribution";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartGenderDistribution.Series.Add(series1);
            this.chartGenderDistribution.Size = new System.Drawing.Size(310, 300);
            this.chartGenderDistribution.TabIndex = 7;
            this.chartGenderDistribution.Text = "chart1";
            this.chartGenderDistribution.Click += new System.EventHandler(this.chartGenderDistribution_Click);
            // 
            // WeiboCrawlerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 717);
            this.Controls.Add(this.chartGenderDistribution);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.btnStartCrawler);
            this.Controls.Add(this.numMaxCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "WeiboCrawlerForm";
            this.Text = "WeiboCrawlerAPP";
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartGenderDistribution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numMaxCount;
        private System.Windows.Forms.Button btnStartCrawler;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGenderDistribution;
    }
}