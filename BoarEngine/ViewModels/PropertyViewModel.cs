using BoarEngine.Misc;
using CustomControlLibrary;
using CustomControlLibrary.Misc;
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
        public PropertyViewModel(MainViewModel mainViewModel, string name, object content, PropertyType type)
        {
            _mainViewModel = mainViewModel;
            PropertyName = name;
            Content = content;
            Type = type;
            _active = true;
            _expanded = true;
        }

        private MainViewModel _mainViewModel;
        private string _name;
        private object _content;
        private RelayCommand _remove;
        private PropertyType _type;
        private bool _active;
        private bool _expanded;

        private void RemoveProp(object prop)
        {
            _mainViewModel.CurrentProperties.Remove(this);
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

        public object Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public PropertyType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public bool Active
        {
            get => _active;
            set => SetProperty(ref _active, value);
        }

        public bool IsExpanded
        {
            get => _expanded;
            set => SetProperty(ref _expanded, value);
        }

        public RelayCommand Remove
        {
            get { return _remove ?? (_remove = new RelayCommand(RemoveProp, RemovePropCanExecute)); }
        }
    }
}
