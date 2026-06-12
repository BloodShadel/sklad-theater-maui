using System.Globalization;

namespace SkladTheater.Maui.Converters;

public class IntEqualsConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || parameter == null) return false;
        if (!int.TryParse(value.ToString(), out var intValue)) return false;
        if (!int.TryParse(parameter.ToString(), out var intParam)) return false;
        return intValue == intParam;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
