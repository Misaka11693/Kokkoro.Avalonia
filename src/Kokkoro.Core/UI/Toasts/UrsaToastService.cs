using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Kokkoro.Core.Helpers;
using Kokkoro.Core.Threading;
using Ursa.Controls;
using ToastModel = Ursa.Controls.Toast;

namespace Kokkoro.Core.UI.Toasts;

/// <summary>
/// 基于 Ursa <see cref="WindowToastManager"/> 的 Toast 服务实现。
/// </summary>
internal sealed class UrsaToastService : IToastService
{
    private readonly IMessageLoop _messageLoop;

    public UrsaToastService(IMessageLoop messageLoop)
    {
        _messageLoop = messageLoop;
    }

    /// <inheritdoc />
    public Task ShowAsync(string message, ToastOptions? options = null, Window? owner = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(() =>
        {
            var resolvedOptions = CreateOptions(options, NotificationType.Information);
            var topLevel = GetHostTopLevel(owner);
            if (topLevel is null)
            {
                return Task.CompletedTask;
            }

            var manager = WindowToastManager.TryGetToastManager(topLevel, out var existingManager)
                ? existingManager
                : new WindowToastManager(topLevel);

            if (resolvedOptions.MaxItems.HasValue)
            {
                manager!.MaxItems = resolvedOptions.MaxItems.Value;
            }

            manager!.Show(
                new ToastModel(message),
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
    public Task ShowInformationAsync(string message, ToastOptions? options = null, Window? owner = null)
    {
        return ShowAsync(message, CreateOptions(options, NotificationType.Information, true), owner);
    }

    /// <inheritdoc />
    public Task ShowSuccessAsync(string message, ToastOptions? options = null, Window? owner = null)
    {
        return ShowAsync(message, CreateOptions(options, NotificationType.Success, true), owner);
    }

    /// <inheritdoc />
    public Task ShowWarningAsync(string message, ToastOptions? options = null, Window? owner = null)
    {
        return ShowAsync(message, CreateOptions(options, NotificationType.Warning, true), owner);
    }

    /// <inheritdoc />
    public Task ShowErrorAsync(string message, ToastOptions? options = null, Window? owner = null)
    {
        return ShowAsync(message, CreateOptions(options, NotificationType.Error, true), owner);
    }

    /// <inheritdoc />
    public Task CloseAllAsync(Window? owner = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(() =>
        {
            var topLevel = GetHostTopLevel(owner);
            if (topLevel is null)
            {
                return Task.CompletedTask;
            }

            if (WindowToastManager.TryGetToastManager(topLevel, out var manager))
            {
                manager!.CloseAll();
            }

            return Task.CompletedTask;
        });
    }

    private static ToastOptions CreateOptions(
        ToastOptions? userOptions,
        NotificationType type,
        bool preferPresetType = false)
    {
        var resolvedType = userOptions?.Type ?? NotificationType.Information;
        if (preferPresetType && resolvedType == NotificationType.Information)
        {
            resolvedType = type;
        }

        return new ToastOptions
        {
            Type = resolvedType,
            Expiration = userOptions?.Expiration ?? TimeSpan.FromSeconds(3),
            ShowIcon = userOptions?.ShowIcon ?? true,
            ShowClose = userOptions?.ShowClose ?? true,
            OnClick = userOptions?.OnClick,
            OnClose = userOptions?.OnClose,
            Classes = userOptions?.Classes,
            MaxItems = userOptions?.MaxItems
        };
    }

    private static TopLevel? GetHostTopLevel(Window? owner)
    {
        owner ??= GetMainWindow();
        return owner is null ? null : TopLevel.GetTopLevel(owner);
    }

    private static Window? GetMainWindow()
    {
        //var lifetime = Application.Current?.ApplicationLifetime;
        //return lifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: { } window } ? window : null;
        return WindowHelper.GetActiveWindow();
    }
}
