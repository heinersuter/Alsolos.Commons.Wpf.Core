using System;
using System.Globalization;

namespace Alsolos.Commons.Wpf.Core.Mvvm.Converters
{
    public class BoolToInverseBoolConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isTrue = (value as bool?) == true;
            return !isTrue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isTrue = (value as bool?) == true;
            return !isTrue;
        }
    }
}