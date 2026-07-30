# Kokkoro Project Conventions

本文件用于帮助新的协作者或 AI 快速理解本项目的技术栈、分层方式、窗口规范、DI 规则和 UI 实现偏好。  
在修改本项目之前，请先阅读本文件，再动手写代码。

---

## 1. 项目技术栈

本项目当前核心技术栈为：

- `Avalonia`
- `Semi.Avalonia`
- `Ursa`
- `Dock.Avalonia`
- `ReactiveUI`

这些库在项目中的定位不是并列随便用，而是各自承担明确职责：

### 1.1 Avalonia

`Avalonia` 是基础 UI 框架，负责：

- 窗口系统
- XAML 布局
- 控件树
- 样式系统
- 事件与绑定
- 跨平台桌面能力

所有界面最终都建立在 Avalonia 之上。

### 1.2 Semi.Avalonia

`Semi.Avalonia` 是本项目当前主要的视觉风格来源之一，负责：

- 主题资源
- 配色体系
- 通用视觉样式
- 常规控件的外观一致性

例如项目中已经在使用的资源：

- `SemiColorBg0`
- `SemiColorBg1`
- `SemiColorBorder`
- `SemiColorText2`
- `SemiColorFill0`

因此，**在新增界面时，优先沿用 Semi 的资源和视觉语言，不要随意自定义一套完全脱节的配色与边框体系。**

### 1.3 Ursa

`Ursa` 是本项目的重要桌面增强库，主要负责：

- 更现代的窗口能力
- 特殊窗口类型
- 对话式窗口体验
- 某些增强控件或宿主能力

例如项目中已经使用过：

- `UrsaWindow`
- `ReactiveUrsaWindow<TViewModel>`
- `SplashWindow`

因此，当需求涉及以下内容时，**应优先考虑 Ursa 是否已有合适的窗口基类、宿主能力或控件风格**：

- 登录窗口
- 关于窗口
- 启动窗口
- 对话框
- 标题栏增强
- 特殊桌面窗口行为

### 1.4 Dock.Avalonia

`Dock.Avalonia` 负责可停靠布局、文档页和工作区组织能力，主要用于：

- 主工作区
- 文档页容器
- 页面切换
- 页面关闭
- 停靠式布局管理

当需求涉及以下内容时，应优先查看现有 Dock 结构与服务：

- 新增工作页面
- 新增文档页
- 关闭当前页 / 其他页 / 全部页
- 停靠布局调整
- 工作区容器行为

不要绕开现有 Dock 体系私自再做一套“标签页管理”。

### 1.5 ReactiveUI

`ReactiveUI` 是本项目 MVVM 交互模式的核心之一，主要负责：

- `IViewFor<T>`
- View 与 ViewModel 绑定
- 响应式 View 基类
- 命令和属性通知协作

项目中已经使用的典型模式包括：

- `ReactiveUrsaWindow<TViewModel>`
- `ReactiveUserControl<TViewModel>`
- `IViewFor<TViewModel>`
- `ReactiveCommand`

因此，普通业务窗口和普通业务视图，优先遵循 ReactiveUI 的结构，不要混入大量 code-behind 业务逻辑。

### 1.6 本地参考源码

查 Semi / Ursa 的控件用法、主题资源、Demo 示例时，**优先在本机源码仓库里搜索**，不必先上网搜。

| 库 | 本地路径 |
|----|----------|
| Semi | `D:\Project\Github\Semi` |
| Ursa | `D:\Project\Github\Ursa` |

简写（协作者 / AI 检索用）：

- `semi`: `D:\Project\Github\Semi`
- `ursa`: `D:\Project\Github\Ursa`

常用子目录：

- Semi Demo：`D:\Project\Github\Semi\demo\Semi.Avalonia.Demo`
- Semi 主题与图标：`D:\Project\Github\Semi\src\Semi.Avalonia`
- Ursa Demo：`D:\Project\Github\Ursa\demo\Ursa.Demo`
- Ursa 控件源码：`D:\Project\Github\Ursa\src\Ursa`

---

## 2. 实现新功能时的优先顺序

这是本项目非常重要的一条约定。

### 2.1 不要一上来就自定义控件或样式

当你要实现一个新功能、新窗口、新弹层、新表单、新按钮区时，请按下面顺序判断：

1. 先看 `Avalonia` 原生控件能不能满足
2. 再看 `Semi.Avalonia` 有没有更合适的主题样式或视觉资源
3. 再看 `Ursa` 有没有更合适的窗口类型、对话能力或增强控件
4. 如果是工作区、文档页、停靠布局相关，再看 `Dock.Avalonia`
5. 只有前面都不合适时，才新增自定义实现

也就是说：

**先复用框架和现有库，再写自定义。**

### 2.2 写界面前先检查这两件事

新增 UI 功能前，至少先确认：

- `Semi` 有没有适合复用的样式、颜色资源、视觉表达
- `Ursa` 有没有适合复用的窗口、宿主、交互形式或控件能力

这条规则非常重要。  
后续 AI 在实现功能时，不应该直接跳过库能力，直接手搓一套样式或窗口。

### 2.3 什么时候优先看 Semi

当需求偏向这些内容时，先看 `Semi.Avalonia`：

- 页面视觉风格
- Border / Background / Foreground
- 文本层级
- 面板底色
- 描边与分隔
- 常规按钮和输入框外观
- 与现有界面保持统一的颜色资源

### 2.4 什么时候优先看 Ursa

当需求偏向这些内容时，先看 `Ursa`：

- Window / Dialog
- Splash / Startup
- 标题栏窗口增强
- 桌面窗口交互
- 更适合桌面应用语义的宿主能力

### 2.5 什么时候优先看 Dock.Avalonia

当需求偏向这些内容时，先看 `Dock.Avalonia`：

- 工作区
- 文档页
- 页签关闭
- 停靠布局
- 主内容区域页面管理

---

## 3. 项目总体结构

项目采用 MVVM，并且强调职责清晰。

目录职责如下：

- `Views/`
  - 视图层
  - 只负责显示、绑定、少量视图生命周期逻辑
- `ViewModels/`
  - 视图状态、展示数据、命令、事件协调
- `Services/`
  - 业务能力或跨页面/跨窗口服务
- `Docking/`
  - Dock 布局、文档页、工作区组织相关实现
- `App.axaml.cs`
  - 应用壳层
  - 负责窗口创建、窗口切换、应用级流程
- `ServiceCollectionExtensions.cs`
  - 依赖注入统一注册入口

---

## 4. MVVM 规范

### 4.1 普通业务窗口和视图

普通业务窗口、普通业务 UserControl、标题栏子视图，应尽量采用统一的 MVVM 写法：

- `ViewModel` 放在 `ViewModels/...`
- `View` 放在 `Views/...`
- 命名一一对应

例如：

- `MainWindowViewModel` <-> `MainWindow`
- `AuthWindowViewModel` <-> `AuthWindow`
- `AboutWindowViewModel` <-> `AboutWindow`

### 4.2 ViewModel 职责

ViewModel 负责：

- 展示数据
- 命令
- 状态
- 供界面绑定的文本
- 界面事件协调

ViewModel 不应依赖具体视觉树结构。

### 4.3 View 职责

View 负责：

- `InitializeComponent()`
- 绑定 ViewModel
- 处理必要的视图生命周期
- 订阅/取消订阅 ViewModel 事件
- 处理纯视图层行为

View 中尽量不要堆积：

- 大段业务逻辑
- 大量显示文本拼装
- 本应放入 ViewModel 的命令处理

### 4.4 关闭窗口的推荐方式

如果 ViewModel 需要触发窗口关闭，推荐：

- ViewModel 发出事件
- View 监听事件并调用 `Close()`

例如：

- `CloseRequested`
- `ConfirmCommand`

这比在 code-behind 里直接塞业务关闭逻辑更清晰。

---

## 5. ReactiveUI / IViewFor 规范

对于普通业务窗口和普通业务视图，优先使用 ReactiveUI 体系。

推荐形式：

- `ReactiveUrsaWindow<TViewModel>`
- `ReactiveUserControl<TViewModel>`
- `IViewFor<TViewModel>`

例如：

- `MainWindow : ReactiveUrsaWindow<MainWindowViewModel>`
- `AuthWindow : ReactiveUrsaWindow<AuthWindowViewModel>`
- `AboutWindow : ReactiveUrsaWindow<AboutWindowViewModel>`

新增普通窗口时，优先考虑：

1. 新建 `XXWindowViewModel`
2. 新建 `XXWindow`
3. 让窗口实现 `IViewFor<XXWindowViewModel>` 或继承合适的 Reactive 基类
4. 在 DI 中注册 `IViewFor<XXWindowViewModel>`
5. 在 `App` 中统一创建和显示

---

## 6. 窗口规范

### 6.1 App 是窗口壳层入口

`App.axaml.cs` 负责：

- 创建窗口
- 注入 ViewModel
- 切换主窗口
- 展示对话窗口

外层视图不要自己散落地 `new Window()` 组织应用流程。

### 6.2 ShowXXWindow 规范

对外公开的窗口操作优先使用：

- `ShowMainWindow(...)`
- `ShowAuthWindow(...)`
- `ShowAboutWindow(...)`

内部创建实例使用：

- `CreateMainWindow()`
- `CreateAuthWindow()`
- `CreateAboutWindow()`

约定：

- `ShowXXWindow(...)` 是外部调用入口
- `CreateXXWindow()` 是内部实现细节
- 能设为 `private` 的优先设为 `private`

### 6.3 参数命名必须体现语义

不要为了表面统一而损失含义。

例如：

- `ShowMainWindow(Window currentWindow)`
- `ShowAuthWindow(Window currentWindow)`

这里 `currentWindow` 表示会被替换或关闭的当前窗口。

而：

- `ShowAboutWindow(Window owner)`

这里 `owner` 表示对话框宿主窗口，不会被关闭。  
所以这里不应机械改成 `currentWindow`。

---

## 7. DI 规范

### 7.1 注册入口统一

所有应用级注册统一放在：

- `ServiceCollectionExtensions.cs`

包括：

- 服务
- ViewModel
- 普通业务窗口的 `IViewFor<T>`
- 特殊窗口本体

### 7.2 普通业务窗口注册方式

普通业务窗口推荐这样注册：

```csharp
services.AddTransient<AboutWindowViewModel>();
services.AddTransient<IViewFor<AboutWindowViewModel>, AboutWindow>();
```

在 `App` 中再统一解析 View 和 ViewModel：

```csharp
var viewModel = Services.GetRequiredService<AboutWindowViewModel>();
var view = Services.GetRequiredService<IViewFor<AboutWindowViewModel>>();
view.ViewModel = viewModel;
```

### 7.3 能进 DI 的尽量进 DI

如果某个窗口、服务、辅助对象能合理放进 DI，就优先放进 DI。  
不要同一种职责一半用容器，一半直接手写构造。

---

## 8. 特殊窗口例外规范

### 8.1 StartupSplashWindow 是特殊窗口

`StartupSplashWindow` 不是普通业务窗口，它继承的是：

- `SplashWindow`

它还要承担：

- 启动阶段过渡
- `CreateNextWindow()` 生命周期钩子

并且 `Ursa` 没有为它提供与普通 `ReactiveUrsaWindow<TViewModel>` 完全一致的现成模式。

因此本项目约定：

- `StartupSplashWindow` 是特殊窗口
- 它允许不走普通 `IViewFor<T>` 模板
- 但它仍然应该纳入 DI 管理

### 8.2 特殊窗口的推荐做法

推荐写法：

```csharp
services.AddTransient<StartupSplashWindow>();
services.AddTransient<StartupSplashWindowViewModel>();
```

然后在 `App` 中：

```csharp
var viewModel = Services.GetRequiredService<StartupSplashWindowViewModel>();
var view = Services.GetRequiredService<StartupSplashWindow>();
view.DataContext = viewModel;
```

这代表本项目的一个重要原则：

**普通业务窗口严格统一；特殊框架窗口允许例外，但要保持可解释、可维护，并尽量纳入容器。**

---

## 9. Dock.Avalonia 相关规范

涉及工作区、文档页、页面关闭时，优先复用现有 Dock 能力，不要再造一套页面管理。

重点关注：

- `Docking/`
- `IDockLayoutManager`
- `DockLayoutManager`
- 现有关闭当前页 / 其他页 / 全部页逻辑

如果需求是：

- 新增主工作区页面
- 页面导航
- 关闭页签
- 文档页切换
- 停靠布局操作

请先理解现有 Dock 组织方式，再动手改。

### 9.1 文档页缩放

#### 目标效果（产品描述，供备忘）

参考 BiTECH / WPF 文档工作台（`LayoutTransform` + `ScaleTransform`，**不要**在缩放根外包 `ScrollViewer`）：

1. **整页一起缩放**  
   查询侧栏、工具栏、表格、分页等同属一页的内容，放大/缩小时**同步**变化，像同一张「画布」被缩放，而不是只放大表格或只放大文字。

2. **组件真的变大，布局仍适应窗口**  
   按钮、表头、行高、输入框、分页控件等**视觉上变大**（可读性提高），但文档区在 Dock 里占用的**布局槽位高度/宽度不变**——窗口不会因为缩放而「长高」，右侧**不出现整页纵向滚动条**。

3. **滚动条分工**  
   - **页面级**：不要因缩放出现外层竖向（或横向）滚动条。  
   - **控件级**：`DataGrid` 等数据区在内容超出可见区域时，仍用**控件自己的**滚动条（例如列太宽时表格底部横条、行太多时表格内部竖条）。

4. **放大后仍「像正常页面」**  
   分页、查询按钮等仍落在页面布局结构里（例如表格下方），而不是被裁掉后只能靠拖整页滚动条才能看见。

5. **交互习惯（与 WPF `SIE.Wpf.Helpers.Zoom` 对齐）**  
   - **Ctrl + 滚轮**：每次 ±10%，范围 **50%～400%**（默认 100%）  
   - **Ctrl + 中键**：恢复 100%  
   - 可选（未实现）：**Ctrl + 0**、**Ctrl + +/-**、**Ctrl + Shift + +/-**

**不要做成这样：**

| 错误做法 | 问题 |
|----------|------|
| 仅 `RenderTransform`、布局尺寸不变 | 只是「画大」，占位不变，底部易被裁切，不像 WPF 自适应 |
| `LayoutTransform` + 外层 `ScrollViewer` | 缩放后布局变高，出现**整页**纵向滚动条，分页要滚页面才能看到 |
| 只缩放 `DataGrid`、不缩放查询/分页 | 与「整页工作台」体验不一致 |

---

#### 两套实现（当前用哪个）

| | **现行：`ZoomHelper`** | **备份：`Zoom`（Attached Property）** |
|--|------------------------|--------------------------------------|
| 路径 | `Helpers/ZoomHelper.cs` | `Backups/Zoom-2026-06-06/`（**不参与编译**） |
| 启用方式 | 代码 `ZoomHelper.EnableZoom(control, scale)` | XAML `zoom:Zoom.IsEnabled="True"` |
| 缩放模式 | 仅 **Layout**（布局缩放） | **Visual** + **Layout** 可切换 |
| 对齐 WPF 文档工作台 | 是 | Layout 模式是；Visual 为画板/预览 |
| ViewModel 持久化 | 读写 `DocumentPageViewModel.Zoom` | 备份版曾支持，可按需接回 |
| 状态 | **生产环境使用** | 归档备用，可随时迁回 `Helpers/` 替换 |

旧备份：`Backups/ZoomHelper-2026-06-06/` 为 `ZoomHelper` 早期归档，仅作对照。

---

#### 现行实现：`Kokkoro.Helpers.ZoomHelper`

对齐 WPF `SIE.Wpf.Helpers.Zoom`。对页面根 `UserControl` 调用一次即可。

**API**

```csharp
ZoomHelper.EnableZoom(control);              // 100%
ZoomHelper.EnableZoom(control, zoomScale);   // 指定比例（会 Clamp 到 0.5～4.0）
ZoomHelper.DisableZoom(control);             // 仅页面销毁或明确要拆包装时调用
```

**机制**

- Avalonia 无 WPF 式 `LayoutTransform`：在 `UserControl.Content` 外包一层 `LayoutTransformControl`（**无外层 `ScrollViewer`**）。
- `ScaleTransform` 挂在 `LayoutTransform` 上，控件**真实变大/变小**，参与重新布局。
- 事件挂在页面根上：**Ctrl + 滚轮** ±10%，**Ctrl + 中键** 复位 100%。
- 缩放比例写入 `DocumentPageViewModel.Zoom`（随 Dock `Document` 实例保留；关闭文档后重开为新实例）。

**文档页接入（推荐）**

继承 `DocumentPageView<TViewModel>` 即可，无需在 XAML 写缩放属性：

```csharp
public partial class UsersPageView : DocumentPageView<UsersPageViewModel> { ... }
```

基类在 `DataContext` 就绪与 `Loaded` 时调用幂等的 `ZoomHelper.EnableZoom(this, document.Zoom)`。  
**不要在 `Unloaded` 里 `DisableZoom`**——切 Dock 标签会触发 `Unloaded`，拆掉包装会导致切回时先闪 100% 再恢复比例。

**页面 XAML 建议**

```xml
<UserControl MinHeight="0" MinWidth="0"
             HorizontalAlignment="Stretch"
             VerticalAlignment="Stretch"
             Background="Transparent"
             Focusable="True">
    <Grid MinHeight="0" MinWidth="0" ... />
</UserControl>
```

- `MinHeight="0"` / `Stretch`：空白区域也能收到滚轮与缩放。
- `Background="Transparent"`：透明区可点、可缩放。

**自定义工厂 / 非 `DocumentPageView` 页面**

```csharp
ZoomHelper.EnableZoom(pageRoot, viewModel.Zoom);
```

---

#### 备份实现：`Zoom`（Attached Property，双模式）

完整说明见 `Backups/Zoom-2026-06-06/README.md`。迁回步骤见该文件 **「替换 ZoomHelper」** 一节。

**XAML 用法（迁回后）**

```xml
xmlns:zoom="using:Kokkoro.Helpers"

<UserControl zoom:Zoom.IsEnabled="True"
             zoom:Zoom.Mode="Layout">
    ...
</UserControl>
```

`Mode` 支持直接写字符串（`TypeConverter`）：`Mode="Layout"` / `Mode="Visual"`。

**两种模式**

| 模式 | 行为 | 适用场景 |
|------|------|----------|
| **Layout** | `LayoutTransformControl`，与 `ZoomHelper` 相同 | 文档工作台、Users/Home/Settings 等整页可读性缩放 |
| **Visual** | `RenderTransform` + 内置 `ScrollViewer`，**左上角固定锚点**，向右下放大/缩小 | 画板、流程图预览、类 Photoshop「不随窗口缩放」的查看 |

**Visual 注意**

- 外层布局占位不变；放大超出视口时出现**页面级**滚动条（备份版设计如此，与 Layout 的「不要整页滚动条」不同）。
- Ctrl + 滚轮使用 **Tunnel** 路由，避免子级 `DataGrid` / `ScrollViewer` 抢走缩放。
- 须在 `Loaded` 且 `Content` 就绪后再包装（备份代码内已 `ScheduleDeferredApply`）。

**备份文件结构**

| 文件 | 职责 |
|------|------|
| `Zoom.cs` | Attached Property、事件、调度 |
| `Zoom.Layout.cs` | Layout 模式 |
| `Zoom.Visual.cs` | Visual 模式 |
| `ZoomMode.cs` | 枚举 + XAML 字符串转换 |
| `ZoomHost.cs` | 运行时状态 |

---

#### ViewModel 约定

- 文档页 ViewModel 继承 `DocumentPageViewModel`。
- `Zoom` 属性（默认 `InitialZoom => 1.0`）由 **`ZoomHelper` 在缩放时自动写入**。
- 切换 Dock 标签：同一 `Document` 实例上比例保留；视图可重建，基类会用 `document.Zoom` 再次 `EnableZoom`。
- 子类可 `override protected virtual double InitialZoom` 设置首次打开默认比例。

---

#### 选型建议

| 需求 | 选用 |
|------|------|
| 与 WPF 文档工作台一致、整页布局缩放 | **现行 `ZoomHelper`** 或备份 **`Zoom` + `Mode=Layout`** |
| 画板式预览、左上角锚点、可滚动查看 | 备份 **`Zoom` + `Mode=Visual`** |
| 一行 XAML 切换模式、少写代码 | 备份 **`Zoom`**（迁回后） |
| 最简、已跑通、少动刀 | **维持 `ZoomHelper`** |

---

## 10. 标题栏、菜单、Flyout 规范

- 标题栏菜单和按钮优先保持交互一致
- Flyout 需要考虑失焦关闭、窗口切换关闭、临时状态清理
- 可复用的临时交互逻辑优先抽 helper，不要在多个视图里各写一遍

当前已有参考：

- `WindowTransientDismissController`

如果后续新增标题栏弹出区、更多菜单、临时工具面板，应优先考虑复用现有模式。

---

## 11. 关于窗口当前约定

`AboutWindow` 当前被视为标准业务对话窗口，约定如下：

- 使用 `UrsaWindow`
- 使用 MVVM
- 使用 `AboutWindowViewModel`
- 使用 `IViewFor<AboutWindowViewModel>`
- 从 `App.ShowAboutWindow(owner)` 打开
- 文本和展示数据由 ViewModel 提供
- 支持窗口缩放
- 支持最大化

如果后续继续扩展关于窗口，也应优先沿用这一结构。

---

## 12. 编写功能时的直接要求

如果你是后续接手本项目的 AI 或开发者，在写新功能前请先遵守以下检查顺序：

1. 这个功能属于普通业务窗口，还是特殊框架窗口？
2. Avalonia 原生控件能否满足？
3. `Semi.Avalonia` 有没有合适的样式、资源、视觉方案？
4. `Ursa` 有没有合适的窗口、宿主、控件或交互形式？
5. 如果是工作区内容，`Dock.Avalonia` 有没有现成组织方式？
6. 列表页是否需要分页外壳？若是，先看 [`docs/PageView.md`](docs/PageView.md)（**不要**在分页容器里耦合选中逻辑）。
7. 是否应该走 MVVM？
8. 是否应该走 `IViewFor<T>`？
9. 是否应该纳入 `ServiceCollectionExtensions.cs`？
10. 是否应该通过 `App.ShowXXWindow(...)` 统一显示？

请不要跳过第 3 和第 4 步直接手写 UI。

---

## 13. 给后续 AI 的工作守则

后续 AI 在本项目中修改代码时，应默认遵循以下规则：

- 先看 `PROJECT_CONVENTIONS.md`
- 涉及分页列表页时先看 `docs/PageView.md`
- 先看 `App.axaml.cs`
- 先看 `ServiceCollectionExtensions.cs`
- 先看现有 `Views/` 与 `ViewModels/` 对应关系
- 先看 `Semi` 和 `Ursa` 是否已有合适能力
- 涉及工作区时先看 `Docking/` 和 `IDockLayoutManager`

默认偏好：

- 先复用，再扩展
- 先统一，再新增
- 先查现有库能力，再写自定义控件或样式

不推荐行为：

- 跳过 MVVM 直接把业务写进 code-behind
- 跳过 DI 直接到处 `new`
- 跳过 `Semi` / `Ursa` / `Dock.Avalonia` 直接另起一套
- 为了表面统一而使用错误的参数语义

---

## 14. 一句话总结

本项目的核心规范是：

**普通业务窗口优先走 Avalonia + Semi.Avalonia + Ursa + ReactiveUI 的统一模式，并纳入 DI 与 ShowXXWindow 流程；涉及工作区时优先复用 Dock.Avalonia；只有在现有库不适合时才做自定义实现。**

---

## 15. PageView 分页容器（V1.0 已落地）

大量列表页共用「内容区 + 底栏统计 + Ursa 分页」。用通用控件 **`PageView`**（`ContentPresenter` + Footer）替代仅面向 `DataGrid` 的 `PagedDataGrid`。

**V1.0 定稿要点：**

- 只做：内容容器 + 统计栏 + Ursa 分页器；**不做** `SelectedItems`、`ItemsSource`、`FooterTemplate` 等。
- **`SelectedCount` 仅显示**，由页面 / VM 维护；推荐 `SelectionChanged` 或后续 `DataGridSelectionBehavior`（不阻塞 V1）。
- **`IsPagingEnabled`**：加载中禁用分页器全部操作。
- **`PageSize` 变更** → 控件内 `CurrentPage = 1`。
- **底栏样式**与现行 `PagedDataGrid.axaml` 一致（Semi 资源 + Ursa Pagination 布局），不另起视觉。
- 命名：**`CurrentPage`**（不用 `PageIndex`）；`TotalCount` 默认 **OneWay**。

完整说明：**[`docs/PageView.md`](docs/PageView.md)**  
现行 `PagedDataGrid` 快照：`Backups/PagedDataGrid-2026-06-08/`。
