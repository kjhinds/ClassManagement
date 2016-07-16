using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class PeriodListPage : ContentPage
    {
        #region Private fields
        private SortableObservableCollection<Period> _periods;
        #endregion

        #region Constructor
        /// <summary>
        /// Create page to list all periods.
        /// </summary>
        /// <param name="data"></param>
        public PeriodListPage(DataModel data)
        {
            InitializeComponent();
            BindingContext = data;
            _periods = data.Periods;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Open Period Details Page to adjust start time, name, and import student CSV list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDetailsMenuItemClicked (object sender, EventArgs e) {
            var selectedPeriod = ((MenuItem)sender).BindingContext as Period;
            Navigation.PushAsync(new PeriodDetailView(_periods, selectedPeriod, true), false);
        }


        /// <summary>
        /// Delete selected period
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleteMenuItemClicked (object sender, EventArgs e) {
            var period = ((MenuItem)sender).BindingContext as Period;
            _periods.Remove(period);
        }
        
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
            Navigation.PushAsync(new PeriodDetailView(_periods), false);
        }

        // TODO: Implement settings page.
        /// <summary>
        /// Go to setting page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSettingsToolbarItemClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(BindingContext  as DataModel));
        }
        #endregion
    }
}

