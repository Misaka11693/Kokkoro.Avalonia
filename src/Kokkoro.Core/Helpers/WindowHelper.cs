using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace Kokkoro.Core.Helpers;

/// <summary>
/// 窗口帮助类。
/// </summary>
public static class WindowHelper
{
    /// <summary>
    /// 获取当前活动窗口。
    /// </summary>
    /// <returns>活动窗口；如果没有活动窗口，则返回主窗口；如果不是桌面应用则返回 null。</returns>
    public static Window? GetActiveWindow()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            return null;

        return desktop.Windows.FirstOrDefault(w => w.IsActive)
               ?? desktop.MainWindow;
    }

    /// <summary>
    /// 获取当前激活窗口的哈希码，若没有激活窗口则返回主窗口的哈希码，若两者都为 null 则返回 null。
    /// </summary>
    public static int? GetActiveWindowHashCode()
    {
        return GetActiveWindow()?.GetHashCode() ?? null;
    }

    /// <summary>
    /// 获取指定控件所属的窗口。
    /// </summary>
    /// <param name="control">目标控件。</param>
    /// <returns>所属窗口，不存在时返回 <c>null</c>。</returns>
    public static Window? GetWindow(Control control)
    {
        return TopLevel.GetTopLevel(control) as Window;
    }

    /// <summary>
    /// 获取指定控件所属的顶层容器。
    /// </summary>
    /// <param name="control">目标控件。</param>
    /// <returns>顶层容器对象。</returns>
    public static TopLevel? GetTopLevel(Control control)
    {
        return TopLevel.GetTopLevel(control);
    }
}
