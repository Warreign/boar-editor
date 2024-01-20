using BoarEngine.Misc;
using BoarEngine.Misc.Helpers;
using BoarEngine.Models;
using PropertyCustomControl;
using PropertyCustomControl.Misc;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace BoarEngine.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public RangedObservableCollection<PropertyViewModel> CurrentProperties { get; set; } = new RangedObservableCollection<PropertyViewModel>();
        public ObservableCollection<ObjectViewModel> Objects { get; set; } = new ObservableCollection<ObjectViewModel>(); 


        private EngineImitation _engine;
        private ObjectViewModel _selectedObject;

        public void SelectObject(object obj)
        {
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

        public MainViewModel()
        {
            _engine = new EngineImitation();

            // Register a method to update objects properties when changed in properties panel
            CurrentProperties.CollectionChanged += new NotifyCollectionChangedEventHandler(PropertiesChanged);


            var obj1 = new ObjectViewModel(this, "Obj 1");
            //SelectObject(obj1);
            obj1.properties.Add(new PropertyViewModel(this, "Transform", "Some content #1", PropertyType.TRANSFORM));
            obj1.properties.Add(new PropertyViewModel(this, "Collider", "Some content #2", PropertyType.VALUE));
            obj1.properties.Add(new PropertyViewModel(this, "Terrain", "Some content #3", PropertyType.VALUE));
            Objects.Add(obj1);
            obj1.AddChild(new ObjectViewModel(this, "SubObj 1"));
            var subobj2 = new ObjectViewModel(this, "SubObj 2");
            obj1.AddChild(subobj2);
            subobj2.AddChild(new ObjectViewModel(this, "SubSubObj 1"));
            subobj2.AddChild(new ObjectViewModel(this, "SubSubObj 2"));
            subobj2.AddChild(new ObjectViewModel(this, "SubSubObj 3"));
            subobj2.AddChild(new ObjectViewModel(this, "SubSubObj 4"));

            obj1.AddChild(new ObjectViewModel(this, "SubObj 3"));
            obj1.AddChild(new ObjectViewModel(this, "SubObj 4"));


            Objects.Add(new ObjectViewModel(this, "Obj 2"));
            Objects.Add(new ObjectViewModel(this, "Obj 3"));
            Objects.Add(new ObjectViewModel(this, "Obj 4"));
        }
    }


}
