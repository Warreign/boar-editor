using CustomControlLibrary.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CustomControlLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PropertyCustomControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PropertyCustomControl;assembly=PropertyCustomControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:PropertyControl/>
    ///
    /// </summary>
    public class PropertyControl : Expander
    {
        static PropertyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyControl), new FrameworkPropertyMetadata(typeof(PropertyControl)));

            Debug.WriteLine("Debug line");
        }

        public string PName
        {
            get => (string)GetValue(PNameProperty);
            set => SetValue(PNameProperty, value);
        }

        public object Prop
        {
            get => (object)GetValue(PropProperty);
            set => SetValue(PropProperty, value);
        }

        public RelayCommand RemoveCommand
        {
            get => (RelayCommand)GetValue(RemoveProperty);
            set => SetValue(RemoveProperty, value);
        }

        public PropertyType Type
        {
            get => (PropertyType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public bool Active
        {
            get => (bool)GetValue(ActiveProperty);
            set => SetValue(ActiveProperty, value);
        }

        public static readonly DependencyProperty PNameProperty =
             DependencyProperty.Register("PName", typeof(string), typeof(PropertyControl));
        public static readonly DependencyProperty PropProperty =
             DependencyProperty.Register("Prop", typeof(object), typeof(PropertyControl));
        public static readonly DependencyProperty RemoveProperty =
             DependencyProperty.Register("RemoveCommand", typeof(RelayCommand), typeof(PropertyControl));
        public static readonly DependencyProperty TypeProperty =
             DependencyProperty.Register("Type", typeof(PropertyType), typeof(PropertyControl));
        public static readonly DependencyProperty ActiveProperty =
             DependencyProperty.Register("Active", typeof(bool), typeof(PropertyControl));

    }
}
