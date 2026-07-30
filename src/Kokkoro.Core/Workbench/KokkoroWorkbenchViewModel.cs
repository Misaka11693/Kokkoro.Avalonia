using Kokkoro.Core.ViewModels;
using Kokkoro.Core.Workbench.Regions.Header;
using Kokkoro.Core.Workbench.Regions.Page;
using Kokkoro.Core.Workbench.Regions.Sidebar;
using Kokkoro.Core.Workbench.Regions.StatusBar;
using Kokkoro.Core.Workbench.Regions.TitleBar;

namespace Kokkoro.Core.Workbench;

/// <summary>
/// Kokkoro 工作台视图模型
/// </summary>
public class KokkoroWorkbenchViewModel : ViewModelBase
{
    public KokkoroWorkbenchViewModel(
        TitleBarLeftViewModel titleBarLeft,
        TitleBarCenterViewModel titleBarCenter,
        TitleBarRightViewModel titleBarRight,
        HeaderBarViewModel headerBar,
        SidebarViewModel sidebar,
        PageViewModel page,
        StatusBarViewModel statusBar)
    {
        TitleBarLeft = titleBarLeft;
        TitleBarCenter = titleBarCenter;
        TitleBarRight = titleBarRight;
        HeaderBar = headerBar;
        Sidebar = sidebar;
        Page = page;
        StatusBar = statusBar;
    }

    /// <summary>
    /// 应用标题
    /// </summary>
    public string AppTitle { get; set; } = "Kokkoro";

    /// <summary>
    /// 标题栏左侧区域
    /// </summary>
    public TitleBarLeftViewModel TitleBarLeft { get; }

    /// <summary>
    /// 标题栏中间区域
    /// </summary>
    public TitleBarCenterViewModel TitleBarCenter { get; }

    /// <summary>
    /// 标题栏右侧区域
    /// </summary>
    public TitleBarRightViewModel TitleBarRight { get; }

    /// <summary>
    /// 头部栏区域
    /// </summary>
    public HeaderBarViewModel HeaderBar { get; }

    /// <summary>
    /// 侧边栏区域
    /// </summary>
    public SidebarViewModel Sidebar { get; }

    /// <summary>
    /// 页面内容区域
    /// </summary>
    public PageViewModel Page { get; }

    /// <summary>
    /// 状态栏区域
    /// </summary>
    public StatusBarViewModel StatusBar { get; }
}