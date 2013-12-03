namespace MessageForm
{
    partial class frm_message
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.linkMessage = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labTime = new System.Windows.Forms.Label();
            this.panClose = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // linkMessage
            // 
            this.linkMessage.BackColor = System.Drawing.Color.Transparent;
            this.linkMessage.LinkArea = new System.Windows.Forms.LinkArea(0, 100);
            this.linkMessage.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkMessage.Location = new System.Drawing.Point(14, 73);
            this.linkMessage.MaximumSize = new System.Drawing.Size(300, 100);
            this.linkMessage.Name = "linkMessage";
            this.linkMessage.Size = new System.Drawing.Size(238, 79);
            this.linkMessage.TabIndex = 0;
            this.linkMessage.TabStop = true;
            this.linkMessage.Text = "右下角提示框";
            this.linkMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkMessage.UseCompatibleTextRendering = true;
            this.linkMessage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMessage_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labTime
            // 
            this.labTime.BackColor = System.Drawing.Color.Transparent;
            this.labTime.Location = new System.Drawing.Point(12, 34);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(171, 21);
            this.labTime.TabIndex = 2;
            this.labTime.Text = "Author:mocklystone";
            this.labTime.Click += new System.EventHandler(this.labTime_Click);
            // 
            // panClose
            // 
            this.panClose.BackColor = System.Drawing.Color.Transparent;
            this.panClose.Location = new System.Drawing.Point(239, 1);
            this.panClose.Name = "panClose";
            this.panClose.Size = new System.Drawing.Size(23, 23);
            this.panClose.TabIndex = 3;
            this.panClose.MouseLeave += new System.EventHandler(this.panClose_MouseLeave);
            this.panClose.Click += new System.EventHandler(this.panClose_Click);
            this.panClose.MouseEnter += new System.EventHandler(this.panClose_MouseEnter);
            // 
            // frm_message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            //this.BackgroundImage = global::MessageForm.Properties.Resources.MSG;
            this.ClientSize = new System.Drawing.Size(262, 161);
            this.Controls.Add(this.labTime);
            this.Controls.Add(this.panClose);
            this.Controls.Add(this.linkMessage);
            this.Name = "frm_message";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frm_message_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkMessage;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panClose;
        public System.Windows.Forms.Label labTime;
    }
}