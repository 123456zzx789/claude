namespace WindowsFormsApp1
{
    partial class Form1
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
            if (disposing && webView != null)
            {
                webView.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private System.Windows.Forms.Button btnOpenPage;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenPage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.SuspendLayout();
            //
            // btnOpenPage
            //
            this.btnOpenPage.Location = new System.Drawing.Point(12, 12);
            this.btnOpenPage.Name = "btnOpenPage";
            this.btnOpenPage.Size = new System.Drawing.Size(160, 40);
            this.btnOpenPage.TabIndex = 0;
            this.btnOpenPage.Text = "打开端午页面";
            this.btnOpenPage.UseVisualStyleBackColor = true;
            this.btnOpenPage.Click += new System.EventHandler(this.btnOpenPage_Click);
            //
            // webView
            //
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.webView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Location = new System.Drawing.Point(12, 60);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(960, 578);
            this.webView.TabIndex = 1;
            this.webView.Visible = false;
            this.webView.ZoomFactor = 1D;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 650);
            this.Controls.Add(this.webView);
            this.Controls.Add(this.btnOpenPage);
            this.Name = "Form1";
            this.Text = "端午节";
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
