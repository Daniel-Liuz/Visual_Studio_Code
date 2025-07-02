namespace WindowsFormsApp1
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.btnTimer = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTimer = new System.Windows.Forms.Label();
            this.dptStartDate = new System.Windows.Forms.DateTimePicker();
            this.dptEndDate = new System.Windows.Forms.DateTimePicker();
            this.start_time = new System.Windows.Forms.Label();
            this.end_time = new System.Windows.Forms.Label();
            this.lblRemainingTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTimer
            // 
            this.btnTimer.Location = new System.Drawing.Point(272, 132);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(152, 45);
            this.btnTimer.TabIndex = 0;
            this.btnTimer.Text = "Start Timer";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(500, 145);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(44, 18);
            this.lblTimer.TabIndex = 1;
            this.lblTimer.Text = "计时";
            // 
            // dptStartDate
            // 
            this.dptStartDate.Location = new System.Drawing.Point(132, 280);
            this.dptStartDate.Name = "dptStartDate";
            this.dptStartDate.Size = new System.Drawing.Size(200, 28);
            this.dptStartDate.TabIndex = 2;
            this.dptStartDate.ValueChanged += new System.EventHandler(this.dptStartDate_ValueChanged);
            // 
            // dptEndDate
            // 
            this.dptEndDate.Location = new System.Drawing.Point(473, 279);
            this.dptEndDate.Name = "dptEndDate";
            this.dptEndDate.Size = new System.Drawing.Size(200, 28);
            this.dptEndDate.TabIndex = 3;
            // 
            // start_time
            // 
            this.start_time.AutoSize = true;
            this.start_time.Location = new System.Drawing.Point(30, 286);
            this.start_time.Name = "start_time";
            this.start_time.Size = new System.Drawing.Size(80, 18);
            this.start_time.TabIndex = 4;
            this.start_time.Text = "开始时间";
            // 
            // end_time
            // 
            this.end_time.AutoSize = true;
            this.end_time.Location = new System.Drawing.Point(376, 286);
            this.end_time.Name = "end_time";
            this.end_time.Size = new System.Drawing.Size(80, 18);
            this.end_time.TabIndex = 5;
            this.end_time.Text = "结束时间";
            // 
            // lblRemainingTime
            // 
            this.lblRemainingTime.AutoSize = true;
            this.lblRemainingTime.Location = new System.Drawing.Point(313, 337);
            this.lblRemainingTime.Name = "lblRemainingTime";
            this.lblRemainingTime.Size = new System.Drawing.Size(80, 18);
            this.lblRemainingTime.TabIndex = 6;
            this.lblRemainingTime.Text = "剩余时间";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(33, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // lbl6
            // 
            this.lbl6.AutoSize = true;
            this.lbl6.Location = new System.Drawing.Point(157, 50);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(80, 18);
            this.lbl6.TabIndex = 8;
            this.lbl6.Text = "加载图片";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblRemainingTime);
            this.Controls.Add(this.end_time);
            this.Controls.Add(this.start_time);
            this.Controls.Add(this.dptEndDate);
            this.Controls.Add(this.dptStartDate);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.btnTimer);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.DateTimePicker dptStartDate;
        private System.Windows.Forms.DateTimePicker dptEndDate;
        private System.Windows.Forms.Label start_time;
        private System.Windows.Forms.Label end_time;
        private System.Windows.Forms.Label lblRemainingTime;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl6;
    }
}