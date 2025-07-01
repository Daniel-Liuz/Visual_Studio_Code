using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string s = string.Empty;
            int currentHour = DateTime.Now.Hour;
            //返回0（午夜）到23（晚上11点）的小时数
            if (currentHour < 12)
            {
                s = "早上好";
            }
            else if (currentHour < 18)
            {
                s = "下午好";
            }
            else
            {
                s = "晚上好";
            }
            lblInfo.Text = s + "，欢迎使用本程序！";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是一个简单的 Windows Forms 应用程序。\n\n" +
                "作者：刘子阳\n" +
                "日期：2025年7月1日", 
                "关于本程序", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

        private void txtNum1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNum2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNum3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 获取文本框中的内容
            string num1Text = txtNum1.Text;
            string num2Text = txtNum2.Text;
            string num3Text = txtNum3.Text;

            // 验证并转换为数值
            if (double.TryParse(num1Text, out double num1) &&
                double.TryParse(num2Text, out double num2) &&
                double.TryParse(num3Text, out double num3))
            {
                double result;
                string message;
                // 根据 checkBox1 的勾选状态决定求最大值还是最小值
                if (checkBox1.Checked)
                {
                    result = Math.Min(num1, Math.Min(num2, num3));
                    message = $"最小值为：{result}";
                }
                else
                {
                    result = Math.Max(num1, Math.Max(num2, num3));
                    message = $"最大值为：{result}";
                }
                // 显示结果
                MessageBox.Show(message, "结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // 输入无效时提示
                MessageBox.Show("请输入有效的数值。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        // 单选按钮的事件处理程序
        private void radA_CheckedChanged(object sender, EventArgs e)
        {
            if (radA.Checked)
            {
                MessageBox.Show("回答正确！", "结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radB_CheckedChanged(object sender, EventArgs e)
        {
            if (radB.Checked)
            {
                MessageBox.Show("错误：正确答案是北京", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radC_CheckedChanged(object sender, EventArgs e)
        {
            if (radC.Checked)
            {
                MessageBox.Show("错误：正确答案是北京", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radD_CheckedChanged(object sender, EventArgs e)
        {
            if (radD.Checked)
            {
                MessageBox.Show("错误：正确答案是北京", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 问题2 单选按钮事件处理程序
        private void radQ2A_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ2A.Checked)
            {
                MessageBox.Show("回答正确！", "结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radQ2B_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ2B.Checked)
            {
                MessageBox.Show("错误：正确答案是曹雪芹", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radQ2C_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ2C.Checked)
            {
                MessageBox.Show("错误：正确答案是曹雪芹", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radQ2D_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ2D.Checked)
            {
                MessageBox.Show("错误：正确答案是曹雪芹", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 问题3 单选按钮事件处理程序
        private void radQ3A_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ3A.Checked)
            {
                MessageBox.Show("回答正确！", "结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radQ3B_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ3B.Checked)
            {
                MessageBox.Show("错误：正确答案是菩提祖师", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radQ3C_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ3C.Checked)
            {
                MessageBox.Show("错误：正确答案是菩提祖师", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radQ3D_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ3D.Checked)
            {
                MessageBox.Show("错误：正确答案是菩提祖师", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        // 问题4 单选按钮事件处理程序
        private void radQ4A_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ4A.Checked)
            {
                MessageBox.Show("回答正确！", "结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radQ4B_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ4B.Checked)
            {
                MessageBox.Show("错误：正确答案是《呐喊》", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radQ4C_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ4C.Checked)
            {
                MessageBox.Show("错误：正确答案是《呐喊》", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radQ4D_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ4D.Checked)
            {
                MessageBox.Show("错误：正确答案是《呐喊》", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 问题5 单选按钮事件处理程序
        private void radQ5A_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ5A.Checked)
            {
                MessageBox.Show("回答正确！", "结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radQ5B_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ5B.Checked)
            {
                MessageBox.Show("错误：正确答案是《离骚》", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radQ5C_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ5C.Checked)
            {
                MessageBox.Show("错误：正确答案是《离骚》", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radQ5D_CheckedChanged(object sender, EventArgs e)
        {
            if (radQ5D.Checked)
            {
                MessageBox.Show("错误：正确答案是《离骚》", "结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
