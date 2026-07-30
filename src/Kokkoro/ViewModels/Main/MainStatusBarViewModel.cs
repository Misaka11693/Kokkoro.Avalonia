using Kokkoro.Enums;
using Kokkoro.Extensions;
using Kokkoro.Services;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Main;

public partial class MainStatusBarViewModel : ViewModelBase, IDisposable
{
    private readonly IThemeService _themeService;

    public MainStatusBarViewModel(IThemeService themeService)
    {
        _themeService = themeService;
        _themeService.ThemeChanged += OnThemeChanged;
        ThemeText = BuildThemeText(_themeService.CurrentThemeMode);
    }

    [Reactive]
    public string _themeText = "主题：跟随系统";

    public void Dispose()
    {
        _themeService.ThemeChanged -= OnThemeChanged;
    }

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        ThemeText = BuildThemeText(_themeService.CurrentThemeMode);
    }

    private static string BuildThemeText(AppThemeMode themeMode)
    {
        return $"主题：{themeMode.GetLabel()}";
    }
}
