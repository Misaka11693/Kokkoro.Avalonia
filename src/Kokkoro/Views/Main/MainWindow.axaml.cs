using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.Messages;
using Kokkoro.ViewModels.Main;
using Ursa.Controls;
using Ursa.ReactiveUIExtension;

namespace Kokkoro.Views.Main;

public partial class MainWindow : ReactiveUrsaWindow<MainWindowViewModel>
{
    private bool _allowClose;
    private bool _confirmingClose;

    public MainWindow()
    {
        Closing += OnClosing;
        InitializeComponent();
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        if (_allowClose || _confirmingClose)
        {
            return;
        }

        e.Cancel = true;
        _confirmingClose = true;
        _ = ConfirmCloseAsync();
    }

    private async Task ConfirmCloseAsync()
    {
        try
        {
            var result = await AppRuntime.MessageService.ShowOverlayStandardAsync(
                "请选择关闭方式：\n\n“是”表示彻底退出应用。\n“否”表示最小化到托盘。",
                new OverlayMessageDialogOptions
                {
                    Title = "退出确认",
                    Icon = MessageBoxIcon.Question,
                    Button = MessageBoxButton.YesNo
                });

            if (result == MessageBoxResult.Yes)
            {
                _allowClose = true;
                Close();
                return;
            }

            if (result == MessageBoxResult.No)
            {
                if (Application.Current is App app)
                {
                    app.MinimizeMainWindowToTray(this);
                }
                return;
            }
        }
        finally
        {
            _confirmingClose = false;
        }
    }

    public void CloseDirectly()
    {
        _allowClose = true;
        Close();
    }
}
