using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Globalization;

namespace Kokkoro.Converters;

public class IsActiveToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isActive = value is bool b && b;
        var activeBrush = Application.Current?.FindResource("SemiColorPrimary") as IBrush;
        var inactiveBrush = Application.Current?.FindResource("SemiColorPrimary") as IBrush;
        //var inactiveBrush = Application.Current?.FindResource("SemiColorPrimaryDisabled") as IBrush;
        return isActive ? (activeBrush ?? Brushes.Blue) : (inactiveBrush ?? Brushes.Gray);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}