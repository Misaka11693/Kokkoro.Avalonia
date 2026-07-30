using Ursa.Controls;

namespace Kokkoro.Core.UI.Messages;

/// <summary>
/// Overlay 消息对话框配置选项。
/// </summary>
public class OverlayMessageDialogOptions
{
    /// <summary>
    /// 对话框标题。
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 对话框图标。
    /// </summary>
    public MessageBoxIcon Icon { get; set; } = MessageBoxIcon.None;

    /// <summary>
    /// 对话框按钮。
    /// </summary>
    public MessageBoxButton Button { get; set; } = MessageBoxButton.OK;

    /// <summary>
    /// Overlay 宿主 ID。
    /// </summary>
    public string? HostId { get; set; }

    /// <summary>
    /// TopLevel 的 HashCode（用于查找宿主）。
    /// </summary>
    public int? TopLevelHashCode { get; set; }

    /// <summary>
    /// 样式类名（空格分隔）。
    /// </summary>
    public string? StyleClass { get; set; }
}
