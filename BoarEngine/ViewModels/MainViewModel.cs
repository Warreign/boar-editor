using BoarEngine.Misc;
using BoarEngine.Misc.Helpers;
using CustomControlLibrary;
using CustomControlLibrary.Misc;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Forms;

namespace BoarEngine.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public RangedObservableCollection<PropertyViewModel> CurrentProperties { get; set; } = new RangedObservableCollection<PropertyViewModel>();
        public RangedObservableCollection<FileViewModel> CurrentFiles { get; set; } = new RangedObservableCollection<FileViewModel>();
        public ObservableCollection<ObjectViewModel> Objects { get; set; } = new ObservableCollection<ObjectViewModel>();


        private EngineImitation _engine;
        private ObjectViewModel _selectedObject;
        private PropertyType _typeToAdd;
        private string _nameToAdd;
        private bool _boolToAdd;
        private string _valueToAdd;

        public void SelectObject(object obj)
        {
            if (obj == null)
            {
                _selectedObject = null;
                CurrentProperties.Clear();
                return;
            }

            if (_selectedObject != null)
            {
                _selectedObject.IsSelected = false;
            }
            _selectedObject = (ObjectViewModel)obj;
            //CurrentProperties = _selectedObject.properties;
            CurrentProperties.ReplaceAll(_selectedObject.properties);

            Debug.WriteLine("Selected " + _selectedObject.ObjectName);
        }

        private void PropertiesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    _selectedObject.properties.Add((PropertyViewModel)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    _selectedObject.properties.Remove((PropertyViewModel)e.OldItems[0]);
                    break;
                default:
                    break;
            }
        }

        private void AddNewProperty()
        {
            if (TypeToAdd == PropertyType.BOOL)
            {
                CurrentProperties.Add(new PropertyViewModel(this, NameToAdd, BoolToAdd, TypeToAdd));
            }
            else if (TypeToAdd == PropertyType.VALUE)
            {
                CurrentProperties.Add(new PropertyViewModel(this, NameToAdd, ValueToAdd, TypeToAdd));
                
            }
        }

        private ObjectViewModel AddNewObject(string name)
        {
            var obj = CreateNewObject(name);
            Objects.Add(obj);
            return obj;
        }

        private ObjectViewModel AddNewObjectToSelected(string name)
        {
            var obj = CreateNewObject(name);
            _selectedObject.children.Add(obj);
            return obj;
        }

        private void AddObject(ObjectViewModel obj)
        {
            Objects.Add(obj);
        }

        private ObjectViewModel CreateNewObject(string name)
        {
            var obj = new ObjectViewModel(this, name);
            obj.properties.Add(new PropertyViewModel(this, "Transform", new Transform(0, 0, 0), PropertyType.TRANSFORM));
            return obj;
        }

        private void BrowseFiles()
        {
            dialog = new OpenFileDialog();
            dialog.Title = "Open Folder";
            dialog.InitialDirectory = @"c:\";
            dialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == true)
            {

            }
        }

        private void OpenObjectFile()
        {

        }

        private bool OpenObjectFileCanExecute()
        {
            return true;
        }

        public PropertyType TypeToAdd
        {
            get => _typeToAdd;
            set => SetProperty(ref _typeToAdd, value);
        }

        public string NameToAdd
        {
            get => _nameToAdd;
            set => SetProperty(ref _nameToAdd, value);
        }

        public string ValueToAdd
        {
            get => _valueToAdd;
            set => SetProperty(ref _valueToAdd, value);
        }

        public bool BoolToAdd
        {
            get => _boolToAdd;
            set => SetProperty(ref _boolToAdd, value);
        }
        

        private RelayCommand _runCommand;
        public RelayCommand RunCommand 
        {
            get { return _runCommand ?? (_runCommand = new RelayCommand(obj => _engine.run(), obj => _engine.runnable())); } 
        }

        private RelayCommand _stopCommand;
        public RelayCommand StopCommand
        {
            get { return _stopCommand ?? (_stopCommand = new RelayCommand(obj => _engine.stop(), obj => !_engine.runnable())); }
        }

        private RelayCommand _addPropertyCommand;
        public RelayCommand AddPropertyCommand
        {
            get { return _addPropertyCommand ?? (_addPropertyCommand = new RelayCommand(obj => AddNewProperty(), obj => _selectedObject != null));  }
        }

        private RelayCommand _browseCommand;
        public RelayCommand BrowseCommand
        {
            get { return _browseCommand ?? (_browseCommand = new RelayCommand(obj => BrowseFiles())); }
        }

        private RelayCommand _openObjectFileCommand;
        public RelayCommand OpenObjectFileCommand
        {
            get { return _openObjectFileCommand ?? (_openObjectFileCommand = new RelayCommand(obj => OpenObjectFile(), obj => OpenObjectFileCanExecute())); }
        }

        public MainViewModel()
        {
            _engine = new EngineImitation();

            // Register a method to update objects properties when changed in properties panel
            CurrentProperties.CollectionChanged += new NotifyCollectionChangedEventHandler(PropertiesChanged);


            var obj1 = AddNewObject("Object #1");
            AddNewObject("Object #2");
            AddNewObject("Object #3");
            AddNewObject("Object #4");

            SelectObject(obj1);
            obj1.properties.Add(new PropertyViewModel(this, "Collider", "Some content #2", PropertyType.VALUE));
            obj1.properties.Add(new PropertyViewModel(this, "Terrain", "Some content #3", PropertyType.VALUE));

            AddNewObjectToSelected("SubObject #1");
            var subobj2 = AddNewObjectToSelected("SubObject #1");
            AddNewObjectToSelected("SubObject #1");
            AddNewObjectToSelected("SubObject #1");

            SelectObject(subobj2);
            subobj2.properties.Add(new PropertyViewModel(this, "Enabled", true, PropertyType.BOOL));

            AddNewObjectToSelected("SubSubObject #1");
            AddNewObjectToSelected("SubSubObject #2");
            AddNewObjectToSelected("SubSubObject #3");
            AddNewObjectToSelected("SubSubObject #4");

            SelectObject(null);

            CurrentFiles.Add(new FileViewModel("File #1"));
            CurrentFiles.Add(new FileViewModel("File #2"));
            CurrentFiles.Add(new FileViewModel("File #3"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
            CurrentFiles.Add(new FileViewModel("File #4"));
        }
    }


}
