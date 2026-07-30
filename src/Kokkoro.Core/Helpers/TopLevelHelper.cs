using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kokkoro.Core.Helpers;

/// <summary>
/// 顶层容器帮助类。
/// 用于获取当前应用的顶层 UI 容器。
/// 支持 Desktop、Mobile、Web 等平台。
/// </summary>
public static class TopLevelHelper
{
    /// <summary>
    /// 获取当前活动的顶层容器。
    /// </summary>
    /// <returns>
    /// 当前活动的 TopLevel。
    /// 如果无法获取，则返回 null。
    /// </returns>
    public static TopLevel? GetActiveTopLevel()
    {
        if (Application.Current?.ApplicationLifetime == null)
            return null;


        switch (Application.Current.ApplicationLifetime)
        {
            // 桌面应用
            case IClassicDesktopStyleApplicationLifetime desktop:

                // 优先返回当前激活窗口
                return desktop.Windows
                    .FirstOrDefault(w => w.IsActive)
                    ??
                    // 没有激活窗口时返回主窗口
                    desktop.MainWindow;


            // 单页面应用
            // Android / iOS / WebAssembly
            case ISingleViewApplicationLifetime singleView:

                return TopLevel.GetTopLevel(singleView.MainView);


            default:

                return null;
        }
    }
}