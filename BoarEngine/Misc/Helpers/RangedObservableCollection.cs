using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace BoarEngine.Misc.Helpers
{
    public class RangedObservableCollection<T> : ObservableCollection<T>
    {
        public RangedObservableCollection()
            : base() 
        {
        }

        public RangedObservableCollection(IEnumerable<T> collection)
            : base(collection) 
        {
        }

        public RangedObservableCollection(List<T> list)
            : base(list) 
        {
        }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                Items.Add(item);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        }

        public void ReplaceAll(IEnumerable<T> range)
        {
            Items.Clear();
            AddRange(range);
        }

    }
}
