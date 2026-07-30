using System.Runtime.CompilerServices;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Kokkoro.ViewModels.Pages;

namespace Kokkoro.Helpers;

/// <summary>
/// 对齐 <c>SIE.Wpf.Helpers.Zoom</c>：对控件 <c>EnableZoom(control)</c> 即可。
/// </summary>
public static class ZoomHelper
{
    private const double MinScale = 0.5;
    private const double MaxScale = 4.0;
    private const double Step = 0.1;

    private static readonly ConditionalWeakTable<Control, ZoomHost> Hosts = new();

    public static void DisableZoom(Control fe)
    {
        if (Hosts.TryGetValue(fe, out var host))
        {
            DetachWheelHandler(fe, host);
        }

        fe.PointerPressed -= Ctrl_PointerPressed;
        Unwrap(fe);
        Hosts.Remove(fe);
    }

    public static void EnableZoom(Control fe)
        => EnableZoom(fe, 1.0);

    public static void EnableZoom(Control fe, double zoomScale)
    {
        var scale = Math.Clamp(zoomScale, MinScale, MaxScale);
        AttachHandlers(fe);

        if (IsZoomActive(fe))
        {
            SetScale(fe, scale);
            return;
        }

        if (!Hosts.TryGetValue(fe, out _))
        {
            Hosts.Add(fe, new ZoomHost());
        }

        SetScale(fe, scale);
    }

    private static bool IsZoomActive(Control fe)
        => Hosts.TryGetValue(fe, out var host) && host.Transform is not null;

    private static void AttachHandlers(Control fe)
    {
        if (!Hosts.TryGetValue(fe, out var host))
        {
            Hosts.Add(fe, host = new ZoomHost());
        }

        DetachWheelHandler(fe, host);
        AttachWheelHandler(fe, host);

        fe.PointerPressed -= Ctrl_PointerPressed;
        fe.PointerPressed += Ctrl_PointerPressed;
    }

    private static void AttachWheelHandler(Control fe, ZoomHost host)
    {
        host.WheelHandler ??= (_, e) => Ctrl_PointerWheelChanged(fe, e);
        fe.AddHandler(
            InputElement.PointerWheelChangedEvent,
            host.WheelHandler,
            RoutingStrategies.Tunnel);
    }

    private static void DetachWheelHandler(Control fe, ZoomHost host)
    {
        if (host.WheelHandler is not { } handler)
        {
            return;
        }

        fe.RemoveHandler(InputElement.PointerWheelChangedEvent, handler);
        host.WheelHandler = null;
    }

    private static void Ctrl_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not Control fe
            || !e.KeyModifiers.HasFlag(KeyModifiers.Control))
        {
            return;
        }

        var p = e.GetCurrentPoint(fe).Properties;
        if ((p.IsMiddleButtonPressed || p.PointerUpdateKind == PointerUpdateKind.MiddleButtonPressed)
            && EnsureScaleTransform(fe) is { } scale)
        {
            scale.ScaleX = 1.0;
            scale.ScaleY = 1.0;
            PersistScale(fe, 1.0);
            e.Handled = true;
        }
    }

    private static void Ctrl_PointerWheelChanged(Control fe, PointerWheelEventArgs e)
    {
        if (!e.KeyModifiers.HasFlag(KeyModifiers.Control))
        {
            return;
        }

        var scale = EnsureScaleTransform(fe);
        scale.ScaleX += e.Delta.Y > 0 ? Step : -Step;
        scale.ScaleY = scale.ScaleX;

        if (scale.ScaleX < MinScale)
        {
            scale.ScaleX = MinScale;
            scale.ScaleY = MinScale;
        }
        else if (scale.ScaleX > MaxScale)
        {
            scale.ScaleX = MaxScale;
            scale.ScaleY = MaxScale;
        }

        PersistScale(fe, scale.ScaleX);
        e.Handled = true;
    }

    private static ScaleTransform EnsureScaleTransform(Control fe)
    {
        if (!Hosts.TryGetValue(fe, out var host))
        {
            Hosts.Add(fe, host = new ZoomHost());
        }

        if (host.Scale is null)
        {
            Wrap(fe, host);
            host.Scale = new ScaleTransform(1, 1);
            host.Transform!.LayoutTransform = host.Scale;
        }

        return host.Scale;
    }

    private static void SetScale(Control fe, double zoomScale)
    {
        var scale = EnsureScaleTransform(fe);
        var value = Math.Clamp(zoomScale, MinScale, MaxScale);
        scale.ScaleX = value;
        scale.ScaleY = value;
        PersistScale(fe, value);
    }

    private static void PersistScale(Control fe, double value)
    {
        if (fe.DataContext is DocumentPageViewModel document)
        {
            document.Zoom = value;
        }
    }

    private static void Wrap(Control fe, ZoomHost host)
    {
        if (host.Transform is not null)
        {
            return;
        }

        if (fe is UserControl userControl && userControl.Content is Control content)
        {
            host.Content = content;
            host.Transform = CreateHost();
            userControl.Content = null;
            host.Transform.Child = content;
            userControl.Content = host.Transform;
            return;
        }

        if (fe.Parent is Panel panel)
        {
            var index = panel.Children.IndexOf(fe);
            if (index < 0)
            {
                return;
            }

            host.Transform = CreateHost();
            panel.Children.RemoveAt(index);
            host.Transform.Child = fe;
            panel.Children.Insert(index, host.Transform);
        }
    }

    private static void Unwrap(Control fe)
    {
        if (!Hosts.TryGetValue(fe, out var host) || host.Transform is null)
        {
            return;
        }

        if (fe is UserControl userControl && userControl.Content == host.Transform && host.Content is not null)
        {
            userControl.Content = null;
            host.Transform.Child = null;
            userControl.Content = host.Content;
        }
        else if (host.Transform.Parent is Panel panel)
        {
            var index = panel.Children.IndexOf(host.Transform);
            if (index >= 0)
            {
                panel.Children.RemoveAt(index);
                host.Transform.Child = null;
                panel.Children.Insert(index, fe);
            }
        }

        host.Transform = null;
        host.Content = null;
        host.Scale = null;
    }

    private static LayoutTransformControl CreateHost() => new()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch,
        VerticalAlignment = VerticalAlignment.Stretch,
    };

    private sealed class ZoomHost
    {
        public Control? Content { get; set; }

        public LayoutTransformControl? Transform { get; set; }

        public ScaleTransform? Scale { get; set; }

        public EventHandler<PointerWheelEventArgs>? WheelHandler { get; set; }
    }
}
