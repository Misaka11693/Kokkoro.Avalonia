using Avalonia;
using Avalonia.Controls;

namespace Kokkoro.Controls;

/// <summary>
/// 文档页顶栏命令区：Border + 横向 ScrollViewer，直接放入 IconButton 等子项即可。
/// </summary>
public partial class DocumentCommandBar : ItemsControl
{
    public static readonly StyledProperty<Thickness> BarPaddingProperty =
        AvaloniaProperty.Register<DocumentCommandBar, Thickness>(nameof(BarPadding), new Thickness(8, 6));

    public static readonly StyledProperty<double> ItemSpacingProperty =
        AvaloniaProperty.Register<DocumentCommandBar, double>(nameof(ItemSpacing), 6);

    public Thickness BarPadding
    {
        get => GetValue(BarPaddingProperty);
        set => SetValue(BarPaddingProperty, value);
    }

    public double ItemSpacing
    {
        get => GetValue(ItemSpacingProperty);
        set => SetValue(ItemSpacingProperty, value);
    }

    public DocumentCommandBar()
    {
        InitializeComponent();
    }
}
