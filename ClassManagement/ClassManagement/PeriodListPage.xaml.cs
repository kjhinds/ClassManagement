using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class PeriodListPage : ContentPage
    {
        #region Private fields
        private DataModel dataModel;
        #endregion

        #region Constructor
        /// <summary>
        /// Create page to list all periods.
        /// </summary>
        /// <param name="dataModel"></param>
        public PeriodListPage(DataModel dataModel)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.dataModel = dataModel;
        }
        #endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<PeriodListViewModel, string>(this, "Message",
                (sender, msg) => HandleMessage(msg));
            // Needed to ensure period list is updated if reset on settings page.
            BindingContext = new PeriodListViewModel(dataModel.Periods);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<PeriodListViewModel, string>(this, "Message");
        }

        #region Private methods

        /// <summary>
        /// Go to student list for selected period
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelection (object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) {
                return; 
            }
            Period period = (Period)e.SelectedItem;
            ((ListView)sender).SelectedItem = null; 
            Navigation.PushModalAsync(new StudentListPage(period), false);
        }

        private void HandleMessage(string msg)
        {
            switch (msg)
            {
                case "_Settings_":
                    OpenSettingsPage();
                    break;
                case "_Add_":
                    AddNewPeriod();
                    break;
                default:
                    ItemDetails(msg);
                    break;
            }
        }

        private void AddNewPeriod()
        {
            Navigation.PushModalAsync(new PeriodDetailPage(dataModel.Periods), false);
        }

        private void OpenSettingsPage()
        {
            Navigation.PushAsync(new SettingsPage(dataModel), false);
        }

        private void ItemDetails(string arg)
        {
            foreach (var period in dataModel.Periods)
            {
                if (period.PeriodName == arg)
                {
                    Navigation.PushModalAsync(new PeriodDetailPage(dataModel.Periods, period), false);
                    break;
                }
            }
        }
        #endregion
    }
}

