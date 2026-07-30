using Kokkoro.Models;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 简单树形页面 ViewModel（无工具栏、无列头，仅展示树结构）
/// </summary>
public partial class SimpleTreePageViewModel : DocumentPageViewModel
{
    [Reactive] public partial bool IsLoading { get; set; }

    public ObservableCollection<TreeNode> TreeNodes { get; } = new();

    public SimpleTreePageViewModel()
    {
        _ = LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            await Task.Delay(300);
            TreeNodes.Clear();
            foreach (var node in BuildMockData())
                TreeNodes.Add(node);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [ReactiveCommand]
    private async Task Refresh() => await LoadDataAsync();

    private static List<TreeNode>   BuildMockData() =>
    [
        new TreeNode
        {
            Id = "1", Name = "组织架构", Type = "根节点", Description = "公司组织架构",
            IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-6),
            Children = new ObservableCollection<TreeNode>
            {
                new TreeNode
                {
                    Id = "1-1", Name = "技术部", Type = "部门", ParentId = "1",
                    IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-5),
                    Children = new ObservableCollection<TreeNode>
                    {
                        new TreeNode { Id = "1-1-1", Name = "前端组", Type = "团队", ParentId = "1-1", IsEnabled = true,  CreatedAt = DateTime.Now.AddMonths(-4) },
                        new TreeNode { Id = "1-1-2", Name = "后端组", Type = "团队", ParentId = "1-1", IsEnabled = true,  CreatedAt = DateTime.Now.AddMonths(-4) },
                        new TreeNode { Id = "1-1-3", Name = "测试组", Type = "团队", ParentId = "1-1", IsEnabled = true,  CreatedAt = DateTime.Now.AddMonths(-3) },
                    }
                },
                new TreeNode
                {
                    Id = "1-2", Name = "产品部", Type = "部门", ParentId = "1",
                    IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-5),
                    Children = new ObservableCollection<TreeNode>
                    {
                        new TreeNode { Id = "1-2-1", Name = "产品设计组", Type = "团队", ParentId = "1-2", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-4) },
                        new TreeNode { Id = "1-2-2", Name = "UI/UX组",   Type = "团队", ParentId = "1-2", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-3) },
                    }
                },
                new TreeNode { Id = "1-3", Name = "运营部", Type = "部门", ParentId = "1", IsEnabled = false, CreatedAt = DateTime.Now.AddMonths(-4) },
            }
        },
        new TreeNode
        {
            Id = "2", Name = "菜单配置", Type = "根节点", Description = "系统菜单配置",
            IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-6),
            Children = new ObservableCollection<TreeNode>
            {
                new TreeNode
                {
                    Id = "2-1", Name = "系统管理", Type = "菜单", ParentId = "2",
                    IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-5),
                    Children = new ObservableCollection<TreeNode>
                    {
                        new TreeNode { Id = "2-1-1", Name = "用户管理", Type = "子菜单", ParentId = "2-1", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-4) },
                        new TreeNode { Id = "2-1-2", Name = "角色管理", Type = "子菜单", ParentId = "2-1", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-4) },
                    }
                },
                new TreeNode
                {
                    Id = "2-2", Name = "业务管理", Type = "菜单", ParentId = "2",
                    IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-5),
                    Children = new ObservableCollection<TreeNode>
                    {
                        new TreeNode { Id = "2-2-1", Name = "订单管理", Type = "子菜单", ParentId = "2-2", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-3) },
                        new TreeNode { Id = "2-2-2", Name = "客户管理", Type = "子菜单", ParentId = "2-2", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-2) },
                    }
                },
            }
        },
    ];
}
