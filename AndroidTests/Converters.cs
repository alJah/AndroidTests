using System;
using System.Globalization;
using Xamarin.Forms;

namespace AndroidTests
{
    /// <summary>
    /// Увеличивет входное значение (индекс массива) на 1
    /// </summary>
    public class PlusOdin : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value + 1;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Вернёт true, если строка не пустая
    /// </summary>
    public class StringToBool : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).Length > 0;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class DeleteB : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).Replace("b", "");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Вернёт красный цвет, если входное значение больше 0
    /// </summary>
    public class IsGreaterZero : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (byte)value > 0 ? Color.Red : Color.Green;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
