# 端午节 WebView2 混合页面 — 实现计划

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 在 WindowsFormsApp1 中添加 WebView2 控件，点击按钮加载 Vue 3 端午节主题页面

**Architecture:** WinForms Form1 上放置 Button + WebView2 控件，本地 duanwu.html (Vue 3 CDN) 由 WebView2 通过 file:// 加载

**Tech Stack:** .NET Framework 4.7.2, WinForms, Microsoft.Web.WebView2, Vue 3 CDN, 纯 CSS

---

### Task 1: 添加 WebView2 NuGet 包

**Files:**
- Modify: `WindowsFormsApp1\WindowsFormsApp1\WindowsFormsApp1.csproj`

- [ ] **Step 1: 安装 NuGet 包**

```bash
cd D:/claude/WindowsFormsApp1/WindowsFormsApp1 && nuget install Microsoft.Web.WebView2 -OutputDirectory packages 2>&1 || dotnet add WindowsFormsApp1.csproj package Microsoft.Web.WebView2 2>&1 || echo "尝试通过 nuget.exe 安装"
```

由于 CLI 环境受限，我们手动编辑 .csproj 添加 PackageReference：

在 `WindowsFormsApp1.csproj` 的 `<ItemGroup>` 中添加：

```xml
<PackageReference Include="Microsoft.Web.WebView2">
  <Version>1.0.2903.40</Version>
</PackageReference>
```

- [ ] **Step 2: 还原包并确认编译成功**

```bash
cd D:/claude/WindowsFormsApp1 && dotnet restore WindowsFormsApp1/WindowsFormsApp1.csproj && dotnet build WindowsFormsApp1/WindowsFormsApp1.csproj
```

Expected: Build succeeded.

- [ ] **Step 3: 提交**

```bash
git add WindowsFormsApp1/WindowsFormsApp1/WindowsFormsApp1.csproj
git commit -m "feat: add WebView2 NuGet package"
```

---

### Task 2: 修改 Form1 布局 — 添加 Button 和 WebView2

**Files:**
- Modify: `WindowsFormsApp1\WindowsFormsApp1\Form1.Designer.cs`
- Modify: `WindowsFormsApp1\WindowsFormsApp1\Form1.cs`

- [ ] **Step 1: 修改 Form1.Designer.cs — 添加控件**

将 `Form1.Designer.cs` 的 `InitializeComponent` 方法替换为：

```csharp
private void InitializeComponent()
{
    this.btnOpenPage = new System.Windows.Forms.Button();
    this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
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
```

同时更新 Dispose 方法，在 `components.Dispose()` 之后添加 `webView.Dispose()`：

```csharp
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
```

并在 `#endregion` 之前添加字段声明，替换现有的字段区域：

```csharp
#region Windows 窗体设计器生成的代码

private System.Windows.Forms.Button btnOpenPage;
private Microsoft.Web.WebView2.WinForms.WebView2 webView;

/// <summary>
/// 设计器支持所需的方法 - 不要修改
/// 使用代码编辑器修改此方法的内容。
/// </summary>
private void InitializeComponent()
{
    // ... 上面的 InitializeComponent 代码
}

#endregion
```

- [ ] **Step 2: 修改 Form1.cs — 添加按钮点击逻辑**

将 `Form1.cs` 替换为：

```csharp
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
```

- [ ] **Step 3: 编译确认无语法错误**

```bash
cd D:/claude/WindowsFormsApp1 && dotnet build WindowsFormsApp1/WindowsFormsApp1.csproj
```

Expected: Build succeeded.

- [ ] **Step 4: 提交**

```bash
git add WindowsFormsApp1/WindowsFormsApp1/Form1.cs WindowsFormsApp1/WindowsFormsApp1/Form1.Designer.cs
git commit -m "feat: add Button + WebView2 to Form1"
```

---

### Task 3: 创建端午节 Vue 页面

**Files:**
- Create: `WindowsFormsApp1\WindowsFormsApp1\duanwu.html`

- [ ] **Step 1: 创建 duanwu.html**

创建 `D:\claude\WindowsFormsApp1\WindowsFormsApp1\duanwu.html`：

```html
<!DOCTYPE html>
<html lang="zh-CN">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>端午安康</title>
<script src="https://unpkg.com/vue@3/dist/vue.global.prod.js"></script>
<style>
* { margin: 0; padding: 0; box-sizing: border-box; }

body {
  font-family: "Microsoft YaHei", "PingFang SC", sans-serif;
  background: radial-gradient(ellipse at 50% 0%, #faf6ee 0%, #e8f0e3 60%, #d4e3c8 100%);
  color: #3b3b3b;
  overflow-x: hidden;
  min-height: 100vh;
}

/* ========== 粽叶飘落粒子 ========== */
.leaf {
  position: fixed;
  top: -60px;
  z-index: 0;
  pointer-events: none;
  animation: leafFall linear infinite;
  opacity: 0.6;
  font-size: 28px;
}
@keyframes leafFall {
  0%   { transform: translateY(0) rotate(0deg) translateX(0); opacity: 0.7; }
  25%  { transform: translateY(25vh) rotate(90deg) translateX(40px); }
  50%  { transform: translateY(50vh) rotate(180deg) translateX(-20px); opacity: 0.4; }
  75%  { transform: translateY(75vh) rotate(270deg) translateX(30px); }
  100% { transform: translateY(105vh) rotate(360deg) translateX(-10px); opacity: 0; }
}

/* ========== 主容器 ========== */
.container {
  position: relative;
  z-index: 1;
  max-width: 900px;
  margin: 0 auto;
  padding: 20px;
}

/* ========== Header ========== */
.header {
  text-align: center;
  padding: 60px 20px 40px;
  position: relative;
}
.header .icon-row { font-size: 48px; margin-bottom: 16px; }
.header .icon-row span { display: inline-block; animation: sway 2s ease-in-out infinite; }
.header .icon-row span:nth-child(2) { animation-delay: 0.3s; }
.header .icon-row span:nth-child(3) { animation-delay: 0.6s; }
@keyframes sway {
  0%, 100% { transform: rotate(0deg); }
  25% { transform: rotate(8deg); }
  75% { transform: rotate(-8deg); }
}
.header h1 {
  font-size: 3.2em;
  font-weight: 700;
  background: linear-gradient(135deg, #4A7C59 0%, #2d5a3a 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
  margin-bottom: 8px;
}
.header .subtitle {
  font-size: 1.2em;
  color: #8a9e7b;
  letter-spacing: 4px;
}
.divider {
  width: 80px; height: 3px;
  background: linear-gradient(90deg, #C41E3A, #e6734a);
  margin: 20px auto;
  border-radius: 2px;
}

/* ========== Section 通用 ========== */
.section {
  margin: 40px 0;
  padding: 36px 40px;
  background: rgba(255,255,255,0.75);
  backdrop-filter: blur(8px);
  border-radius: 16px;
  box-shadow: 0 2px 20px rgba(74,124,89,0.08);
  transition: transform 0.5s ease, opacity 0.5s ease;
}
.section-title {
  font-size: 1.6em;
  font-weight: 700;
  color: #4A7C59;
  margin-bottom: 20px;
  padding-left: 16px;
  border-left: 4px solid #C41E3A;
}
.section.fade-in {
  transform: translateY(30px);
  opacity: 0;
}
.section.fade-in.visible {
  transform: translateY(0);
  opacity: 1;
}

/* ========== 由来 ========== */
.origin { background: linear-gradient(135deg, rgba(74,124,89,0.06), rgba(196,30,58,0.04)); }
.origin p { line-height: 2; text-indent: 2em; margin-bottom: 8px; font-size: 1.05em; }

/* ========== 习俗卡片 ========== */
.customs-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 20px;
}
.custom-card {
  text-align: center;
  padding: 24px 12px;
  background: #fff;
  border-radius: 12px;
  box-shadow: 0 2px 12px rgba(0,0,0,0.06);
  transition: transform 0.25s ease, box-shadow 0.25s ease;
  cursor: default;
}
.custom-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 8px 24px rgba(74,124,89,0.15);
}
.custom-card .emoji { font-size: 2.6em; margin-bottom: 10px; }
.custom-card h4 { font-size: 1.1em; color: #4A7C59; margin-bottom: 6px; }
.custom-card p { font-size: 0.88em; color: #777; line-height: 1.5; }

/* ========== 诗词 ========== */
.poem { text-align: center; }
.poem h3 { font-size: 1.2em; color: #C41E3A; margin-bottom: 6px; }
.poem .author { font-size: 0.9em; color: #999; margin-bottom: 14px; }
.poem .lines { line-height: 2.2; font-size: 1.1em; color: #555; font-style: italic; }

/* ========== Footer ========== */
.footer {
  text-align: center;
  padding: 40px 20px;
  color: #aaa;
  font-size: 0.95em;
}

/* ========== 响应式 ========== */
@media (max-width: 700px) {
  .customs-grid { grid-template-columns: repeat(2, 1fr); }
  .header h1 { font-size: 2em; }
  .section { padding: 24px 20px; }
}
</style>
</head>
<body>

<div id="app">
  <!-- 粽叶飘落粒子 -->
  <span v-for="leaf in leaves" :key="leaf.id" class="leaf"
    :style="{
      left: leaf.x + '%',
      animationDuration: leaf.duration + 's',
      animationDelay: leaf.delay + 's',
      fontSize: leaf.size + 'px'
    }">🍃</span>

  <div class="container">
    <!-- Header -->
    <header class="header">
      <div class="icon-row">
        <span>🐲</span><span>🎋</span><span>🚣</span>
      </div>
      <h1>端午安康</h1>
      <p class="subtitle">五 月 初 五 · 仲 夏 端 午</p>
      <div class="divider"></div>
    </header>

    <!-- 由来 -->
    <section class="section origin">
      <h2 class="section-title">端午由来</h2>
      <p>端午节，又称端阳节、龙舟节，是中华民族古老的传统节日之一，始于春秋战国时期，至今已有两千多年历史。端午的起源说法众多，其中最广为流传的是纪念伟大的爱国诗人屈原。</p>
      <p>相传公元前278年，秦国攻破楚国都城，屈原在绝望和悲愤之下于五月初五抱石投汨罗江而亡。楚国百姓闻讯后纷纷划船追赶拯救，并将饭团、鸡蛋投入江中，以免鱼虾咬食屈原的身体。此后，每年五月初五便有了龙舟竞渡、吃粽子等习俗，以此表达对屈原的缅怀与敬仰。</p>
    </section>

    <!-- 习俗 -->
    <section class="section">
      <h2 class="section-title">端午习俗</h2>
      <div class="customs-grid">
        <div class="custom-card" v-for="item in customs" :key="item.name">
          <div class="emoji">{{ item.emoji }}</div>
          <h4>{{ item.name }}</h4>
          <p>{{ item.desc }}</p>
        </div>
      </div>
    </section>

    <!-- 诗词 -->
    <section class="section">
      <h2 class="section-title">端午诗词</h2>
      <div class="poem" v-for="poem in poems" :key="poem.title" style="margin-bottom: 28px;">
        <h3>{{ poem.title }}</h3>
        <p class="author">{{ poem.author }} · {{ poem.dynasty }}</p>
        <p class="lines" v-html="poem.lines"></p>
      </div>
    </section>

    <!-- Footer -->
    <footer class="footer">
      <p>🐲 端午节快乐 · 岁岁安康 🎋</p>
      <p style="margin-top: 4px;">2026 农历五月初五</p>
    </footer>
  </div>
</div>

<script>
const { createApp, ref, onMounted } = Vue;

createApp({
  setup() {
    // 粽叶粒子数据
    const leaves = ref([]);
    for (let i = 0; i < 15; i++) {
      leaves.value.push({
        id: i,
        x: Math.random() * 100,
        duration: 8 + Math.random() * 10,
        delay: Math.random() * 12,
        size: 18 + Math.random() * 22
      });
    }

    // 习俗数据
    const customs = ref([
      { emoji: '🚣', name: '赛龙舟', desc: '龙舟竞渡，鼓声震天，团结争先，纪念屈原' },
      { emoji: '🫔', name: '吃粽子', desc: '糯米包裹粽叶清香，甜咸各有风味，唇齿留香' },
      { emoji: '🌿', name: '挂艾草', desc: '艾叶菖蒲悬于门楣，驱虫避邪，祈求平安健康' },
      { emoji: '🧧', name: '佩香囊', desc: '五色丝线绣香囊，内装香药，佩戴于身以求吉祥' }
    ]);

    // 诗词数据
    const poems = ref([
      {
        title: '端午即事',
        author: '文天祥',
        dynasty: '宋',
        lines: '五月五日午，赠我一枝艾。<br>故人不可见，新知万里外。<br>丹心照夙昔，鬓发日已改。<br>我欲从灵均，三湘隔辽海。'
      },
      {
        title: '浣溪沙·端午',
        author: '苏轼',
        dynasty: '宋',
        lines: '轻汗微微透碧纨，明朝端午浴芳兰。<br>流香涨腻满晴川。<br>彩线轻缠红玉臂，小符斜挂绿云鬟。<br>佳人相见一千年。'
      }
    ]);

    // 滚动渐入动画
    onMounted(() => {
      const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
          if (entry.isIntersecting) {
            entry.target.classList.add('visible');
          }
        });
      }, { threshold: 0.15 });

      document.querySelectorAll('.section.fade-in').forEach(el => observer.observe(el));
    });

    return { leaves, customs, poems };
  }
}).mount('#app');
</script>
</body>
</html>
```

- [ ] **Step 2: 确认文件已创建**

检查 `D:\claude\WindowsFormsApp1\WindowsFormsApp1\duanwu.html` 存在。

- [ ] **Step 3: 提交**

```bash
git add WindowsFormsApp1/WindowsFormsApp1/duanwu.html
git commit -m "feat: add 端午节 Vue 3 网页"
```

---

### Task 4: 配置 duanwu.html 复制到输出目录

**Files:**
- Modify: `WindowsFormsApp1\WindowsFormsApp1\WindowsFormsApp1.csproj`

- [ ] **Step 1: 在 .csproj 中添加文件复制配置**

在 `<Project>` 内的 `<ItemGroup>` 中添加：

```xml
<None Update="duanwu.html">
  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</None>
```

- [ ] **Step 2: 提交**

```bash
git add WindowsFormsApp1/WindowsFormsApp1/WindowsFormsApp1.csproj
git commit -m "build: copy duanwu.html to output directory"
```

---

### Task 5: 编译验证

- [ ] **Step 1: 完整构建**

```bash
cd D:/claude/WindowsFormsApp1 && dotnet build WindowsFormsApp1/WindowsFormsApp1.csproj
```

Expected: Build succeeded, 0 errors.

- [ ] **Step 2: 提交（如有改动）**

```bash
git status
git add -A
git commit -m "chore: final adjustments for 端午节 page"
```
