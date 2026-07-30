namespace Kokkoro.Core.Helpers;

/// <summary>
/// 平台帮助类。
/// </summary>
public static class PlatformHelper
{
    /// <summary>
    /// 获取当前是否为 Windows 平台。
    /// </summary>
    public static bool IsWindows => OperatingSystem.IsWindows();

    /// <summary>
    /// 获取当前是否为 Linux 平台。
    /// </summary>
    public static bool IsLinux => OperatingSystem.IsLinux();

    /// <summary>
    /// 获取当前是否为 macOS 平台。
    /// </summary>
    public static bool IsMacOS => OperatingSystem.IsMacOS();
}
