namespace Kokkoro.ViewModels.Pages;

internal static class MessagesViewModelHelper
{
    public static string? NullIfEmpty(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value;

    public static string[]? CreateStyleClasses(MessageFeedbackStyleMode mode)
        => mode == MessageFeedbackStyleMode.Light ? ["Light"] : null;

    public static Exception CreateDemoException()
    {
        return new InvalidOperationException(
            "这是一个模拟的异常，InnerException 内容在此。",
            new Exception("InnerException：底层原因示例"));
    }
}
