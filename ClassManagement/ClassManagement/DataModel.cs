using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using Plugin.Settings;

namespace ClassManagement
{
    public class DataModel : ViewModelBase
    {
        private const string DATA_FILE = "data.txt";
        public static string[] BehaviorList;
        public static string[] InterventionList;

        private SortableObservableCollection<Period> _periods;

        public SortableObservableCollection<Period> Periods {
            get { return _periods; }
            set { SetProperty(ref _periods, value); }
        }

        public DataModel()
        {
            LoadSettings();
            LoadData();
        }

        public void SaveSettings()
        {
            string csvBehaviorString = string.Join(",", BehaviorList);
            CrossSettings.Current.AddOrUpdateValue("BehaviorList", csvBehaviorString);

            string csvInterventionString = string.Join(",", InterventionList);
            CrossSettings.Current.AddOrUpdateValue("InterventionList", csvInterventionString);
        }

        public void SaveData() {
            string json = JsonConvert.SerializeObject (_periods);
            DependencyService.Get<ISaveAndLoad>().SaveText(DATA_FILE, json);
        }

        public void LoadSettings()
        {
            string csvBehaviorString = CrossSettings.Current.GetValueOrDefault("BehaviorList",
                    "Academic Dishonesty,Bullying,Disrespect,Disruption,Inappropriate Lang," +
                    "Physical Aggression,Technology Violation,Other");
            BehaviorList = csvBehaviorString.Split(',');

            string csvInterventionString = CrossSettings.Current.GetValueOrDefault("InterVentionList",
                    "Apology,Campus Beautification,Classroom Detention,Loss of Privilege,Lunch Detention," +
                    "Parent Contact,Prompting,Proximity,Redirection,Restitution,Seating Change," +
                    "Student Conference,Other");
            InterventionList = csvInterventionString.Split(',');
        }

        public void LoadData()
        {
            if (DependencyService.Get<ISaveAndLoad>().FileExists(DATA_FILE)) {
                string data = DependencyService.Get<ISaveAndLoad>().LoadText(DATA_FILE);
                _periods = JsonConvert.DeserializeObject<SortableObservableCollection<Period>>(data);
            } else {
                LoadDefaults();
            }
        }

        public void LoadDefaults()
        {
            _periods = new SortableObservableCollection<Period>();
            for (int i = 0; i < 3; i++)
            {
                Period period = new Period("Period " + i);
                for (int j = 0; j < 3; j++)
                {
                    Student student = new Student("Student", j.ToString());
                    for (int k = 0; k < 3; k++)
                    {
                        Incident incident = new Incident(DateTime.Now, "Naughty", "Talked", k.ToString());
                        student.Incidents.Add(incident);
                        student.UpdateWorstBehavior();
                    }
                    period.Students.Add(student);
                }
                _periods.Add(period);
            }
        }


    }
}

