using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using SQL; // 请确保这是您的 SQLHelper 所在的命名空间

namespace WindowsFormsApp1
{
    public partial class frmPasswordReset : Form
    {
        // 【硬编码】指定“安全问题”功能仅对这个特定学号生效
        private const string TargetStudentNo = "10235501456";

        public frmPasswordReset()
        {
            InitializeComponent();
        }

        // 窗体加载时，确保安全问题组合框是隐藏的
        private void frmPasswordReset_Load(object sender, EventArgs e)
        {
            gbSecurityQuestion.Visible = false;
        }

        #region =========== 1. 通过邮箱找回 ===========

        // “发送重置邮件”按钮的逻辑
        private void btnSendResetEmail_Click(object sender, EventArgs e)
        {
            string studentNo = txtResetStudentNo.Text.Trim();
            if (string.IsNullOrEmpty(studentNo))
            {
                MessageBox.Show("学号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 如果安全问题框是可见的，先将其隐藏，避免界面混乱
            if (gbSecurityQuestion.Visible)
            {
                gbSecurityQuestion.Visible = false;
            }

            SQLHelper sh = new SQLHelper();
            try
            {
                string sqlGetEmail = $"SELECT Email FROM tblTopStudents WHERE studentNo = '{studentNo}'";
                string userEmail = sh.RunSelectSQLToScalar(sqlGetEmail);

                if (string.IsNullOrEmpty(userEmail))
                {
                    MessageBox.Show("未找到该学号对应的用户，或该用户未在系统中预留邮箱地址。", "查询失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tempPassword = System.IO.Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
                string hashedTempPassword = HashPassword(tempPassword);
                string sqlUpdatePass = $"UPDATE tblTopStudents SET pwd = '{hashedTempPassword}' WHERE studentNo = '{studentNo}'";
                sh.RunSQL(sqlUpdatePass);

                SendEmail(userEmail, tempPassword);

                MessageBox.Show("操作成功！\n一封包含临时密码的邮件已发送到您的邮箱，请查收并使用新密码登录。", "发送成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败，请检查您的网络或邮箱配置。\n错误详情: " + ex.Message, "发送失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sh.Close();
            }
        }

        // 邮件发送方法 (代码保持不变)
        private void SendEmail(string recipientEmail, string password)
        {
            string fromEmail = "1195773178@qq.com";
            string fromPassword = "jidwtfljqebkicej"; // 这是邮箱的授权码，不是登录密码
            string smtpHost = "smtp.qq.com";
            int smtpPort = 587;

            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmail, fromPassword);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromEmail, "学生管理系统");
            mailMessage.To.Add(recipientEmail);
            mailMessage.Subject = "【学生管理系统】 - 密码重置通知";
            mailMessage.Body = $"您好：\n\n您正在使用密码重置功能。您的新临时密码是：\n\n【 {password} 】\n\n请使用此密码登录系统，并尽快在个人中心修改为您自己的常用密码。\n\n【学生管理系统】";
            mailMessage.IsBodyHtml = false;
            client.Send(mailMessage);
        }

        #endregion

        #region =========== 2. 通过安全问题找回 ===========

        // “使用安全问题找回”按钮的逻辑
        private void btnShowSecurityQuestion_Click(object sender, EventArgs e)
        {
            if (txtResetStudentNo.Text.Trim().Equals(TargetStudentNo))
            {
                // 【硬编码】设置问题文本
                lblQuestion1.Text = "问题1: 您最喜欢的颜色是什么?";
                lblQuestion2.Text = "问题2: 您的小学叫什么名字?";

                // 清空可能残留的答案输入
                txtAnswer1.Clear();
                txtAnswer2.Clear();

                // 显示安全问题验证框
                gbSecurityQuestion.Visible = true;
            }
            else
            {
                MessageBox.Show("此用户不支持通过安全问题找回密码，请使用邮箱方式或联系管理员。", "操作受限", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // 确保在不支持的情况下，安全问题框是隐藏的
                gbSecurityQuestion.Visible = false;
            }
        }

        // “验证答案”按钮的逻辑
        private void btnVerifyAnswers_Click(object sender, EventArgs e)
        {
            string answer1 = txtAnswer1.Text.Trim();
            string answer2 = txtAnswer2.Text.Trim();

            // 【硬编码】直接进行答案的字符串匹配
            if (answer1.Equals("蓝色") && answer2.Equals("东二小学"))
            {
                try
                {
                    string tempPassword = System.IO.Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
                    string hashedTempPassword = HashPassword(tempPassword);

                    SQLHelper sh = new SQLHelper();
                    try
                    {
                        string sqlUpdatePass = $"UPDATE tblTopStudents SET pwd = '{hashedTempPassword}' WHERE studentNo = '{TargetStudentNo}'";
                        sh.RunSQL(sqlUpdatePass);

                        MessageBox.Show(
                            $"验证成功！您的密码已重置。\n\n您的新临时密码是： 【 {tempPassword} 】\n\n请使用此密码登录。",
                            "密码已重置",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        this.Close(); // 完成后关闭窗口
                    }
                    finally
                    {
                        sh.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("重置密码时发生错误: " + ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("安全问题答案错误，请重试！", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region =========== 3. 公用加密方法 ===========

        // 密码哈希方法 (代码保持不变)
        private string HashPassword(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
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

        #endregion
    }
}
