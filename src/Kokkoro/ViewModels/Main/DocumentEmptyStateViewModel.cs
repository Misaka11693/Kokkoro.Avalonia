namespace Kokkoro.ViewModels.Main;

/// <summary>
/// Placeholder content displayed when the main document host has no open documents.
/// </summary>
public sealed class DocumentEmptyStateViewModel
{
    public string Title { get; } = "暂无打开的文档";

    public string Description { get; } = "可使用左侧菜单打开页面，或将文档重新停靠回工作区。";
}
