using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Kokkoro.Core.UI.Dialogs;

/// <summary>
/// 弹窗配置选项
/// </summary>
public interface IDialogOptions //: IViewContent
{
    /// <summary>
    /// 窗口标题
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// 窗口图标
    /// </summary>
    WindowIcon? Icon { get; set; }

    /// <summary>
    /// 窗口宽度
    /// </summary>
    double? Width { get; set; }

    /// <summary>
    /// 窗口高度
    /// </summary>
    double? Height { get; set; }

    /// <summary>
    /// 窗口最小宽度
    /// </summary>
    double MinWidth { get; set; }

    /// <summary>
    /// 窗口最小高度
    /// </summary>
    double MinHeight { get; set; }

    /// <summary>
    /// 窗口最大宽度
    /// </summary>
    double? MaxWidth { get; set; }

    /// <summary>
    /// 窗口最大高度
    /// </summary>

    double? MaxHeight { get; set; }

    ///// <summary>
    ///// 窗口尺寸模式
    ///// </summary>
    //SizeToContent SizeToContent { get; set; }

    /// <summary>
    /// 尺寸模式
    /// </summary>
    DialogSizeMode SizeMode { get; set; }

    /// <summary>
    /// 是否允许通过拖拽窗口边框来调整窗口大小
    /// </summary>
    bool CanResize { get; set; }

    /// <summary>
    /// 是否启用最小化按钮
    /// </summary>
    bool CanMinimize { get; set; }

    /// <summary>
    /// 是否启用最大化按钮
    /// </summary>
    bool CanMaximize { get; set; }

    /// <summary>
    /// 窗口的当前状态
    /// </summary>
    WindowState WindowState { get; set; }

    /// <summary>
    /// 是否让窗口始终置顶（保持在所有其他窗口之上）
    /// </summary>
    bool Topmost { get; set; }

    /// <summary>
    /// 是否在操作系统的任务栏中显示该窗口
    /// </summary>
    bool ShowInTaskbar { get; set; }

    /// <summary>
    /// 设置窗口首次显示时的定位方式
    /// </summary>
    WindowStartupLocation StartupLocation { get; set; }

    /// <summary>
    /// 设置当 <see cref="StartupLocation"/> 为 <see cref="WindowStartupLocation.Manual"/> 时的精确位置（屏幕坐标）。
    /// 若为 null，则回退到 <see cref="WindowStartupLocation.CenterOwner"/>。
    /// </summary>
    PixelPoint? Position { get; set; }
    ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
    ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
    Func<KokkoroDialogWindow, int, Task<bool>>? BeforeButtonCloseAsync { get; set; }

    /// <summary>
    /// 底部命令按钮集合
    /// </summary>
    /// <remarks>
    /// 按钮索引即返回结果：
    ///
    /// 第一个按钮 -> 0
    /// 第二个按钮 -> 1
    /// 第三个按钮 -> 2
    /// ...
    ///
    /// ESC 或右上角关闭按钮 -> -1
    /// </remarks>
    IList<string> Commands { get; }

    /// <summary>
    /// 默认按钮索引
    /// </summary>
    /// <remarks>
    /// 当用户按 Enter 时触发对应按钮。
    /// 小于 0 表示没有默认按钮。
    /// </remarks>
    int DefaultButton { get; set; }
}

