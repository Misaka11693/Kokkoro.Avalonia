using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media;
using Kokkoro.Core.Workbench.Docking;
using ReactiveUI;
using Semi.Avalonia;

namespace Kokkoro.Sample.ViewModels;

public class IconDemoViewModel : DocumentPage
{
    private readonly Icons _resources = new();
    private readonly Dictionary<string, IconItem> _fillIcons = new();
    private readonly Dictionary<string, IconItem> _strokedIcons = new();
    private readonly Dictionary<string, IconItem> _aiIcons = new();
    private bool _resourcesInitialized;
    private string _searchText = string.Empty;

    public ObservableCollection<IconTab> IconTabs { get; } = [];
    public ObservableCollection<IconItem> FilteredFillIcons { get; } = [];
    public ObservableCollection<IconItem> FilteredStrokedIcons { get; } = [];
    public ObservableCollection<IconItem> FilteredAIIcons { get; } = [];

    public string SearchText
    {
        get => _searchText;
        set
        {
            this.RaiseAndSetIfChanged(ref _searchText, value);
            FilterIcons(value);
        }
    }

    public void InitializeResources()
    {
        if (_resourcesInitialized)
        {
            return;
        }

        foreach (var provider in _resources.MergedDictionaries)
        {
            if (provider is not ResourceDictionary dictionary)
            {
                continue;
            }

            foreach (var key in dictionary.Keys)
            {
                if (dictionary[key] is not Geometry geometry)
                {
                    continue;
                }

                var resourceKey = key.ToString() ?? string.Empty;
                var icon = new IconItem(resourceKey, geometry);
                if (resourceKey.StartsWith("SemiIconAI", StringComparison.InvariantCultureIgnoreCase))
                {
                    _aiIcons[resourceKey] = icon;
                }
                else if (resourceKey.EndsWith("Stroked", StringComparison.InvariantCultureIgnoreCase))
                {
                    _strokedIcons[resourceKey] = icon;
                }
                else
                {
                    _fillIcons[resourceKey] = icon;
                }
            }
        }

        AssignOrders(_fillIcons);
        AssignOrders(_strokedIcons);
        AssignOrders(_aiIcons);
        _resourcesInitialized = true;
        FilterIcons(SearchText);

        IconTabs.Clear();
        IconTabs.Add(new IconTab("Fill icons", FilteredFillIcons));
        IconTabs.Add(new IconTab("Stroked icons", FilteredStrokedIcons));
        IconTabs.Add(new IconTab("AI icons", FilteredAIIcons));
    }

    private void FilterIcons(string? searchText)
    {
        var search = string.IsNullOrWhiteSpace(searchText) ? string.Empty : searchText.Trim();

        UpdateFilteredIcons(FilteredFillIcons, _fillIcons, search);
        UpdateFilteredIcons(FilteredStrokedIcons, _strokedIcons, search);
        UpdateFilteredIcons(FilteredAIIcons, _aiIcons, search);
    }

    private static void UpdateFilteredIcons(ObservableCollection<IconItem> target, Dictionary<string, IconItem> source, string search)
    {
        target.Clear();
        foreach (var pair in source.Where(pair => pair.Key.Contains(search, StringComparison.InvariantCultureIgnoreCase)))
        {
            target.Add(pair.Value);
        }
    }

    private static void AssignOrders(Dictionary<string, IconItem> icons)
    {
        var order = 1;
        foreach (var key in icons.Keys)
        {
            icons[key] = icons[key] with { Order = order++ };
        }
    }
}

public sealed record IconTab(string Header, ObservableCollection<IconItem> IconItems);

public sealed record IconItem(string ResourceKey, Geometry Geometry, int Order = 0)
{
    public string DisplayName => $"{Order:D3}. {ResourceKey}";
}
