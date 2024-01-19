using BoarEngine.Misc;
using BoarEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoarEngine.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {

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
        }
    }


}
