using Xamarin.Forms;

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
            MainPage = new NavigationPage(new PeriodListPage { BindingContext = data, Periods = data.Periods });

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