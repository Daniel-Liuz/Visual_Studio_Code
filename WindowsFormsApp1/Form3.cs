using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private List<string> currentImages = new List<string>(); // 存储当前子类别的图片路径
        private int currentImageIndex = 0; // 当前显示的图片索引

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // 添加一级分类
            TreeNode sportsNode = treeView1.Nodes.Add("体育");
            TreeNode artNode = treeView1.Nodes.Add("艺术");

            // 添加二级分类
            sportsNode.Nodes.Add("篮球");
            sportsNode.Nodes.Add("羽毛球");
            sportsNode.Nodes.Add("网球");

            artNode.Nodes.Add("钢琴");
            artNode.Nodes.Add("小提琴");
            artNode.Nodes.Add("萨克斯");
        }
        private void LoadImages(string categoryPath)
        {
            // 清空当前图片列表和索引
            currentImages.Clear();
            currentImageIndex = 0;

            // 构建图片文件夹路径
            string folderPath = Path.Combine(Application.StartupPath, "Images", categoryPath);

            if (Directory.Exists(folderPath))
            {
                // 获取所有 jpg 文件
                string[] imageFiles = Directory.GetFiles(folderPath, "*.png");

                // 将图片路径添加到列表
                currentImages.AddRange(imageFiles);

                // 如果有图片，显示第一张并启动 Timer
                if (currentImages.Count > 0)
                {
                    try
                    {
                        pictureBox1.Image = Image.FromFile(currentImages[currentImageIndex]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("加载图片时出错: " + ex.Message);
                    }
                    timer1.Start(); // 启动 Timer
                }
                else
                {
                    pictureBox1.Image = null;
                    timer1.Stop(); // 无图片时停止 Timer
                }
            }
            else
            {
                pictureBox1.Image = null;
                timer1.Stop(); // 文件夹不存在时停止 Timer
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 获取选中的节点路径（例如 "体育\篮球"）
            string selectedCategory = e.Node.FullPath;

            // 如果是二级分类，加载图片
            if (e.Node.Parent != null) // 确保不是一级分类
            {
                LoadImages(selectedCategory);
            }
            else
            {
                // 选择一级分类时，停止 Timer 并清空图片
                timer1.Stop();
                pictureBox1.Image = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentImages.Count > 0)
            {
                // 切换到下一张图片，循环播放
                currentImageIndex = (currentImageIndex + 1) % currentImages.Count;
                pictureBox1.Image = Image.FromFile(currentImages[currentImageIndex]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentImages.Count > 0)
    {
        currentImageIndex = (currentImageIndex + 1) % currentImages.Count;
        pictureBox1.Image = Image.FromFile(currentImages[currentImageIndex]);
    }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentImages.Count > 0)
            {
                currentImageIndex = (currentImageIndex - 1 + currentImages.Count) % currentImages.Count;
                pictureBox1.Image = Image.FromFile(currentImages[currentImageIndex]);
            }
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                btnPlayPause.Text = "播放";
            }
            else
            {
                timer1.Start();
                btnPlayPause.Text = "暂停";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.ToLower(); // 获取用户输入的关键词并转为小写
            foreach (TreeNode node in treeView1.Nodes) // 遍历一级分类
            {
                foreach (TreeNode subNode in node.Nodes) // 遍历二级分类
                {
                    if (subNode.Text.ToLower().Contains(keyword)) // 检查是否包含关键词
                    {
                        treeView1.SelectedNode = subNode; // 选中匹配的节点
                        LoadImages(subNode.FullPath); // 加载该类别的图片
                        return; // 找到后退出循环
                    }
                }
            }
            MessageBox.Show("未找到匹配的类别！"); // 如果没找到，提示用户
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
