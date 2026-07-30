using Avalonia.Controls;
using Avalonia.Controls.Notifications;

namespace Kokkoro.Core.UI.Toasts;

/// <summary>
/// Toast 服务，用于显示短时自动消失的轻提示消息。
/// </summary>
public interface IToastService
{
    /// <summary>
    /// 显示 Toast。
    /// </summary>
    /// <param name="message">提示内容。</param>
    /// <param name="options">Toast 配置。</param>
    /// <param name="owner">宿主窗口。</param>
    Task ShowAsync(string message, ToastOptions? options = null, Window? owner = null);

    /// <summary>
    /// 显示信息 Toast。
    /// </summary>
    Task ShowInformationAsync(string message, ToastOptions? options = null, Window? owner = null);

    /// <summary>
    /// 显示成功 Toast。
    /// </summary>
    Task ShowSuccessAsync(string message, ToastOptions? options = null, Window? owner = null);

    /// <summary>
    /// 显示警告 Toast。
    /// </summary>
    Task ShowWarningAsync(string message, ToastOptions? options = null, Window? owner = null);

    /// <summary>
    /// 显示错误 Toast。
    /// </summary>
    Task ShowErrorAsync(string message, ToastOptions? options = null, Window? owner = null);

    /// <summary>
    /// 关闭指定窗口上的所有 Toast。
    /// </summary>
    Task CloseAllAsync(Window? owner = null);
}
