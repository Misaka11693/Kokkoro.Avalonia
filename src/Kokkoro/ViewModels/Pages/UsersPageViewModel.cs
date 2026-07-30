using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.Messages;
using Kokkoro.Models;
using Kokkoro.Services.Users;
using Kokkoro.Services.Users.Dtos;
using Mapster;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 用户页文档 ViewModel。
/// </summary>
public partial class UsersPageViewModel : DocumentPageViewModel
{
    private readonly IUserService _userService;

    [Reactive]
    public partial bool IsLoading { get; set; }

    [Reactive]
    public partial int CurrentPage { get; set; } = 1;

    [Reactive]
    public partial int PageSize { get; set; } = 5;

    [Reactive]
    public partial int TotalCount { get; set; }

    [Reactive]
    public partial IList? SelectedUsers { get; set; }

    [Reactive]
    public partial int SelectedCount { get; set; }

    [Reactive]
    public partial bool HasUnsavedChanges { get; set; }

    private IObservable<bool> CanEdit => this.WhenAnyValue(x => x.SelectedUsers).Select(users => users?.Count == 1);
    private IObservable<bool> CanDelete => this.WhenAnyValue(x => x.SelectedUsers).Select(users => users is { Count: > 0 });
    private IObservable<bool> CanSave => this.WhenAnyValue(x => x.HasUnsavedChanges);

    /// <summary>
    /// 构造函数
    /// </summary>
    public UsersPageViewModel(IUserService userService)
    {
        _userService = userService;

        QueryCriteria = new UserQueryCriteria();
        Users = new ObservableCollection<User>();
        _ = LoadDataAsync();
    }

    public Func<UserEditRequest, Task<User?>>? RequestUserEditAsync { get; set; }

    public Func<string, string, Task<bool>>? RequestConfirmAsync { get; set; }

    public Func<string, string, Task>? RequestNotifyAsync { get; set; }

    public UserQueryCriteria QueryCriteria { get; }

    public ObservableCollection<User> Users { get; }

    private async Task LoadDataAsync()
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            var result = await _userService.GetPageAsync(QueryCriteria.ToDto(CurrentPage, PageSize));
            TotalCount = result.TotalCount;

            Users.Clear();
            foreach (var item in result.Items)
            {
                Users.Add(item.Adapt<User>());
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    private IEnumerable<User> GetSelectedUserItems()
        => SelectedUsers?.OfType<User>() ?? [];

    #region Commands


    /// <summary>
    /// 加载分页命令
    /// </summary>
    [ReactiveCommand]
    private async Task LoadPage()
    {
        await LoadDataAsync();
    }

    /// <summary>
    /// 查询命令
    /// </summary>
    [ReactiveCommand]
    private async Task QueryAsync()
    {
        // throw new Exception("异常测试");

        CurrentPage = 1;
        var uimodel = AppRuntime.Service.Resolve<RolePageViewModel>();
        var dataIndex = await AppRuntime.DialogService.ShowKokkoroDialogAsync(uimodel, null, a =>
        {
            a.BeforeButtonCloseAsync = async (w, e) =>
            {
                Console.WriteLine(1);
                throw new Exception("异常测试");
                Console.WriteLine(1);
                return true;
            };

            //a.BeforeButtonCloseAsync = async (window, resultIndex) =>
            //{
            //    if (resultIndex != 0)
            //    {
            //        return true;
            //    }

            //    var uimodel2 = AppRuntime.Service.Resolve<RolePageViewModel>();
            //    var s = await AppRuntime.KokkoroDialogService.ShowKokkoroDialogAsync(uimodel2, null, b =>
            //    {
            //        b.Title = "弹窗2";
            //        b.BeforeButtonCloseAsync = async (window2, nestedResultIndex) =>
            //        {
            //            if (nestedResultIndex != 0)
            //            {
            //                return true;
            //            }

            //            if (!await AppRuntime.MessageService.AskQuestionAsync("对话框测试", owner: window2))
            //            {
            //                return false;
            //            }

            //            await AppRuntime.MessageService.ShowMessageAsync("点击了确定按钮", owner: window2);
            //            return false;
            //        };
            //    });

            //    return s == 0;
            //};
        });
        if (dataIndex == -1)
        {
            Console.WriteLine("cc");
        }
        //Console.WriteLine("21");
    }

    /// <summary>
    /// 重置命令
    /// </summary>
    [ReactiveCommand]
    private async Task Reset()
    {
        QueryCriteria.Reset();
        CurrentPage = 1;
        await LoadDataAsync();
    }

    /// <summary>
    /// 新增命令
    /// </summary>
    [ReactiveCommand]
    private async Task Add()
    {
        if (RequestUserEditAsync is null)
        {
            return;
        }

        var user = await RequestUserEditAsync(new UserEditRequest(null, true));
        if (user is null)
        {
            return;
        }

        if (await _userService.ContainsCodeAsync(user.Code))
        {
            if (RequestNotifyAsync is not null)
            {
                await RequestNotifyAsync("用户编码已存在，请使用其他编码。", "无法新增");
            }

            return;
        }

        await _userService.AddAsync(user.Adapt<UserDto>());
        HasUnsavedChanges = true;
        await LoadDataAsync();
    }

    /// <summary>
    /// 编辑命令
    /// </summary>
    [ReactiveCommand(CanExecute = nameof(CanEdit))]
    private async Task Edit()
    {
        if (RequestUserEditAsync is null)
        {
            return;
        }

        var selected = GetSelectedUserItems().ToList();
        if (selected.Count != 1)
        {
            return;
        }

        var user = await RequestUserEditAsync(new UserEditRequest(selected[0], false));
        if (user is null)
        {
            return;
        }

        await _userService.UpdateAsync(user.Adapt<UserDto>());
        HasUnsavedChanges = true;
        await LoadDataAsync();
    }

    /// <summary>
    /// 删除命令
    /// </summary>
    [ReactiveCommand(CanExecute = nameof(CanDelete))]
    private async Task Delete()
    {
        if (RequestConfirmAsync is not null)
        {
            var selectedUsers = GetSelectedUserItems().ToArray();
            var message = selectedUsers.Length == 1
                ? $"确定删除用户「{selectedUsers[0].Name}」吗？"
                : $"确定删除选中的 {selectedUsers.Length} 个用户吗？";

            var confirmed = await RequestConfirmAsync(message, "删除确认");
            if (!confirmed)
            {
                return;
            }
        }

        foreach (var user in GetSelectedUserItems().ToArray())
        {
            await _userService.DeleteAsync(user.Code);
        }

        SelectedUsers = new List<object>();
        SelectedCount = 0;
        HasUnsavedChanges = true;
        await LoadDataAsync();
    }

    /// <summary>
    /// 保存命令
    /// </summary>
    [ReactiveCommand(CanExecute = nameof(CanSave))]
    private async Task Save()
    {
        IsLoading = true;
        try
        {
            await Task.Delay(200);
            HasUnsavedChanges = false;

            if (RequestNotifyAsync is not null)
            {
                await RequestNotifyAsync("用户数据已保存。", "保存成功");
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    private static bool CanDeleteRow(User? user) => user is not null;

    [ReactiveCommand(CanExecute = nameof(CanDeleteRow))]
    private async Task DeleteRow(User? user)
    {
        if (user is null)
        {
            return;
        }

        if (RequestConfirmAsync is not null)
        {
            var confirmed = await RequestConfirmAsync($"确定删除用户「{user.Name}」吗？", "删除确认");
            if (!confirmed)
            {
                return;
            }
        }

        await _userService.DeleteAsync(user.Code);
        HasUnsavedChanges = true;
        await LoadDataAsync();
    }

    ///// <summary>
    ///// 编辑行命令
    ///// </summary>
    //private IObservable<bool> CanEditRow =>
    //    this.WhenAnyValue(x => x.HasUnsavedChanges);

    //[ReactiveCommand(CanExecute = nameof(CanEditRow))]
    [ReactiveCommand]
    private async Task EditRow(User? user)
    {
        if (user is null || RequestUserEditAsync is null)
        {
            return;
        }

        var edited = await RequestUserEditAsync(new UserEditRequest(user, false));
        if (edited is null)
        {
            return;
        }

        await _userService.UpdateAsync(edited.Adapt<UserDto>());
        HasUnsavedChanges = true;
        await LoadDataAsync();
    }

    #endregion
}
