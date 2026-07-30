using Kokkoro.ViewModels.Core;

namespace Kokkoro.Core.MetaModels;

public static class MenuManager
{
    private static readonly List<MenuItemMeta> _items = [];

    public static IReadOnlyList<MenuItemMeta> Items => _items;

    public static void AddModules(params MenuItemMeta[] modules)
    {
        foreach (var module in modules)
        {
            if (module is null)
                throw new ArgumentNullException(nameof(module));
            _items.Add(module);
        }
    }
}
