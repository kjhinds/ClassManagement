using System;
using System.Collections.Generic;

namespace ClassManagement
{
    public class Student : ViewModelBase, IComparable<Student>
    {
        #region Private Fields
        private string _lastName;
        private string _firstName;
        private SortableObservableCollection<Incident> _incidents;
        private string _worstNumAndType;
        private double _worstBehaviorNum;  // Needed to update color coding on student list
        #endregion

        #region Public Properties
        public string LastName {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        public string FirstName {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        public string FullName {
            get 
            {
                if (Settings.LastNameFirstSetting) 
                {
                    return _lastName + ", " + _firstName;
                } else 
                {
                    return _firstName + " " + _lastName;
                }
            }
        }

        public SortableObservableCollection<Incident> Incidents {
            get { return _incidents; }
            set { SetProperty(ref _incidents, value);
                  UpdateWorstBehavior(); }
        }

        public string WorstNumAndType {
            get { return _worstNumAndType; }
            set { SetProperty(ref _worstNumAndType, value); }
        }

        public double WorstBehaviorNum
        {
            get { return _worstBehaviorNum; }
            set { SetProperty(ref _worstBehaviorNum, value); }
        }
        #endregion

        #region Constructor
        public Student(string last = "", string first = "")
        {
            LastName = last;
            FirstName = first;
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
            WorstNumAndType = highestCount + " " + highestBehavior;
        }
        #endregion

        #region IComparer Required Methods
        public int CompareTo (Student other){
            return string.Compare(LastName, other.LastName);
        }

        public static IComparer<Student> GetSortPreference ()
        {
            if (Settings.LastNameFirstSetting) {
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
                return string.Compare (a.FirstName, b.FirstName);
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

