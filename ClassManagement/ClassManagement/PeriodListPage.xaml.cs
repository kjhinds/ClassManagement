using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class PeriodListPage : ContentPage
    {
        #region Private fields
        private SortableObservableCollection<Period> _periods;
        private PeriodListViewModel viewModel;
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
            viewModel = new PeriodListViewModel(dataModel.Periods, Navigation);
            this.dataModel = dataModel;
            BindingContext = viewModel;
            _periods = dataModel.Periods;
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
            Navigation.PushAsync(new StudentListPage(period), false);
        }

        /// <summary>
        /// Go to period add page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddToolbarItemClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new PeriodDetailPage(_periods), false);
        }

        // TODO: Implement settings page.
        /// <summary>
        /// Go to setting page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSettingsToolbarItemClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(dataModel  as DataModel));
        }

        private void OnEditToolbarItemClicked(object sender, EventArgs e)
        {
            viewModel.ToggleEditMode();
        }
        #endregion
    }
}

