using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Threading;

namespace Kokkoro.Core.Extensions;

/// <summary>
/// TreeView 展开与折叠扩展。
/// </summary>
public static class TreeViewExtension
{
    public static void ExpandAll(this TreeView treeView)
    {
        ArgumentNullException.ThrowIfNull(treeView);
        ExpandAllCore(treeView);
    }

    public static void CollapseAll(this TreeView treeView)
    {
        ArgumentNullException.ThrowIfNull(treeView);
        SetExpandedCore(treeView, false);
    }

    private static void SetExpandedCore(ItemsControl container, bool expanded)
    {
        foreach (var child in container.GetLogicalChildren().OfType<TreeViewItem>())
        {
            child.IsExpanded = expanded;
            SetExpandedCore(child, expanded);
        }
    }

    private static void ExpandAllCore(ItemsControl container)
    {
        foreach (var child in container.GetLogicalChildren().OfType<TreeViewItem>())
        {
            if (!child.IsExpanded)
            {
                child.IsExpanded = true;
                Dispatcher.UIThread.Post(() => ExpandAllCore(child), DispatcherPriority.Loaded);
            }
            else
            {
                ExpandAllCore(child);
            }
        }
    }
}
