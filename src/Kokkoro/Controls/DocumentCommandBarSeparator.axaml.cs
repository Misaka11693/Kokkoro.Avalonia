using Avalonia.Controls;
using Avalonia.Layout;

namespace Kokkoro.Controls;

/// <summary>
/// <see cref="DocumentCommandBar"/> 内命令组之间的竖向分隔线。
/// </summary>
public class DocumentCommandBarSeparator : Border
{
    public DocumentCommandBarSeparator()
    {
        Width = 1;
        Margin = new Thickness(2, 4);
        VerticalAlignment = VerticalAlignment.Stretch;
    }
}
