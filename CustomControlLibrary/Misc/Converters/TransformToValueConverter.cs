using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CustomControlLibrary.Misc;

namespace CustomControlLibrary.Misc.Converters
{
    internal class TransformToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter) 
            {
                case "X":
                    return ((Transform)value).X;
                case "Y":
                    return ((Transform)value).Y;
                case "Z":
                    return ((Transform)value).Z;
                default: 
                    return 0d;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter)
            {
                case "X":
                    return null;
                case "Y":
                    return ((Transform)value).Y;
                case "Z":
                    return ((Transform)value).Z;
                default:
                    return 0d;
            }
        }
    }
}
