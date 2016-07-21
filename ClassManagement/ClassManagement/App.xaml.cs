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

            // The root page of your application
            MainPage = new NavigationPage(new PeriodListPage(data));
            NavigationPage.SetHasNavigationBar(this, false);

            // Navigate to current period if setting enabled.
            Days.Add("Monday", 0);
            Days.Add("Tuesday", 1);
            Days.Add("Wednesday", 2);
            Days.Add("Thursday", 3);
            Days.Add("Friday", 4);

            if (Settings.JumpToPeriod)
            {
                NavigateToPeriod();
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            data.SaveData();
        }

        protected override void OnResume()
        {
            data.LoadData();
            NavigateToPeriod();
        }

        private void NavigateToPeriod()
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
