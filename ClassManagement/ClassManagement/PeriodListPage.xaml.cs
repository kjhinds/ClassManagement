using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class PeriodListPage : ContentPage
    {
        #region Private fields
        private SortableObservableCollection<Period> periods;
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

            BindingContext = new PeriodListViewModel(dataModel.Periods, Navigation);
            this.dataModel = dataModel;
            periods = dataModel.Periods;
        }
        #endregion

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
            Navigation.PushModalAsync(new PeriodDetailPage(periods), false);
        }

        private void OpenSettingsPage()
        {
            Navigation.PushModalAsync(new SettingsPage(dataModel), false);
        }
        #endregion
    }
}

