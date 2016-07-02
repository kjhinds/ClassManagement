using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class AddPeriodPage : ContentPage
    {
        SortableObservableCollection<Period> periods;

        public AddPeriodPage(SortableObservableCollection<Period> periods)
        {
            InitializeComponent();
            this.periods = periods;
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            PeriodNameEntry.Focus ();
            DoneButton.IsEnabled = false;
        }

        void PeriodEntryCompleted(object sender, EventArgs e) {
            if (PeriodNameEntry.Text != "") {
                periods.Add (new Period (PeriodNameEntry.Text));
                periods.Sort (Period.GetSortPreference());
                Navigation.PopModalAsync (false);
            }
        }

        void CancelAddPeriod (object sender, EventArgs e)
        {
            Navigation.PopModalAsync (false);
        }

        void Handle_TextChanged (object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (PeriodNameEntry.Text != "") {
                DoneButton.IsEnabled = true;
            } else {
                DoneButton.IsEnabled = false;
            }
        }
    }
}

