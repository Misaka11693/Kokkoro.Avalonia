using System.Windows.Input;
using Avalonia.Media;

namespace Kokkoro.Core.Workbench.Models;

public sealed class MenuItemViewModel
{
    public MenuItemViewModel(string key, string title)
    {
        Key = key;
        Title = title;
    }

    public string Key { get; }

    public string Title { get; }

    public Geometry? Icon { get; init; }

    public object? Content { get; init; }

    public IReadOnlyList<MenuItemViewModel> Children { get; init; } = [];

    public bool IsSeparator { get; init; }

    public ICommand? ActivateCommand { get; init; }
}