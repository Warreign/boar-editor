using CustomControlLibrary.Misc.ValidationRules;
using BoarEngine.Misc;
using BoarEngine.Misc.Helpers;
using CustomControlLibrary;
using CustomControlLibrary.Misc;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Input;
using System;

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
        private FileViewModel _selectedFile;

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

        // Update object property collections when adding or removing proeprties
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

        // Add property based on type
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

        // Check if there is a seelcted object and if the name isn't empty
        private bool AddPropertyCanExecute()
        {
            return _selectedObject != null;
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

        // Create an empty object with mandatory transform proprety 
        private ObjectViewModel CreateNewObject(string name)
        {
            var obj = new ObjectViewModel(this, name);
            obj.properties.Add(new PropertyViewModel(this, "Transform", new Transform(0, 0, 0), PropertyType.TRANSFORM));
            return obj;
        }

        private void BrowseFiles()
        {
            Collection<FileViewModel> newFiles = new Collection<FileViewModel>();
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Open Folder";
            dialog.InitialDirectory = @"c:\";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Debug.WriteLine("Chosen file " + dialog.FileName);

                DirectoryInfo d = new DirectoryInfo(dialog.FileName); ;
                FileInfo[] infos = d.GetFiles();
                DirectoryInfo[] dinfos = d.GetDirectories();

                Debug.WriteLine(string.Format("{0} directories and {1} files.", infos.GetLength(0), dinfos.GetLength(0)));

                foreach (var i in infos)
                {
                    newFiles.Add(new FileViewModel(i.Name, FSEntryType.FILE));
                }

                foreach (var di in dinfos)
                {
                    newFiles.Add(new FileViewModel(di.Name, FSEntryType.DIRECTORY));
                }

                CurrentFiles.ReplaceAll(newFiles);
            }

        }

        private void OpenObjectFile()
        {
            Debug.WriteLine("Selected file " + SelectedFile.FilePath);

            string name = SelectedFile.FilePath;

            if (_selectedObject == null)
            {
                AddNewObject(name);
            }
            else
            {
                AddNewObjectToSelected(name);
            }
        }

        private bool OpenObjectFileCanExecute()
        {
            return SelectedFile != null && SelectedFile.Type == FSEntryType.FILE;
        }

        public PropertyType TypeToAdd
        {
            get => _typeToAdd;
            set => SetProperty(ref _typeToAdd, value);
        }

        public string NameToAdd
        {
            get => _nameToAdd;
            set
            {
                SetProperty(ref _nameToAdd, value);
            }
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

        public FileViewModel SelectedFile
        {
            get => _selectedFile;
            set => SetProperty(ref _selectedFile, value);
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
            get { return _addPropertyCommand ?? (_addPropertyCommand = new RelayCommand(obj => AddNewProperty(), obj => AddPropertyCanExecute()));  }
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

            CurrentFiles.Add(new FileViewModel("File #1", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #2", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #3", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
            CurrentFiles.Add(new FileViewModel("File #4", FSEntryType.FILE));
        }
    }


}
