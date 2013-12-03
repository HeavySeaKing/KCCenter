using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//下面的命名空间一定要加上
using System.Runtime.InteropServices;
using KCCenter;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

//大家要是觉得windows的边框不好看的话，可以去掉边框
//然后设置FormBorderStyle为none就没有了边框
//然后添加一个背景图片，再添加一个X型的关闭按钮
//这样就和我们一般所见的弹出窗口是一样的
namespace WindowsApplication2
{
    public partial class Form2 : Form
    {
       
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        //下面是可用的常量，根据不同的动画效果声明自己需要的
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        private const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        private const int AW_HIDE = 0x10000;//隐藏窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        private const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AW_BLEND = 0x80000;//使用淡入淡出效果

        public Form2()
        {

            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            notifyIcon1.Icon = new Icon("tips.ico"); //指定一个图标      
            notifyIcon1.Visible = true;
            notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
         
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_BLEND);
            //buttonX1.Text = DateTime.Now.AddDays(-2).ToString("MM月dd日(" + Convert.ToInt32(DateTime.Now.AddDays(-2).DayOfWeek) + ")");
            buttonX2.Text = DateTime.Now.AddDays(-1).ToString("MM月dd日(" + day(Convert.ToInt32(DateTime.Now.AddDays(-1).DayOfWeek)) + ")");
            buttonX3.Text = DateTime.Now.ToString("今天(" + day(Convert.ToInt32(DateTime.Now.DayOfWeek)) + ")");
            buttonX4.Text = DateTime.Now.AddDays(1).ToString("MM月dd日(" + day(Convert.ToInt32(DateTime.Now.AddDays(1).DayOfWeek)) + ")");
            buttonX5.Text = DateTime.Now.AddDays(2).ToString("MM月dd日(" + day(Convert.ToInt32(DateTime.Now.AddDays(2).DayOfWeek)) + ")");
            //buttonX6.Text = DateTime.Now.AddDays(3).ToString("MM月dd日(" + Convert.ToInt32(DateTime.Now.AddDays(3).DayOfWeek) + ")");
            //buttonX7.Text = DateTime.Now.AddDays(4).ToString("MM月dd日(" + Convert.ToInt32(DateTime.Now.AddDays(4).DayOfWeek) + ")");
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
            var dtSource = MysqlHelper.ExecuteDataTable("select * from kc_info where date = '" + dateTime.ToShortDateString()+ "' and MDName like '" + textBoxX1.Text + "%'", null);
          
            var dtNew = new DataTable();
            dtNew.Columns.Add("mdname",typeof(string));
            dtNew.Columns.Add("waisong", typeof(string));
            dtNew.Columns.Add("ziqu", typeof(string));
            dtNew.Columns.Add("diancilu", typeof(string));
            dtNew.Columns.Add("date", typeof(string));
            dtNew.Columns.Add("guo", typeof(string));
            dtNew.Columns.Add("telnotice", typeof(string));
            dtNew.Columns.Add("remark", typeof(string));

            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                DataRow dr = dtNew.NewRow();
                dr[0] = dtSource.Rows[i][0].ToString();
                dr[1] = dtSource.Rows[i][1].ToString();
                dr[2] = dtSource.Rows[i][2].ToString();
                dr[3] = ConvertBool(Convert.ToBoolean(dtSource.Rows[i][3].ToString()));
                dr[4] = dtSource.Rows[i][4].ToString();
                dr[5] = ConvertBool(Convert.ToBoolean(dtSource.Rows[i][5].ToString()));
                dr[6] = ConvertBool(Convert.ToBoolean(dtSource.Rows[i][6].ToString()));
                dr[7] = dtSource.Rows[i][7].ToString();
                dtNew.Rows.Add(dr);
            }
            dataGridViewX1.DataSource = dtNew;
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

        private void notifyIcon1_Click(object sender, EventArgs e)
        {

            Rectangle area = new Rectangle();
            this.Height = this.Height == 40 ? 500 : 40;
            if (this.Height == 40)
            {
                button2.Text = "显示";
                button2.BackColor = Color.LightSeaGreen;
            }
            else
            {
                button2.Text = "隐藏";
                button2.BackColor = Color.Khaki;
            }
            area = Screen.GetWorkingArea(this);  // 获取显示器的工作区域，即显示器的桌面区域，不包括任务栏及停靠窗口和停靠工具栏
            this.Left = area.Width - this.Width;
            this.Top = area.Height - this.Height;
            this.Activate();
            this.notifyIcon1.Visible = true;
            this.ShowInTaskbar = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SelectBindGrid();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Rectangle area = new Rectangle();

            if (this.Height == 40)
            {
                this.Height = 500;
             
                button2.Text = "隐藏";
                button2.BackColor = Color.Khaki;
            }
            else
            {
                this.Height = 40;
                button2.Text = "显示";
                button2.BackColor = Color.LightSeaGreen;
            }
            area = Screen.GetWorkingArea(this);  // 获取显示器的工作区域，即显示器的桌面区域，不包括任务栏及停靠窗口和停靠工具栏
            this.Left = area.Width - this.Width;
            this.Top = area.Height - this.Height;
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

        private void dateTimeInput1_TextChanged(object sender, EventArgs e)
        {
            BindGrid(Convert.ToDateTime(dateTimeInput1.Text));
            PublicInfo.Choose.ChooseDate = Convert.ToDateTime(dateTimeInput1.Text);
        }


        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            SelectBindGrid();
        }

        public string ConvertBool(bool bo)
        {
            return bo ? "√" : "●";
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            SelectBindGrid();
        }


        public void SelectBindGrid()
        {
            if (textBoxX1.Text.Length >= 2)
            {
                var dtSource =
                    MysqlHelper.ExecuteDataTable(
                        "select * from kc_info where date = '" + PublicInfo.Choose.ChooseDate.ToShortDateString() +
                        "' and MDName like '" + textBoxX1.Text + "%'", null);
                var dtNew = new DataTable();
                dtNew.Columns.Add("mdname", typeof(string));
                dtNew.Columns.Add("waisong", typeof(string));
                dtNew.Columns.Add("ziqu", typeof(string));
                dtNew.Columns.Add("diancilu", typeof(string));
                dtNew.Columns.Add("date", typeof(string));
                dtNew.Columns.Add("guo", typeof(string));
                dtNew.Columns.Add("telnotice", typeof(string));
                dtNew.Columns.Add("remark", typeof(string));

                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    DataRow dr = dtNew.NewRow();
                    dr[0] = dtSource.Rows[i][0].ToString();
                    dr[1] = dtSource.Rows[i][1].ToString();
                    dr[2] = dtSource.Rows[i][2].ToString();
                    dr[3] = ConvertBool(Convert.ToBoolean(dtSource.Rows[i][3].ToString()));
                    dr[4] = dtSource.Rows[i][4].ToString();
                    dr[5] = ConvertBool(Convert.ToBoolean(dtSource.Rows[i][5].ToString()));
                    dr[6] = ConvertBool(Convert.ToBoolean(dtSource.Rows[i][6].ToString()));
                    dr[7] = dtSource.Rows[i][7].ToString();
                    dtNew.Rows.Add(dr);
                }
                dataGridViewX1.DataSource = dtNew;
            }
            else
            {
                BindGrid(PublicInfo.Choose.ChooseDate);
            }
        }

        public string day(int dayOfWeek)
        {
            string WEEK = "";
            switch (dayOfWeek)
            {
                case 0:
                    WEEK = "周日";
                    break;
                case 1:
                    WEEK = "周一";
                    break;
                case 2:
                    WEEK = "周二";
                    break;
                case 3:
                    WEEK = "周三";
                    break;
                case 4:
                    WEEK = "周四";
                    break;
                case 5:
                    WEEK = "周五";
                    break;
                case 6:
                    WEEK = "周六";
                    break;
            }
            return WEEK;
        }
    }
}