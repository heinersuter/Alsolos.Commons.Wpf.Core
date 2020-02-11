using System;
using System.Globalization;
using System.Windows;

namespace Alsolos.Commons.Wpf.Core.Mvvm.Converters
{
    public class NullToVisibilityConverter : ValueConverter
    {
        public NullToVisibilityConverter()
        {
            NullVisibility = Visibility.Collapsed;
            NotNullVisibility = Visibility.Visible;
        }

        public Visibility NullVisibility { get; set; }

        public Visibility NotNullVisibility { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isNull = value == null;

            if (parameter != null)
            {
                isNull = !isNull;
            }

            return isNull ? NullVisibility : NotNullVisibility;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
