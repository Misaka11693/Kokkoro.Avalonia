using Avalonia.Input;
using Kokkoro.ViewModels.Auth;
using Ursa.Controls;

namespace Kokkoro.Views.Startup;

public partial class StartupSplashWindow : SplashWindow
{
    public StartupSplashWindow()
    {
        InitializeComponent();
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            BeginMoveDrag(e);
        }
    }

    protected override Task<Window?> CreateNextWindow()
    {
        //return Task.FromResult(Application.Current is App app ? app.CreateAuthWindow() : null);
        return Task.FromResult(Application.Current is App app ? app.CreateWindow<AuthWindowViewModel>() : null);
    }
}
