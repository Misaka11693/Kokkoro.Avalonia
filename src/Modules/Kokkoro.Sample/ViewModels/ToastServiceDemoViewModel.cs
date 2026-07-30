using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.Toasts;
using Kokkoro.Core.Workbench.Docking;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.Sample.ViewModels;

public partial class ToastServiceDemoViewModel : DocumentPage
{
    [Reactive]
    public partial string Message { get; set; } = "这是由 ToastService 显示的轻提示。";

    [Reactive]
    public partial int Duration { get; set; } = 3;

    [Reactive]
    public partial int MaxItems { get; set; } = 3;

    [Reactive]
    public partial bool ShowIcon { get; set; } = true;

    [Reactive]
    public partial bool ShowClose { get; set; } = true;

    [Reactive]
    public partial string SelectedStyle { get; set; } = "默认";

    public IReadOnlyList<string> Styles { get; } = ["默认", "浅色"];

    [Reactive]
    public partial string LastResult { get; set; } = "尚未显示轻提示。";

    [ReactiveCommand]
    private async Task ShowInformation()
    {
        await AppRuntime.ToastService.ShowInformationAsync(Message, CreateOptions());
        LastResult = "已显示信息轻提示。";
    }

    [ReactiveCommand]
    private async Task ShowSuccess()
    {
        await AppRuntime.ToastService.ShowSuccessAsync(Message, CreateOptions());
        LastResult = "已显示成功轻提示。";
    }

    [ReactiveCommand]
    private async Task ShowWarning()
    {
        await AppRuntime.ToastService.ShowWarningAsync(Message, CreateOptions());
        LastResult = "已显示警告轻提示。";
    }

    [ReactiveCommand]
    private async Task ShowError()
    {
        await AppRuntime.ToastService.ShowErrorAsync(Message, CreateOptions());
        LastResult = "已显示错误轻提示。";
    }

    [ReactiveCommand]
    private async Task CloseAll()
    {
        await AppRuntime.ToastService.CloseAllAsync();
        LastResult = "已关闭当前窗口的全部轻提示。";
    }

    private ToastOptions CreateOptions()
    {
        return new ToastOptions
        {
            Expiration = TimeSpan.FromSeconds(Duration),
            MaxItems = MaxItems,
            ShowIcon = ShowIcon,
            ShowClose = ShowClose,
            Classes = SelectedStyle == "浅色" ? ["Light"] : null
        };
    }
}
