namespace Kokkoro.Core.UI.Notifications;

/// <summary>
/// 窗口通知服务。
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// 显示自定义通知。
    /// </summary>
    /// <param name="message">通知消息内容。</param>
    /// <param name="options">通知配置选项。</param>
    Task ShowAsync(
        string message,
        NotificationOptions? options = null);


    /// <summary>
    /// 显示信息通知。
    /// </summary>
    /// <param name="message">通知消息内容。</param>
    /// <param name="options">通知配置选项。</param>
    Task ShowInformationAsync(
        string message,
        NotificationOptions? options = null);


    /// <summary>
    /// 显示成功通知。
    /// </summary>
    /// <param name="message">通知消息内容。</param>
    /// <param name="options">通知配置选项。</param>
    Task ShowSuccessAsync(
        string message,
        NotificationOptions? options = null);


    /// <summary>
    /// 显示警告通知。
    /// </summary>
    /// <param name="message">通知消息内容。</param>
    /// <param name="options">通知配置选项。</param>
    Task ShowWarningAsync(
        string message,
        NotificationOptions? options = null);


    /// <summary>
    /// 显示错误通知。
    /// </summary>
    /// <param name="message">通知消息内容。</param>
    /// <param name="options">通知配置选项。</param>
    Task ShowErrorAsync(
        string message,
        NotificationOptions? options = null);
}