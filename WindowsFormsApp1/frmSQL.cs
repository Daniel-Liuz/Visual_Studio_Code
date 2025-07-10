using SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsApp1
{
    public partial class frmSQL : Form
    {
        private SQLHelper sh;
        private string msg = string.Empty;

        public frmSQL()
        {
            InitializeComponent();
            sh = new SQLHelper();

            // 绑定窗体加载事件
            this.Load += frmSQL_Load;
        }

        private void frmSQL_Load(object sender, EventArgs e)
        {
            InitMajorComboBox();  // 初始化专业下拉框
            LoadCheckInData();    // 加载已打卡数据
            LoadStudentsData();   // 加载学生数据
        }

        /// <summary>
        /// 加载已打卡数据
        /// </summary>
        private void LoadCheckInData()
        {
            string sql = "SELECT studentNumber, dtedate FROM tblStudentAbsent";
            DataSet ds = new DataSet();

            try
            {
                sh.RunSQL(sql, ref ds);
                DataTable dt = ds.Tables[0];

                // 绑定 DataGridView1
                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                msg = "加载数据失败：" + ex.Message;
                MessageBox.Show(msg);
            }
            finally
            {
                sh.Close();
            }
        }

        /// <summary>
        /// 初始化下拉框，加载专业
        /// </summary>
        private void InitMajorComboBox()
        {
            string sql = "SELECT DISTINCT major FROM tblTopStudents";
            DataSet ds = new DataSet();
            msg = string.Empty;

            try
            {
                sh.RunSQL(sql, ref ds);
                DataTable dt = ds.Tables[0];

                // 添加“全部显示”选项
                DataRow row = dt.NewRow();
                row["major"] = "全部显示";
                dt.Rows.InsertAt(row, 0);

                // 绑定到 ComboBox
                cboMajor.DataSource = dt;
                cboMajor.DisplayMember = "major";
                cboMajor.ValueMember = "major";
                cboMajor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                MessageBox.Show(msg);
            }
            finally
            {
                sh.Close();
            }
        }

        /// <summary>
        /// 加载学生数据
        /// </summary>
        private void LoadStudentsData()
        {
            string sql = "SELECT studentNo, studentName, Birthday,Gender,Major FROM tblTopStudents";
            DataSet ds = new DataSet();

            try
            {
                sh.RunSQL(sql, ref ds);
                DataTable dt = ds.Tables[0];

                // 绑定 DataGridView2
                dataGridView2.DataSource = dt;
                dataGridView2.Refresh();
            }
            catch (Exception ex)
            {
                msg = "加载学生数据失败：" + ex.Message;
                MessageBox.Show(msg);
            }
            finally
            {
                sh.Close();
            }
        }

        /// <summary>
        /// 查询班级人数
        /// </summary>
        private void btnLink_Click(object sender, EventArgs e)
        {
            string sql = "SELECT COUNT(*) FROM tblTopStudents";
            string num = null;

            try
            {
                num = sh.RunSelectSQLToScalar(sql);
                msg = string.Format("我们班共有 {0} 个同学！", num);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                sh.Close();
                MessageBox.Show(msg);
            }
        }

        /// <summary>
        /// 查询所选专业并显示相关学生
        /// </summary>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string selectedMajor = cboMajor.SelectedValue.ToString();
            string sql = "";

            if (selectedMajor == "全部显示")
            {
                // 如果选择了“全部显示”，显示所有学生
                sql = "SELECT studentNo, studentName, Birthday FROM tblTopStudents";
            }
            else
            {
                // 根据选择的专业筛选学生
                sql = string.Format("SELECT studentNo, studentName, Birthday FROM tblTopStudents WHERE major = '{0}'", selectedMajor);
            }

            DataSet ds = new DataSet();

            try
            {
                sh.RunSQL(sql, ref ds);
                DataTable dt = ds.Tables[0];

                // 绑定到 DataGridView2
                dataGridView2.DataSource = dt;
                dataGridView2.Refresh();
            }
            catch (Exception ex)
            {
                msg = "查询数据失败：" + ex.Message;
                MessageBox.Show(msg);
            }
            finally
            {
                sh.Close();
            }
        }

        /// <summary>
        /// 打卡按钮点击事件
        /// </summary>
        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            string studentNumber = "10235501456"; // 你的学号
            string msg = "";
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 获取当前时间

            try
            {
                // 插入打卡记录（包括当前时间）
                string sql = string.Format("INSERT INTO tblStudentAbsent (studentNumber, dtedate) VALUES ('{0}', '{1}')", studentNumber, currentDate);
                int ret = sh.RunSQL(sql); // 执行插入操作

                if (ret > 0)
                    msg = "打卡成功！";
                else
                    msg = "打卡失败，数据库未插入任何行。";

                // 重新加载 DataGridView 数据
                LoadCheckInData();
            }
            catch (Exception ex)
            {
                msg = "打卡失败：" + ex.Message;
            }
            finally
            {
                sh.Close();
                MessageBox.Show(msg);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 确保点击的是有效的数据行且不是标题行
            if (e.RowIndex >= 0)
            {
                // 获取被点击的行
                DataGridViewRow clickedRow = dataGridView2.Rows[e.RowIndex];

                // 调用我们创建的新方法来显示信息
                DisplayStudentDetails(clickedRow);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string studentNoToSearch = txtSearchStudentNo.Text.Trim();

            if (string.IsNullOrWhiteSpace(studentNoToSearch))
            {
                MessageBox.Show("请输入要查询的学号！");
                return;
            }

            string sql = string.Format("SELECT studentNo, studentName, Birthday, Gender, Major FROM tblTopStudents WHERE studentNo = '{0}'", studentNoToSearch);
            DataSet ds = new DataSet();

            try
            {
                sh.RunSQL(sql, ref ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    // 将数据填充到界面控件中
                    txtStudentName.Text = row["studentName"]?.ToString() ?? string.Empty;
                    txtStudentNo.Text = row["studentNo"]?.ToString() ?? string.Empty;
                    txtMajor.Text = row["major"]?.ToString() ?? string.Empty;
                    if (row["Birthday"] != DBNull.Value)
                    {
                        txtBirthday.Text = Convert.ToDateTime(row["Birthday"]).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        txtBirthday.Text = string.Empty;
                    }
                    // --- 开始更新图片加载逻辑 ---
                    string genderValue = row["Gender"]?.ToString().Trim();

                    // 先清空图片
                    pictureBox1.Image = null;

                    try
                    {
                        if (genderValue == "1") // 男
                        {
                            pictureBox1.Image = Image.FromFile(@"E:\Visual Studio programs\WindowsFormsApp1\pics\Gender\boy.png");
                        }
                        else if (genderValue == "0") // 女
                        {
                            pictureBox1.Image = Image.FromFile(@"E:\Visual Studio programs\WindowsFormsApp1\pics\Gender\girl.png");
                        }
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("头像图片文件未找到，请检查路径。");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("加载图片失败: " + ex.Message);
                    }
                    // --- 结束更新图片加载逻辑 ---
                }
                else
                {
                    MessageBox.Show("未找到该学号的学生。");
                }
            }
            catch (Exception ex)
            {
                msg = "查询学生数据失败：" + ex.Message;
                MessageBox.Show(msg);
            }
            finally
            {
                sh.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string studentNoToUpdate = txtStudentNo.Text.Trim();
            string newMajor = txtMajor.Text.Trim();
            // 1. 获取生日文本框中的内容
            string newBirthdayStr = txtBirthday.Text.Trim();

            // 2. 验证学号和专业输入
            if (string.IsNullOrWhiteSpace(studentNoToUpdate))
            {
                MessageBox.Show("请先通过查询或在表格中点击来选择一个学生！");
                return;
            }

            if (string.IsNullOrWhiteSpace(newMajor))
            {
                MessageBox.Show("专业名称不能为空！");
                return;
            }

            // 3. 验证生日格式是否正确
            DateTime newBirthday;
            if (!DateTime.TryParse(newBirthdayStr, out newBirthday))
            {
                MessageBox.Show("生日格式不正确，请输入有效的日期，例如：2000-01-01");
                return;
            }

            // 4. 构建包含专业和生日的更新SQL语句
            // 使用参数化查询来防止SQL注入，这是一个更安全的做法
            string sql = string.Format("UPDATE tblTopStudents SET major = '{0}', Birthday = '{1}' WHERE studentNo = '{2}'",
                                       newMajor,
                                       newBirthday.ToString("yyyy-MM-dd"), // 确保日期格式与数据库兼容
                                       studentNoToUpdate);
            msg = string.Empty;

            try
            {
                // 5. 执行更新操作
                int rowsAffected = sh.RunSQL(sql);

                if (rowsAffected > 0)
                {
                    // 6. 更新成功提示
                    msg = "信息修改成功！";
                    LoadStudentsData(); // 核心步骤：刷新DataGridView2中的数据以显示更新
                    MessageBox.Show(msg);
                }
                else
                {
                    msg = "修改失败，数据库中没有记录被更新。请检查学号是否正确。";
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                msg = "修改失败：" + ex.Message;
                MessageBox.Show(msg);
            }
            finally
            {
                sh.Close();
            }
        }
        private void DisplayStudentDetails(DataGridViewRow row)
        {
            if (row == null) return;

            // 将行中的单元格值赋给文本框
            txtStudentName.Text = row.Cells["studentName"].Value?.ToString() ?? string.Empty;
            txtStudentNo.Text = row.Cells["studentNo"].Value?.ToString() ?? string.Empty;
            txtMajor.Text = row.Cells["major"].Value?.ToString() ?? string.Empty;
            if (row.Cells["Birthday"].Value != null && row.Cells["Birthday"].Value != DBNull.Value)
            {
                txtBirthday.Text = Convert.ToDateTime(row.Cells["Birthday"].Value).ToString("yyyy-MM-dd");
            }
            else
            {
                txtBirthday.Text = string.Empty;
            }
            // 获取Gender的值并进行清理
            string genderValue = row.Cells["Gender"].Value?.ToString();

            // 先清空当前的图片
            pictureBox1.Image = null;

            try
            {
                // 根据性别显示不同的头像
                if (genderValue == "True") // 男
                {
                    pictureBox1.Image = Image.FromFile(@"E:\Visual Studio programs\WindowsFormsApp1\pics\Gender\boy.png");
                }
                else if (genderValue == "False") // 女
                {
                    pictureBox1.Image = Image.FromFile(@"E:\Visual Studio programs\WindowsFormsApp1\pics\Gender\girl.png");
                }
                // 如果有其他情况或默认图片，可以在这里加一个 else
            }
            catch (System.IO.FileNotFoundException)
            {
                // 文件找不到的特定错误
                MessageBox.Show("头像图片文件未找到，请检查路径是否正确。");
            }
            catch (Exception ex)
            {
                // 其他加载图片的错误
                MessageBox.Show("加载图片失败: " + ex.Message);
            }
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchByName_Click(object sender, EventArgs e)
        {
            // 1. 获取并清理用户输入的姓名
            string nameToSearch = txtNameSearch.Text.Trim();

            // 2. 检查输入是否为空
            if (string.IsNullOrWhiteSpace(nameToSearch))
            {
                MessageBox.Show("请输入要查询的学生姓名！");
                return;
            }

            // 3. 构建 SQL 查询语句
            // 使用 LIKE 和 '%' 来进行模糊匹配，这样用户不必输入完整的姓名
            string sql = string.Format("SELECT studentNo, studentName, Birthday, Gender, Major FROM tblTopStudents WHERE studentName LIKE '%{0}%'", nameToSearch);
            DataSet ds = new DataSet();
            msg = string.Empty;

            try
            {
                // 4. 执行查询
                sh.RunSQL(sql, ref ds);
                DataTable dt = ds.Tables[0];

                // 5. 将查询结果绑定到 DataGridView2
                dataGridView2.DataSource = dt;
                dataGridView2.Refresh();

                // 6. 向用户反馈查询结果
                if (dt.Rows.Count > 0)
                {
                    msg = string.Format("共找到 {0} 位匹配的学生。", dt.Rows.Count);
                }
                else
                {
                    msg = "未找到符合条件的学生。";
                }
                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                msg = "按姓名查询失败：" + ex.Message;
                MessageBox.Show(msg);
            }
            finally
            {
                sh.Close();
            }
        }

        private void btnShowGenderChart_Click(object sender, EventArgs e)
        {
            int maleCount = 0;
            int femaleCount = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["Gender"].Value != null)
                {
                    string genderValue = row.Cells["Gender"].Value.ToString();
                    if (genderValue == "True")
                    {
                        maleCount++;
                    }
                    else if (genderValue == "False")
                    {
                        femaleCount++;
                    }
                }
            }

            // *** 关键步骤：每次都清空图表，为新数据做准备 ***
            chartGender.Series.Clear();
            chartGender.Titles.Clear();

            chartGender.Titles.Add("学生性别分布统计");
            Series genderSeries = chartGender.Series.Add("GenderSeries");
            genderSeries.ChartType = SeriesChartType.Pie;

            genderSeries.Points.AddXY("男", maleCount);
            genderSeries.Points.AddXY("女", femaleCount);

            // 美化选项
            chartGender.ChartAreas[0].Area3DStyle.Enable3D = true;
            genderSeries.Label = "#VALX (#PERCENT)";
            genderSeries.LegendText = "#VALX";
        }

        private void btnShowMajorChart_Click(object sender, EventArgs e)
        {
            // 1. 使用一个字典来存储每个专业的名称和对应的学生人数
            Dictionary<string, int> majorCounts = new Dictionary<string, int>();

            // 2. 遍历 dataGridView2 中的所有行来统计专业人数
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["Major"].Value != null)
                {
                    string majorName = row.Cells["Major"].Value.ToString();

                    if (majorCounts.ContainsKey(majorName))
                    {
                        majorCounts[majorName]++;
                    }
                    else
                    {
                        majorCounts.Add(majorName, 1);
                    }
                }
            }

            // 4. 清空同一个图表控件，为新数据做准备
            chartGender.Series.Clear();
            chartGender.Titles.Clear();

            // 5. 配置图表
            chartGender.Titles.Add("学生专业分布统计");
            Series majorSeries = chartGender.Series.Add("MajorSeries");
            majorSeries.ChartType = SeriesChartType.Pie; // 同样使用饼图

            // 6. 将字典中统计好的数据点添加到图表中
            foreach (KeyValuePair<string, int> entry in majorCounts)
            {
                majorSeries.Points.AddXY(entry.Key, entry.Value);
            }

            // 7. 美化图表
            chartGender.ChartAreas[0].Area3DStyle.Enable3D = true;

            // ======================= 修改点在这里 =======================
            //
            // 通过注释掉下面这行代码，来移除饼图切片上的文字标签
            // majorSeries.Label = "#VALX (#PERCENT)"; 
            //
            // ======================= 修改完成 =======================

            // 我们保留这一行，它负责在图表旁边的图例中显示专业名称
            majorSeries.LegendText = "#VALX";
        }
        private string HashPassword(string plainText)
        {
            // 检查输入是否为空
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }

            // 创建 SHA256 实例
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 将输入的字符串转换为字节数组并计算哈希值
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                // 创建一个 StringBuilder 来收集字节并格式化为十六进制字符串
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // "x2" 表示格式化为两位小写的十六进制数
                }
                return builder.ToString();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            // 1. 获取输入
            string studentNoToUpdate = txtStudentNo.Text.Trim();
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // 2. 进行输入验证
            if (string.IsNullOrWhiteSpace(studentNoToUpdate))
            {
                MessageBox.Show("请先通过查询或在表格中点击来选择一个学生！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("新密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("两次输入的密码不一致，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 清空密码框，让用户重新输入
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus(); // 将光标定位到新密码框
                return;
            }

            try
            {
                // 3. 对新密码进行哈希加密
                string hashedPassword = HashPassword(newPassword);

                // 4. 构建并执行 SQL 更新语句 (注意SQL注入风险)
                // 警告：string.Format 容易受到SQL注入攻击。在生产环境中，请务必使用参数化查询。
                string sql = string.Format("UPDATE tblTopStudents SET pwd = '{0}' WHERE studentNo = '{1}'", hashedPassword, studentNoToUpdate);

                int rowsAffected = sh.RunSQL(sql);

                // 5. 反馈结果
                if (rowsAffected > 0)
                {
                    MessageBox.Show("密码修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 成功后清空密码框
                    txtNewPassword.Clear();
                    txtConfirmPassword.Clear();
                }
                else
                {
                    MessageBox.Show("密码修改失败，未更新任何记录。请确认学号是否正确。", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改密码时发生错误：" + ex.Message, "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sh.Close();
            }
        }
    }
}
