using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace Kokkoro.Sample.Controls;

public partial class ThemeColorSwatch : UserControl
{
    public static readonly StyledProperty<string> TokenProperty =
        AvaloniaProperty.Register<ThemeColorSwatch, string>(nameof(Token));

    static ThemeColorSwatch()
    {
        TokenProperty.Changed.AddClassHandler<ThemeColorSwatch>((control, _) => control.ApplyToken());
    }

    public ThemeColorSwatch()
    {
        InitializeComponent();
    }

    public string Token
    {
        get => GetValue(TokenProperty);
        set => SetValue(TokenProperty, value);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        ApplyToken();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ThemeVariantScope.ActualThemeVariantProperty)
        {
            ApplyToken();
        }
    }

    private void ApplyToken()
    {
        if (KeyText is null || Swatch is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(Token))
        {
            KeyText.Text = "—";
            ToolTip.SetTip(KeyText, null);
            Swatch.Background = Brushes.Transparent;
            return;
        }

        KeyText.Text = Token;
        ToolTip.SetTip(KeyText, Token);

        if (TryResolveBrush(Token, out var brush))
        {
            Swatch.Background = brush;
            return;
        }

        Swatch.Background = Brushes.Transparent;
        KeyText.Text = $"{Token} (未找到)";
        ToolTip.SetTip(KeyText, KeyText.Text);
    }

    private bool TryResolveBrush(string token, out IBrush brush)
    {
        brush = Brushes.Transparent;

        foreach (var theme in new ThemeVariant?[] { ActualThemeVariant, null })
        {
            if (!TryLookupResource(token, theme, out var resource))
            {
                continue;
            }

            if (ToBrush(resource) is { } resolved)
            {
                brush = resolved;
                return true;
            }
        }

        return false;
    }

    private bool TryLookupResource(string token, ThemeVariant? theme, out object? resource)
    {
        if (TryGetResource(token, theme, out resource))
        {
            return true;
        }

        if (Application.Current?.TryGetResource(token, theme, out resource) == true)
        {
            return true;
        }

        resource = null;
        return false;
    }

    private static IBrush? ToBrush(object? resource) =>
        resource switch
        {
            IBrush brush => brush,
            Color color => new SolidColorBrush(color),
            _ => null
        };
}
