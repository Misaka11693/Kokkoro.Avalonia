using Kokkoro.ViewModels.Core;
using System.ComponentModel;

namespace Kokkoro.ViewModels.Main;

public sealed class MainWindowViewModel : ViewModelBase, IDisposable
{
    private const string AppTitle = "Kokkoro";

    public MainWindowViewModel(
        MainTitleBarLeftViewModel titleBarLeft,
        MainTitleBarCenterViewModel titleBarCenter,
        MainTitleBarRightViewModel titleBarRight,
        MainTopBarViewModel topBar,
        MainContentViewModel content,
        MainStatusBarViewModel statusBar)
    {
        TitleBarLeft = titleBarLeft;
        TitleBarCenter = titleBarCenter;
        TitleBarRight = titleBarRight;
        TopBar = topBar;
        MainContent = content;
        StatusBar = statusBar;
    }

    public MainTitleBarLeftViewModel TitleBarLeft { get; }

    public MainTitleBarCenterViewModel TitleBarCenter { get; }

    public MainTitleBarRightViewModel TitleBarRight { get; }

    public MainTopBarViewModel TopBar { get; }

    public MainContentViewModel MainContent { get; }

    public MainStatusBarViewModel StatusBar { get; }

    public string WindowTitle => AppTitle;

    public void Dispose()
    {
        StatusBar.Dispose();
    }
}
