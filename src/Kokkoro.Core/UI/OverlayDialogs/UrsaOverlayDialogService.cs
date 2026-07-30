using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Kokkoro.Core.Apps;
using Kokkoro.Core.Helpers;
using Kokkoro.Core.Threading;
using ReactiveUI;
using Ursa.Controls;

namespace Kokkoro.Core.UI.OverlayDialogs;

/// <summary>
/// 基于 Ursa OverlayDialog 的覆盖层弹窗服务。
/// 注意：它与窗口型 <see cref="Dialogs.IKokkoroDialogService"/> 是两套独立体系。
/// </summary>
public sealed class UrsaOverlayDialogService : IOverlayDialogService
{
    private readonly IMessageLoop _messageLoop;

    public UrsaOverlayDialogService(IMessageLoop messageLoop)
    {
        _messageLoop = messageLoop;
    }

    /// <inheritdoc />
    public void ShowStandard(Control view, object? viewModel = null, OverlayDialogOptions? options = null)
    {
        _messageLoop.InvokeIfRequired(() =>
        {
            OverlayDialog.ShowStandard(view, viewModel, null, CreateOptions(options));
        });
    }

    /// <inheritdoc />
    public Task<DialogResult> ShowStandardAsync(Control view, object? viewModel = null, OverlayDialogOptions? options = null, CancellationToken? cancellationToken = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(() =>
        {
            return OverlayDialog.ShowStandardAsync(view, viewModel, null, CreateOptions(options), cancellationToken);
        });
    }

    /// <inheritdoc />
    public void ShowStandard<TViewModel>(TViewModel? viewModel = null, OverlayDialogOptions? options = null)
        where TViewModel : class
    {
        var vm = viewModel ?? AppRuntime.Service.Resolve<TViewModel>();
        var view = ResolveView(vm);
        ShowStandard(view, vm, options);
    }

    /// <inheritdoc />
    public Task<DialogResult> ShowStandardAsync<TViewModel>(TViewModel? viewModel = null, OverlayDialogOptions? options = null, CancellationToken? cancellationToken = null)
        where TViewModel : class
    {

        var vm = viewModel ?? AppRuntime.Service.Resolve<TViewModel>();
        var view = ResolveView(vm);
        return ShowStandardAsync(view, vm, options, cancellationToken);
    }

    /// <inheritdoc />
    public void ShowCustom(Control view, object? viewModel = null, OverlayDialogOptions? options = null)
    {
        _messageLoop.InvokeIfRequired(() =>
        {
            OverlayDialog.ShowCustom(view, viewModel, null, CreateOptions(options));
        });
    }

    /// <inheritdoc />
    public Task<TResult?> ShowCustomAsync<TResult>(Control view, object? viewModel = null, OverlayDialogOptions? options = null, CancellationToken? cancellationToken = null)
    {
        return _messageLoop.InvokeIfRequiredAsync(() =>
        {
            return OverlayDialog.ShowCustomAsync<TResult>(view, viewModel, null, CreateOptions(options), cancellationToken);
        });
    }

    /// <inheritdoc />
    public void ShowCustom<TViewModel>(TViewModel? viewModel = null, OverlayDialogOptions? options = null)
        where TViewModel : class
    {
        var vm = viewModel ?? AppRuntime.Service.Resolve<TViewModel>();
        var view = ResolveView(vm);
        ShowCustom(view, vm, options);
    }

    /// <inheritdoc />
    public Task<TResult?> ShowCustomAsync<TViewModel, TResult>(TViewModel? viewModel = null, OverlayDialogOptions? options = null, CancellationToken? cancellationToken = null)
        where TViewModel : class
    {
        var vm = viewModel ?? AppRuntime.Service.Resolve<TViewModel>();
        var view = ResolveView(vm);
        return ShowCustomAsync<TResult>(view, vm, options, cancellationToken);
    }

    private static Control ResolveView<TViewModel>(TViewModel viewModel)
        where TViewModel : class
    {
        var view = AppRuntime.Service.Resolve<IViewFor<TViewModel>>();
        view.ViewModel = viewModel;

        if (view is not Control control)
        {
            throw new InvalidOperationException($"无法将 IViewFor<{typeof(TViewModel).Name}> 转换为 Control。");
        }

        return control;
    }

    private static OverlayDialogOptions CreateOptions(OverlayDialogOptions? options)
    {
        var effectiveOptions = new OverlayDialogOptions
        {
            FullScreen = options?.FullScreen ?? false,
            HorizontalAnchor = options?.HorizontalAnchor ?? HorizontalPosition.Center,
            VerticalAnchor = options?.VerticalAnchor ?? VerticalPosition.Center,
            HorizontalOffset = options?.HorizontalOffset,
            VerticalOffset = options?.VerticalOffset,
            Mode = options?.Mode ?? DialogMode.None,
            Buttons = options?.Buttons ?? DialogButton.OKCancel,
            Title = options?.Title,
            IsCloseButtonVisible = options?.IsCloseButtonVisible ?? true,
            CanLightDismiss = options?.CanLightDismiss ?? false,
            CanDragMove = options?.CanDragMove ?? true,
            TopLevelHashCode = WindowHelper.GetActiveWindowHashCode(),
            CanResize = options?.CanResize ?? false,
            StyleClass = options?.StyleClass,
            HorizontalScrollBarVisibility = options?.HorizontalScrollBarVisibility ?? ScrollBarVisibility.Auto,
            VerticalScrollBarVisibility = options?.VerticalScrollBarVisibility ?? ScrollBarVisibility.Auto
        };

        return effectiveOptions;
    }
}
