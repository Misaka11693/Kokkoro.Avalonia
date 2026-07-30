using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Kokkoro.Core.Helpers;
using Kokkoro.Core.Threading;
using Notification = Ursa.Controls.Notification;
using WindowNotificationManager = Ursa.Controls.WindowNotificationManager;

namespace Kokkoro.Core.UI.Notifications;

/// <summary>
/// 基于 Ursa WindowNotificationManager 的通知服务实现。
/// </summary>
internal sealed class UrsaNotificationService : INotificationService
{
    private readonly IMessageLoop _messageLoop;

    public UrsaNotificationService(IMessageLoop messageLoop)
    {
        _messageLoop = messageLoop;
    }


    /// <inheritdoc />
    public Task ShowAsync(
        string message,
        NotificationOptions? options = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(() =>
        {
            var resolvedOptions = options ?? new NotificationOptions();

            var window = WindowHelper.GetActiveWindow();
            if (window is null)
            {
                return Task.CompletedTask;
            }

            var topLevel = TopLevel.GetTopLevel(window);
            if (topLevel is null)
            {
                return Task.CompletedTask;
            }


            var manager =
                WindowNotificationManager.TryGetNotificationManager(
                    topLevel,
                    out var existingManager)
                    ? existingManager
                    : new WindowNotificationManager(topLevel);


            if (resolvedOptions.MaxItems.HasValue)
            {
                manager!.MaxItems = resolvedOptions.MaxItems.Value;
            }

            if (resolvedOptions.Position.HasValue)
            {
                manager!.Position = resolvedOptions.Position.Value;
            }


            manager!.Show(
                new Notification(
                    resolvedOptions.Title ?? "通知",
                    message,
                    resolvedOptions.Type,
                    resolvedOptions.Expiration,
                    resolvedOptions.ShowClose,
                    resolvedOptions.OnClick,
                    resolvedOptions.OnClose)
                {
                    ShowIcon = resolvedOptions.ShowIcon
                },
                type: resolvedOptions.Type,
                expiration: resolvedOptions.Expiration,
                showIcon: resolvedOptions.ShowIcon,
                showClose: resolvedOptions.ShowClose,
                onClick: resolvedOptions.OnClick,
                onClose: resolvedOptions.OnClose,
                classes: resolvedOptions.Classes);


            return Task.CompletedTask;
        });
    }


    /// <inheritdoc />
    public Task ShowInformationAsync(
        string message,
        NotificationOptions? options = null)
    {
        return ShowAsync(
            message,
            CreateOptions(options, NotificationType.Information));
    }


    /// <inheritdoc />
    public Task ShowSuccessAsync(
        string message,
        NotificationOptions? options = null)
    {
        return ShowAsync(
            message,
            CreateOptions(options, NotificationType.Success));
    }


    /// <inheritdoc />
    public Task ShowWarningAsync(
        string message,
        NotificationOptions? options = null)
    {
        return ShowAsync(
            message,
            CreateOptions(options, NotificationType.Warning));
    }


    /// <inheritdoc />
    public Task ShowErrorAsync(
        string message,
        NotificationOptions? options = null)
    {
        return ShowAsync(
            message,
            CreateOptions(options, NotificationType.Error));
    }


    /// <summary>
    /// 创建通知配置。
    /// 保留用户其它配置，只覆盖通知类型。
    /// </summary>
    private static NotificationOptions CreateOptions(
        NotificationOptions? options,
        NotificationType type)
    {
        return new NotificationOptions
        {
            Type = type,
            Title = options?.Title,
            Expiration = options?.Expiration ?? TimeSpan.FromSeconds(5),
            ShowIcon = options?.ShowIcon ?? true,
            ShowClose = options?.ShowClose ?? true,
            OnClick = options?.OnClick,
            OnClose = options?.OnClose,
            Classes = options?.Classes,
            MaxItems = options?.MaxItems,
            Position = options?.Position
        };
    }
}