using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PseudoLocalizeThis.Wpf.Converters
{
    /// <summary>
    /// Converts a null to a visibility.
    /// </summary>
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}