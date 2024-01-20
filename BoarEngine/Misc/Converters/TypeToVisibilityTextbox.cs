using CustomControlLibrary.Misc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BoarEngine.Misc.Converters
{
    internal class TypeToVisibilityTextbox : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((ComboBoxItem)value).Content.Equals("Value"))
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;    
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
