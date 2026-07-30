using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 消息演示结果区 ViewModel。
/// </summary>
public partial class MessagesResultViewModel : ViewModelBase
{
    [Reactive]
    public partial string LastResult { get; set; } = "（暂无结果）";

    public void SetLastResult(string result)
    {
        LastResult = result;
    }
}
