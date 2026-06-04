# 端午节 WebView2 混合页面 — 设计文档

**日期：** 2026-06-04
**目标：** 在 WindowsFormsApp1 中使用 WebView2 嵌入 Vue 3 端午节主题网页

---

## 1. 架构概览

```
┌──────────────────────────────────────────────┐
│ WinForms App (Form1)                         │
│  ┌────────────────────────────────────────┐  │
│  │ Button "打开端午页面"                    │  │
│  └────────────────────────────────────────┘  │
│  ┌────────────────────────────────────────┐  │
│  │ Microsoft.Web.WebView2 控件             │  │
│  │  ┌──────────────────────────────────┐  │  │
│  │  │ duanwu.html (Vue 3 CDN 单页面)   │  │  │
│  │  │ - Header (标题 + 装饰)            │  │  │
│  │  │ - 由来 (屈原故事)                 │  │  │
│  │  │ - 习俗 (卡片式展示)               │  │  │
│  │  │ - 诗词 (经典引用)                 │  │  │
│  │  │ - Footer                         │  │  │
│  │  └──────────────────────────────────┘  │  │
│  └────────────────────────────────────────┘  │
└──────────────────────────────────────────────┘
```

## 2. 技术选型

| 层级 | 技术 | 说明 |
|------|------|------|
| 桌面容器 | .NET Framework 4.7.2 WinForms | 现有项目 |
| 浏览器控件 | Microsoft.Web.WebView2 | NuGet 包，Win11 自带 Runtime |
| 前端框架 | Vue 3 CDN | 无需构建工具 |
| 样式 | 纯 CSS | 动画、渐变、过渡 |

## 3. WinForms 变更

### 3.1 NuGet 依赖
- `Microsoft.Web.WebView2` — 添加至 WindowsFormsApp1.csproj

### 3.2 Form1 布局
- 顶部放置 Button `btnOpenPage`，Text = "打开端午页面"
- 下方放置 WebView2 `webView`，Dock = Fill（在按钮下方）
- 按钮点击事件 `btnOpenPage_Click`：设置 webView.Source 为 duanwu.html 的 file:// URI

### 3.3 文件部署
- `duanwu.html` 文件设置为"复制到输出目录"或作为嵌入式资源
- 使用 `Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "duanwu.html")` 定位文件

## 4. Vue 页面设计

### 4.1 页面结构

| Section | 内容 |
|---------|------|
| Header | 大标题"端午安康" + 龙舟/粽子装饰、副标题 |
| 由来 | 屈原投江的故事，简短2-3段文字，配深色背景突出 |
| 习俗 | 4 张卡片横向排列：赛龙舟、吃粽子、挂艾草、佩香囊，每张含 emoji 图标 + 说明 |
| 诗词 | 2 首经典端午诗词（如文天祥《端午即事》），带书名和引用样式 |
| Footer | "端午节快乐 🎋" + 年份 |

### 4.2 视觉规范

| 属性 | 值 |
|------|------|
| 主色 | 粽叶绿 `#4A7C59` |
| 辅色 | 暖黄 `#F5E6C8` |
| 点缀 | 中国红 `#C41E3A` |
| 背景 | 米白到淡绿的径向渐变 |
| 字体 | `"Microsoft YaHei", sans-serif` |
| 卡片 | 圆角 12px，阴影，hover 轻微上浮 |
| 最大宽度 | 960px，居中 |

### 4.3 动画效果
- **滚动渐入**：各 section 在进入视口时淡入上移（Intersection Observer）
- **粽叶飘落**：CSS keyframes 粒子效果，缓慢旋转飘落
- **龙舟微动**：header 中的龙舟装饰左右轻微晃动
- **卡片 hover**：上浮 4px + 阴影加深过渡

### 4.4 Vue 数据驱动
- 习俗列表使用 `v-for` 渲染
- 诗词列表使用 `v-for` 渲染
- 粽叶粒子使用 Vue 动态生成

## 5. 边界与约束

- WebView2 需要在 Form.Load 或按钮点击前初始化
- 确保 `WebView2.EnsureCoreWebView2Async` 已完成后再设置 Source
- 页面内所有资源使用内联或 CDN，不依赖外部文件
- 目标：Win11 运行，.NET Framework 4.7.2
