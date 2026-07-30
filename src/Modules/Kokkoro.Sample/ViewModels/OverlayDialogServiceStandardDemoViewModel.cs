using Kokkoro.Core.ViewModels;

namespace Kokkoro.Sample.ViewModels;

public class OverlayDialogServiceStandardDemoViewModel : ViewModelBase
{
    public string Message { get; } = "这是由 OverlayDialogService.ShowStandardAsync 打开的标准覆盖层对话框。";
}
