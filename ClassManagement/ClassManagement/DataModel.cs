﻿using System;
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
            SettingsData.LoadSettings();
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

