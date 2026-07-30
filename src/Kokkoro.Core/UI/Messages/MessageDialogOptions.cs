using Ursa.Controls;

namespace Kokkoro.Core.UI.Messages;

/// <summary>
/// 消息对话框配置选项。
/// </summary>
public class MessageDialogOptions
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
    /// 追加样式类。
    /// </summary>
    public string? StyleClass { get; set; }
}
