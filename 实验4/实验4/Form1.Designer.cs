namespace 实验4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.btnStartAll = new System.Windows.Forms.Button();
            this.btnStopAll = new System.Windows.Forms.Button();
            this.lblConcurrency = new System.Windows.Forms.Label();
            this.grpRoom1 = new System.Windows.Forms.GroupBox();
            this.cmbRoom1 = new System.Windows.Forms.ComboBox();
            this.lblRoom1FPS = new System.Windows.Forms.Label();
            this.lblRoom1Status = new System.Windows.Forms.Label();
            this.btnRoom1Ctrl = new System.Windows.Forms.Button();
            this.grpRoom2 = new System.Windows.Forms.GroupBox();
            this.cmbRoom2 = new System.Windows.Forms.ComboBox();
            this.lblRoom2FPS = new System.Windows.Forms.Label();
            this.lblRoom2Status = new System.Windows.Forms.Label();
            this.btnRoom2Ctrl = new System.Windows.Forms.Button();
            this.grpRoom3 = new System.Windows.Forms.GroupBox();
            this.cmbRoom3 = new System.Windows.Forms.ComboBox();
            this.lblRoom3FPS = new System.Windows.Forms.Label();
            this.lblRoom3Status = new System.Windows.Forms.Label();
            this.btnRoom3Ctrl = new System.Windows.Forms.Button();
            this.pnlRoom1 = new System.Windows.Forms.Panel();
            this.pnlRoom2 = new System.Windows.Forms.Panel();
            this.pnlRoom3 = new System.Windows.Forms.Panel();
            this.grpRoom1.SuspendLayout();
            this.grpRoom2.SuspendLayout();
            this.grpRoom3.SuspendLayout();
            this.SuspendLayout();
            //
            // lblConcurrency
            //
            this.lblConcurrency.AutoSize = true;
            this.lblConcurrency.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblConcurrency.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblConcurrency.Location = new System.Drawing.Point(12, 12);
            this.lblConcurrency.Text = "并发状态：空闲 | 活跃直播：0 | 信号量：3/3";
            //
            // btnStartAll
            //
            this.btnStartAll.BackColor = System.Drawing.Color.SteelBlue;
            this.btnStartAll.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btnStartAll.ForeColor = System.Drawing.Color.White;
            this.btnStartAll.Location = new System.Drawing.Point(12, 40);
            this.btnStartAll.Size = new System.Drawing.Size(130, 42);
            this.btnStartAll.TabIndex = 0;
            this.btnStartAll.Text = "▶ 全部开播";
            this.btnStartAll.UseVisualStyleBackColor = false;
            this.btnStartAll.Click += new System.EventHandler(this.btnStartAll_Click);
            //
            // btnStopAll
            //
            this.btnStopAll.BackColor = System.Drawing.Color.DarkRed;
            this.btnStopAll.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btnStopAll.ForeColor = System.Drawing.Color.White;
            this.btnStopAll.Location = new System.Drawing.Point(155, 40);
            this.btnStopAll.Size = new System.Drawing.Size(130, 42);
            this.btnStopAll.TabIndex = 1;
            this.btnStopAll.Text = "■ 全部停播";
            this.btnStopAll.UseVisualStyleBackColor = false;
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            //
            // grpRoom1
            //
            this.grpRoom1.Controls.Add(this.cmbRoom1);
            this.grpRoom1.Controls.Add(this.lblRoom1FPS);
            this.grpRoom1.Controls.Add(this.lblRoom1Status);
            this.grpRoom1.Controls.Add(this.btnRoom1Ctrl);
            this.grpRoom1.Controls.Add(this.pnlRoom1);
            this.grpRoom1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.grpRoom1.ForeColor = System.Drawing.Color.DarkGreen;
            this.grpRoom1.Location = new System.Drawing.Point(12, 92);
            this.grpRoom1.Size = new System.Drawing.Size(253, 325);
            this.grpRoom1.Text = "直播间 A";
            //
            // lblRoom1Status
            //
            this.lblRoom1Status.AutoSize = true;
            this.lblRoom1Status.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblRoom1Status.ForeColor = System.Drawing.Color.Gray;
            this.lblRoom1Status.Location = new System.Drawing.Point(6, 22);
            this.lblRoom1Status.Text = "● 待机中";
            //
            // lblRoom1FPS
            //
            this.lblRoom1FPS.AutoSize = true;
            this.lblRoom1FPS.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblRoom1FPS.Location = new System.Drawing.Point(80, 22);
            this.lblRoom1FPS.Text = "";
            //
            // pnlRoom1
            //
            this.pnlRoom1.BackColor = System.Drawing.Color.Black;
            this.pnlRoom1.Location = new System.Drawing.Point(6, 48);
            this.pnlRoom1.Size = new System.Drawing.Size(241, 215);
            //
            // cmbRoom1
            //
            this.cmbRoom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoom1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cmbRoom1.Items.AddRange(new object[] {
                "CCTV-1 综合",
                "CCTV-5 体育",
                "CCTV-13 新闻",
                "CCTV-3 综艺",
                "CCTV-6 电影",
                "CCTV-9 纪录"});
            this.cmbRoom1.Location = new System.Drawing.Point(6, 267);
            this.cmbRoom1.Size = new System.Drawing.Size(140, 24);
            this.cmbRoom1.SelectedIndex = 0;
            //
            // btnRoom1Ctrl
            //
            this.btnRoom1Ctrl.BackColor = System.Drawing.Color.DarkGreen;
            this.btnRoom1Ctrl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.btnRoom1Ctrl.ForeColor = System.Drawing.Color.White;
            this.btnRoom1Ctrl.Location = new System.Drawing.Point(150, 265);
            this.btnRoom1Ctrl.Size = new System.Drawing.Size(97, 27);
            this.btnRoom1Ctrl.Text = "开播";
            this.btnRoom1Ctrl.UseVisualStyleBackColor = false;
            this.btnRoom1Ctrl.Click += new System.EventHandler(this.btnRoom1Ctrl_Click);
            //
            // grpRoom2
            //
            this.grpRoom2.Controls.Add(this.cmbRoom2);
            this.grpRoom2.Controls.Add(this.lblRoom2FPS);
            this.grpRoom2.Controls.Add(this.lblRoom2Status);
            this.grpRoom2.Controls.Add(this.btnRoom2Ctrl);
            this.grpRoom2.Controls.Add(this.pnlRoom2);
            this.grpRoom2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.grpRoom2.ForeColor = System.Drawing.Color.DarkBlue;
            this.grpRoom2.Location = new System.Drawing.Point(271, 92);
            this.grpRoom2.Size = new System.Drawing.Size(253, 325);
            this.grpRoom2.Text = "直播间 B";
            //
            // lblRoom2Status
            //
            this.lblRoom2Status.AutoSize = true;
            this.lblRoom2Status.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblRoom2Status.ForeColor = System.Drawing.Color.Gray;
            this.lblRoom2Status.Location = new System.Drawing.Point(6, 22);
            this.lblRoom2Status.Text = "● 待机中";
            //
            // lblRoom2FPS
            //
            this.lblRoom2FPS.AutoSize = true;
            this.lblRoom2FPS.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblRoom2FPS.Location = new System.Drawing.Point(80, 22);
            this.lblRoom2FPS.Text = "";
            //
            // pnlRoom2
            //
            this.pnlRoom2.BackColor = System.Drawing.Color.Black;
            this.pnlRoom2.Location = new System.Drawing.Point(6, 48);
            this.pnlRoom2.Size = new System.Drawing.Size(241, 215);
            //
            // cmbRoom2
            //
            this.cmbRoom2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoom2.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cmbRoom2.Items.AddRange(new object[] {
                "CCTV-1 综合",
                "CCTV-5 体育",
                "CCTV-13 新闻",
                "CCTV-3 综艺",
                "CCTV-6 电影",
                "CCTV-9 纪录"});
            this.cmbRoom2.Location = new System.Drawing.Point(6, 267);
            this.cmbRoom2.Size = new System.Drawing.Size(140, 24);
            this.cmbRoom2.SelectedIndex = 1;
            //
            // btnRoom2Ctrl
            //
            this.btnRoom2Ctrl.BackColor = System.Drawing.Color.DarkGreen;
            this.btnRoom2Ctrl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.btnRoom2Ctrl.ForeColor = System.Drawing.Color.White;
            this.btnRoom2Ctrl.Location = new System.Drawing.Point(150, 265);
            this.btnRoom2Ctrl.Size = new System.Drawing.Size(97, 27);
            this.btnRoom2Ctrl.Text = "开播";
            this.btnRoom2Ctrl.UseVisualStyleBackColor = false;
            this.btnRoom2Ctrl.Click += new System.EventHandler(this.btnRoom2Ctrl_Click);
            //
            // grpRoom3
            //
            this.grpRoom3.Controls.Add(this.cmbRoom3);
            this.grpRoom3.Controls.Add(this.lblRoom3FPS);
            this.grpRoom3.Controls.Add(this.lblRoom3Status);
            this.grpRoom3.Controls.Add(this.btnRoom3Ctrl);
            this.grpRoom3.Controls.Add(this.pnlRoom3);
            this.grpRoom3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.grpRoom3.ForeColor = System.Drawing.Color.DarkRed;
            this.grpRoom3.Location = new System.Drawing.Point(530, 92);
            this.grpRoom3.Size = new System.Drawing.Size(253, 325);
            this.grpRoom3.Text = "直播间 C";
            //
            // lblRoom3Status
            //
            this.lblRoom3Status.AutoSize = true;
            this.lblRoom3Status.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblRoom3Status.ForeColor = System.Drawing.Color.Gray;
            this.lblRoom3Status.Location = new System.Drawing.Point(6, 22);
            this.lblRoom3Status.Text = "● 待机中";
            //
            // lblRoom3FPS
            //
            this.lblRoom3FPS.AutoSize = true;
            this.lblRoom3FPS.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblRoom3FPS.Location = new System.Drawing.Point(80, 22);
            this.lblRoom3FPS.Text = "";
            //
            // pnlRoom3
            //
            this.pnlRoom3.BackColor = System.Drawing.Color.Black;
            this.pnlRoom3.Location = new System.Drawing.Point(6, 48);
            this.pnlRoom3.Size = new System.Drawing.Size(241, 215);
            //
            // cmbRoom3
            //
            this.cmbRoom3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoom3.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cmbRoom3.Items.AddRange(new object[] {
                "CCTV-1 综合",
                "CCTV-5 体育",
                "CCTV-13 新闻",
                "CCTV-3 综艺",
                "CCTV-6 电影",
                "CCTV-9 纪录"});
            this.cmbRoom3.Location = new System.Drawing.Point(6, 267);
            this.cmbRoom3.Size = new System.Drawing.Size(140, 24);
            this.cmbRoom3.SelectedIndex = 2;
            //
            // btnRoom3Ctrl
            //
            this.btnRoom3Ctrl.BackColor = System.Drawing.Color.DarkGreen;
            this.btnRoom3Ctrl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.btnRoom3Ctrl.ForeColor = System.Drawing.Color.White;
            this.btnRoom3Ctrl.Location = new System.Drawing.Point(150, 265);
            this.btnRoom3Ctrl.Size = new System.Drawing.Size(97, 27);
            this.btnRoom3Ctrl.Text = "开播";
            this.btnRoom3Ctrl.UseVisualStyleBackColor = false;
            this.btnRoom3Ctrl.Click += new System.EventHandler(this.btnRoom3Ctrl_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 430);
            this.Controls.Add(this.grpRoom3);
            this.Controls.Add(this.grpRoom2);
            this.Controls.Add(this.grpRoom1);
            this.Controls.Add(this.btnStopAll);
            this.Controls.Add(this.btnStartAll);
            this.Controls.Add(this.lblConcurrency);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "直播并发控制系统 - 实验4";
            this.grpRoom1.ResumeLayout(false);
            this.grpRoom1.PerformLayout();
            this.grpRoom2.ResumeLayout(false);
            this.grpRoom2.PerformLayout();
            this.grpRoom3.ResumeLayout(false);
            this.grpRoom3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnStartAll;
        private System.Windows.Forms.Button btnStopAll;
        private System.Windows.Forms.Label lblConcurrency;
        private System.Windows.Forms.GroupBox grpRoom1;
        private System.Windows.Forms.Label lblRoom1FPS;
        private System.Windows.Forms.Label lblRoom1Status;
        private System.Windows.Forms.Button btnRoom1Ctrl;
        private System.Windows.Forms.ComboBox cmbRoom1;
        private System.Windows.Forms.Panel pnlRoom1;
        private System.Windows.Forms.GroupBox grpRoom2;
        private System.Windows.Forms.Label lblRoom2FPS;
        private System.Windows.Forms.Label lblRoom2Status;
        private System.Windows.Forms.Button btnRoom2Ctrl;
        private System.Windows.Forms.ComboBox cmbRoom2;
        private System.Windows.Forms.Panel pnlRoom2;
        private System.Windows.Forms.GroupBox grpRoom3;
        private System.Windows.Forms.Label lblRoom3FPS;
        private System.Windows.Forms.Label lblRoom3Status;
        private System.Windows.Forms.Button btnRoom3Ctrl;
        private System.Windows.Forms.ComboBox cmbRoom3;
        private System.Windows.Forms.Panel pnlRoom3;
    }
}
