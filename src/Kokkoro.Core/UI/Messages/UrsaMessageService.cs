using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Kokkoro.Core.Helpers;
using Kokkoro.Core.Threading;
using Kokkoro.Core.UI.Dialogs;
using Ursa.Controls;

namespace Kokkoro.Core.UI.Messages;

/// <summary>
/// 基于 Ursa 的消息服务实现。
/// </summary>
internal sealed class UrsaMessageService : IMessageService
{
    private readonly IMessageLoop _messageLoop;

    public UrsaMessageService(IMessageLoop messageLoop)
    {
        _messageLoop = messageLoop;
    }

    private static string DefaultTitle { get; } = "提示";
    private static string DefaultSuccessTitle { get; } = "成功";
    private static string DefaultErrorTitle { get; } = "错误";
    private static string DefaultWarningTitle { get; } = "警告";
    private static string DefaultQuestionTitle { get; } = "询问";

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowStandardAsync(string message, MessageDialogOptions? options = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            options ??= new MessageDialogOptions();
            return await ShowMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowInformationAsync(string message, string? title = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var options = new MessageDialogOptions { Title = title ?? DefaultTitle, Icon = MessageBoxIcon.Information, Button = MessageBoxButton.OK };
            return await ShowMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowSuccessAsync(string message, string? title = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var options = new MessageDialogOptions { Title = title ?? DefaultSuccessTitle, Icon = MessageBoxIcon.Success, Button = MessageBoxButton.OK };
            return await ShowMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowWarningAsync(string message, string? title = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var options = new MessageDialogOptions { Title = title ?? DefaultWarningTitle, Icon = MessageBoxIcon.Warning, Button = MessageBoxButton.OK };
            return await ShowMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowErrorAsync(string message, string? title = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var options = new MessageDialogOptions { Title = title ?? DefaultErrorTitle, Icon = MessageBoxIcon.Error, Button = MessageBoxButton.OK };
            return await ShowMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowExceptionAsync(Exception exception)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var message = exception.GetBaseException().Message;
            var options = new MessageDialogOptions { Title = DefaultErrorTitle, Icon = MessageBoxIcon.Error, Button = MessageBoxButton.OK };
            return await ShowMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task<bool> AskQuestionAsync(string question, string? title = null, MessageBoxButton button = MessageBoxButton.YesNo)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            if (button is not MessageBoxButton.YesNo and not MessageBoxButton.OKCancel)
            {
                throw new ArgumentException($"{nameof(AskQuestionAsync)} 仅支持 YesNo 和 OKCancel。", nameof(button));
            }

            var options = new MessageDialogOptions { Title = title ?? DefaultQuestionTitle, Icon = MessageBoxIcon.Question, Button = button };

            var result = await ShowMessageBoxCoreAsync(question, options);

            return button switch
            {
                MessageBoxButton.YesNo => result == MessageBoxResult.Yes,
                MessageBoxButton.OKCancel => result == MessageBoxResult.OK,
                _ => false
            };
        });
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowOverlayStandardAsync(string message, OverlayMessageDialogOptions? options = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            options ??= new OverlayMessageDialogOptions();
            return await ShowOverlayMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowOverlayInfoAsync(string message, string? title = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var options = new OverlayMessageDialogOptions { Title = title ?? DefaultTitle, Icon = MessageBoxIcon.Information, Button = MessageBoxButton.OK };
            return await ShowOverlayMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowOverlaySuccessAsync(string message, string? title = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var options = new OverlayMessageDialogOptions { Title = title ?? DefaultSuccessTitle, Icon = MessageBoxIcon.Success, Button = MessageBoxButton.OK };
            return await ShowOverlayMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowOverlayWarningAsync(string message, string? title = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var options = new OverlayMessageDialogOptions { Title = title ?? DefaultWarningTitle, Icon = MessageBoxIcon.Warning, Button = MessageBoxButton.OK };
            return await ShowOverlayMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowOverlayErrorAsync(string message, string? title = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var options = new OverlayMessageDialogOptions { Title = title ?? DefaultErrorTitle, Icon = MessageBoxIcon.Error, Button = MessageBoxButton.OK };
            return await ShowOverlayMessageBoxCoreAsync(message, options);
        });
    }

    /// <inheritdoc />
    public Task ShowOverlayExceptionAsync(Exception exception)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            var options = new OverlayMessageDialogOptions { Title = DefaultErrorTitle, Icon = MessageBoxIcon.Error, Button = MessageBoxButton.OK };
            return await ShowOverlayMessageBoxCoreAsync(exception.GetBaseException().Message, options);
        });
    }

    /// <inheritdoc />
    public Task<bool> AskOverlayQuestionAsync(string question, string? title = null, MessageBoxButton button = MessageBoxButton.YesNo)
    {
        return _messageLoop.InvokeIfRequiredAsync(async () =>
        {
            if (button is not MessageBoxButton.YesNo and not MessageBoxButton.OKCancel)
            {
                throw new ArgumentException($"{nameof(AskOverlayQuestionAsync)} 仅支持 YesNo 和 OKCancel。", nameof(button));
            }

            var options = new OverlayMessageDialogOptions { Title = title ?? DefaultQuestionTitle, Icon = MessageBoxIcon.Question, Button = button };

            var result = await ShowOverlayMessageBoxCoreAsync(question, options);

            return button switch
            {
                MessageBoxButton.YesNo => result == MessageBoxResult.Yes,
                MessageBoxButton.OKCancel => result == MessageBoxResult.OK,
                _ => false
            };
        });
    }

    private static Task<MessageBoxResult> ShowMessageBoxCoreAsync(string message, MessageDialogOptions options)
    {
        var owner = WindowHelper.GetActiveWindow()!;

        return MessageBox.ShowAsync(owner, message, options.Title, options.Icon, options.Button, options.StyleClass);
    }

    private static Task<MessageBoxResult> ShowOverlayMessageBoxCoreAsync(string message, OverlayMessageDialogOptions options)
    {
        if (options.TopLevelHashCode == null)
        {
            options.TopLevelHashCode = WindowHelper.GetActiveWindowHashCode();
        }

        return OverlayMessageBox.ShowAsync(message, options.Title, options.HostId, options.Icon, options.Button, options.TopLevelHashCode, options.StyleClass);
    }
}
