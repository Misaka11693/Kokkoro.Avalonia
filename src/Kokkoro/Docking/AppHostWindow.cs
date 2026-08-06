using Avalonia.Controls.Chrome;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Dock.Avalonia.Controls;
using Ursa.Controls;

namespace Kokkoro.Docking;

/// <summary>
/// 应用程序主窗口，继承自 <see cref="HostWindow"/>。
/// </summary>
public class AppHostWindow : HostWindow
{
    /// <summary>
    /// 用于标识 UrsaWindow 自定义绘制装饰的键。
    /// </summary>
    public const string KEY_KOKKORO_URSAWINDOW_DRAWN_DECORATIONS = "KEY_KOKKORO_URSAWINDOW_DRAWN_DECORATIONS";

    /// <summary>
    /// 控件模板中对话框宿主部件的名称。
    /// </summary>
    public const string PART_DialogHost = "PART_AppHost_DialogHost";

    /// <summary>
    /// 定义全屏按钮的可见性。
    /// </summary>
    public static readonly StyledProperty<bool> IsFullScreenButtonVisibleProperty = AvaloniaProperty.Register<AppHostWindow, bool>(nameof(IsFullScreenButtonVisible));

    /// <summary>
    /// 定义最小化按钮的可见性。
    /// </summary>
    public static readonly StyledProperty<bool> IsMinimizeButtonVisibleProperty = AvaloniaProperty.Register<AppHostWindow, bool>(nameof(IsMinimizeButtonVisible), true);

    /// <summary>
    /// 定义还原按钮的可见性。
    /// </summary>
    public static readonly StyledProperty<bool> IsRestoreButtonVisibleProperty = AvaloniaProperty.Register<AppHostWindow, bool>(nameof(IsRestoreButtonVisible), true);

    /// <summary>
    /// 定义关闭按钮的可见性。
    /// </summary>
    public static readonly StyledProperty<bool> IsCloseButtonVisibleProperty = AvaloniaProperty.Register<AppHostWindow, bool>(nameof(IsCloseButtonVisible), true);

    /// <summary>
    /// 定义标题栏的可见性。
    /// </summary>
    public static readonly StyledProperty<bool> IsTitleBarVisibleProperty = AvaloniaProperty.Register<AppHostWindow, bool>(nameof(IsTitleBarVisible), true);

    /// <summary>
    /// 定义托管大小调整器的可见性。
    /// </summary>
    public static readonly StyledProperty<bool> IsManagedResizerVisibleProperty = AvaloniaProperty.Register<AppHostWindow, bool>(nameof(IsManagedResizerVisible));

    /// <summary>
    /// 定义标题栏的内容。
    /// </summary>
    public static readonly StyledProperty<object?> TitleBarContentProperty = AvaloniaProperty.Register<AppHostWindow, object?>(nameof(TitleBarContent));

    /// <summary>
    /// 定义窗口左侧的内容。
    /// </summary>
    public static readonly StyledProperty<object?> LeftContentProperty = AvaloniaProperty.Register<AppHostWindow, object?>(nameof(LeftContent));

    /// <summary>
    /// 定义窗口右侧的内容。
    /// </summary>
    public static readonly StyledProperty<object?> RightContentProperty = AvaloniaProperty.Register<AppHostWindow, object?>(nameof(RightContent));

    /// <summary>
    /// 定义标题栏的外边距。
    /// </summary>
    public static readonly StyledProperty<Thickness> TitleBarMarginProperty =AvaloniaProperty.Register<AppHostWindow, Thickness>(nameof(TitleBarMargin));

    private bool _canClose;

    /// <summary>
    /// 获取控件的样式键重写。
    /// </summary>
    protected override Type StyleKeyOverride => typeof(AppHostWindow);

    /// <summary>
    /// 获取或设置一个值，该值指示全屏按钮是否可见。
    /// </summary>
    public bool IsFullScreenButtonVisible
    {
        get => GetValue(IsFullScreenButtonVisibleProperty);
        set => SetValue(IsFullScreenButtonVisibleProperty, value);
    }

    /// <summary>
    /// 获取或设置一个值，该值指示最小化按钮是否可见。
    /// </summary>
    public bool IsMinimizeButtonVisible
    {
        get => GetValue(IsMinimizeButtonVisibleProperty);
        set => SetValue(IsMinimizeButtonVisibleProperty, value);
    }

    /// <summary>
    /// 获取或设置一个值，该值指示还原按钮是否可见。
    /// </summary>
    public bool IsRestoreButtonVisible
    {
        get => GetValue(IsRestoreButtonVisibleProperty);
        set => SetValue(IsRestoreButtonVisibleProperty, value);
    }

    /// <summary>
    /// 获取或设置一个值，该值指示关闭按钮是否可见。
    /// </summary>
    public bool IsCloseButtonVisible
    {
        get => GetValue(IsCloseButtonVisibleProperty);
        set => SetValue(IsCloseButtonVisibleProperty, value);
    }

    /// <summary>
    /// 获取或设置一个值，该值指示标题栏是否可见。
    /// </summary>
    public bool IsTitleBarVisible
    {
        get => GetValue(IsTitleBarVisibleProperty);
        set => SetValue(IsTitleBarVisibleProperty, value);
    }

    /// <summary>
    /// 获取或设置一个值，该值指示托管大小调整器是否可见。
    /// </summary>
    public bool IsManagedResizerVisible
    {
        get => GetValue(IsManagedResizerVisibleProperty);
        set => SetValue(IsManagedResizerVisibleProperty, value);
    }

    /// <summary>
    /// 获取或设置标题栏的内容。
    /// </summary>
    public object? TitleBarContent
    {
        get => GetValue(TitleBarContentProperty);
        set => SetValue(TitleBarContentProperty, value);
    }

    /// <summary>
    /// 获取或设置窗口左侧的内容。
    /// </summary>
    public object? LeftContent
    {
        get => GetValue(LeftContentProperty);
        set => SetValue(LeftContentProperty, value);
    }

    /// <summary>
    /// 获取或设置窗口右侧的内容。
    /// </summary>
    public object? RightContent
    {
        get => GetValue(RightContentProperty);
        set => SetValue(RightContentProperty, value);
    }

    /// <summary>
    /// 获取或设置标题栏的外边距。
    /// </summary>
    public Thickness TitleBarMargin
    {
        get => GetValue(TitleBarMarginProperty);
        set => SetValue(TitleBarMarginProperty, value);
    }

    private TitleBar? _titleBar;
    private OverlayDialogHost? _dialogHost;

    /// <inheritdoc/>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _dialogHost = e.NameScope.Find<OverlayDialogHost>(PART_DialogHost);
        if (_dialogHost is not null) LogicalChildren.Add(_dialogHost);
        _titleBar = e.NameScope.Find<TitleBar>("PART_TitleBar");
    }

    /// <inheritdoc/>
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        // 获取自定义窗口装饰
        var decorations = this.GetLogicalDescendants().OfType<WindowDrawnDecorations>().FirstOrDefault();
        var buttons = decorations?.GetLogicalDescendants().OfType<Button>().ToList();
        Button? maxX = null;
        if (buttons is { Count: > 0 })
        {
            maxX = buttons.MaxBy(a => a.Bounds.X);
        }
        var marginRight = maxX?.Bounds.Right;
        var height = decorations?.TitleBarHeight;
        if (marginRight is not null && _titleBar is not null)
        {
            // 调整标题栏的右边距，避免与窗口按钮重叠
            _titleBar.Margin = new Thickness(_titleBar.Margin.Left, _titleBar.Margin.Top, marginRight.Value,
                _titleBar.Margin.Bottom);
            _titleBar.MinHeight = height ?? 0;
        }
    }

    /// <summary>
    /// 确定窗口是否可以关闭。
    /// </summary>
    /// <returns>一个任务，其结果为 <c>true</c> 表示可以关闭；否则为 <c>false</c>。</returns>
    protected virtual async Task<bool> CanClose()
    {
        return await Task.FromResult(true);
    }

    /// <summary>
    /// 处理窗口关闭事件，并决定窗口是否应关闭。
    /// </summary>
    /// <param name="e">关闭事件的事件参数。</param>
    protected override async void OnClosing(WindowClosingEventArgs e)
    {
        VerifyAccess();
        if (!_canClose)
        {
            e.Cancel = true;
            _canClose = await CanClose();
            if (_canClose)
            {
                Close();
                return;
            }
        }
        base.OnClosing(e);
    }
}