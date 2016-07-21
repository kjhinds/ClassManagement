using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ClassManagement
{
    public class DataModel : ViewModelBase
    {
        private const string DATA_FILE = "data.txt";

        private SortableObservableCollection<Period> _periods;

        public SortableObservableCollection<Period> Periods {
            get { return _periods; }
            set { SetProperty(ref _periods, value); }
        }

        public DataModel()
        {
            LoadData();
        }

        public string csvSerialize()
        {
            string csv = "Period,Last Name,First Name,Incident Date,Incident Time,Behavior,Intervention,Comment" + Environment.NewLine;

            foreach (var period in _periods)
            {
                foreach (var student in period.Students)
                {
                    foreach (var incident in student.Incidents)
                    {
                        csv += period.PeriodName + "," +
                                student.LastName + "," +
                                student.FirstName + "," +
                                incident.Date.ToString("d") + "," +
                                incident.Date.ToString("t") + "," +
                                incident.Behavior + "," +
                                incident.Intervention + "," +
                                incident.Comment + Environment.NewLine;
                    }
                }
            }

            return csv;
        }

        public void SaveData() {
            string json = JsonConvert.SerializeObject (_periods);
            DependencyService.Get<ISaveAndLoad>().SaveText(DATA_FILE, json);
        }

        public void LoadData()
        {
            string data = DependencyService.Get<ISaveAndLoad>().LoadText(DATA_FILE);
            if (data == "")
            {
                LoadDefaults();
            }
            else
            { 
                _periods = JsonConvert.DeserializeObject<SortableObservableCollection<Period>>(data);
            }
        }

        public void LoadDefaults()
        {
            Periods = new SortableObservableCollection<Period>();
            Period period = new Period("Sample Period");
            Student student = new Student("Last Name", "First Name");
            Incident incident = new Incident(DateTime.Now, "Sample Behavior", "Sample Intervention", "Sample Comment");
            student.Incidents.Add(incident);
            student.UpdateWorstBehavior();
            period.Students.Add(student);
            Periods.Add(period);
            SaveData();
        }


    }
}

