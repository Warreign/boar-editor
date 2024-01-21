using BoarEngine.Misc;
using CustomControlLibrary.Misc.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoarEngine.ViewModels
{
    public class FileViewModel : ViewModelBase
    {
        public FileViewModel(string path, FSEntryType type)
        {
            FilePath = path;
            Type = type;
        }

        private string _filepath;
        private FSEntryType _type;

        public string FilePath
        {
            get { return _filepath; }
            set { SetProperty(ref _filepath, value); }
        }

        public FSEntryType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }


    }
}
