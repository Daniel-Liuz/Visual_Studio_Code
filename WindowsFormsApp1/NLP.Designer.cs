namespace WindowsFormsApp1
{
    partial class NLP
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
            this.txtInputText = new System.Windows.Forms.TextBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.lbxRelations = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtInputText
            // 
            this.txtInputText.Location = new System.Drawing.Point(105, 81);
            this.txtInputText.Name = "txtInputText";
            this.txtInputText.Size = new System.Drawing.Size(148, 28);
            this.txtInputText.TabIndex = 0;
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(365, 59);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(181, 68);
            this.btnExtract.TabIndex = 1;
            this.btnExtract.Text = "关系模型";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // lbxRelations
            // 
            this.lbxRelations.FormattingEnabled = true;
            this.lbxRelations.ItemHeight = 18;
            this.lbxRelations.Location = new System.Drawing.Point(105, 137);
            this.lbxRelations.Name = "lbxRelations";
            this.lbxRelations.Size = new System.Drawing.Size(441, 274);
            this.lbxRelations.TabIndex = 2;
            // 
            // NLP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbxRelations);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.txtInputText);
            this.Name = "NLP";
            this.Text = "NLP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInputText;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.ListBox lbxRelations;
    }
}