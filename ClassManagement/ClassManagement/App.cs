using Xamarin.Forms;
using System;

namespace ClassManagement
{
    public class App : Application
    {
        public DataModel data;

        public App()
        {
            Toolkit.Init();
            data = new DataModel();

            // The root page of your application
            MainPage = new NavigationPage(new PeriodListPage(data));

            // TODO: Add check for going to current period once settings are implemented
            double currentTimeMinutes = DateTime.Now.TimeOfDay.TotalMinutes;
            foreach (var period in data.Periods)
            {
                if (currentTimeMinutes >= period.PeriodStartTime.TotalMinutes && currentTimeMinutes <= period.PeriodEndTime.TotalMinutes)
                {
                    MainPage.Navigation.PushAsync(new StudentListPage(period), false);
                    break;
                }
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
        }
    }
}