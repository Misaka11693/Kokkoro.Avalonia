using Avalonia.Controls.Notifications;
using Ursa.Controls;

namespace Kokkoro.Core.UI.Toasts;

/// <summary>
/// Toast 配置选项。
/// </summary>
public sealed class ToastOptions
{
    /// <summary>
    /// Toast 类型。
    /// </summary>
    public NotificationType Type { get; set; } = NotificationType.Information;

    /// <summary>
    /// 自动关闭时长。
    /// 设为 <see cref="TimeSpan.Zero"/> 表示不自动关闭。
    /// </summary>
    public TimeSpan Expiration { get; set; } = TimeSpan.FromSeconds(3);

    /// <summary>
    /// 是否显示图标。
    /// </summary>
    public bool ShowIcon { get; set; } = true;

    /// <summary>
    /// 是否显示关闭按钮。
    /// </summary>
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// Toast 点击回调。
    /// </summary>
    public Action? OnClick { get; set; }

    /// <summary>
    /// Toast 关闭回调。
    /// </summary>
    public Action<MessageCloseReason>? OnClose { get; set; }

    /// <summary>
    /// 追加样式类。
    /// </summary>
    public string[]? Classes { get; set; }

    /// <summary>
    /// 当前窗口允许同时显示的最大 Toast 数量。
    /// </summary>
    public int? MaxItems { get; set; }
}
