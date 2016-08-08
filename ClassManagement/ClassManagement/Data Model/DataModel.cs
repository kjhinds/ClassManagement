﻿using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Diagnostics;

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

        public void AddStudents(string studentData)
        {
            string[] dataRows = studentData.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            for (int i = 1; i < dataRows.Length; i++) {  //skip header row
                string[] rowItems = dataRows[i].Split(',');
                bool studentAdded = false;
                foreach (var period in Periods) {
                    if (period.PeriodName == rowItems[0] && rowItems[1] != null && rowItems[2] != null) 
                    {
                            Student newStudent = new Student(rowItems[1], rowItems[2]);
                            period.Students.Add(newStudent);
                            studentAdded = true;
                            break;
                    }
                }
                if (!studentAdded && rowItems[0] != "" && rowItems[1] != null && rowItems[2] != null)
                {
                    Period newPeriod = new Period(rowItems[0]);
                    Student newStudent = new Student(rowItems[1], rowItems[2]);
                    newPeriod.Students.Add(newStudent);
                    Periods.Add(newPeriod);
                }
            }
            SaveData();
        }

    }
}

