using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Kokkoro.Models;

namespace Kokkoro.Behaviors;

/// <summary>
/// 年龄列：按年龄段设置文字颜色。
/// </summary>
public sealed class AgeBracketBehavior : DataGridTextColumn
{
    protected override Control GenerateElement(DataGridCell cell, object dataItem)
    {
        var element = base.GenerateElement(cell, dataItem);

        if (element is TextBlock textBlock
            && dataItem is User user)
        {
            textBlock.Foreground = GetForeground(user.Age);
        }

        return element;
    }

    private static IBrush? GetForeground(int age)
        => ResolveBrush(age switch
        {
            < 18 => "SemiRed5",
            < 30 => "SemiBlue5",
            < 45 => "SemiGreen5",
            < 60 => "SemiOrange5",
            _ => "SemiViolet5",
        });

    private static IBrush? ResolveBrush(string resourceKey)
        => Application.Current?.TryGetResource(resourceKey, null, out var resource) == true
            ? resource as IBrush
            : null;
}
