using Avalonia;
using Avalonia.Styling;
using Kokkoro.Enums;
using Kokkoro.Themes;
using Semi.Avalonia;

namespace Kokkoro.Services;

public sealed class ThemeService : IThemeService
{
    public ThemeService()
    {
        if (Application.Current is { } application)
        {
            application.PropertyChanged += OnRequestedThemeVariantChanged;
        }
    }

    public event EventHandler? ThemeChanged;

    public AppThemeMode CurrentThemeMode => ResolveThemeMode(Application.Current?.RequestedThemeVariant);

    public void SetTheme(AppThemeMode themeMode)
    {
        if (Application.Current is null)
        {
            return;
        }

        Application.Current.RequestedThemeVariant = themeMode switch
        {
            AppThemeMode.Light => ThemeVariant.Light,
            AppThemeMode.Dark => ThemeVariant.Dark,
            AppThemeMode.KokkoroLight => PresetKokkoroThemes.KokkoroLight,
            AppThemeMode.KokkoroDark => PresetKokkoroThemes.KokkoroDark,
            AppThemeMode.OceanLight => PresetKokkoroThemes.OceanLight,
            AppThemeMode.OceanDark => PresetKokkoroThemes.OceanDark,
            AppThemeMode.ForestLight => PresetKokkoroThemes.ForestLight,
            AppThemeMode.ForestDark => PresetKokkoroThemes.ForestDark,
            AppThemeMode.Aquatic => SemiTheme.Aquatic,
            AppThemeMode.Desert => SemiTheme.Desert,
            AppThemeMode.Dusk => SemiTheme.Dusk,
            AppThemeMode.NightSky => SemiTheme.NightSky,
            _ => ThemeVariant.Default
        };
    }

    private void OnRequestedThemeVariantChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property != ThemeVariantScope.RequestedThemeVariantProperty)
        {
            return;
        }

        if (Equals(e.OldValue, e.NewValue))
        {
            return;
        }

        ThemeChanged?.Invoke(this, EventArgs.Empty);
    }

    private static AppThemeMode ResolveThemeMode(ThemeVariant? themeVariant)
    {
        if (themeVariant == ThemeVariant.Light)
        {
            return AppThemeMode.Light;
        }

        if (themeVariant == ThemeVariant.Dark)
        {
            return AppThemeMode.Dark;
        }

        if (themeVariant == PresetKokkoroThemes.KokkoroLight)
        {
            return AppThemeMode.KokkoroLight;
        }

        if (themeVariant == PresetKokkoroThemes.KokkoroDark)
        {
            return AppThemeMode.KokkoroDark;
        }

        if (themeVariant == PresetKokkoroThemes.OceanLight)
        {
            return AppThemeMode.OceanLight;
        }

        if (themeVariant == PresetKokkoroThemes.OceanDark)
        {
            return AppThemeMode.OceanDark;
        }

        if (themeVariant == PresetKokkoroThemes.ForestLight)
        {
            return AppThemeMode.ForestLight;
        }

        if (themeVariant == PresetKokkoroThemes.ForestDark)
        {
            return AppThemeMode.ForestDark;
        }

        if (themeVariant == SemiTheme.Aquatic)
        {
            return AppThemeMode.Aquatic;
        }

        if (themeVariant == SemiTheme.Desert)
        {
            return AppThemeMode.Desert;
        }

        if (themeVariant == SemiTheme.Dusk)
        {
            return AppThemeMode.Dusk;
        }

        if (themeVariant == SemiTheme.NightSky)
        {
            return AppThemeMode.NightSky;
        }

        return AppThemeMode.System;
    }
}
