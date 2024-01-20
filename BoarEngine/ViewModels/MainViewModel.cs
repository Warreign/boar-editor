using BoarEngine.Misc;
using BoarEngine.Models;
using PropertyCustomControl;
using PropertyCustomControl.Misc;
using System.Collections.ObjectModel;

namespace BoarEngine.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public ObservableCollection<PropertyViewModel> Properties { get; set; } = new ObservableCollection<PropertyViewModel>();

        private int _canvasWidth;
        private int _canvasHeight;

        private EngineImitation _engine;


        public int CanvasWidth { get { return _canvasWidth; } set { _canvasWidth = value; } }
        public int CanvasHeight { get { return _canvasHeight; } set { _canvasHeight = value; } }


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

            Properties.Add(new PropertyViewModel(this, "Transform", "Some content #1", PropertyType.TRANSFORM));
            Properties.Add(new PropertyViewModel(this, "Collider", "Some content #2", PropertyType.VALUE));
            Properties.Add(new PropertyViewModel(this,"Terrain", "Some content #3", PropertyType.VALUE));
        }
    }


}
