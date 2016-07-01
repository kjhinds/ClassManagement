using System;
using System.Collections.Generic;

namespace ClassManagement
{
    public class Student : ViewModelBase, IComparable<Student>
    {
        #region Private Fields
        private string _lastName;
        private string _firstName;
        private string _fullName;
        private SortableObservableCollection<Incident> _incidents;
        private int _worstBehaviorNum;
        private string _worstBehaviorType;
        private string _worstNumAndType;
        #endregion

        #region Public Properties
        public string LastName {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value);
                FullName = _lastName + ", " + _firstName; } // Update to set FullName property based on user settings
        }

        public string FirstName {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value);
                FullName = _lastName + ", " + _firstName; } // Update to set FullName property based on user settings
        }

        public string FullName {
            get { return _fullName; }
            set { SetProperty(ref _fullName, value); }
        }

        public SortableObservableCollection<Incident> Incidents {
            get { return _incidents; }
            set { SetProperty(ref _incidents, value);
                  UpdateWorstBehavior(); }
        }

        public double WorstBehaviorNum {
            get { return _worstBehaviorNum; }
            set { SetProperty(ref _worstBehaviorNum, (int)value); }
        }

        public string WorstBehaviorType {
            get { return _worstBehaviorType; }
            set { SetProperty(ref _worstBehaviorType, value); }
        }

        public string WorstNumAndType {
            get { return _worstNumAndType; }
            set { SetProperty(ref _worstNumAndType, value); }
        }
        #endregion

        #region Constructor
        public Student(string last = "", string first = "")
        {
            LastName = last;
            FirstName = first;
            FullName = last + ", " + first;
            Incidents = new SortableObservableCollection<Incident>();
        }
        #endregion

        #region Public Methods
        public void UpdateWorstBehavior ()
        {
            int highestCount = 0;
            string highestBehavior = "";
            List<string> behaviorList = new List<string>();
            foreach (Incident incident in Incidents) {
                if (incident.Behavior!=null) {
                    behaviorList.Add (incident.Behavior);
                }
            }
            var count = new Dictionary<string, int> ();
            foreach (string behavior in behaviorList) {
                if (count.ContainsKey (behavior)) {
                    count [behavior]++;
                } else {
                    count.Add (behavior, 1);
                }
            }
            foreach (KeyValuePair<string, int> pair in count) {
                if (pair.Value > highestCount) {
                    highestBehavior = pair.Key;
                    highestCount = pair.Value;
                }
            }
            WorstBehaviorNum = highestCount;
            WorstBehaviorType = highestBehavior;
            WorstNumAndType = WorstBehaviorNum + " " + WorstBehaviorType;
        }
        #endregion

        #region IComparer Required Methods
        public int CompareTo (Student other){
            return string.Compare(LastName, other.LastName);
        }

        public static IComparer<Student> GetSortPreference ()
        {
            string preference = "LastName";             // Change to get dictionary property from
            if (preference == "LastName") {             // settings later on.
                return sortStudentLastName ();
            }
            return sortStudentFirstName ();
        }

        public static IComparer<Student> sortStudentFirstName ()
        {
            return (IComparer<Student>)new sortStudentFirstNameHelper ();
        }

        public static IComparer<Student> sortStudentLastName ()
        {
            return (IComparer<Student>)new sortStudentLastNameHelper ();
        }

        private class sortStudentFirstNameHelper : IComparer<Student>
        {
            int IComparer<Student>.Compare (Student a, Student b)
            {
                return string.Compare (b.FirstName, a.FirstName);
            }
        }

        private class sortStudentLastNameHelper : IComparer<Student>
        {
            int IComparer<Student>.Compare (Student a, Student b)
            {
                return string.Compare (a.LastName, b.LastName);
            }
        }
        #endregion
    }
}

