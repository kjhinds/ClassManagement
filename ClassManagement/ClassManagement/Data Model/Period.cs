using System;
using System.Collections.Generic;

namespace ClassManagement
{
    public class Period : ViewModelBase, IComparable<Period>
    {
        #region Private Fields
        private string _periodName;
        private TimeSpan dailyStartTime;
        private TimeSpan dailyEndTime;
        private SortableObservableCollection<TimeSpan> customStartTimes;
        private SortableObservableCollection<TimeSpan> customEndTimes;
        private bool customTimes;
        private SortableObservableCollection<Student> _students;
        #endregion
        
        #region Public Properties
        public string PeriodName {
            get { return _periodName; }
            set { SetProperty(ref _periodName, value); }
        }

        public TimeSpan DailyStartTime
        {
            get { return dailyStartTime; }
            set { SetProperty(ref dailyStartTime, value); }
        } 

        public TimeSpan DailyEndTime
        {
            get { return dailyEndTime; }
            set { SetProperty(ref dailyEndTime, value); }
        }

        public SortableObservableCollection<TimeSpan> CustomStartTimes
        {
            get { return customStartTimes; }
            set { SetProperty(ref customStartTimes, value); }
        }

        public SortableObservableCollection<TimeSpan> CustomEndTimes
        {
            get { return customEndTimes; }
            set { SetProperty(ref customEndTimes, value); }
        }

        public bool CustomTimes
        {
            get { return customTimes; }
            set { SetProperty(ref customTimes, value); }
        }

        public SortableObservableCollection<Student> Students {
            get { return _students; }
            set { SetProperty(ref _students, value); }
        }
        #endregion


        #region Constructor
        public Period (string periodName = "")
        {
            PeriodName = periodName;
            CustomTimes = false;
            Students = new SortableObservableCollection<Student> ();
            CustomStartTimes = new SortableObservableCollection<TimeSpan> {
                new TimeSpan(),
                new TimeSpan(),
                new TimeSpan(),
                new TimeSpan(),
                new TimeSpan() };
            CustomEndTimes = new SortableObservableCollection<TimeSpan> {
                new TimeSpan(),
                new TimeSpan(),
                new TimeSpan(),
                new TimeSpan(),
                new TimeSpan() };

        }
        #endregion

        #region IComparable required methods
        public int CompareTo (Period other)
        {
            return string.Compare (PeriodName, other.PeriodName);
        }

        public static IComparer<Period> GetSortPreference () 
        {
            string preference = "Ascending";
            if (preference == "Ascending") {
                return sortPeriodAscending ();
            }
            return sortPeriodDescending ();
        }

        public static IComparer<Period> sortPeriodDescending ()
        {
            return (IComparer<Period>)new sortPeriodDescendingHelper ();
        }

        public static IComparer<Period> sortPeriodAscending ()
        {
            return (IComparer<Period>)new sortPeriodAscendingHelper ();
        }

        private class sortPeriodDescendingHelper : IComparer<Period>
        {
            int IComparer<Period>.Compare (Period a, Period b)
            {
                return string.Compare (b.PeriodName, a.PeriodName);
            }
        }

        private class sortPeriodAscendingHelper : IComparer<Period>
        {
            int IComparer<Period>.Compare (Period a, Period b)
            {
                return string.Compare (a.PeriodName, b.PeriodName);
            }
        }
        #endregion
    }
}

