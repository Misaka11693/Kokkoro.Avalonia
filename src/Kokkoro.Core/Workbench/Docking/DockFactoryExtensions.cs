using Dock.Model.Controls;
using Dock.Model.Core;

namespace Kokkoro.Core.Workbench.Docking;

public static class DockFactoryExtensions
{
    public static IEnumerable<IDockable> FindAllDocument(this IFactory? factory, IDock dock, Func<IDockable, bool> predicate)
    {
        if (factory is null)
        {
            yield break;
        }

        foreach (var dockable in EnumerateDockTree(dock))
        {
            if (predicate(dockable))
            {
                yield return dockable;
            }
        }
    }

    private static IEnumerable<IDockable> EnumerateDockTree(IDock dock)
    {
        yield return dock;

        foreach (var dockable in EnumerateDockChildren(dock))
        {
            yield return dockable;
        }
    }

    private static IEnumerable<IDockable> EnumerateDockChildren(IDock dock)
    {
        if (dock.VisibleDockables is not null)
        {
            foreach (var dockable in dock.VisibleDockables)
            {
                foreach (var item in EnumerateDockableTree(dockable))
                {
                    yield return item;
                }
            }
        }

        if (dock is ISplitViewDock splitViewDock)
        {
            foreach (var dockable in EnumerateSplitViewDockables(dock, splitViewDock))
            {
                foreach (var item in EnumerateDockableTree(dockable))
                {
                    yield return item;
                }
            }
        }

        if (dock is IRootDock { Windows: not null } rootDock)
        {
            foreach (var window in rootDock.Windows)
            {
                if (window.Layout is null)
                {
                    continue;
                }

                foreach (var item in EnumerateDockTree(window.Layout))
                {
                    yield return item;
                }
            }
        }
    }

    private static IEnumerable<IDockable> EnumerateDockableTree(IDockable dockable)
    {
        yield return dockable;

        if (dockable is IDock childDock)
        {
            foreach (var item in EnumerateDockChildren(childDock))
            {
                yield return item;
            }
        }
    }

    private static IEnumerable<IDockable> EnumerateSplitViewDockables(IDock dock, ISplitViewDock splitViewDock)
    {
        var paneDockable = splitViewDock.PaneDockable;
        if (paneDockable is not null && dock.VisibleDockables?.Contains(paneDockable) != true)
        {
            yield return paneDockable;
        }

        var contentDockable = splitViewDock.ContentDockable;
        if (contentDockable is not null
            && !ReferenceEquals(contentDockable, paneDockable)
            && dock.VisibleDockables?.Contains(contentDockable) != true)
        {
            yield return contentDockable;
        }
    }
}
