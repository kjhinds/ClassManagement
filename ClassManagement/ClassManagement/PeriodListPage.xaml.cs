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

            SubscribeToMessages();

            this.dataModel = dataModel;
        }
        #endregion

        // Needed to ensure period list is updated if reset on settings page.
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new PeriodListViewModel(dataModel.Periods, Navigation);
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

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<PeriodListViewModel>(this, "Add Period",
                (sender) => AddNewPeriod());
            MessagingCenter.Subscribe<PeriodListViewModel>(this, "Open Settings Page",
                (sender) => OpenSettingsPage());
        }

        private void AddNewPeriod()
        {
            Navigation.PushModalAsync(new PeriodDetailPage(dataModel.Periods), false);
        }

        private void OpenSettingsPage()
        {
            Navigation.PushModalAsync(new SettingsPage(dataModel), false);
        }
        #endregion
    }
}

