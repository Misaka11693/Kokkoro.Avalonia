using Irihi.Avalonia.Shared.Contracts;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 自定义 OverlayDialog 内容区 ViewModel。
/// </summary>
public partial class OverlayDialogCustomDemoViewModel : ViewModelBase, IDialogContext
{
    [Reactive]
    public partial string Header { get; set; } = "自定义 OverlayDialog";

    [Reactive]
    public partial string Message { get; set; } = "这是一个用于演示的自定义 OverlayDialog。";

    [Reactive]
    public partial string InputText { get; set; } = "可以继续编辑这里的内容。";

    public event EventHandler<object?>? RequestClose;

    public void Close()
    {
        RequestClose?.Invoke(this, null);
    }

    [ReactiveCommand]
    private void Confirm()
    {
        RequestClose?.Invoke(this, true);
    }

    [ReactiveCommand]
    private void Cancel()
    {
        RequestClose?.Invoke(this, false);
    }
}
