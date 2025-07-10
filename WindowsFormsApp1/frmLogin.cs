using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQL; // 这是你的 SQLHelper 所在的命名空间
using System.Text;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            panel1.BackColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
        }
        private string HashPassword(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 1. 获取用户输入
            string studentNo = txtLoginStudentNo.Text.Trim();
            string password = txtLoginPassword.Text;

            // 2. 验证输入是否为空
            if (string.IsNullOrWhiteSpace(studentNo) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("学号和密码不能为空！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 实例化 SQLHelper
            SQLHelper sh = new SQLHelper();

            try
            {
                // 3. 构建SQL查询，只查询该学号在数据库中存储的、已经加密的密码哈希
                string sql = string.Format("SELECT pwd FROM tblTopStudents WHERE studentNo = '{0}'", studentNo);

                // 使用 RunSelectSQLToScalar 方法，它只返回查询结果的第一行第一列，非常适合这种场景
                string dbHashedPassword = sh.RunSelectSQLToScalar(sql);

                // 4. 验证用户是否存在
                // 如果返回的密码哈希是空的，说明这个学号不存在
                if (string.IsNullOrEmpty(dbHashedPassword))
                {
                    MessageBox.Show("学号或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 5. 对用户刚刚输入的密码进行同样的哈希加密
                string inputHashedPassword = HashPassword(password);

                // 6. 核心：比较数据库中存储的哈希值 和 用户输入内容生成的哈希值
                if (dbHashedPassword.Equals(inputHashedPassword))
                {
                    // 登录成功！
                    MessageBox.Show("登录成功！", "欢迎", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 创建主窗体的实例
                    frmSQL mainForm = new frmSQL();
                    // 显示主窗体
                    mainForm.Show();
                    // 隐藏当前的登录窗体
                    this.Hide();
                }
                else
                {
                    // 登录失败！
                    MessageBox.Show("学号或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // 清空密码框，方便用户重新输入
                    txtLoginPassword.Clear();
                    txtLoginPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录时发生数据库错误：" + ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sh.Close(); // 确保数据库连接被关闭
            }
        }


    }
}
