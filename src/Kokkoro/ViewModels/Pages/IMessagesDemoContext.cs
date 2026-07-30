namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 消息演示区域可访问的最小上下文。
/// </summary>
public interface IMessagesDemoContext
{
    string CustomMessage { get; }

    string CustomTitle { get; }

    void SetLastResult(string result);
}
