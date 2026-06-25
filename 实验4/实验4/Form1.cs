using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace 实验4
{
    public partial class Form1 : Form
    {
        private LiveStreamManager _manager = new LiveStreamManager(3);

        private CancellationTokenSource _c1, _c2, _c3;
        private WebView2 _w1, _w2, _w3;
        private bool _ready = false;

        // CSS: 只保留 video 播放区域，隐藏其他所有元素
        private static readonly string HIDE_CSS = @"
(function() {
    function hide() {
        try {
            var style = document.createElement('style');
            style.id = 'clean-style';
            style.textContent = '*{margin:0!important;padding:0!important}body{background:#000!important;overflow:hidden!important}video{position:fixed!important;top:0!important;left:0!important;width:100vw!important;height:100vh!important;object-fit:contain!important;z-index:9999!important}';
            var old = document.getElementById('clean-style');
            if (old) old.remove();
            document.head.appendChild(style);
        } catch(e) {}
    }
    hide();
    setInterval(hide, 1500);
})();
";

        private Dictionary<string, string> _urls = new Dictionary<string, string>
        {
            { "CCTV-1 综合",  "https://tv.cctv.com/live/cctv1/" },
            { "CCTV-5 体育",  "https://tv.cctv.com/live/cctv5/" },
            { "CCTV-13 新闻", "https://tv.cctv.com/live/cctv13/" },
            { "CCTV-3 综艺",  "https://tv.cctv.com/live/cctv3/" },
            { "CCTV-6 电影",  "https://tv.cctv.com/live/cctv6/" },
            { "CCTV-9 纪录",  "https://tv.cctv.com/live/cctv9/" },
        };

        public Form1()
        {
            InitializeComponent();

            _w1 = new WebView2 { Dock = DockStyle.Fill, DefaultBackgroundColor = Color.Black };
            _w2 = new WebView2 { Dock = DockStyle.Fill, DefaultBackgroundColor = Color.Black };
            _w3 = new WebView2 { Dock = DockStyle.Fill, DefaultBackgroundColor = Color.Black };
            pnlRoom1.Controls.Add(_w1);
            pnlRoom2.Controls.Add(_w2);
            pnlRoom3.Controls.Add(_w3);


            this.Shown += Form1_Shown;
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Text = "直播并发控制系统 — 初始化中...";

                // 并行初始化三个 WebView2
                await Task.WhenAll(
                    _w1.EnsureCoreWebView2Async(null),
                    _w2.EnsureCoreWebView2Async(null),
                    _w3.EnsureCoreWebView2Async(null));

                // 设置属性 + 注册导航完成回调
                foreach (var wv in new[] { _w1, _w2, _w3 })
                {
                    wv.CoreWebView2.Settings.IsScriptEnabled = true;
                    wv.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;
                    wv.CoreWebView2.Settings.AreDevToolsEnabled = false;
                    wv.CoreWebView2.Settings.IsStatusBarEnabled = false;
                }

                // ⚠️ CoreWebView2.NavigationCompleted 的 sender 是 CoreWebView2，不是 WebView2
                // 用闭包捕获 WebView2 引用
                _w1.CoreWebView2.NavigationCompleted += (_, __) => InjectCSS(_w1);
                _w2.CoreWebView2.NavigationCompleted += (_, __) => InjectCSS(_w2);
                _w3.CoreWebView2.NavigationCompleted += (_, __) => InjectCSS(_w3);

                // 频道切换事件：切换频道时自动重启直播流
                cmbRoom1.SelectedIndexChanged += (_, __) => SwitchChannel(1);
                cmbRoom2.SelectedIndexChanged += (_, __) => SwitchChannel(2);
                cmbRoom3.SelectedIndexChanged += (_, __) => SwitchChannel(3);

                _ready = true;
                this.Text = "直播并发控制系统 — 自动开播...";

                Start(1, "CCTV-1 综合");
                Start(2, "CCTV-5 体育");
                Start(3, "CCTV-13 新闻");
            }
            catch (Exception ex)
            {
                _ready = false;
                this.Text = "初始化失败";
                MessageBox.Show("初始化失败: " + ex.Message);
            }
        }

        private async void InjectCSS(WebView2 wb)
        {
            if (wb?.CoreWebView2 == null) return;
            try { await wb.CoreWebView2.ExecuteScriptAsync(HIDE_CSS); }
            catch { }
        }

        private async void Start(int id, string ch)
        {
            if (!_ready) return;
            if (!_manager.TryStart(id)) return;
            if (!_urls.TryGetValue(ch, out string url)) { _manager.Stop(id); return; }

            UpdateLabel();
            var ct = new CancellationTokenSource();
            SetC(id, ct);
            var wb = Wb(id);

            try
            {
                wb.ZoomFactor = 0.22;
                wb.CoreWebView2.Navigate(url);
                await Task.Delay(-1, ct.Token); // 无限等待，直到 Cancel
            }
            catch (OperationCanceledException) { }
            catch { }
            finally
            {
                try { wb.CoreWebView2.Navigate("about:blank"); } catch { }
                _manager.Stop(id);
                UpdateLabel();
                ct.Dispose();
            }
        }

        private void Stop(int id)
        {
            var c = GetC(id);
            if (c != null) c.Cancel();
        }

        // ── 按钮事件（按钮已隐藏，保留以备后用）──
        private void btnStartAll_Click(object sender, EventArgs e) { Start(1, G(1)); Start(2, G(2)); Start(3, G(3)); }
        private void btnStopAll_Click(object sender, EventArgs e) { Stop(1); Stop(2); Stop(3); }
        private void btnRoom1Ctrl_Click(object sender, EventArgs e) { if (_manager.IsRunning(1)) Stop(1); else Start(1, G(1)); }
        private void btnRoom2Ctrl_Click(object sender, EventArgs e) { if (_manager.IsRunning(2)) Stop(2); else Start(2, G(2)); }
        private void btnRoom3Ctrl_Click(object sender, EventArgs e) { if (_manager.IsRunning(3)) Stop(3); else Start(3, G(3)); }

        private string G(int id)
        {
            var cmb = id == 1 ? cmbRoom1 : id == 2 ? cmbRoom2 : cmbRoom3;
            return cmb.SelectedItem?.ToString() ?? "CCTV-1 综合";
        }

        /// <summary>
        /// 切换频道：如果当前正在直播该流，自动停止并重启到新频道
        /// </summary>
        private async void SwitchChannel(int id)
        {
            if (!_ready) return;
            if (_manager.IsRunning(id))
            {
                Stop(id);
                // 等待旧流清理完成，避免 TryStart 竞态
                await Task.Delay(150);
                Start(id, G(id));
            }
        }

        private WebView2 Wb(int id) { return id == 1 ? _w1 : id == 2 ? _w2 : _w3; }
        private CancellationTokenSource GetC(int id) { return id == 1 ? _c1 : id == 2 ? _c2 : _c3; }
        private void SetC(int id, CancellationTokenSource c) { if (id == 1) _c1 = c; else if (id == 2) _c2 = c; else _c3 = c; }

        private void UpdateLabel()
        {
            // 窗口标题栏
            this.Text = string.Format("直播并发控制系统 | 活跃: {0} / 槽位: {1}/{2}",
                _manager.ActiveCount, _manager.AvailableSlots, _manager.MaxConcurrency);

            // 顶部状态栏
            string state = _manager.ActiveCount > 0 ? "运行中" : "空闲";
            lblConcurrency.Text = string.Format("并发状态：{0} | 活跃直播：{1} | 信号量：{2}/{3}",
                state, _manager.ActiveCount, _manager.AvailableSlots, _manager.MaxConcurrency);

            // 各直播间状态标签 + 按钮文字
            UpdateRoomUI(1, lblRoom1Status, btnRoom1Ctrl);
            UpdateRoomUI(2, lblRoom2Status, btnRoom2Ctrl);
            UpdateRoomUI(3, lblRoom3Status, btnRoom3Ctrl);
        }

        private void UpdateRoomUI(int id, Label lbl, Button btn)
        {
            bool running = _manager.IsRunning(id);
            lbl.Text = running ? "● 直播中" : "● 待机中";
            lbl.ForeColor = running ? System.Drawing.Color.Green : System.Drawing.Color.Gray;
            btn.Text = running ? "停播" : "开播";
            btn.BackColor = running ? System.Drawing.Color.Crimson : System.Drawing.Color.DarkGreen;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Stop(1); Stop(2); Stop(3);
            _manager.Dispose();
            base.OnFormClosing(e);
        }
    }
}
