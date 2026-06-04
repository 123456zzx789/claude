using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnOpenPage_Click(object sender, EventArgs e)
        {
            btnOpenPage.Enabled = false;
            btnOpenPage.Text = "加载中...";

            try
            {
                // 确保 WebView2 运行时初始化完成
                await webView.EnsureCoreWebView2Async(null);

                // 定位 HTML 文件
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "duanwu.html");
                if (!File.Exists(htmlPath))
                {
                    MessageBox.Show($"找不到文件: {htmlPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnOpenPage.Enabled = true;
                    btnOpenPage.Text = "打开端午页面";
                    return;
                }

                webView.CoreWebView2.Navigate(new Uri(htmlPath).AbsoluteUri);
                webView.Visible = true;
                btnOpenPage.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"页面加载失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOpenPage.Enabled = true;
                btnOpenPage.Text = "打开端午页面";
            }
        }
    }
}
