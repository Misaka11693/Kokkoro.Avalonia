using Kokkoro.Core.ViewModels;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.Sample.ViewModels;

public partial class DialogServiceDemoContentViewModel : ViewModelBase
{
    [Reactive]
    public partial string Name { get; set; } = "发布通知";

    [Reactive]
    public partial string Description { get; set; } = "将在项目更新完成后发送给相关成员。";

    [Reactive]
    public partial bool IsEnabled { get; set; } = true;
}
