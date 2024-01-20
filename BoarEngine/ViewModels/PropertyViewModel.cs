using BoarEngine.Misc;
using PropertyCustomControl;
using PropertyCustomControl.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BoarEngine.ViewModels
{
    internal class PropertyViewModel : ViewModelBase
    {
        public PropertyViewModel(MainViewModel mainViewModel, string name, string content, PropertyType type)
        {
            _mainViewModel = mainViewModel;
            PropertyName = name;
            Content = content;
            Type = type;
        }

        private MainViewModel _mainViewModel;
        private string _name;
        private string _content;
        private RelayCommand _remove;
        private PropertyType _type;

        private void RemoveProp(object prop)
        {
            _mainViewModel.Properties.Remove(this);
            Trace.WriteLine("Deleted property " + _name + " " + _type);
        }

        private bool RemovePropCanExecute(object prop)
        {
            return _type != PropertyType.TRANSFORM ;
        }

        public string PropertyName
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public PropertyType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public RelayCommand Remove
        {
            get { return _remove ?? (_remove = new RelayCommand(RemoveProp, RemovePropCanExecute)); }
        }
    }
}
