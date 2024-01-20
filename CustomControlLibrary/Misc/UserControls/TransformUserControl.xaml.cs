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
    /// Interaction logic for TransformUserControl.xaml
    /// </summary>
    public partial class TransformUserControl : UserControl
    {
        public TransformUserControl()
        {
            InitializeComponent();
        }

        public string X
        {
            get => (string)GetValue(XProperty);
            set => SetValue(XProperty, value);
        }
        public string Y
        {
            get => (string)GetValue(YProperty);
            set => SetValue(YProperty, value);
        }
        public string Z
        {
            get => (string)GetValue(ZProperty);
            set => SetValue(ZProperty, value);
        }

        public static readonly DependencyProperty XProperty =
             DependencyProperty.Register("X", typeof(string), typeof(TransformUserControl));
        public static readonly DependencyProperty YProperty =
             DependencyProperty.Register("Y", typeof(string), typeof(TransformUserControl));
        public static readonly DependencyProperty ZProperty =
             DependencyProperty.Register("Z", typeof(string), typeof(TransformUserControl));
    }
}
