using BoarEngine.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoarEngine.ViewModels
{
    public class FileViewModel : ViewModelBase
    {
        public FileViewModel(string path)
        {
            FilePath = path;
        }

        private string _filepath;

        public string FilePath
        {
            get { return _filepath; }
            set { SetProperty(ref _filepath, value); }
        }
    }
}
