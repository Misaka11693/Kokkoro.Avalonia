using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Threading;
using Kokkoro.ViewModels.Pages;

namespace Kokkoro.Views.Pages;

public partial class SimpleTreePageView : DocumentPageView<SimpleTreePageViewModel>
{
    public SimpleTreePageView()
    {
        InitializeComponent();
    }

    private void OnExpandAllClick(object? sender, RoutedEventArgs e)
        => ExpandAllAsync(NodeTreeView);

    private void OnCollapseAllClick(object? sender, RoutedEventArgs e)
        => SetAllItemsExpanded(NodeTreeView, false);

    // ─── 折叠：子节点已存在，直接同步递归 ──────────────────────────

    private static void SetAllItemsExpanded(ItemsControl container, bool expand)
    {
        foreach (var child in container.GetLogicalChildren().OfType<TreeViewItem>())
        {
            child.IsExpanded = expand;
            SetAllItemsExpanded(child, expand);
        }
    }

    // ─── 展开：需等容器创建后再递归展开子层 ─────────────────────────

    private static void ExpandAllAsync(ItemsControl container)
    {
        foreach (var child in container.GetLogicalChildren().OfType<TreeViewItem>())
        {
            if (!child.IsExpanded)
            {
                child.IsExpanded = true;

                // 展开后容器尚未渲染，等下一个 Layout 周期再继续展开子层
                Dispatcher.UIThread.Post(
                    () => ExpandAllAsync(child),
                    DispatcherPriority.Loaded);
            }
            else
            {
                // 已展开的节点子容器已存在，直接递归
                ExpandAllAsync(child);
            }
        }
    }
}
