using System;
using System.Collections.Generic;

namespace ClassManagement
{
    public class Period : ViewModelBase, IComparable<Period>
    {
        #region Private Fields
        private string _periodName;
        private SortableObservableCollection<Student> _students;
        #endregion


        #region Public Properties
        public string PeriodName {
            get { return _periodName; }
            set { SetProperty(ref _periodName, value); }
        }

        public SortableObservableCollection<Student> Students {
            get { return _students; }
            set { SetProperty(ref _students, value); }
        }
        #endregion


        #region Constructor
        public Period (string PeriodName = "")
        {
            this.PeriodName = PeriodName;
            Students = new SortableObservableCollection<Student> ();
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

