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

        public void OnDetails (object sender, EventArgs e) {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("More Details Action", menuItem.CommandParameter + " more context action", "OK");
        }

        public void OnDelete (object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var period = mi.CommandParameter as Period;
            Periods.Remove(period);
        }

        void OnSelection (object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            Period period = (Period)e.SelectedItem;
            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
            //Navigation.PushAsync(new StudentListPage((e.SelectedItem as Period).PeriodName, (e.SelectedItem as Period).Students), false);
            Navigation.PushAsync(new StudentListPage {
                Title = period.PeriodName,
                BindingContext = period,
                Students = period.Students
            }, false);
        }

        void AddPeriod(object sender, EventArgs e) {
            Navigation.PushModalAsync(new AddPeriodPage(Periods), false);
        }

        void OnPreviousPageButtonClicked (object sender, EventArgs e)
        {
            // Page appearance not animated
            Navigation.PopAsync (false);
        }

    }
}

