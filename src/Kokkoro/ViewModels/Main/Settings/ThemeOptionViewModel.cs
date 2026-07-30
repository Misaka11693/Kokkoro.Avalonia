using Kokkoro.Enums;
using Kokkoro.Extensions;

namespace Kokkoro.ViewModels.Main.Settings;

public sealed class ThemeOptionViewModel
{
    public ThemeOptionViewModel(AppThemeMode mode)
    {
        Mode = mode;
        Label = mode.GetLabel();
    }

    public string Label { get; }

    public AppThemeMode Mode { get; }
}
