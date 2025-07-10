using System;
using System.Configuration; // 用于 App.config
using System.Security.Cryptography; // 用于加密和哈希
using System.Text;
using System.Windows.Forms;
using SQL; // 这是您的 SQLHelper 所在的命名空间

namespace WindowsFormsApp1
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            panel1.BackColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
        }

        // 窗体加载事件 (已修改为安全版本)
        private void frmLogin_Load(object sender, EventArgs e)
        {
            // 窗体加载时检查 App.config
            string rememberMe = ConfigurationManager.AppSettings["RememberMe"];

            // 只有当明确记录了 "true" 时才执行自动填充
            if (rememberMe == "true")
            {
                chkRememberMe.Checked = true;

                string savedStudentNo = ConfigurationManager.AppSettings["studentNo"];
                string encryptedPassword = ConfigurationManager.AppSettings["pwd"]; // 这是加密后的密码

                if (!string.IsNullOrEmpty(savedStudentNo) && !string.IsNullOrEmpty(encryptedPassword))
                {
                    txtLoginStudentNo.Text = savedStudentNo;
                    // 使用我们的解密方法来还原密码
                    txtLoginPassword.Text = DecryptString(encryptedPassword);
                }
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        // 登录按钮点击事件 (已修改为安全版本)
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

            SQLHelper sh = new SQLHelper();
            try
            {
                // 3. 查询数据库中存储的密码哈希
                string sql = string.Format("SELECT pwd FROM tblTopStudents WHERE studentNo = '{0}'", studentNo);
                string dbHashedPassword = sh.RunSelectSQLToScalar(sql);

                // 4. 验证用户是否存在
                if (string.IsNullOrEmpty(dbHashedPassword))
                {
                    MessageBox.Show("学号或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 5. 对用户输入的密码进行同样的哈希加密
                string inputHashedPassword = HashPassword(password);

                // 6. 比较两个哈希值
                if (dbHashedPassword.Equals(inputHashedPassword))
                {
                    // 登录成功！处理“记住我”功能
                    HandleRememberMe();

                    MessageBox.Show("登录成功！", "欢迎", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 显示主窗体
                    frmSQL mainForm = new frmSQL();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    // 登录失败
                    MessageBox.Show("学号或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // 忘记密码链接
        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPasswordReset resetForm = new frmPasswordReset();
            resetForm.ShowDialog();
        }


        #region 辅助方法

        /// <summary>
        /// 处理“记住我”的逻辑，在登录成功后调用
        /// </summary>
        private void HandleRememberMe()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (chkRememberMe.Checked)
            {
                // 使用我们新的加密方法来加密密码
                string encryptedPassword = EncryptString(txtLoginPassword.Text);

                // 将学号、加密后的密码和勾选状态写入配置文件
                SetAppSetting(config, "studentNo", txtLoginStudentNo.Text);
                SetAppSetting(config, "pwd", encryptedPassword); // 保存的是加密后的密码！
                SetAppSetting(config, "RememberMe", "true");
            }
            else
            {
                // 如果未勾选，则清除凭据
                SetAppSetting(config, "studentNo", "");
                SetAppSetting(config, "pwd", "");
                SetAppSetting(config, "RememberMe", "false");
            }

            // 保存对配置文件的修改
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 一个辅助方法，用于添加或更新 appSettings 中的键值对，避免重复代码
        /// </summary>
        private void SetAppSetting(Configuration config, string key, string value)
        {
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
        }

        /// <summary>
        /// 对输入的明文密码进行 SHA256 哈希计算，用于数据库比对
        /// </summary>
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

        #endregion


        #region 安全加密与解密方法 (用于 App.config)

        // 定义一个固定的“盐”值（熵），增加破解难度。
        private static readonly byte[] s_entropy = { 1, 22, 33, 4, 15, 66, 17, 88 };

        /// <summary>
        /// 使用当前 Windows 用户凭据加密字符串
        /// </summary>
        private string EncryptString(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            byte[] dataToProtect = Encoding.UTF8.GetBytes(input);
            byte[] protectedData = ProtectedData.Protect(dataToProtect, s_entropy, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(protectedData);
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        private string DecryptString(string encryptedInput)
        {
            if (string.IsNullOrEmpty(encryptedInput)) return "";
            try
            {
                byte[] protectedData = Convert.FromBase64String(encryptedInput);
                byte[] data = ProtectedData.Unprotect(protectedData, s_entropy, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(data);
            }
            catch
            {
                // 如果解密失败（例如，换了电脑或Windows用户），返回空字符串
                return "";
            }
        }

        #endregion

        private void chkRememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}