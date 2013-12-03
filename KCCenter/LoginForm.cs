using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsApplication2;

namespace KCCenter
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            var userName = txtUserName.Text;
            var password = txtUserPwd.Text;
            var dt = MysqlHelper.ExecuteDataTable("select * from KC_UserInfo where userName = '" + userName + "' and userpwd='" + password + "'", null);
            if (dt.Rows.Count > 0)
            {
                GlobalProperty gp = new GlobalProperty();
                gp.role = dt.Rows[0]["UserRole"].ToString();
                //ToastNotification.Show(this, "登录成功！",null,2000,eToastGlowColor.Green,eToastPosition.BottomCenter);
                //Thread.Sleep(2000);
                this.Hide();
                if (gp.role == "user")
                {
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
                    f2.Activate();
                }
                else
                {
                    MainForm mf = new MainForm();
                    mf.ShowDialog();
                    mf.Activate();
                }
            }
            else
            {
                ToastNotification.Show(this, "用户名或密码错误！", null, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter);
            }
        }
    }
}
