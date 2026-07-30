using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.Dialogs;
using Kokkoro.Core.Workbench.Docking;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.Sample.ViewModels;

public partial class DialogServiceDemoViewModel : DocumentPage
{
    [Reactive]
    public partial string LastResult { get; set; } = "尚未打开对话框。";

    [ReactiveCommand]
    private async Task ShowDialog()
    {
        var content = new DialogServiceDemoContentViewModel();
        var result = await AppRuntime.DialogService.ShowKokkoroDialogAsync(
            content,
            configureOptions: options =>
            {
                options.Title = "编辑通知模板";
                options.Commands.Clear();
                options.Commands.Add("取消");
                options.Commands.Add("确认");
                options.DefaultButton = 1;
            });

        LastResult = result switch
        {
            1 => $"已确认：{content.Name}",
            0 => "已取消编辑。",
            _ => "对话框已关闭。"
        };
    }
}
