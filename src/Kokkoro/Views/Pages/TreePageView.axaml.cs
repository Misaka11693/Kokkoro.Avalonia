using Avalonia.Controls;
using Avalonia.Interactivity;
using Kokkoro.Core.Extensions;
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
        => NodeTreeView.ExpandAll();

    private void OnCollapseAllClick(object? sender, RoutedEventArgs e)
        => NodeTreeView.CollapseAll();
}
