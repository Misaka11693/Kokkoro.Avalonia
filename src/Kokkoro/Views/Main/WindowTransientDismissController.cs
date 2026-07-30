using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace Kokkoro.Views.Main;

internal sealed class WindowTransientDismissController : IDisposable
{
    private readonly Control _owner;
    private readonly Action _dismiss;
    private Window? _hostWindow;
    private bool _disposed;

    public WindowTransientDismissController(Control owner, Action dismiss)
    {
        _owner = owner;
        _dismiss = dismiss;
        _owner.AttachedToVisualTree += OnAttachedToVisualTree;
        _owner.DetachedFromVisualTree += OnDetachedFromVisualTree;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _owner.AttachedToVisualTree -= OnAttachedToVisualTree;
        _owner.DetachedFromVisualTree -= OnDetachedFromVisualTree;
        DetachWindowHandlers();
    }

    private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (_disposed)
        {
            return;
        }

        if (TopLevel.GetTopLevel(_owner) is not Window window || ReferenceEquals(window, _hostWindow))
        {
            return;
        }

        DetachWindowHandlers();
        _hostWindow = window;
        _hostWindow.Deactivated += OnHostWindowDeactivated;
        _hostWindow.PropertyChanged += OnHostWindowPropertyChanged;
    }

    private void OnDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        DetachWindowHandlers();
    }

    private void OnHostWindowDeactivated(object? sender, EventArgs e)
    {
        _dismiss();
    }

    private void OnHostWindowPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == Window.WindowStateProperty)
        {
            _dismiss();
        }
    }

    private void DetachWindowHandlers()
    {
        if (_hostWindow is null)
        {
            return;
        }

        _hostWindow.Deactivated -= OnHostWindowDeactivated;
        _hostWindow.PropertyChanged -= OnHostWindowPropertyChanged;
        _hostWindow = null;
    }
}
