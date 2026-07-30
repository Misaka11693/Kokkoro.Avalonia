using System.Windows.Input;
using Avalonia.Media;

namespace Kokkoro.ViewModels.Pages;

public sealed class HomeQuickLinkViewModel
{
    public required string RouteKey { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public Geometry? Icon { get; init; }

    public IBrush? IconBackground { get; init; }

    public IBrush? IconForeground { get; init; }

    public required ICommand OpenCommand { get; init; }
}
