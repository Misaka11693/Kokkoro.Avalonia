using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Kokkoro.Core.UI.Dialogs;

/// <summary>
/// Kokkoro 弹窗配置选项对象
/// </summary>
public class DialogOptions : IDialogOptions
{
    #region 基础信息

    /// <summary>
    /// 窗口标题
    /// </summary>
    /// <remarks>
    /// 默认值：null
    /// 原因：不同业务窗口标题必须显式指定，避免误用通用标题
    /// </remarks>
    public string? Title { get; set; }

    /// <summary>
    /// 窗口图标
    /// </summary>
    /// <remarks>
    /// 默认值：null
    /// 原因：允许继承主程序图标，避免重复资源加载
    /// </remarks>
    public WindowIcon? Icon { get; set; }

    #endregion

    #region 尺寸控制

    /// <summary>
    /// 屏幕比例（默认窗口大小）
    /// </summary>
    public double ScreenRatio { get; set; } = 0.3;

    /// <summary>
    /// 尺寸模式
    /// </summary>
    public DialogSizeMode SizeMode { get; set; } = DialogSizeMode.Default;

    /// <summary>
    /// 宽度
    /// </summary>
    /// <remarks>
    /// 默认值：null（自动）
    /// 原因：让 Avalonia Window 自适应或由 Content 决定
    /// </remarks>
    public double? Width { get; set; }

    /// <summary>
    /// 高度
    /// </summary>
    /// <remarks>
    /// 默认值：null（自动）
    /// 原因：避免强制固定窗口高度导致布局问题
    /// </remarks>
    public double? Height { get; set; }

    /// <summary>
    /// 最小宽度
    /// </summary>
    public double MinWidth { get; set; } = 0;

    /// <summary>
    /// 最小高度
    /// </summary>
    /// <remarks>
    /// 默认值：0
    /// 原因：允许极简弹窗（如提示框）不受限制
    /// </remarks>
    public double MinHeight { get; set; } = 0;

    public double? MaxWidth { get; set; } = double.PositiveInfinity;

    /// <summary>
    /// 最大高度
    /// </summary>
    /// <remarks>
    /// 默认值：∞（不限制）
    /// 原因：避免内容被裁剪（尤其 DataGrid / 表单）
    /// </remarks>
    public double? MaxHeight { get; set; } = double.PositiveInfinity;

    #endregion

    #region 行为控制


    /// <summary>
    /// 是否允许调整窗口大小
    /// </summary>
    /// <remarks>
    /// 默认值：true
    /// 原因：
    /// 1. 提升用户体验
    /// 2. 避免内容被截断
    /// 3. 仅确认类弹窗可关闭
    /// </remarks>
    public bool CanDragMove { get; set; } = true;

    /// <summary>
    /// 是否允许调整窗口大小
    /// </summary>
    /// <remarks>
    /// 默认值：true
    /// 原因：
    /// 1. 提升用户体验
    /// 2. 避免内容被截断
    /// 3. 仅确认类弹窗可关闭
    /// </remarks>
    public bool CanResize { get; set; } = true;

    /// <summary>
    /// 是否允许最小化
    /// </summary>
    /// <remarks>
    /// 默认值：false
    /// 原因：弹窗通常不需要最小化行为
    /// </remarks>
    public bool CanMinimize { get; set; } = true;

    /// <summary>
    /// 是否允许最大化
    /// </summary>
    /// <remarks>
    /// 默认值：false
    /// 原因：弹窗一般不进入沉浸式状态
    /// </remarks>
    public bool CanMaximize { get; set; } = true;

    /// <summary>
    /// 窗口状态
    /// </summary>
    /// <remarks>
    /// 默认值：Normal
    /// 原因：避免弹窗打开即最大化或最小化
    /// </remarks>
    public WindowState WindowState { get; set; } = WindowState.Normal;

    /// <summary>
    /// 是否置顶
    /// </summary>
    /// <remarks>
    /// 默认值：false
    /// 原因：避免干扰用户正常多窗口操作
    /// </remarks>
    public bool Topmost { get; set; } = false;

    /// <summary>
    /// 是否显示任务栏
    /// </summary>
    /// <remarks>
    /// 默认值：true
    /// 原因：标准窗口行为，方便 Alt+Tab 切换
    /// </remarks>
    public bool ShowInTaskbar { get; set; } = true;

    #endregion

    #region 位置控制

    /// <summary>
    /// 窗口启动位置
    /// </summary>
    /// <remarks>
    /// 默认值：CenterOwner
    /// 原因：弹窗最符合用户认知的位置策略
    /// </remarks>
    public WindowStartupLocation WindowStartupLocation { get; set; } = WindowStartupLocation.CenterOwner;

    /// <summary>
    /// 手动位置
    /// </summary>
    /// <remarks>
    /// 默认值：null
    /// 原因：仅在 Manual 模式下生效
    /// </remarks>
    public PixelPoint? Position { get; set; }

    #endregion

    public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; } = ScrollBarVisibility.Auto;
    public ScrollBarVisibility VerticalScrollBarVisibility { get; set; } = ScrollBarVisibility.Auto;
    public WindowStartupLocation StartupLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Func<KokkoroDialogWindow, int, Task<bool>>? BeforeButtonCloseAsync { get; set; }
    public IList<string> Commands { get; } =["取消","确认"];
    public int DefaultButton { get; set; } = 1;
}
