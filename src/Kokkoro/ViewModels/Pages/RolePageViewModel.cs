using Avalonia.Threading;
using Kokkoro.Models;
using Kokkoro.Services.Roles;
using Mapster;
using ReactiveUI.SourceGenerators;
using System.Collections;
using System.Collections.ObjectModel;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 角色页文档 ViewModel。
/// </summary>
public partial class RolePageViewModel : DocumentPageViewModel
{
    private readonly IRoleService _roleService;

    [Reactive]
    public partial bool IsLoading { get; set; }

    [Reactive]
    public partial int CurrentPage { get; set; } = 1;

    [Reactive]
    public partial int PageSize { get; set; } = 5;

    [Reactive]
    public partial int TotalCount { get; set; }

    [Reactive]
    public partial IList? SelectedRoles { get; set; }

    [Reactive]
    public partial int SelectedCount { get; set; }

    [Reactive]
    public partial bool IsQueryPanelExpanded { get; set; } = true;

    public RolePageViewModel(IRoleService roleService)
    {
        _roleService = roleService;
        QueryCriteria = new RoleQueryCriteria();
        Roles = new ObservableCollection<Role>();
        StatusOptions = ["全部", "启用", "停用"];
        _ = LoadDataAsync();
    }

    public RoleQueryCriteria QueryCriteria { get; }

    public ObservableCollection<Role> Roles { get; }

    public IReadOnlyList<string> StatusOptions { get; }

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
        QueryCriteria.Reset();
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
            var result = await _roleService.GetPageAsync(QueryCriteria.ToDto(CurrentPage, PageSize));
            TotalCount = result.TotalCount;

            Roles.Clear();
            foreach (var item in result.Items)
            {
                Roles.Add(item.Adapt<Role>());
            }
            //SelectedRoles = new List<object>();
            //SelectedCount = 0;
        }
        finally
        {
            IsLoading = false;
        }
    }
}
