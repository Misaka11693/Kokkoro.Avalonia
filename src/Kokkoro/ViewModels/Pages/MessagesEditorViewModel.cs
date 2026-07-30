using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 消息演示公共输入区 ViewModel。
/// </summary>
public partial class MessagesEditorViewModel : ViewModelBase
{
    [Reactive]
    public partial string CustomMessage { get; set; } = "这是一条自定义消息内容。";

    [Reactive]
    public partial string CustomTitle { get; set; } = "自定义标题";
}
