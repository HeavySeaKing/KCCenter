using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace MessageForm
{

    public partial class frm_message : Form
    {
        Rectangle area = new Rectangle();
        bool isStop = false; // 初始化提示框窗体为随时可上升

        int step =12; // 设置窗体每次上升与下降时的高度
        int StayTime = 10; //设置窗体停靠的时间(单位为秒)

        DateTime beginTime;
        bool isClose = false; // 设置窗体是否关闭的状态标志位

        public frm_message()
        {
            InitializeComponent();

            // 初始化提示框窗体的状态及位置
            area = Screen.GetWorkingArea(this);  // 获取显示器的工作区域，即显示器的桌面区域，不包括任务栏及停靠窗口和停靠工具栏
            this.Left = area.Width - this.Width;
            this.Top = area.Height;
            this.FormBorderStyle = FormBorderStyle.None; //设置窗体边框为无
            this.ControlBox = false; // 设置标题栏不显示控件框
            this.ShowInTaskbar = false; // 设置窗体不显示到任务栏中

            setMessage("Author:mocklystone describ:右下角提示框实现", false);

        }

        // 设置记录消息
        public void setMessage(string message, bool isLink)
        {
            //this.labTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //this.linkMessage.Text = message;
            this.linkMessage.Enabled =isLink;
        }
   
        // 监控消息状态
        private void timer1_Tick(object sender, EventArgs e)
        {
            // 如果状态为显示提示框（即isStop为false），则使其距离显示器的上部越来越小（即减小Top的值），窗体的位置及慢慢上升
            // 如果上升到一定的高度则停止，若停止时间超过我们设置的StayTime或是用户点击关闭图标则让窗体位置下降到用户看不到的地方
            if (!isStop)
            {
                this.Top -= step;
                if (this.Bottom <= area.Height)
                {
                    isStop = true;
                    beginTime = DateTime.Now;
                }
            }
            else if (Differ(DateTime.Now, beginTime) >= StayTime || isClose)
            {
                this.Top += step;
                if (this.Bottom >= area.Height + this.Height)
                {
                    this.Close();
                    
                }
            }
        }

        // 计算时间差返回秒数
        private double Differ(DateTime t1, DateTime t2)
        {
            TimeSpan ts1 = new TimeSpan(t1.Ticks);
            TimeSpan ts2 = new TimeSpan(t2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.TotalSeconds;
        }


        // 关闭窗体
        private void panClose_Click(object sender, EventArgs e)
        {
            isClose = true;
        }

        // 鼠标经过
        private void panClose_MouseEnter(object sender, EventArgs e)
        {
            ((Panel)sender).BorderStyle = BorderStyle.Fixed3D;
            this.Cursor = Cursors.Hand;
        }

        //  鼠标离开
        private void panClose_MouseLeave(object sender, EventArgs e)
        {
            ((Panel)sender).BorderStyle = BorderStyle.None;
            this.Cursor = Cursors.Default;
        }

        private void frm_message_Load(object sender, EventArgs e)
        {

        }

        private void linkMessage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void labTime_Click(object sender, EventArgs e)
        {

        }

    }
}