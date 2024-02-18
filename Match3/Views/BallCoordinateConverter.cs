using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Match3.Views;


public sealed class BallCoordinateConverter : IMultiValueConverter
{
    object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Any(v => v == DependencyProperty.UnsetValue)) return Binding.DoNothing;

        var unitSize = (int) values[0];
        var baseCoord = (int) values[1];
        var displacement = (double) values[2];

        return (baseCoord + displacement) * unitSize;
    }


    object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
