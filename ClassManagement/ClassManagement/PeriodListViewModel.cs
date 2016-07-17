using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ClassManagement
{
    class PeriodListViewModel : ViewModelBase
    {
        #region Private Fields
        private SortableObservableCollection<Period> periods;
        //private string periodName;
        //private TimeSpan _periodStartTime;
        //private TimeSpan _periodEndTime;
        //private SortableObservableCollection<Student> _students;
        #endregion

        #region Public Properties

        public SortableObservableCollection<Period> Periods
        {
            get { return periods; }
            set { SetProperty(ref periods, value); }
        }



        //public string PeriodName
        //{
        //    get { return periodName; }
        //    set { SetProperty(ref periodName, value); }
        //}

        //public TimeSpan PeriodStartTime
        //{
        //    get { return _periodStartTime; }
        //    set { SetProperty(ref _periodStartTime, value); }
        //}

        //public TimeSpan PeriodEndTime
        //{
        //    get { return _periodEndTime; }
        //    set { SetProperty(ref _periodEndTime, value); }
        //}

        //public SortableObservableCollection<Student> Students
        //{
        //    get { return _students; }
        //    set { SetProperty(ref _students, value); }
        //}
        #endregion


        #region Constructor
        public PeriodListViewModel(SortableObservableCollection<Period> periods)
        {
            Periods = periods;
            //PeriodName = Period.PeriodName;
        }
        #endregion
    }
}
