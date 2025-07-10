namespace WindowsFormsApp1
{
    partial class frmPasswordReset
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtResetStudentNo = new System.Windows.Forms.TextBox();
            this.btnSendResetEmail = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbSecurityQuestion = new System.Windows.Forms.GroupBox();
            this.txtAnswer1 = new System.Windows.Forms.TextBox();
            this.lblQuestion2 = new System.Windows.Forms.Label();
            this.txtAnswer2 = new System.Windows.Forms.TextBox();
            this.btnVerifyAnswers = new System.Windows.Forms.Button();
            this.lblQuestion1 = new System.Windows.Forms.Label();
            this.gbSecurityQuestion.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入您的学号：";
            // 
            // txtResetStudentNo
            // 
            this.txtResetStudentNo.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtResetStudentNo.Location = new System.Drawing.Point(324, 19);
            this.txtResetStudentNo.Name = "txtResetStudentNo";
            this.txtResetStudentNo.Size = new System.Drawing.Size(241, 42);
            this.txtResetStudentNo.TabIndex = 1;
            // 
            // btnSendResetEmail
            // 
            this.btnSendResetEmail.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSendResetEmail.Location = new System.Drawing.Point(558, 101);
            this.btnSendResetEmail.Name = "btnSendResetEmail";
            this.btnSendResetEmail.Size = new System.Drawing.Size(195, 41);
            this.btnSendResetEmail.TabIndex = 2;
            this.btnSendResetEmail.Text = "发送重置邮件";
            this.btnSendResetEmail.UseVisualStyleBackColor = true;
            this.btnSendResetEmail.Click += new System.EventHandler(this.btnSendResetEmail_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 33);
            this.label2.TabIndex = 4;
            this.label2.Text = "方法1：邮箱找回";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(13, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(383, 33);
            this.label3.TabIndex = 5;
            this.label3.Text = "方法2：使用安全问题找回";
            // 
            // gbSecurityQuestion
            // 
            this.gbSecurityQuestion.Controls.Add(this.lblQuestion1);
            this.gbSecurityQuestion.Controls.Add(this.txtAnswer1);
            this.gbSecurityQuestion.Controls.Add(this.btnVerifyAnswers);
            this.gbSecurityQuestion.Controls.Add(this.lblQuestion2);
            this.gbSecurityQuestion.Controls.Add(this.txtAnswer2);
            this.gbSecurityQuestion.Location = new System.Drawing.Point(31, 241);
            this.gbSecurityQuestion.Name = "gbSecurityQuestion";
            this.gbSecurityQuestion.Size = new System.Drawing.Size(642, 411);
            this.gbSecurityQuestion.TabIndex = 6;
            this.gbSecurityQuestion.TabStop = false;
            this.gbSecurityQuestion.Text = "安全问题验证";
            // 
            // txtAnswer1
            // 
            this.txtAnswer1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAnswer1.Location = new System.Drawing.Point(58, 129);
            this.txtAnswer1.Name = "txtAnswer1";
            this.txtAnswer1.Size = new System.Drawing.Size(288, 39);
            this.txtAnswer1.TabIndex = 1;
            // 
            // lblQuestion2
            // 
            this.lblQuestion2.AutoSize = true;
            this.lblQuestion2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQuestion2.Location = new System.Drawing.Point(53, 197);
            this.lblQuestion2.Name = "lblQuestion2";
            this.lblQuestion2.Size = new System.Drawing.Size(376, 28);
            this.lblQuestion2.TabIndex = 2;
            this.lblQuestion2.Text = "问题2：您的小学叫什么名字?";
            // 
            // txtAnswer2
            // 
            this.txtAnswer2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAnswer2.Location = new System.Drawing.Point(58, 243);
            this.txtAnswer2.Name = "txtAnswer2";
            this.txtAnswer2.Size = new System.Drawing.Size(288, 39);
            this.txtAnswer2.TabIndex = 3;
            // 
            // btnVerifyAnswers
            // 
            this.btnVerifyAnswers.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnVerifyAnswers.Location = new System.Drawing.Point(58, 316);
            this.btnVerifyAnswers.Name = "btnVerifyAnswers";
            this.btnVerifyAnswers.Size = new System.Drawing.Size(288, 68);
            this.btnVerifyAnswers.TabIndex = 4;
            this.btnVerifyAnswers.Text = "验证答案";
            this.btnVerifyAnswers.UseVisualStyleBackColor = true;
            this.btnVerifyAnswers.Click += new System.EventHandler(this.btnVerifyAnswers_Click);
            // 
            // lblQuestion1
            // 
            this.lblQuestion1.AutoSize = true;
            this.lblQuestion1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQuestion1.Location = new System.Drawing.Point(54, 81);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new System.Drawing.Size(362, 28);
            this.lblQuestion1.TabIndex = 5;
            this.lblQuestion1.Text = "问题1：您最喜欢什么颜色？";
            // 
            // frmPasswordReset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 993);
            this.Controls.Add(this.gbSecurityQuestion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSendResetEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResetStudentNo);
            this.Name = "frmPasswordReset";
            this.Text = "frmPasswordReset";
            this.gbSecurityQuestion.ResumeLayout(false);
            this.gbSecurityQuestion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResetStudentNo;
        private System.Windows.Forms.Button btnSendResetEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbSecurityQuestion;
        private System.Windows.Forms.Label lblQuestion1;
        private System.Windows.Forms.TextBox txtAnswer1;
        private System.Windows.Forms.Button btnVerifyAnswers;
        private System.Windows.Forms.Label lblQuestion2;
        private System.Windows.Forms.TextBox txtAnswer2;
    }
}