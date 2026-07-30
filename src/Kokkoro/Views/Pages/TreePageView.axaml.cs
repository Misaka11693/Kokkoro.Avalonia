using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Kokkoro.Models;
using Kokkoro.ViewModels.Pages;

namespace Kokkoro.Views.Pages;

/// <summary>
/// 树形页面视图
/// </summary>
public partial class TreePageView : DocumentPageView<TreePageViewModel>
{
    public TreePageView()
    {
        InitializeComponent();
    }

    // ─── 选择同步 ────────────────────────────────────────────────

    private void OnTreeViewSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is not TreePageViewModel vm)
            return;

        vm.SelectedNode   = NodeTreeView.SelectedItem as TreeNode;
        vm.SelectedCount  = vm.SelectedNode is null ? 0 : 1;
    }

    // ─── 展开 / 折叠全部 ─────────────────────────────────────────

    private void OnExpandAllClick(object? sender, RoutedEventArgs e)
        => SetAllItemsExpanded(NodeTreeView, true);

    private void OnCollapseAllClick(object? sender, RoutedEventArgs e)
        => SetAllItemsExpanded(NodeTreeView, false);

    /// <summary>递归设置所有 <see cref="TreeViewItem"/> 的展开状态。</summary>
    private static void SetAllItemsExpanded(ItemsControl container, bool expand)
    {
        foreach (var child in container.GetLogicalChildren().OfType<TreeViewItem>())
        {
            child.IsExpanded = expand;
            SetAllItemsExpanded(child, expand);
        }
    }
}
