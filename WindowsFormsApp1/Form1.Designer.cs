namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnInfo = new System.Windows.Forms.Button();
            this.txtNum1 = new System.Windows.Forms.TextBox();
            this.txtNum2 = new System.Windows.Forms.TextBox();
            this.txtNum3 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radA = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radD = new System.Windows.Forms.RadioButton();
            this.radC = new System.Windows.Forms.RadioButton();
            this.radB = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radQ2A = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.radQ2D = new System.Windows.Forms.RadioButton();
            this.radQ2C = new System.Windows.Forms.RadioButton();
            this.radQ2B = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radQ3A = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radQ3D = new System.Windows.Forms.RadioButton();
            this.radQ3C = new System.Windows.Forms.RadioButton();
            this.radQ3B = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radQ4A = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.radQ4D = new System.Windows.Forms.RadioButton();
            this.radQ4C = new System.Windows.Forms.RadioButton();
            this.radQ4B = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radQ5A = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.radQ5D = new System.Windows.Forms.RadioButton();
            this.radQ5C = new System.Windows.Forms.RadioButton();
            this.radQ5B = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(26, 31);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(98, 18);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "HelloWorld";
            this.lblInfo.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Font = new System.Drawing.Font("Comic Sans MS", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInfo.Location = new System.Drawing.Point(677, 31);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(101, 44);
            this.btnInfo.TabIndex = 1;
            this.btnInfo.Text = "关于";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // txtNum1
            // 
            this.txtNum1.BackColor = System.Drawing.SystemColors.Info;
            this.txtNum1.Font = new System.Drawing.Font("Comic Sans MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNum1.Location = new System.Drawing.Point(25, 94);
            this.txtNum1.Name = "txtNum1";
            this.txtNum1.Size = new System.Drawing.Size(100, 37);
            this.txtNum1.TabIndex = 2;
            this.txtNum1.TextChanged += new System.EventHandler(this.txtNum1_TextChanged);
            // 
            // txtNum2
            // 
            this.txtNum2.BackColor = System.Drawing.SystemColors.Info;
            this.txtNum2.Font = new System.Drawing.Font("Comic Sans MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNum2.Location = new System.Drawing.Point(205, 94);
            this.txtNum2.Name = "txtNum2";
            this.txtNum2.Size = new System.Drawing.Size(100, 37);
            this.txtNum2.TabIndex = 3;
            this.txtNum2.TextChanged += new System.EventHandler(this.txtNum2_TextChanged);
            // 
            // txtNum3
            // 
            this.txtNum3.BackColor = System.Drawing.SystemColors.Info;
            this.txtNum3.Font = new System.Drawing.Font("Comic Sans MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNum3.Location = new System.Drawing.Point(380, 97);
            this.txtNum3.Name = "txtNum3";
            this.txtNum3.Size = new System.Drawing.Size(100, 37);
            this.txtNum3.TabIndex = 4;
            this.txtNum3.TextChanged += new System.EventHandler(this.txtNum3_TextChanged);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Comic Sans MS", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(673, 97);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 44);
            this.button2.TabIndex = 5;
            this.button2.Text = "极值";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(523, 104);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(115, 22);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radA);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radD);
            this.groupBox1.Controls.Add(this.radC);
            this.groupBox1.Controls.Add(this.radB);
            this.groupBox1.Location = new System.Drawing.Point(29, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 258);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "做题区1";
            // 
            // radA
            // 
            this.radA.AutoSize = true;
            this.radA.Location = new System.Drawing.Point(6, 85);
            this.radA.Name = "radA";
            this.radA.Size = new System.Drawing.Size(69, 22);
            this.radA.TabIndex = 12;
            this.radA.TabStop = true;
            this.radA.Text = "北京";
            this.radA.UseVisualStyleBackColor = true;
            this.radA.CheckedChanged += new System.EventHandler(this.radA_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 31);
            this.label1.TabIndex = 11;
            this.label1.Text = "请问中国的首都是？";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // radD
            // 
            this.radD.AutoSize = true;
            this.radD.Location = new System.Drawing.Point(6, 220);
            this.radD.Name = "radD";
            this.radD.Size = new System.Drawing.Size(69, 22);
            this.radD.TabIndex = 10;
            this.radD.TabStop = true;
            this.radD.Text = "重庆";
            this.radD.UseVisualStyleBackColor = true;
            this.radD.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radC
            // 
            this.radC.AutoSize = true;
            this.radC.Location = new System.Drawing.Point(6, 177);
            this.radC.Name = "radC";
            this.radC.Size = new System.Drawing.Size(69, 22);
            this.radC.TabIndex = 9;
            this.radC.TabStop = true;
            this.radC.Text = "天津";
            this.radC.UseVisualStyleBackColor = true;
            this.radC.CheckedChanged += new System.EventHandler(this.radC_CheckedChanged);
            // 
            // radB
            // 
            this.radB.AutoSize = true;
            this.radB.Location = new System.Drawing.Point(6, 130);
            this.radB.Name = "radB";
            this.radB.Size = new System.Drawing.Size(69, 22);
            this.radB.TabIndex = 8;
            this.radB.TabStop = true;
            this.radB.Text = "上海";
            this.radB.UseVisualStyleBackColor = true;
            this.radB.CheckedChanged += new System.EventHandler(this.radB_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radQ2A);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.radQ2D);
            this.groupBox2.Controls.Add(this.radQ2C);
            this.groupBox2.Controls.Add(this.radQ2B);
            this.groupBox2.Location = new System.Drawing.Point(644, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 258);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "做题区2";
            // 
            // radQ2A
            // 
            this.radQ2A.AutoSize = true;
            this.radQ2A.Location = new System.Drawing.Point(6, 85);
            this.radQ2A.Name = "radQ2A";
            this.radQ2A.Size = new System.Drawing.Size(87, 22);
            this.radQ2A.TabIndex = 12;
            this.radQ2A.TabStop = true;
            this.radQ2A.Text = "曹雪芹";
            this.radQ2A.UseVisualStyleBackColor = true;
            this.radQ2A.CheckedChanged += new System.EventHandler(this.radQ2A_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 31);
            this.label2.TabIndex = 11;
            this.label2.Text = "谁是《红楼梦》的作者？";
            // 
            // radQ2D
            // 
            this.radQ2D.AutoSize = true;
            this.radQ2D.Location = new System.Drawing.Point(6, 220);
            this.radQ2D.Name = "radQ2D";
            this.radQ2D.Size = new System.Drawing.Size(87, 22);
            this.radQ2D.TabIndex = 10;
            this.radQ2D.TabStop = true;
            this.radQ2D.Text = "施耐庵";
            this.radQ2D.UseVisualStyleBackColor = true;
            this.radQ2D.CheckedChanged += new System.EventHandler(this.radQ2D_CheckedChanged);
            // 
            // radQ2C
            // 
            this.radQ2C.AutoSize = true;
            this.radQ2C.Location = new System.Drawing.Point(6, 177);
            this.radQ2C.Name = "radQ2C";
            this.radQ2C.Size = new System.Drawing.Size(87, 22);
            this.radQ2C.TabIndex = 9;
            this.radQ2C.TabStop = true;
            this.radQ2C.Text = "罗贯中";
            this.radQ2C.UseVisualStyleBackColor = true;
            this.radQ2C.CheckedChanged += new System.EventHandler(this.radQ2C_CheckedChanged);
            // 
            // radQ2B
            // 
            this.radQ2B.AutoSize = true;
            this.radQ2B.Location = new System.Drawing.Point(6, 130);
            this.radQ2B.Name = "radQ2B";
            this.radQ2B.Size = new System.Drawing.Size(87, 22);
            this.radQ2B.TabIndex = 8;
            this.radQ2B.TabStop = true;
            this.radQ2B.Text = "吴承恩";
            this.radQ2B.UseVisualStyleBackColor = true;
            this.radQ2B.CheckedChanged += new System.EventHandler(this.radQ2B_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radQ3A);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.radQ3D);
            this.groupBox3.Controls.Add(this.radQ3C);
            this.groupBox3.Controls.Add(this.radQ3B);
            this.groupBox3.Location = new System.Drawing.Point(41, 517);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(461, 258);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "做题区3";
            // 
            // radQ3A
            // 
            this.radQ3A.AutoSize = true;
            this.radQ3A.Location = new System.Drawing.Point(6, 85);
            this.radQ3A.Name = "radQ3A";
            this.radQ3A.Size = new System.Drawing.Size(105, 22);
            this.radQ3A.TabIndex = 12;
            this.radQ3A.TabStop = true;
            this.radQ3A.Text = "菩提祖师";
            this.radQ3A.UseVisualStyleBackColor = true;
            this.radQ3A.CheckedChanged += new System.EventHandler(this.radQ3A_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(374, 31);
            this.label3.TabIndex = 11;
            this.label3.Text = "《西游记》中孙悟空的师傅是谁？";
            // 
            // radQ3D
            // 
            this.radQ3D.AutoSize = true;
            this.radQ3D.Location = new System.Drawing.Point(6, 220);
            this.radQ3D.Name = "radQ3D";
            this.radQ3D.Size = new System.Drawing.Size(105, 22);
            this.radQ3D.TabIndex = 10;
            this.radQ3D.TabStop = true;
            this.radQ3D.Text = "如来佛祖";
            this.radQ3D.UseVisualStyleBackColor = true;
            this.radQ3D.CheckedChanged += new System.EventHandler(this.radQ3D_CheckedChanged);
            // 
            // radQ3C
            // 
            this.radQ3C.AutoSize = true;
            this.radQ3C.Location = new System.Drawing.Point(6, 177);
            this.radQ3C.Name = "radQ3C";
            this.radQ3C.Size = new System.Drawing.Size(105, 22);
            this.radQ3C.TabIndex = 9;
            this.radQ3C.TabStop = true;
            this.radQ3C.Text = "太上老君";
            this.radQ3C.UseVisualStyleBackColor = true;
            this.radQ3C.CheckedChanged += new System.EventHandler(this.radQ3C_CheckedChanged);
            // 
            // radQ3B
            // 
            this.radQ3B.AutoSize = true;
            this.radQ3B.Location = new System.Drawing.Point(6, 130);
            this.radQ3B.Name = "radQ3B";
            this.radQ3B.Size = new System.Drawing.Size(69, 22);
            this.radQ3B.TabIndex = 8;
            this.radQ3B.TabStop = true;
            this.radQ3B.Text = "唐僧";
            this.radQ3B.UseVisualStyleBackColor = true;
            this.radQ3B.CheckedChanged += new System.EventHandler(this.radQ3B_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radQ4A);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.radQ4D);
            this.groupBox4.Controls.Add(this.radQ4C);
            this.groupBox4.Controls.Add(this.radQ4B);
            this.groupBox4.Location = new System.Drawing.Point(644, 517);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(461, 258);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "做题区4";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // radQ4A
            // 
            this.radQ4A.AutoSize = true;
            this.radQ4A.Location = new System.Drawing.Point(6, 85);
            this.radQ4A.Name = "radQ4A";
            this.radQ4A.Size = new System.Drawing.Size(105, 22);
            this.radQ4A.TabIndex = 12;
            this.radQ4A.TabStop = true;
            this.radQ4A.Text = "《呐喊》";
            this.radQ4A.UseVisualStyleBackColor = true;
            this.radQ4A.CheckedChanged += new System.EventHandler(this.radQ4A_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(278, 31);
            this.label4.TabIndex = 11;
            this.label4.Text = "以下哪部作品是鲁迅的？";
            // 
            // radQ4D
            // 
            this.radQ4D.AutoSize = true;
            this.radQ4D.Location = new System.Drawing.Point(6, 220);
            this.radQ4D.Name = "radQ4D";
            this.radQ4D.Size = new System.Drawing.Size(141, 22);
            this.radQ4D.TabIndex = 10;
            this.radQ4D.TabStop = true;
            this.radQ4D.Text = "《骆驼祥子》";
            this.radQ4D.UseVisualStyleBackColor = true;
            this.radQ4D.CheckedChanged += new System.EventHandler(this.radQ4D_CheckedChanged);
            // 
            // radQ4C
            // 
            this.radQ4C.AutoSize = true;
            this.radQ4C.Location = new System.Drawing.Point(6, 177);
            this.radQ4C.Name = "radQ4C";
            this.radQ4C.Size = new System.Drawing.Size(105, 22);
            this.radQ4C.TabIndex = 9;
            this.radQ4C.TabStop = true;
            this.radQ4C.Text = "《茶馆》";
            this.radQ4C.UseVisualStyleBackColor = true;
            this.radQ4C.CheckedChanged += new System.EventHandler(this.radQ4C_CheckedChanged);
            // 
            // radQ4B
            // 
            this.radQ4B.AutoSize = true;
            this.radQ4B.Location = new System.Drawing.Point(6, 130);
            this.radQ4B.Name = "radQ4B";
            this.radQ4B.Size = new System.Drawing.Size(105, 22);
            this.radQ4B.TabIndex = 8;
            this.radQ4B.TabStop = true;
            this.radQ4B.Text = "《边城》";
            this.radQ4B.UseVisualStyleBackColor = true;
            this.radQ4B.CheckedChanged += new System.EventHandler(this.radQ4B_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radQ5A);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.radQ5D);
            this.groupBox5.Controls.Add(this.radQ5C);
            this.groupBox5.Controls.Add(this.radQ5B);
            this.groupBox5.Location = new System.Drawing.Point(41, 867);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(636, 258);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "做题区5";
            // 
            // radQ5A
            // 
            this.radQ5A.AutoSize = true;
            this.radQ5A.Location = new System.Drawing.Point(6, 85);
            this.radQ5A.Name = "radQ5A";
            this.radQ5A.Size = new System.Drawing.Size(105, 22);
            this.radQ5A.TabIndex = 12;
            this.radQ5A.TabStop = true;
            this.radQ5A.Text = "《离骚》";
            this.radQ5A.UseVisualStyleBackColor = true;
            this.radQ5A.CheckedChanged += new System.EventHandler(this.radQ5A_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(590, 31);
            this.label5.TabIndex = 11;
            this.label5.Text = "“路漫漫其修远兮，吾将上下而求索”出自哪部作品？";
            // 
            // radQ5D
            // 
            this.radQ5D.AutoSize = true;
            this.radQ5D.Location = new System.Drawing.Point(6, 220);
            this.radQ5D.Name = "radQ5D";
            this.radQ5D.Size = new System.Drawing.Size(105, 22);
            this.radQ5D.TabIndex = 10;
            this.radQ5D.TabStop = true;
            this.radQ5D.Text = "《史记》";
            this.radQ5D.UseVisualStyleBackColor = true;
            this.radQ5D.CheckedChanged += new System.EventHandler(this.radQ5D_CheckedChanged);
            // 
            // radQ5C
            // 
            this.radQ5C.AutoSize = true;
            this.radQ5C.Location = new System.Drawing.Point(6, 177);
            this.radQ5C.Name = "radQ5C";
            this.radQ5C.Size = new System.Drawing.Size(105, 22);
            this.radQ5C.TabIndex = 9;
            this.radQ5C.TabStop = true;
            this.radQ5C.Text = "《楚辞》";
            this.radQ5C.UseVisualStyleBackColor = true;
            this.radQ5C.CheckedChanged += new System.EventHandler(this.radQ5C_CheckedChanged);
            // 
            // radQ5B
            // 
            this.radQ5B.AutoSize = true;
            this.radQ5B.Location = new System.Drawing.Point(6, 130);
            this.radQ5B.Name = "radQ5B";
            this.radQ5B.Size = new System.Drawing.Size(105, 22);
            this.radQ5B.TabIndex = 8;
            this.radQ5B.TabStop = true;
            this.radQ5B.Text = "《诗经》";
            this.radQ5B.UseVisualStyleBackColor = true;
            this.radQ5B.CheckedChanged += new System.EventHandler(this.radQ5B_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1532, 1570);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtNum3);
            this.Controls.Add(this.txtNum2);
            this.Controls.Add(this.txtNum1);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.lblInfo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.TextBox txtNum1;
        private System.Windows.Forms.TextBox txtNum2;
        private System.Windows.Forms.TextBox txtNum3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radD;
        private System.Windows.Forms.RadioButton radC;
        private System.Windows.Forms.RadioButton radB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radA;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radQ2A;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radQ2D;
        private System.Windows.Forms.RadioButton radQ2C;
        private System.Windows.Forms.RadioButton radQ2B;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radQ3A;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radQ3D;
        private System.Windows.Forms.RadioButton radQ3C;
        private System.Windows.Forms.RadioButton radQ3B;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radQ4A;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radQ4D;
        private System.Windows.Forms.RadioButton radQ4C;
        private System.Windows.Forms.RadioButton radQ4B;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radQ5A;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radQ5D;
        private System.Windows.Forms.RadioButton radQ5C;
        private System.Windows.Forms.RadioButton radQ5B;
    }
}

