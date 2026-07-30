using Ursa.Controls;

namespace Kokkoro.Core.UI.Messages;

/// <summary>
/// 消息服务。
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// 显示标准对话框（完全可配置）。
    /// </summary>
    /// <param name="message">消息内容。</param>
    /// <param name="options">对话框配置选项。</param>
    /// <param name="owner">宿主窗口。</param>
    /// <returns>对话框结果。</returns>
    Task<MessageBoxResult> ShowStandardAsync(string message, MessageDialogOptions? options = null);

    /// <summary>
    /// 显示信息消息。
    /// </summary>
    Task ShowInformationAsync(string message, string? title = null);

    /// <summary>
    /// 显示成功消息。
    /// </summary>
    Task ShowSuccessAsync(string message, string? title = null);

    /// <summary>
    /// 显示警告消息。
    /// </summary>
    Task ShowWarningAsync(string message, string? title = null);

    /// <summary>
    /// 显示错误消息。
    /// </summary>
    Task ShowErrorAsync(string message, string? title = null);

    /// <summary>
    /// 显示异常信息。
    /// </summary>
    Task ShowExceptionAsync(Exception exception);

    /// <summary>
    /// 显示提问对话框。
    /// </summary>
    Task<bool> AskQuestionAsync(string question, string? title = null, MessageBoxButton button = MessageBoxButton.YesNo);

    /// <summary>
    /// 显示 Overlay 标准对话框（完全可配置）。
    /// </summary>
    /// <param name="message">消息内容。</param>
    /// <param name="options">Overlay 对话框配置选项。</param>
    /// <returns>对话框结果。</returns>
    Task<MessageBoxResult> ShowOverlayStandardAsync(string message, OverlayMessageDialogOptions? options = null);

    /// <summary>
    /// 显示 Overlay 信息消息。
    /// </summary>
    Task ShowOverlayInfoAsync(string message, string? title = null);

    /// <summary>
    /// 显示 Overlay 成功消息。
    /// </summary>
    Task ShowOverlaySuccessAsync(string message, string? title = null);

    /// <summary>
    /// 显示 Overlay 警告消息。
    /// </summary>
    Task ShowOverlayWarningAsync(string message, string? title = null);

    /// <summary>
    /// 显示 Overlay 错误消息。
    /// </summary>
    Task ShowOverlayErrorAsync(string message, string? title = null);

    /// <summary>
    /// 显示 Overlay 异常信息。
    /// </summary>
    Task ShowOverlayExceptionAsync(Exception exception);

    /// <summary>
    /// 显示 Overlay 提问对话框。
    /// </summary>
    Task<bool> AskOverlayQuestionAsync(string question, string? title = null, MessageBoxButton button = MessageBoxButton.YesNo);
}