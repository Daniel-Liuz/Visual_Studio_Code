namespace BiliCommentAnalysis
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.KeywordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.displayCountNumeric = new System.Windows.Forms.NumericUpDown();
            this.startButton = new System.Windows.Forms.Button();
            this.commentsDataGridView = new System.Windows.Forms.DataGridView();
            this.genderChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.sentimentChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.wordCloudPictureBox = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.displayCountNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commentsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.genderChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sentimentChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wordCloudPictureBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "关键词：";
            // 
            // KeywordTextBox
            // 
            this.KeywordTextBox.Location = new System.Drawing.Point(147, 55);
            this.KeywordTextBox.Name = "KeywordTextBox";
            this.KeywordTextBox.Size = new System.Drawing.Size(181, 28);
            this.KeywordTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "显示评论数";
            // 
            // displayCountNumeric
            // 
            this.displayCountNumeric.Location = new System.Drawing.Point(152, 109);
            this.displayCountNumeric.Name = "displayCountNumeric";
            this.displayCountNumeric.Size = new System.Drawing.Size(120, 28);
            this.displayCountNumeric.TabIndex = 3;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(466, 58);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(172, 80);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "执行所有操作";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // commentsDataGridView
            // 
            this.commentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.commentsDataGridView.Location = new System.Drawing.Point(62, 163);
            this.commentsDataGridView.Name = "commentsDataGridView";
            this.commentsDataGridView.RowHeadersWidth = 62;
            this.commentsDataGridView.RowTemplate.Height = 30;
            this.commentsDataGridView.Size = new System.Drawing.Size(567, 205);
            this.commentsDataGridView.TabIndex = 5;
            // 
            // genderChart
            // 
            chartArea3.Name = "ChartArea1";
            this.genderChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.genderChart.Legends.Add(legend3);
            this.genderChart.Location = new System.Drawing.Point(47, 397);
            this.genderChart.Name = "genderChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.genderChart.Series.Add(series3);
            this.genderChart.Size = new System.Drawing.Size(300, 300);
            this.genderChart.TabIndex = 6;
            this.genderChart.Text = "chart1";
            // 
            // sentimentChart
            // 
            chartArea4.Name = "ChartArea1";
            this.sentimentChart.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.sentimentChart.Legends.Add(legend4);
            this.sentimentChart.Location = new System.Drawing.Point(391, 397);
            this.sentimentChart.Name = "sentimentChart";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.sentimentChart.Series.Add(series4);
            this.sentimentChart.Size = new System.Drawing.Size(300, 300);
            this.sentimentChart.TabIndex = 7;
            this.sentimentChart.Text = "chart1";
            // 
            // wordCloudPictureBox
            // 
            this.wordCloudPictureBox.Location = new System.Drawing.Point(50, 718);
            this.wordCloudPictureBox.Name = "wordCloudPictureBox";
            this.wordCloudPictureBox.Size = new System.Drawing.Size(588, 294);
            this.wordCloudPictureBox.TabIndex = 8;
            this.wordCloudPictureBox.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1046);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 31);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(82, 24);
            this.statusLabel.Text = "准备就绪";
            // 
            // bili
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 1077);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.wordCloudPictureBox);
            this.Controls.Add(this.sentimentChart);
            this.Controls.Add(this.genderChart);
            this.Controls.Add(this.commentsDataGridView);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.displayCountNumeric);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KeywordTextBox);
            this.Controls.Add(this.label1);
            this.Name = "bili";
            this.Text = "bili";
            ((System.ComponentModel.ISupportInitialize)(this.displayCountNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commentsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.genderChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sentimentChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wordCloudPictureBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox KeywordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown displayCountNumeric;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.DataGridView commentsDataGridView;
        private System.Windows.Forms.DataVisualization.Charting.Chart genderChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart sentimentChart;
        private System.Windows.Forms.PictureBox wordCloudPictureBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}