using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace LIcensesPO.Utils;

public class DateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.ToString("yyyy.MM.dd");
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is string dateStr)
        {
            if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
        }

        // Вернуть null или исходное значение в случае неудачи
        return value;
    }
}