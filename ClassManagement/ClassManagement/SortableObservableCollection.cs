using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ClassManagement
{
    public class SortableObservableCollection<T> : ObservableCollection<T> 
    {

        public void Sort () {
            Sort (Comparer<T>.Default);
        }

        public void Sort (IComparer<T> comparer)
        {
            int i, j;
            T index;

            for (i = 1; i < Count; i++) {
                index = this [i];
                j = i;

                while ((j > 0) && (comparer.Compare (this [j - 1], index) == 1)) {
                    this [j] = this [j - 1];
                    j = j - 1;
                }

                this [j] = index;
            }
        }
    }
}

