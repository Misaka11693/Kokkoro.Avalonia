using Irihi.Avalonia.Shared.Contracts;
using Kokkoro.Core.ViewModels;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.Sample.ViewModels;

public partial class OverlayDialogServiceDemoContentViewModel : ViewModelBase, IDialogContext
{
    [Reactive]
    public partial string Name { get; set; } = "发布通知";

    [Reactive]
    public partial string Description { get; set; } = "将在项目更新完成后发送给相关成员。";

    [Reactive]
    public partial bool IsEnabled { get; set; } = true;

    public event EventHandler<object?>? RequestClose;

    public void Close()
    {
        RequestClose?.Invoke(this, null);
    }

    [ReactiveCommand]
    private void Confirm()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            return;
        }

        RequestClose?.Invoke(this, Name.Trim());
    }

    [ReactiveCommand]
    private void Cancel()
    {
        RequestClose?.Invoke(this, null);
    }
}
