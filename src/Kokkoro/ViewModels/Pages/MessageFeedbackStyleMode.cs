using System.ComponentModel;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 消息反馈样式模式。
/// </summary>
public enum MessageFeedbackStyleMode
{
    /// <summary>
    /// 默认样式。
    /// </summary>
    [Description("默认样式")]
    Default,

    /// <summary>
    /// 浅色样式。
    /// </summary>
    [Description("浅色样式")]
    Light
}
