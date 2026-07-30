using Avalonia.Controls.Notifications;
using Ursa.Controls;

namespace Kokkoro.Core.UI.Notifications;

/// <summary>
/// 窗口通知配置选项。
/// </summary>
public sealed class NotificationOptions
{
    /// <summary>
    /// 通知类型。
    /// </summary>
    public NotificationType Type { get; set; } = NotificationType.Information;


    /// <summary>
    /// 通知标题。
    /// </summary>
    public string? Title { get; set; }


    /// <summary>
    /// 自动关闭时长。
    /// 设为 <see cref="TimeSpan.Zero"/> 表示不自动关闭。
    /// </summary>
    public TimeSpan Expiration { get; set; } = TimeSpan.FromSeconds(5);


    /// <summary>
    /// 是否显示图标。
    /// </summary>
    public bool ShowIcon { get; set; } = true;


    /// <summary>
    /// 是否显示关闭按钮。
    /// </summary>
    public bool ShowClose { get; set; } = true;


    /// <summary>
    /// 通知点击回调。
    /// </summary>
    public Action? OnClick { get; set; }


    /// <summary>
    /// 通知关闭回调。
    /// </summary>
    public Action<MessageCloseReason>? OnClose { get; set; }


    /// <summary>
    /// 样式类名。
    /// </summary>
    public string[]? Classes { get; set; }


    /// <summary>
    /// 同屏最大通知数。
    /// </summary>
    public int? MaxItems { get; set; } = 5;


    /// <summary>
    /// 通知显示位置。
    /// </summary>
    public NotificationPosition? Position { get; set; }
        = NotificationPosition.BottomRight;
}