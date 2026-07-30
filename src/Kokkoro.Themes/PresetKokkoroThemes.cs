using Avalonia.Styling;

namespace Kokkoro.Themes;

/// <summary>
/// Preset theme variants that inherit from Semi Light/Dark as fallback,
/// without modifying Semi default Light/Dark resources.
/// </summary>
public class PresetKokkoroThemes : Styles
{
    public static ThemeVariant KokkoroLight => new(nameof(KokkoroLight), ThemeVariant.Light);

    public static ThemeVariant KokkoroDark => new(nameof(KokkoroDark), ThemeVariant.Dark);

    public static ThemeVariant OceanLight => new(nameof(OceanLight), ThemeVariant.Light);

    public static ThemeVariant OceanDark => new(nameof(OceanDark), ThemeVariant.Dark);

    public static ThemeVariant ForestLight => new(nameof(ForestLight), ThemeVariant.Light);

    public static ThemeVariant ForestDark => new(nameof(ForestDark), ThemeVariant.Dark);
}

