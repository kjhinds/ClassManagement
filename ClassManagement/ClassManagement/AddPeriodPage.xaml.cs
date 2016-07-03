using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace ClassManagement
{
    public partial class AddPeriodPage : ContentPage
    {
        public SortableObservableCollection<Period> Periods;

        public AddPeriodPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            PeriodNameEntry.Focus ();
            DoneButton.IsEnabled = false;
        }

        void PeriodEntryCompleted(object sender, EventArgs e) {
            if (PeriodNameEntry.Text != "") {
                Periods.Add (new Period (PeriodNameEntry.Text));
                Periods.Sort (Period.GetSortPreference());
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

