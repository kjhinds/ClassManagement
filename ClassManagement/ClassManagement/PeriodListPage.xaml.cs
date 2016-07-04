using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class PeriodListPage : ContentPage
    {
        public SortableObservableCollection<Period> Periods;

        public PeriodListPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open Period Details Page to adjust start time, name, and import student CSV list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDetails (object sender, EventArgs e) {
            var menuItem = ((MenuItem)sender);
            Navigation.PushAsync(new AddPeriodPage(menuItem.BindingContext as Period, true) { Periods = Periods }, false);
        }


        /// <summary>
        /// Delete selected period
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDelete (object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var period = mi.BindingContext as Period;
            Periods.Remove(period);
        }
        
        /// <summary>
        /// Go to student list for selected period
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSelection (object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            Period period = (Period)e.SelectedItem;
            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
            Navigation.PushAsync(new StudentListPage {
                Title = period.PeriodName,
                BindingContext = period,
                Students = period.Students
            }, false);
        }

        /// <summary>
        /// Go to period add page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddPeriod(object sender, EventArgs e) {
            Navigation.PushAsync(new AddPeriodPage { Periods = Periods }, false);
        }

        void Settings(object sender, EventArgs e)
        {
            DisplayAlert("Settings", "Settings button clicked", "cancel");
        }
        
        // TODO Implement custom back button to remove animation
        /// <summary>
        /// NYI Go to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPreviousPageButtonClicked (object sender, EventArgs e)
        {
            // Page appearance not animated
            Navigation.PopAsync (false);
        }

    }
}

