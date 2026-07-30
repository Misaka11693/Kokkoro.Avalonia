using System.Collections.ObjectModel;
using Kokkoro.Admin.Models;
using Kokkoro.Core.Workbench.Docking;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.Admin.ViewModels;

public partial class AdminListViewModel : DocumentPage
{
    private static readonly IReadOnlyList<AdminListItem> AllItems =
    [
        new() { Code = "USR-001", Name = "张晨", Department = "产品部", Status = "启用", UpdatedAt = new DateTime(2026, 7, 30, 9, 20, 0) },
        new() { Code = "USR-002", Name = "李娜", Department = "研发部", Status = "启用", UpdatedAt = new DateTime(2026, 7, 29, 16, 45, 0) },
        new() { Code = "USR-003", Name = "王磊", Department = "运营部", Status = "停用", UpdatedAt = new DateTime(2026, 7, 28, 11, 10, 0) },
        new() { Code = "USR-004", Name = "陈曦", Department = "财务部", Status = "启用", UpdatedAt = new DateTime(2026, 7, 27, 14, 30, 0) },
        new() { Code = "USR-005", Name = "周明", Department = "研发部", Status = "停用", UpdatedAt = new DateTime(2026, 7, 26, 10, 5, 0) },
        new() { Code = "USR-006", Name = "赵悦", Department = "市场部", Status = "启用", UpdatedAt = new DateTime(2026, 7, 25, 15, 40, 0) },
        new() { Code = "USR-007", Name = "孙岩", Department = "产品部", Status = "启用", UpdatedAt = new DateTime(2026, 7, 24, 9, 15, 0) },
        new() { Code = "USR-008", Name = "刘婷", Department = "运营部", Status = "停用", UpdatedAt = new DateTime(2026, 7, 23, 17, 25, 0) },
        new() { Code = "USR-009", Name = "吴昊", Department = "研发部", Status = "启用", UpdatedAt = new DateTime(2026, 7, 22, 10, 50, 0) },
        new() { Code = "USR-010", Name = "郑敏", Department = "财务部", Status = "启用", UpdatedAt = new DateTime(2026, 7, 21, 13, 35, 0) },
        new() { Code = "USR-011", Name = "冯雪", Department = "市场部", Status = "停用", UpdatedAt = new DateTime(2026, 7, 20, 16, 5, 0) },
        new() { Code = "USR-012", Name = "何宇", Department = "产品部", Status = "启用", UpdatedAt = new DateTime(2026, 7, 19, 11, 45, 0) }
    ];

    [Reactive]
    public partial string Keyword { get; set; } = string.Empty;

    [Reactive]
    public partial string SelectedStatus { get; set; } = "全部";

    [Reactive]
    public partial bool IsLoading { get; set; }

    [Reactive]
    public partial int CurrentPage { get; set; } = 1;

    [Reactive]
    public partial int PageSize { get; set; } = 5;

    [Reactive]
    public partial int TotalCount { get; set; }

    [Reactive]
    public partial int SelectedCount { get; set; }

    public IReadOnlyList<string> StatusOptions { get; } = ["全部", "启用", "停用"];

    public ObservableCollection<AdminListItem> Items { get; } = [];

    public AdminListViewModel()
    {
        _ = LoadDataAsync();
    }

    [ReactiveCommand]
    private async Task LoadPage()
    {
        await LoadDataAsync();
    }

    [ReactiveCommand]
    private async Task Query()
    {
        CurrentPage = 1;
        await LoadDataAsync();
    }

    [ReactiveCommand]
    private async Task Reset()
    {
        Keyword = string.Empty;
        SelectedStatus = "全部";
        CurrentPage = 1;
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        if (IsLoading)
        {
            return;
        }

        IsLoading = true;
        try
        {
            await Task.Delay(200);

            var filteredItems = AllItems.Where(item =>
                (string.IsNullOrWhiteSpace(Keyword)
                 || item.Code.Contains(Keyword, StringComparison.OrdinalIgnoreCase)
                 || item.Name.Contains(Keyword, StringComparison.OrdinalIgnoreCase))
                && (SelectedStatus == "全部" || item.Status == SelectedStatus))
                .ToArray();

            TotalCount = filteredItems.Length;

            Items.Clear();
            foreach (var item in filteredItems.Skip((CurrentPage - 1) * PageSize).Take(PageSize))
            {
                Items.Add(item);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }
}
