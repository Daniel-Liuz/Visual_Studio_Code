using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private int counter = 0;  // 用于存储计时器的计数

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            lblTimer.Text = "0";  // 初始化标签显示为0
            timer1.Interval = 1000;  // 设置计时器每1秒触发一次
                                     // 图片文件路径
            string imagePath = @"E:\Visual Studio programs\WindowsFormsApp1\pics\pic1.jpg";

            // 加载图片到 PictureBox
            pictureBox1.Image = Image.FromFile(imagePath);

            // 设置图片的显示模式（可选）
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // 将图片拉伸到适应控件大小
                                                                    // pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage; // 图片居中显示
        }



        // Timer的Tick事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;  // 每秒递增
            lblTimer.Text = counter.ToString();  // 更新显示
        }

        // 按钮点击事件
        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();  // 停止计时器
                btnTimer.Text = "Start Timer";  // 修改按钮文本为“Start Timer”
            }
            else
            {
                timer1.Start();  // 启动计时器
                btnTimer.Text = "Stop Timer";  // 修改按钮文本为“Stop Timer”
            }
        }

        private void cboNative_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dptStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
