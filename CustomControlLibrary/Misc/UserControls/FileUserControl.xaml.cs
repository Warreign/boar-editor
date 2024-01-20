using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControlLibrary.Misc.UserControls
{
    /// <summary>
    /// Interaction logic for FileUserControl.xaml
    /// </summary>
    public partial class FileUserControl : UserControl
    {
        public FileUserControl()
        {
            InitializeComponent();
        }

        public string FilePath
        {
            get => (string)GetValue(FilepathProperty);
            set => SetValue(FilepathProperty, value);
        }

        public static readonly DependencyProperty FilepathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(FileUserControl));
    }
}
