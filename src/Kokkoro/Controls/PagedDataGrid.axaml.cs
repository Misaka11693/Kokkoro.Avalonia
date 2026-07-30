using System.Collections;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;

namespace Kokkoro.Controls;

/// <summary>
/// 分页数据表格控件：DataGrid + 底栏统计 + Ursa Pagination。
/// </summary>
/// <remarks>
/// <para>绑定约定：</para>
/// <list type="bullet">
///   <item><description><see cref="ItemsSource"/>：当前页行数据，由 ViewModel 提供。</description></item>
///   <item><description><see cref="CurrentPage"/>、<see cref="PageSize"/>：建议 TwoWay 绑定到 ViewModel；改 <see cref="PageSize"/> 时控件自动将 <see cref="CurrentPage"/> 置为 1。</description></item>
///   <item><description><see cref="TotalCount"/>：筛选后的总条数，由 ViewModel 提供。</description></item>
///   <item><description><see cref="SelectedItems"/>：建议 OneWayToSource 绑定到 ViewModel，同步当前选中的行数据。</description></item>
///   <item><description><see cref="SelectedCount"/>：控件内部统计，仅供底栏展示。</description></item>
/// </list>
/// </remarks>
public partial class PagedDataGrid : UserControl
{
    public static readonly StyledProperty<IEnumerable?> ItemsSourceProperty =
        AvaloniaProperty.Register<PagedDataGrid, IEnumerable?>(nameof(ItemsSource));

    public static readonly StyledProperty<int> CurrentPageProperty =
        AvaloniaProperty.Register<PagedDataGrid, int>(
            nameof(CurrentPage),
            1,
            defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    public static readonly StyledProperty<int> PageSizeProperty =
        AvaloniaProperty.Register<PagedDataGrid, int>(
            nameof(PageSize),
            10,
            defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    public static readonly StyledProperty<int> TotalCountProperty =
        AvaloniaProperty.Register<PagedDataGrid, int>(nameof(TotalCount));

    public static readonly StyledProperty<bool> IsPagingEnabledProperty =
        AvaloniaProperty.Register<PagedDataGrid, bool>(nameof(IsPagingEnabled), true);

    public static readonly StyledProperty<AvaloniaList<int>> PageSizeOptionsProperty =
        AvaloniaProperty.Register<PagedDataGrid, AvaloniaList<int>>(nameof(PageSizeOptions));

    public static readonly StyledProperty<IList?> SelectedItemsProperty =
        AvaloniaProperty.Register<PagedDataGrid, IList?>(
            nameof(SelectedItems),
            defaultBindingMode: Avalonia.Data.BindingMode.OneWayToSource);

    public static readonly DirectProperty<PagedDataGrid, int> SelectedCountProperty =
        AvaloniaProperty.RegisterDirect<PagedDataGrid, int>(
            nameof(SelectedCount),
            o => o.SelectedCount);

    private int _selectedCount;
    private bool _resettingPageForPageSize;
    /// <summary>
    /// 批量选中同步抑制标志（suppress selection sync）。
    /// 全选等会连续改动 <c>PART_DataGrid.SelectedItems</c> 时，每次 Add 都会触发
    /// <see cref="OnDataGridSelectionChanged"/>；置为 <c>true</c> 可跳过中间同步，
    /// 待批量操作结束后再统一调用一次 <see cref="SyncSelectionFromGrid"/>。
    /// </summary>
    private bool _suppressSelectionSync;

    static PagedDataGrid()
    {
        PageSizeProperty.Changed.AddClassHandler<PagedDataGrid>((grid, e) => grid.OnPageSizeChanged(e));
    }

    public PagedDataGrid()
    {
        InitializeComponent();
        PageSizeOptions = [5, 10, 20, 50];
        PART_DataGrid.SelectionChanged += OnDataGridSelectionChanged;
    }

    public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;

    public IList<DataGridColumn> Columns => PART_DataGrid.Columns;

    /// <summary>当前选中的行对象集合，建议 OneWayToSource 绑定到 ViewModel。</summary>
    public IList? SelectedItems
    {
        get => GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    public IEnumerable? ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
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

    public int SelectedCount
    {
        get => _selectedCount;
        private set => SetAndRaise(SelectedCountProperty, ref _selectedCount, value);
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
            if (CurrentPage != 1)
            {
                CurrentPage = 1;
            }
            else
            {
                SetValue(CurrentPageProperty, 0);
                SetValue(CurrentPageProperty, 1);
            }
        }
        finally
        {
            _resettingPageForPageSize = false;
        }
    }

    /// <summary>选中当前页 <see cref="ItemsSource"/> 中的全部行（不跨页）。</summary>
    public void SelectAllCurrentPage()
    {
        _suppressSelectionSync = true;
        try
        {
            PART_DataGrid.SelectedItems.Clear();

            if (ItemsSource is not null)
            {
                foreach (var item in ItemsSource)
                {
                    PART_DataGrid.SelectedItems.Add(item);
                }
            }
        }
        finally
        {
            _suppressSelectionSync = false;
            SyncSelectionFromGrid();
        }
    }

    private void OnDataGridSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_suppressSelectionSync)
        {
            return;
        }

        SyncSelectionFromGrid();
        SelectionChanged?.Invoke(this, e);
    }

    private void SyncSelectionFromGrid()
    {
        var selected = PART_DataGrid.SelectedItems;
        if (selected is null || selected.Count == 0)
        {
            SelectedCount = 0;
            SetValue(SelectedItemsProperty, new List<object>());
            return;
        }

        var snapshot = new List<object>(selected.Count);
        foreach (var item in selected)
        {
            snapshot.Add(item);
        }

        SelectedCount = snapshot.Count;
        SetValue(SelectedItemsProperty, snapshot);
    }
}
