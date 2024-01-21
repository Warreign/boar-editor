using CustomControlLibrary.Misc.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CustomControlLibrary.Misc.Converters
{
    internal class EntryTypeToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string typeString;

            switch ((FSEntryType)value)
            {
                case FSEntryType.FILE:
                    typeString = "file";
                    break;
                case FSEntryType.DIRECTORY:
                    typeString = "directory";
                    break;
                default:
                    typeString = "file";
                    break;
            }

            var path = String.Format("/CustomControlLibrary;component/Images/{0}.png", typeString);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Relative);
            bitmap.EndInit();

            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
