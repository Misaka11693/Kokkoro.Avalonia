using Kokkoro.ViewModels.Core;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 标准 OverlayDialog 内容区 ViewModel。
/// </summary>
public sealed class OverlayDialogStandardDemoViewModel : ViewModelBase
{
    public string Message { get; set; } = "这是一条来自标准 OverlayDialog 的演示消息。";

    public string Description { get; set; } = "标准 OverlayDialog 会使用 Ursa 自带的标题栏、图标和按钮区。";
}
