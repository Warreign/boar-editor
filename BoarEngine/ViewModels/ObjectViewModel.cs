using BoarEngine.Misc;
using CustomControlLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BoarEngine.ViewModels
{
    internal class ObjectViewModel : ViewModelBase
    {
        public ObjectViewModel(MainViewModel mainViewModel, string name) 
        { 
            _mainViewModel = mainViewModel;
            ObjectName = name;
            IsSelected = false;
        }

        public ObservableCollection<ObjectViewModel> children { get; set; } = new ObservableCollection<ObjectViewModel>();
        public Collection<PropertyViewModel> properties { get; set; } = new Collection<PropertyViewModel>();

        private bool _isSelected;
        private string _name;
        private MainViewModel _mainViewModel;

        public void AddChild(ObjectViewModel val)
        {
            children.Add(val);
        }

        public void RemoveChild(ObjectViewModel val)
        {
            if (val == null) throw new ArgumentNullException(nameof(val));
            children.Remove(val);
        }


        private RelayCommand _selectedObjectCommand;
        public RelayCommand SelectObjectCommand
        {
            get { return _selectedObjectCommand ?? (_selectedObjectCommand = new RelayCommand(obj => _mainViewModel.SelectObject(this))); }
        }

        public string ObjectName 
        {
            get => _name; 
            set => SetProperty(ref _name, value); 
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

    }
}
