using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.Messages;
using Kokkoro.Core.Workbench.Docking;
using ReactiveUI.SourceGenerators;
using Ursa.Controls;

namespace Kokkoro.Sample.ViewModels;

public partial class MessageServiceDemoViewModel : DocumentPage
{
    [Reactive]
    public partial string Message { get; set; } = "这是由 MessageService 显示的消息内容。";

    [Reactive]
    public partial string Title { get; set; } = "消息演示";

    [Reactive]
    public partial string LastResult { get; set; } = "尚未显示消息对话框。";

    [ReactiveCommand]
    private async Task ShowInformation()
    {
        await AppRuntime.MessageService.ShowInformationAsync(Message, Title);
        LastResult = "已关闭信息对话框。";
    }

    [ReactiveCommand]
    private async Task ShowSuccess()
    {
        await AppRuntime.MessageService.ShowSuccessAsync(Message, Title);
        LastResult = "已关闭成功对话框。";
    }

    [ReactiveCommand]
    private async Task ShowWarning()
    {
        await AppRuntime.MessageService.ShowWarningAsync(Message, Title);
        LastResult = "已关闭警告对话框。";
    }

    [ReactiveCommand]
    private async Task ShowError()
    {
        await AppRuntime.MessageService.ShowErrorAsync(Message, Title);
        LastResult = "已关闭错误对话框。";
    }

    [ReactiveCommand]
    private async Task AskQuestion()
    {
        var confirmed = await AppRuntime.MessageService.AskQuestionAsync(
            Message,
            Title,
            MessageBoxButton.YesNo);

        LastResult = confirmed ? "确认结果：是" : "确认结果：否或已关闭";
    }

    [ReactiveCommand]
    private async Task ShowOverlayInformation()
    {
        await AppRuntime.MessageService.ShowOverlayInfoAsync(Message, Title);
        LastResult = "已关闭 Overlay 信息对话框。";
    }

    [ReactiveCommand]
    private async Task ShowOverlaySuccess()
    {
        await AppRuntime.MessageService.ShowOverlaySuccessAsync(Message, Title);
        LastResult = "已关闭 Overlay 成功对话框。";
    }

    [ReactiveCommand]
    private async Task ShowOverlayWarning()
    {
        await AppRuntime.MessageService.ShowOverlayWarningAsync(Message, Title);
        LastResult = "已关闭 Overlay 警告对话框。";
    }

    [ReactiveCommand]
    private async Task ShowOverlayError()
    {
        await AppRuntime.MessageService.ShowOverlayErrorAsync(Message, Title);
        LastResult = "已关闭 Overlay 错误对话框。";
    }

    [ReactiveCommand]
    private async Task AskOverlayQuestion()
    {
        var confirmed = await AppRuntime.MessageService.AskOverlayQuestionAsync(
            Message,
            Title,
            MessageBoxButton.YesNo);

        LastResult = confirmed ? "Overlay 确认结果：是" : "Overlay 确认结果：否或已关闭";
    }
}
