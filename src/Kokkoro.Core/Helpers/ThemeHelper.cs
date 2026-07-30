using Avalonia;
using Avalonia.Styling;

namespace Kokkoro.Core.Helpers;

/// <summary>
/// 应用主题帮助类。
/// </summary>
public static class ThemeHelper
{
    /// <summary>
    /// 切换为浅色主题。
    /// </summary>
    public static void SetLight()
    {
        Application.Current!.RequestedThemeVariant = ThemeVariant.Light;
    }

    /// <summary>
    /// 切换为深色主题。
    /// </summary>
    public static void SetDark()
    {
        Application.Current!.RequestedThemeVariant = ThemeVariant.Dark;
    }

    /// <summary>
    /// 跟随系统主题。
    /// </summary>
    public static void SetDefault()
    {
        Application.Current!.RequestedThemeVariant = ThemeVariant.Default;
    }
}
