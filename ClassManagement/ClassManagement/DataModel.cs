using Newtonsoft.Json;
using PCLStorage;
using System.Threading.Tasks;
using System;

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

        public void LoadData()
        {
            string data = GetData();
            if (data != string.Empty)
            {
                _periods = JsonConvert.DeserializeObject<SortableObservableCollection<Period>>(data);
            }
            else
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
                        }
                        period.Students.Add(student);
                    }
                    _periods.Add(period);
                }
            }
        }

        public string GetData()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            if (rootFolder.CheckExistsAsync("data.txt").Result == ExistenceCheckResult.FileExists)
            {
                IFile file = rootFolder.GetFileAsync("data.txt").Result;
                //return file.ReadAllTextAsync().Result; //This wouldn't work. You will hit deadlock. 
                return ReadFile(file, "data.txt").Result;
            }
            return string.Empty;
        }

        public async Task<string> ReadFile(IFile f, string fileName)
        {
            return await Task.Run(() => f.ReadAllTextAsync()).ConfigureAwait(false);
        }

        public void SaveData()
        {
            string json = JsonConvert.SerializeObject(_periods);

            // get hold of the file system
            IFolder rootFolder = FileSystem.Current.LocalStorage;

            // create a file, overwriting any existing file
            IFile file = rootFolder.CreateFileAsync("data.txt", CreationCollisionOption.ReplaceExisting).Result;

            // populate the file with some text
            file.WriteAllTextAsync(json);
        }

    }
}

