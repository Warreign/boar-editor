using CustomControlLibrary.Misc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BoarEngine.Misc.Converters
{
    public class TypeToIndex : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((PropertyType)value) 
            {
                case PropertyType.VALUE:
                    return 0;
                case PropertyType.BOOL:
                    return 1;
                default:
                    return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case 0:
                    return PropertyType.VALUE;
                case 1:
                    return PropertyType.BOOL;
                default:
                    return PropertyType.NONE;
            }
        }
    }
}
