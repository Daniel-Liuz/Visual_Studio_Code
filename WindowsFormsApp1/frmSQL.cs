using System;
using System.Data;
using System.Windows.Forms;
using SQL;

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
            string sql = "SELECT studentNo, studentName, Birthday FROM tblStudents";
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
                sql = "SELECT studentNo, studentName, Birthday FROM tblStudents";
            }
            else
            {
                // 根据选择的专业筛选学生
                sql = string.Format("SELECT studentNo, studentName, Birthday FROM tblStudents WHERE major = '{0}'", selectedMajor);
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
    }
}
