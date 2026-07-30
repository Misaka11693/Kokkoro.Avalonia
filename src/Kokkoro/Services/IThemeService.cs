using Kokkoro.Enums;

namespace Kokkoro.Services;

public interface IThemeService
{
    event EventHandler? ThemeChanged;

    AppThemeMode CurrentThemeMode { get; }

    void SetTheme(AppThemeMode themeMode);
}
