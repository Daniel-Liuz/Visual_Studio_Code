using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQL;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsApp1
{
    public partial class frmPasswordReset : Form
    {
        public frmPasswordReset()
        {
            InitializeComponent();
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
        private void SendEmail(string recipientEmail, string password)
        {
            // --- 已更新为您的QQ邮箱信息 ---
            string fromEmail = "1195773178@qq.com";        // 您的QQ邮箱地址
            string fromPassword = "jidwtfljqebkicej";      // 您的QQ邮箱授权码
            string smtpHost = "smtp.qq.com";               // QQ邮箱的SMTP服务器地址
            int smtpPort = 587;                            // QQ邮箱推荐的端口号，启用STARTTLS加密

            // 创建邮件发送客户端
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);

            // 关键：对于587端口，必须启用SSL，这会自动使用STARTTLS安全连接
            client.EnableSsl = true;

            client.UseDefaultCredentials = false;
            // 提供发件人的凭据（QQ邮箱地址和授权码）
            client.Credentials = new System.Net.NetworkCredential(fromEmail, fromPassword);

            // 创建邮件内容对象
            MailMessage mailMessage = new MailMessage();
            // 设置发件人，可以额外指定一个显示名称
            mailMessage.From = new MailAddress(fromEmail, "学生管理系统");
            // 添加收件人
            mailMessage.To.Add(recipientEmail);
            // 设置邮件主题
            mailMessage.Subject = "【学生管理系统】 - 密码重置通知";
            // 设置邮件正文
            mailMessage.Body = string.Format(
                "您好：\n\n" +
                "您正在使用密码重置功能。您的新临时密码是：\n\n" +
                "【 {0} 】\n\n" +
                "请使用此密码登录系统，并尽快在个人中心修改为您自己的常用密码。\n\n" +
                "【学生管理系统】",
                password
            );
            // 确保邮件正文是纯文本格式
            mailMessage.IsBodyHtml = false;

            // 正式发送邮件
            client.Send(mailMessage);
        }
        private void btnSendResetEmail_Click(object sender, EventArgs e)
        {
            // 1. 获取用户输入的学号 (您的代码，无需改动)
            string studentNo = txtResetStudentNo.Text.Trim();
            if (string.IsNullOrEmpty(studentNo))
            {
                MessageBox.Show("学号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SQLHelper sh = new SQLHelper();
            try
            {
                // 2. 根据学号查询用户的邮箱地址 (您的代码，无需改动)
                string sqlGetEmail = string.Format("SELECT Email FROM tblTopStudents WHERE studentNo = '{0}'", studentNo);
                string userEmail = sh.RunSelectSQLToScalar(sqlGetEmail);

                if (string.IsNullOrEmpty(userEmail))
                {
                    MessageBox.Show("未找到该学号对应的用户，或该用户未在系统中预留邮箱地址。", "查询失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. 生成一个新的8位随机临时密码 (您的代码，无需改动)
                string tempPassword = System.IO.Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

                // 4. 将新生成的【临时密码】进行哈希加密 (您的代码，无需改动)
                string hashedTempPassword = HashPassword(tempPassword);

                // 5. 构建SQL语句，更新数据库中的密码 (您的代码，无需改动)
                string sqlUpdatePass = string.Format("UPDATE tblTopStudents SET pwd = '{0}' WHERE studentNo = '{1}'", hashedTempPassword, studentNo);
                int rowsAffected = sh.RunSQL(sqlUpdatePass);

                if (rowsAffected == 0)
                {
                    MessageBox.Show("更新密码时发生未知错误，请稍后重试。", "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 6. 调用邮件发送方法 (您的代码，无需改动)
                SendEmail(userEmail, tempPassword);

                // 7. 提示用户操作成功 (您的代码，无需改动)
                MessageBox.Show("操作成功！\n一封包含临时密码的邮件已发送到您的邮箱，请查收并使用新密码登录。", "发送成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            // === 以下是【增强版】的异常处理 ===
            catch (SmtpException smtpEx) // 专门捕获邮件发送相关的异常
            {
                // SmtpException 提供了更丰富的邮件错误信息
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.AppendLine("邮件发送失败！请检查您的邮箱配置或网络。");
                errorMessage.AppendLine("----------------------------------------");
                errorMessage.AppendLine("错误类型: SMTP 错误");
                errorMessage.AppendLine("错误代码: " + smtpEx.StatusCode); // 例如: 535, 451
                errorMessage.AppendLine("详细信息: " + smtpEx.Message);

                // 内部异常 InnerException 往往包含最根本的原因！
                if (smtpEx.InnerException != null)
                {
                    errorMessage.AppendLine("根本原因: " + smtpEx.InnerException.Message);
                }

                MessageBox.Show(errorMessage.ToString(), "邮件发送错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Data.SqlClient.SqlException sqlEx) // 专门捕获数据库相关的异常
            {
                MessageBox.Show("操作失败，与数据库交互时发生错误。\n\n错误详情: " + sqlEx.Message, "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // 捕获其他所有未预料到的异常
            {
                MessageBox.Show("发生未知错误。\n\n错误详情: " + ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 无论成功还是失败，最后都要确保关闭数据库连接
                sh.Close();
            }
        }

    }
}
