using Avalonia;
using Avalonia.Media;

namespace Kokkoro.ViewModels.Core;

public static class MenuItemUtilities
{
    public static IEnumerable<MenuItemViewModel> EnumerateLeafItems(IEnumerable<MenuItemViewModel> items)
    {
        foreach (var item in items)
        {
            if (item.Children.Count > 0)
            {
                foreach (var child in EnumerateLeafItems(item.Children))
                {
                    yield return child;
                }

                continue;
            }

            if (!item.IsSeparator)
            {
                yield return item;
            }
        }
    }

    public static MenuItemViewModel? FindByKey(IEnumerable<MenuItemViewModel> items, string key)
    {
        foreach (var item in items)
        {
            if (string.Equals(item.Key, key, StringComparison.OrdinalIgnoreCase))
            {
                return item;
            }

            if (item.Children.Count == 0)
            {
                continue;
            }

            var childMatch = FindByKey(item.Children, key);
            if (childMatch is not null)
            {
                return childMatch;
            }
        }

        return null;
    }

    public static Geometry? GetIcon(string? resourceKey)
    {
        if (string.IsNullOrWhiteSpace(resourceKey))
        {
            return null;
        }

        return Application.Current?.TryGetResource(resourceKey, null, out var resource) == true
            ? resource as Geometry
            : null;
    }
}
