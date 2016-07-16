using System;

namespace ClassManagement
{
    public class Incident : ViewModelBase, IComparable<Incident>
    {
        #region Private fields
        private DateTime _date;
        private string _behavior;
        private string _intervention;
        private string _comment;
        #endregion

        #region Public properties
        public DateTime Date {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        public string Behavior {
            get { return _behavior; }
            set { SetProperty(ref _behavior, value); }
        }

        public string Intervention {
            get { return _intervention; }
            set { SetProperty(ref _intervention, value); }
        }

        public string Comment {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }
        #endregion

        #region Constructor
        public Incident(DateTime date, string behavior = "", string intervention = "", string comment = "" )
        {
            Date = date;
            Behavior = behavior;
            Intervention = intervention;
            Comment = comment;
        }
        #endregion

        #region IComparer implementation
        public int CompareTo (Incident other)
        {
            return DateTime.Compare (Date, other.Date);
        }
        #endregion

    }
}

