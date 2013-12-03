using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KCCenter
{
    public partial class MainForm : Office2007Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX1.Text.Length >= 2)
            {
                var dtSource =
                    MysqlHelper.ExecuteDataTable(
                        "select * from kc_info where date = '" + PublicInfo.Choose.ChooseDate.ToShortDateString() +
                        "' and MDName like '" + textBoxX1.Text + "%'", null);
                dataGridViewX1.DataSource = dtSource;
            }
            else
            {
                BindGrid(PublicInfo.Choose.ChooseDate);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            buttonX1.Text = DateTime.Now.AddDays(-2).ToString("MM月dd日(" + Convert.ToInt32(DateTime.Now.AddDays(-2).DayOfWeek) + ")");
            buttonX2.Text = DateTime.Now.AddDays(-1).ToString("MM月dd日(" + Convert.ToInt32(DateTime.Now.AddDays(-1).DayOfWeek) + ")");
            buttonX3.Text = DateTime.Now.ToString("今天(" + Convert.ToInt32(DateTime.Now.DayOfWeek) + ")");
            buttonX4.Text = DateTime.Now.AddDays(1).ToString("MM月dd日(" + Convert.ToInt32(DateTime.Now.AddDays(1).DayOfWeek) + ")");
            buttonX5.Text = DateTime.Now.AddDays(2).ToString("MM月dd日(" + Convert.ToInt32(DateTime.Now.AddDays(2).DayOfWeek) + ")");
            buttonX6.Text = DateTime.Now.AddDays(3).ToString("MM月dd日(" + Convert.ToInt32(DateTime.Now.AddDays(3).DayOfWeek) + ")");
            buttonX7.Text = DateTime.Now.AddDays(4).ToString("MM月dd日(" + Convert.ToInt32(DateTime.Now.AddDays(4).DayOfWeek) + ")");
            PublicInfo.Choose.ChooseDate = DateTime.Now;
            BindGrid(DateTime.Now);
            var dt = MysqlHelper.ExecuteScalar("select * from kc_PublicNotify where id=1", null).ToString();
            richTextBoxEx1.Text = dt;
        }


        public void BindGrid(DateTime dateTime)
        {
            var dt = MysqlHelper.ExecuteScalar("select count(*) from kc_info where date = '" + dateTime.ToShortDateString() + "'", null);
            if (Convert.ToInt32(dt) > 0)
            {

            }
            else
            {
                GenerateInfo(dateTime);

            }
            var dtSource = MysqlHelper.ExecuteDataTable("select * from kc_info where date = '" + dateTime.ToShortDateString() + "'", null);
            dataGridViewX1.DataSource = dtSource;
            dataGridViewX1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            dataGridViewX1.Columns[0].HeaderText = "店名";
            dataGridViewX1.Columns[0].Width = 75;
            dataGridViewX1.Columns[1].HeaderText = "外送";
            dataGridViewX1.Columns[1].Width = 75;
            dataGridViewX1.Columns[2].HeaderText = "自取";
            dataGridViewX1.Columns[2].Width = 75;
            dataGridViewX1.Columns[3].HeaderText = "电磁炉";
            dataGridViewX1.Columns[3].Width = 75;
            dataGridViewX1.Columns[4].Visible = false;
            dataGridViewX1.Columns[5].HeaderText = "锅";
            dataGridViewX1.Columns[5].Width = 75;
            dataGridViewX1.Columns[6].HeaderText = "电话通知";
            dataGridViewX1.Columns[6].Width = 75;
            dataGridViewX1.Columns[7].HeaderText = "其他";
            dataGridViewX1.Columns[7].Width = 250;

        }
        public void GenerateInfo(DateTime dateTime)
        {
            var dt = MysqlHelper.ExecuteDataTable("select * from kc_mdlist", null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MysqlHelper.ExecuteNonQuery("insert into kc_info(mdname,date) values('" + dt.Rows[i]["mdname"].ToString() +
                                            "','" +
                                            dateTime.ToShortDateString() + "')");
            }
        }

        public class dpModel
        {
            public string mdName { set; get; }
            public DateTime date { set; get; }
            public bool waisong { set; get; }
            public bool ziqu { set; get; }
            public bool diancilu { set; get; }
            public bool guodi { set; get; }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            BindGrid(DateTime.Now.AddDays(-2));
            PublicInfo.Choose.ChooseDate = DateTime.Now.AddDays(-2);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            BindGrid(DateTime.Now.AddDays(-1));
            PublicInfo.Choose.ChooseDate = DateTime.Now.AddDays(-1);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            BindGrid(DateTime.Now);
            PublicInfo.Choose.ChooseDate = DateTime.Now;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            BindGrid(DateTime.Now.AddDays(1));
            PublicInfo.Choose.ChooseDate = DateTime.Now.AddDays(1);
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            BindGrid(DateTime.Now.AddDays(2));
            PublicInfo.Choose.ChooseDate = DateTime.Now.AddDays(2);
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            BindGrid(DateTime.Now.AddDays(3));
            PublicInfo.Choose.ChooseDate = DateTime.Now.AddDays(3);
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            BindGrid(DateTime.Now.AddDays(4));
            PublicInfo.Choose.ChooseDate = DateTime.Now.AddDays(4);
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
                {
                    string mdName = dataGridViewX1.Rows[i].Cells[0].Value.ToString();
                    string waiSong = dataGridViewX1.Rows[i].Cells[1].Value.ToString();
                    string ziqu = dataGridViewX1.Rows[i].Cells[2].Value.ToString();
                    string diancilu = dataGridViewX1.Rows[i].Cells[3].Value.ToString();
                    string guo = dataGridViewX1.Rows[i].Cells[5].Value.ToString();
                    string telNotice = dataGridViewX1.Rows[i].Cells[6].Value.ToString();
                    string remark = dataGridViewX1.Rows[i].Cells[7].Value.ToString();
                    MysqlHelper.ExecuteNonQuery("update kc_info set waisong='" + waiSong + "' , ziqu='" + ziqu +
                                                "',diancilu=" + Convert.ToBoolean(diancilu) + ",guo=" +
                                                Convert.ToBoolean(guo) + ",telNotice=" + Convert.ToBoolean(telNotice) + ",remark='" +
                                                remark + "' where mdName='" + mdName + "' and date='" +
                                                PublicInfo.Choose.ChooseDate.ToShortDateString() + "'");
                }
                MessageBox.Show("修改成功！");
            }
            else
            {
                MessageBox.Show("没有要修改的数据！");
            }
        }

        private void dateTimeInput1_TextChanged(object sender, EventArgs e)
        {
            BindGrid(Convert.ToDateTime(dateTimeInput1.Text));
            PublicInfo.Choose.ChooseDate = Convert.ToDateTime(dateTimeInput1.Text);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确实要退出系统吗?", "Hi捞送控餐辅助系统", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            var dt = MysqlHelper.ExecuteNonQuery("update kc_PublicNotify set remark='" + richTextBoxEx1.Text + "' where id=1", null);
            ToastNotification.Show(this, "发布成功！", null, 2000, eToastGlowColor.Green, eToastPosition.BottomCenter);
        }
    }
}
