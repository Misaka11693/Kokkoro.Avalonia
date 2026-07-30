namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 消息演示独立页面的公共基类。
/// </summary>
public abstract class MessagesDemoPageViewModelBase : DocumentPageViewModel
{
    protected MessagesDemoPageViewModelBase()
    {
        Editor = new MessagesEditorViewModel();
        Result = new MessagesResultViewModel();
        DemoContext = new MessagesDemoContext(Editor, Result);
    }

    public MessagesEditorViewModel Editor { get; }

    public MessagesResultViewModel Result { get; }

    protected IMessagesDemoContext DemoContext { get; }
}
