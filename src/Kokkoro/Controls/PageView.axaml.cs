using Avalonia;
using Avalonia.Animation;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Threading;
using System.Windows.Input;

namespace Kokkoro.Controls;

/// <summary>
/// 通用分页容器：ContentPresenter + 底栏统计 + Ursa Pagination。
/// </summary>
/// <remarks>
/// <para>见 <c>docs/PageView.md</c>。不管理 <c>SelectedItems</c>；<see cref="SelectedCount"/> 仅展示，由页面维护。</para>
/// </remarks>
public partial class PageView : ContentControl
{
    public static readonly StyledProperty<int> CurrentPageProperty =
        AvaloniaProperty.Register<PageView, int>(
            nameof(CurrentPage),
            1,
            defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    public static readonly StyledProperty<int> PageSizeProperty =
        AvaloniaProperty.Register<PageView, int>(
            nameof(PageSize),
            10,
            defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    public static readonly StyledProperty<int> TotalCountProperty =
        AvaloniaProperty.Register<PageView, int>(
            nameof(TotalCount),
            defaultBindingMode: Avalonia.Data.BindingMode.OneWay);

    public static readonly StyledProperty<int> SelectedCountProperty =
        AvaloniaProperty.Register<PageView, int>(
            nameof(SelectedCount),
            defaultBindingMode: Avalonia.Data.BindingMode.OneWay);

    public static readonly StyledProperty<bool> ShowSelectedCountProperty =
        AvaloniaProperty.Register<PageView, bool>(nameof(ShowSelectedCount));

    public static readonly StyledProperty<bool> ShowQuickJumperProperty =
        AvaloniaProperty.Register<PageView, bool>(nameof(ShowQuickJumper), true);

    public static readonly StyledProperty<bool> ShowPageSizeSelectorProperty =
        AvaloniaProperty.Register<PageView, bool>(nameof(ShowPageSizeSelector), true);

    public static readonly StyledProperty<bool> DisplayCurrentPageInQuickJumperProperty =
        AvaloniaProperty.Register<PageView, bool>(nameof(DisplayCurrentPageInQuickJumper), true);

    public static readonly StyledProperty<bool> IsPagingEnabledProperty =
        AvaloniaProperty.Register<PageView, bool>(
            nameof(IsPagingEnabled),
            true,
            defaultBindingMode: Avalonia.Data.BindingMode.OneWay);

    public static readonly StyledProperty<AvaloniaList<int>> PageSizeOptionsProperty =
        AvaloniaProperty.Register<PageView, AvaloniaList<int>>(nameof(PageSizeOptions));

    public static readonly StyledProperty<ICommand?> LoadPageCommandProperty =
        AvaloniaProperty.Register<PageView, ICommand?>(nameof(LoadPageCommand));

    public static readonly StyledProperty<object?> LoadPageCommandParameterProperty =
        AvaloniaProperty.Register<PageView, object?>(nameof(LoadPageCommandParameter));

    private bool _resettingPageForPageSize;

    static PageView()
    {
        PageSizeProperty.Changed.AddClassHandler<PageView>((view, e) => view.OnPageSizeChanged(e));
    }

    public PageView()
    {
        InitializeComponent();
        PageSizeOptions = [5, 10, 20, 50];
    }

    public int CurrentPage
    {
        get => GetValue(CurrentPageProperty);
        set => SetValue(CurrentPageProperty, value);
    }

    public int PageSize
    {
        get => GetValue(PageSizeProperty);
        set => SetValue(PageSizeProperty, value);
    }

    public int TotalCount
    {
        get => GetValue(TotalCountProperty);
        set => SetValue(TotalCountProperty, value);
    }

    public int SelectedCount
    {
        get => GetValue(SelectedCountProperty);
        set => SetValue(SelectedCountProperty, value);
    }

    public bool ShowSelectedCount
    {
        get => GetValue(ShowSelectedCountProperty);
        set => SetValue(ShowSelectedCountProperty, value);
    }

    public bool ShowQuickJumper
    {
        get => GetValue(ShowQuickJumperProperty);
        set => SetValue(ShowQuickJumperProperty, value);
    }

    public bool ShowPageSizeSelector
    {
        get => GetValue(ShowPageSizeSelectorProperty);
        set => SetValue(ShowPageSizeSelectorProperty, value);
    }

    public bool DisplayCurrentPageInQuickJumper
    {
        get => GetValue(DisplayCurrentPageInQuickJumperProperty);
        set => SetValue(DisplayCurrentPageInQuickJumperProperty, value);
    }

    public bool IsPagingEnabled
    {
        get => GetValue(IsPagingEnabledProperty);
        set => SetValue(IsPagingEnabledProperty, value);
    }

    public AvaloniaList<int> PageSizeOptions
    {
        get => GetValue(PageSizeOptionsProperty);
        set => SetValue(PageSizeOptionsProperty, value);
    }

    /// <summary>页码变更或 <see cref="PageSize"/> 变更回第一页后，由 Ursa Pagination 或控件内触发。</summary>
    public ICommand? LoadPageCommand
    {
        get => GetValue(LoadPageCommandProperty);
        set => SetValue(LoadPageCommandProperty, value);
    }

    /// <summary>传给 <see cref="LoadPageCommand"/> 的可选参数；不绑定时为 <c>null</c>。</summary>
    public object? LoadPageCommandParameter
    {
        get => GetValue(LoadPageCommandParameterProperty);
        set => SetValue(LoadPageCommandParameterProperty, value);
    }

    private void OnPageSizeChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (!IsInitialized || _resettingPageForPageSize || Equals(e.NewValue, e.OldValue))
        {
            return;
        }

        _resettingPageForPageSize = true;
        try
        {
            CurrentPage = 1;
            Dispatcher.UIThread.Post(InvokeLoadPageCommand, DispatcherPriority.Background);
        }
        finally
        {
            _resettingPageForPageSize = false;
        }
    }

    private void InvokeLoadPageCommand()
    {
        var parameter = LoadPageCommandParameter;
        if (LoadPageCommand?.CanExecute(parameter) == true)
        {
            LoadPageCommand.Execute(parameter);
        }
    }
}
