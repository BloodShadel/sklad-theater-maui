using System.Globalization;

namespace SkladTheater.Maui.Converters;

public class StringNotEmptyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var str = value?.ToString();
        return !string.IsNullOrWhiteSpace(str);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
