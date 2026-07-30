using Avalonia.Controls;
using Ursa.Controls;

namespace Kokkoro.Core.UI.OverlayDialogs;

/// <summary>
/// OverlayDialog 服务。
/// 与窗口型 Dialog 是两套独立体系。
/// </summary>
public interface IOverlayDialogService
{
    /// <summary>
    /// 显示标准 OverlayDialog。
    /// 适合需要标题、图标、按钮栏的覆盖层弹窗。
    /// </summary>
    void ShowStandard(Control view, object? viewModel = null, OverlayDialogOptions? options = null);

    /// <summary>
    /// 异步显示标准 OverlayDialog，并等待按钮结果。
    /// </summary>
    Task<DialogResult> ShowStandardAsync(Control view, object? viewModel = null, OverlayDialogOptions? options = null, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 通过 ViewModel 解析视图并显示标准 OverlayDialog。
    /// </summary>
    void ShowStandard<TViewModel>(TViewModel? viewModel = null, OverlayDialogOptions? options = null) where TViewModel : class;

    /// <summary>
    /// 通过 ViewModel 解析视图并异步显示标准 OverlayDialog。
    /// </summary>
    Task<DialogResult> ShowStandardAsync<TViewModel>(TViewModel? viewModel = null, OverlayDialogOptions? options = null, CancellationToken? cancellationToken = null) where TViewModel : class;

    /// <summary>
    /// 显示自定义 OverlayDialog。
    /// 适合承载复杂表单或交互内容。
    /// </summary>
    void ShowCustom(Control view, object? viewModel = null, OverlayDialogOptions? options = null);

    /// <summary>
    /// 异步显示自定义 OverlayDialog，并等待自定义结果。
    /// </summary>
    Task<TResult?> ShowCustomAsync<TResult>(Control view, object? viewModel = null, OverlayDialogOptions? options = null, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 通过 ViewModel 解析视图并显示自定义 OverlayDialog。
    /// </summary>
    void ShowCustom<TViewModel>(TViewModel? viewModel = null, OverlayDialogOptions? options = null) where TViewModel : class;

    /// <summary>
    /// 通过 ViewModel 解析视图并异步显示自定义 OverlayDialog。
    /// </summary>
    Task<TResult?> ShowCustomAsync<TViewModel, TResult>(TViewModel? viewModel = null, OverlayDialogOptions? options = null, CancellationToken? cancellationToken = null) where TViewModel : class;
}
