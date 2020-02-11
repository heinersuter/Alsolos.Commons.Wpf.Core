using System;
using System.Globalization;
using System.Windows.Data;

namespace Alsolos.Commons.Wpf.Core.Mvvm.Converters
{
    public class StringToSingleLineStringConverter : IValueConverter
    {
        // ↲ ↵ ⤶ ␍ ⏎ ¶
        public const string Space = " ";
        public const string ReturnArrow = "↵";
        public const string Pilcrow = "¶";

        public StringToSingleLineStringConverter()
        {
            NewLineReplacement = Space;
        }

        public string NewLineReplacement { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue))
            {
                return null;
            }

            return stringValue.Replace(Environment.NewLine, NewLineReplacement, StringComparison.Ordinal);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}