using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Ursa.Controls;

namespace Kokkoro.Views.Main;

internal static class MainWindowTitleBarChrome
{
    public static void Apply(Window window, TitleBar? titleBar)
    {
        if (!window.TryGetResource("KokkoroChromeBackground", null, out var chromeResource) ||
            chromeResource is not IBrush chrome)
        {
            return;
        }

        window.TryGetResource("KokkoroDivider", null, out var dividerResource);
        var divider = dividerResource as IBrush;

        if (titleBar is not null)
        {
            titleBar.Background = chrome;
            if (divider is not null)
            {
                titleBar.BorderBrush = divider;
                titleBar.BorderThickness = new Thickness(0, 0, 0, 1);
            }
        }

        foreach (var decorations in window.GetLogicalDescendants().OfType<WindowDrawnDecorations>())
        {
            foreach (var frame in decorations.GetLogicalDescendants().OfType<Border>())
            {
                frame.Background = chrome;
                if (divider is not null)
                {
                    frame.BorderBrush = divider;
                    frame.BorderThickness = new Thickness(0, 0, 0, 1);
                }

                return;
            }
        }
    }
}
