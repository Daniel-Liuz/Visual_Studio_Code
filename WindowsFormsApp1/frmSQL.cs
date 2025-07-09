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
            InitMajorComboBox();
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
        /// 查询班级人数（按钮点击）
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
        /// 查询所选专业（另一个按钮点击）
        /// </summary>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string selectedMajor = cboMajor.SelectedValue.ToString();

            if (selectedMajor == "全部显示")
            {
                MessageBox.Show("你选择的是：全部专业");
            }
            else
            {
                MessageBox.Show("你选择的专业是：" + selectedMajor);
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            string studentNumber = "10235501456"; // 你的学号
            string msg = "";

            try
            {
                string sql = string.Format("INSERT INTO tblStudentAbsent(studentNumber) VALUES('{0}')", studentNumber);
                int ret = sh.RunSQL(sql); // 执行插入操作

                if (ret > 0)
                    msg = "打卡成功！";
                else
                    msg = "打卡失败，数据库未插入任何行。";
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
    }
}
