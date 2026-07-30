namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 连接公共输入区与结果区的演示上下文。
/// </summary>
public sealed class MessagesDemoContext : IMessagesDemoContext
{
    private readonly MessagesEditorViewModel _editor;
    private readonly MessagesResultViewModel _result;

    public MessagesDemoContext(MessagesEditorViewModel editor, MessagesResultViewModel result)
    {
        _editor = editor;
        _result = result;
    }

    public string CustomMessage => _editor.CustomMessage;

    public string CustomTitle => _editor.CustomTitle;

    public void SetLastResult(string result)
    {
        _result.SetLastResult(result);
    }
}
