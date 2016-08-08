using Xamarin.Forms;
using System;
using System.Collections.Generic;

namespace ClassManagement
{
    public partial class App : Application
    {
        public DataModel data;

        Dictionary<string, int> Days = new Dictionary<string, int>();

        public App()
        {
            InitializeComponent();

            Toolkit.Init();
            data = new DataModel();
           
            Days.Add("Monday", 0);
            Days.Add("Tuesday", 1);
            Days.Add("Wednesday", 2);
            Days.Add("Thursday", 3);
            Days.Add("Friday", 4);
            Days.Add("Saturday", 5);
            Days.Add("Sunday", 6);
            
        }

        protected override void OnStart()
        {
            data.LoadData();

            MainPage = new NavigationPage(new PeriodListPage(data));
            NavigationPage.SetHasNavigationBar(this, false);

            NavigateToPeriod();
        }

        protected override void OnSleep()
        {
            data.SaveData();
        }

        protected override void OnResume()
        {
            data.LoadData();

            MainPage = new NavigationPage(new PeriodListPage(data));
            NavigationPage.SetHasNavigationBar(this, false);

            NavigateToPeriod();
        }

        public void ImportStudentList(string filePath)
        {
            data.AddStudents(filePath);
        }

        private void NavigateToPeriod()
        {
            if (Settings.JumpToPeriod)
            {
                double currentTimeMinutes = DateTime.Now.TimeOfDay.TotalMinutes;
                string currentDayOfWeek = DateTime.Now.DayOfWeek.ToString();
                int dayIndex = Days[currentDayOfWeek];

                foreach (var period in data.Periods)
                {
                    if (period.CustomTimes)
                    {

                        if (currentTimeMinutes >= period.CustomStartTimes[dayIndex].TotalMinutes &&
                                currentTimeMinutes <= period.CustomEndTimes[dayIndex].TotalMinutes)
                        {
                            MainPage.Navigation.PushModalAsync(new StudentListPage(period), false);
                            break;
                        }
                    }
                    else
                    {
                        if (currentTimeMinutes >= period.DailyStartTime.TotalMinutes && currentTimeMinutes <= period.DailyEndTime.TotalMinutes)
                        {
                            MainPage.Navigation.PushModalAsync(new StudentListPage(period), false);
                            break;
                        }
                    }
                }
            }
        }
    }
}
