using Kokkoro.Core.Apps;
using Kokkoro.Models;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Ursa.Controls;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 树形页面 ViewModel
/// </summary>
public partial class TreePageViewModel : DocumentPageViewModel
{
    [Reactive] public partial bool IsLoading { get; set; }
    [Reactive] public partial int TotalCount { get; set; }
    [Reactive] public partial int SelectedCount { get; set; }
    [Reactive] public partial bool HasUnsavedChanges { get; set; }
    [Reactive] public partial TreeNode? SelectedNode { get; set; }

    private IObservable<bool> CanEdit   => this.WhenAnyValue(x => x.SelectedNode).Select(n => n is not null);
    private IObservable<bool> CanDelete => this.WhenAnyValue(x => x.SelectedNode).Select(n => n is not null);
    private IObservable<bool> CanSave   => this.WhenAnyValue(x => x.HasUnsavedChanges);

    /// <summary>树节点根集合，绑定到 TreeView.ItemsSource。</summary>
    public ObservableCollection<TreeNode> TreeNodes { get; } = new();

    public TreePageViewModel()
    {
        _ = LoadDataAsync();
    }

    // ─── 数据加载 ───────────────────────────────────────────────

    private async Task LoadDataAsync()
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            await Task.Delay(500);
            var data = BuildMockData();
            TreeNodes.Clear();
            foreach (var node in data)
                TreeNodes.Add(node);
            RefreshTotalCount();
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void RefreshTotalCount() => TotalCount = CountNodes(TreeNodes);

    private static int CountNodes(IEnumerable<TreeNode> nodes)
    {
        var total = 0;
        foreach (var n in nodes)
        {
            total++;
            if (n.HasChildren) total += CountNodes(n.Children);
        }
        return total;
    }

    // ─── 节点查找 ───────────────────────────────────────────────

    private ObservableCollection<TreeNode>? FindParentCollection(
        ObservableCollection<TreeNode> collection, TreeNode node)
    {
        foreach (var item in collection)
        {
            if (item.Children.Contains(node)) return item.Children;
            var found = FindParentCollection(item.Children, node);
            if (found is not null) return found;
        }
        return null;
    }

    // ─── Commands ───────────────────────────────────────────────

    [ReactiveCommand]
    private async Task Refresh() => await LoadDataAsync();

    /// <summary>
    /// 新增节点：弹出编辑对话框，追加到选中节点下或根集合。
    /// </summary>
    [ReactiveCommand]
    private async Task Add()
    {
        var vm = new TreeNodeEditViewModel
        {
            DialogTitle    = SelectedNode is null ? "新增根节点" : $"在「{SelectedNode.Name}」下新增子节点",
            ParentNodeHint = SelectedNode is null ? null : $"父节点：{SelectedNode.Name}（{SelectedNode.Type}）",
            NodeType       = SelectedNode is null ? "根节点" : "子节点",
            Order          = SelectedNode is null ? TreeNodes.Count + 1 : SelectedNode.Children.Count + 1
        };

        var result = await AppRuntime.OverlayDialogService.ShowCustomAsync<TreeNodeEditViewModel, TreeNode>(
            vm,
            options: new OverlayDialogOptions { Title = vm.DialogTitle, CanDragMove = true });

        if (result is null) return;

        result.Id       = Guid.NewGuid().ToString("N")[..8];
        result.ParentId = SelectedNode?.Id;
        result.CreatedAt = DateTime.Now;

        if (SelectedNode is not null)
            SelectedNode.Children.Add(result);
        else
            TreeNodes.Add(result);

        HasUnsavedChanges = true;
        RefreshTotalCount();
    }

    /// <summary>
    /// 编辑选中节点：弹出编辑对话框，将结果写回节点。
    /// </summary>
    [ReactiveCommand(CanExecute = nameof(CanEdit))]
    private async Task Edit()
    {
        if (SelectedNode is null) return;

        var vm = new TreeNodeEditViewModel { DialogTitle = $"编辑「{SelectedNode.Name}」" };
        vm.LoadFrom(SelectedNode);

        //var result = await AppRuntime.OverlayDialogService.ShowCustomAsync<TreeNodeEditViewModel, TreeNode>(
        //    vm,
        //    options: new OverlayDialogOptions { Title = vm.DialogTitle, CanDragMove = true });

        //if (result is null) return;

        var result2 = await AppRuntime.DialogService.ShowKokkoroDialogAsync(new TreeNodeEditViewModel(), null);


        var result = await AppRuntime.OverlayDialogService.ShowCustomAsync<TreeNodeEditViewModel, TreeNode>(
    vm,
    options: new OverlayDialogOptions { Title = vm.DialogTitle, CanDragMove = true });

        if (result is null) return;

        SelectedNode.Name        = result.Name;
        SelectedNode.Type        = result.Type;
        SelectedNode.Description = result.Description;
        SelectedNode.Order       = result.Order;
        SelectedNode.IsEnabled   = result.IsEnabled;

        HasUnsavedChanges = true;
    }

    /// <summary>
    /// 删除选中节点（含所有子节点），删前确认。
    /// </summary>
    [ReactiveCommand(CanExecute = nameof(CanDelete))]
    private async Task Delete()
    {
        if (SelectedNode is null) return;

        var confirmed = await AppRuntime.MessageService.AskQuestionAsync(
            $"确定删除节点「{SelectedNode.Name}」及其所有子节点吗？");
        if (!confirmed) return;

        var parentCollection = FindParentCollection(TreeNodes, SelectedNode)
                               ?? (TreeNodes.Contains(SelectedNode) ? TreeNodes : null);

        if (parentCollection is null) return;

        parentCollection.Remove(SelectedNode);
        SelectedNode  = null;
        SelectedCount = 0;
        HasUnsavedChanges = true;
        RefreshTotalCount();
    }

    /// <summary>保存变更（模拟）。</summary>
    [ReactiveCommand(CanExecute = nameof(CanSave))]
    private async Task Save()
    {
        IsLoading = true;
        try
        {
            await Task.Delay(300);
            HasUnsavedChanges = false;
            await AppRuntime.MessageService.ShowInformationAsync("节点数据已保存。");
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─── Mock Data ──────────────────────────────────────────────

    private static List<TreeNode> BuildMockData()
    {
        return new List<TreeNode>
        {
            new TreeNode
            {
                Id = "1", Name = "组织架构", Type = "根节点",
                Description = "公司组织架构", Order = 1, IsEnabled = true,
                CreatedAt = DateTime.Now.AddMonths(-6),
                Children = new ObservableCollection<TreeNode>
                {
                    new TreeNode
                    {
                        Id = "1-1", Name = "技术部", Type = "部门",
                        Description = "负责技术研发", Order = 1, ParentId = "1",
                        IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-5),
                        Children = new ObservableCollection<TreeNode>
                        {
                            new TreeNode { Id = "1-1-1", Name = "前端组", Type = "团队", Description = "前端开发团队", Order = 1, ParentId = "1-1", IsEnabled = true,  CreatedAt = DateTime.Now.AddMonths(-4) },
                            new TreeNode { Id = "1-1-2", Name = "后端组", Type = "团队", Description = "后端开发团队", Order = 2, ParentId = "1-1", IsEnabled = true,  CreatedAt = DateTime.Now.AddMonths(-4) },
                            new TreeNode { Id = "1-1-3", Name = "测试组", Type = "团队", Description = "质量保障团队", Order = 3, ParentId = "1-1", IsEnabled = true,  CreatedAt = DateTime.Now.AddMonths(-3) },
                        }
                    },
                    new TreeNode
                    {
                        Id = "1-2", Name = "产品部", Type = "部门",
                        Description = "产品设计与规划", Order = 2, ParentId = "1",
                        IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-5),
                        Children = new ObservableCollection<TreeNode>
                        {
                            new TreeNode { Id = "1-2-1", Name = "产品设计组", Type = "团队", Description = "产品需求与设计", Order = 1, ParentId = "1-2", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-4) },
                            new TreeNode { Id = "1-2-2", Name = "UI/UX组",   Type = "团队", Description = "用户体验设计",   Order = 2, ParentId = "1-2", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-3) },
                        }
                    },
                    new TreeNode { Id = "1-3", Name = "运营部", Type = "部门", Description = "市场运营与推广", Order = 3, ParentId = "1", IsEnabled = false, CreatedAt = DateTime.Now.AddMonths(-4) },
                }
            },
            new TreeNode
            {
                Id = "2", Name = "菜单配置", Type = "根节点",
                Description = "系统菜单配置", Order = 2, IsEnabled = true,
                CreatedAt = DateTime.Now.AddMonths(-6),
                Children = new ObservableCollection<TreeNode>
                {
                    new TreeNode
                    {
                        Id = "2-1", Name = "系统管理", Type = "菜单",
                        Description = "系统管理菜单", Order = 1, ParentId = "2",
                        IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-5),
                        Children = new ObservableCollection<TreeNode>
                        {
                            new TreeNode { Id = "2-1-1", Name = "用户管理", Type = "子菜单", Description = "管理系统用户", Order = 1, ParentId = "2-1", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-4) },
                            new TreeNode { Id = "2-1-2", Name = "角色管理", Type = "子菜单", Description = "管理角色权限", Order = 2, ParentId = "2-1", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-4) },
                        }
                    },
                    new TreeNode
                    {
                        Id = "2-2", Name = "业务管理", Type = "菜单",
                        Description = "业务功能菜单", Order = 2, ParentId = "2",
                        IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-5),
                        Children = new ObservableCollection<TreeNode>
                        {
                            new TreeNode { Id = "2-2-1", Name = "订单管理", Type = "子菜单", Description = "订单处理",     Order = 1, ParentId = "2-2", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-3) },
                            new TreeNode { Id = "2-2-2", Name = "客户管理", Type = "子菜单", Description = "客户信息管理", Order = 2, ParentId = "2-2", IsEnabled = true, CreatedAt = DateTime.Now.AddMonths(-2) },
                        }
                    },
                }
            },
        };
    }
}
