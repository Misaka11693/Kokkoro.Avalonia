using System.Collections.ObjectModel;
using Kokkoro.Admin.Models;
using Kokkoro.Core.Workbench.Docking;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.Admin.ViewModels;

public partial class AdminTreeViewModel : DocumentPage
{
    private static readonly IReadOnlyList<AdminTreeNode> AllNodes =
    [
        new()
        {
            Name = "组织架构",
            Type = "根节点",
            Description = "公司组织关系",
            Children =
            [
                new()
                {
                    Name = "研发部",
                    Type = "部门",
                    Description = "产品研发团队",
                    Children =
                    [
                        new() { Name = "前端组", Type = "团队", Description = "桌面端与前端开发" },
                        new() { Name = "后端组", Type = "团队", Description = "服务与数据开发" },
                        new() { Name = "测试组", Type = "团队", Description = "质量保障" }
                    ]
                },
                new()
                {
                    Name = "产品部",
                    Type = "部门",
                    Description = "产品设计与规划",
                    Children =
                    [
                        new() { Name = "产品设计组", Type = "团队", Description = "产品设计" },
                        new() { Name = "体验设计组", Type = "团队", Description = "交互与视觉设计" }
                    ]
                },
                new() { Name = "运营部", Type = "部门", Description = "用户与内容运营" }
            ]
        },
        new()
        {
            Name = "菜单配置",
            Type = "根节点",
            Description = "系统菜单结构",
            Children =
            [
                new()
                {
                    Name = "系统管理",
                    Type = "菜单",
                    Description = "基础管理功能",
                    Children =
                    [
                        new() { Name = "用户管理", Type = "子菜单", Description = "维护用户信息" },
                        new() { Name = "角色管理", Type = "子菜单", Description = "维护角色权限" }
                    ]
                },
                new()
                {
                    Name = "业务管理",
                    Type = "菜单",
                    Description = "业务管理功能",
                    Children =
                    [
                        new() { Name = "订单管理", Type = "子菜单", Description = "查询业务订单" },
                        new() { Name = "客户管理", Type = "子菜单", Description = "维护客户资料" }
                    ]
                }
            ]
        }
    ];

    [Reactive]
    public partial string Keyword { get; set; } = string.Empty;

    [Reactive]
    public partial bool IsLoading { get; set; }

    [Reactive]
    public partial int VisibleNodeCount { get; set; }

    public ObservableCollection<AdminTreeNode> TreeNodes { get; } = [];

    public AdminTreeViewModel()
    {
        _ = LoadDataAsync();
    }

    [ReactiveCommand]
    private async Task Query()
    {
        await LoadDataAsync();
    }

    [ReactiveCommand]
    private async Task Reset()
    {
        Keyword = string.Empty;
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

            var nodes = AllNodes
                .Select(node => FilterNode(node, Keyword))
                .OfType<AdminTreeNode>()
                .ToArray();

            TreeNodes.Clear();
            foreach (var node in nodes)
            {
                TreeNodes.Add(node);
            }

            VisibleNodeCount = CountNodes(nodes);
        }
        finally
        {
            IsLoading = false;
        }
    }

    private static AdminTreeNode? FilterNode(AdminTreeNode node, string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)
            || node.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            || node.Type.Contains(keyword, StringComparison.OrdinalIgnoreCase))
        {
            return node;
        }

        var children = node.Children
            .Select(child => FilterNode(child, keyword))
            .OfType<AdminTreeNode>()
            .ToArray();

        return children.Length == 0
            ? null
            : new AdminTreeNode
            {
                Name = node.Name,
                Type = node.Type,
                Description = node.Description,
                Children = children
            };
    }

    private static int CountNodes(IEnumerable<AdminTreeNode> nodes)
    {
        return nodes.Sum(node => 1 + CountNodes(node.Children));
    }
}
