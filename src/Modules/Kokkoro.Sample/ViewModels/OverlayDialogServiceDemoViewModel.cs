using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.OverlayDialogs;
using Kokkoro.Core.Workbench.Docking;
using ReactiveUI.SourceGenerators;
using Ursa.Controls;

namespace Kokkoro.Sample.ViewModels;

public partial class OverlayDialogServiceDemoViewModel : DocumentPage
{
    [Reactive]
    public partial string LastResult { get; set; } = "尚未打开 Overlay 对话框。";

    [ReactiveCommand]
    private async Task ShowStandardDialog()
    {
        var result = await AppRuntime.OverlayDialogService.ShowStandardAsync(
            new OverlayDialogServiceStandardDemoViewModel(),
            new OverlayDialogOptions
            {
                Title = "操作提示",
                Mode = DialogMode.Info,
                Buttons = DialogButton.OKCancel,
                CanLightDismiss = false
            });

        LastResult = result switch
        {
            DialogResult.OK => "标准 Overlay 对话框结果：已确认。",
            DialogResult.Cancel => "标准 Overlay 对话框结果：已取消。",
            _ => "标准 Overlay 对话框已关闭。"
        };
    }

    [ReactiveCommand]
    private async Task ShowDialog()
    {
        var content = new OverlayDialogServiceDemoContentViewModel();
        var result = await AppRuntime.OverlayDialogService.ShowCustomAsync<OverlayDialogServiceDemoContentViewModel, string>(
            content,
            new OverlayDialogOptions
            {
                Title = "编辑通知模板",
                Buttons = DialogButton.None,
                CanLightDismiss = false,
                CanDragMove = true,
                IsCloseButtonVisible = true
            });

        LastResult = result is null
            ? "已取消编辑或关闭对话框。"
            : $"已确认：{result}";
    }
}
