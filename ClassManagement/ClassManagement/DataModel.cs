using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using PCLStorage;
using System.Threading.Tasks;

namespace ClassManagement
{
    public class DataModel : ViewModelBase
    {

        private SortableObservableCollection<Period> _periods;

        public SortableObservableCollection<Period> Periods {
            get { return _periods; }
            set { SetProperty(ref _periods, value); }
        }

        public DataModel()
        {
            LoadData();
        }

        public void SaveData() {
            string json = JsonConvert.SerializeObject (_periods);
            DependencyService.Get<ISaveAndLoad>().SaveText("periods.txt", json);
        }

        public void LoadData()
        {
            if (DependencyService.Get<ISaveAndLoad>().FileExists("periods.txt")) {
                string data = DependencyService.Get<ISaveAndLoad>().LoadText("periods.txt");
                _periods = JsonConvert.DeserializeObject<SortableObservableCollection<Period>>(data);
            } else {
                _periods = new SortableObservableCollection<Period>();
                for (int i = 0; i < 3; i++) {
                    Period period = new Period("Period " + i);
                    for (int j = 0; j < 3; j++) {
                        Student student = new Student("Student", j.ToString());
                        for (int k = 0; k < 3; k++) {
                            Incident incident = new Incident(DateTime.Now, "Naughty", "Talked", k.ToString());
                            student.Incidents.Add(incident);
                        }
                        period.Students.Add(student);
                    }
                    _periods.Add(period);
                }
            }
        }


    }
}

