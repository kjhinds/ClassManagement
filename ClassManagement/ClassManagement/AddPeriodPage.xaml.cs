using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace ClassManagement
{
    public partial class AddPeriodPage : ContentPage
    {
        public SortableObservableCollection<Period> Periods;
        private Period period;
        bool isEditingPeriod;

        public AddPeriodPage(Period period = null, bool isEditingPeriod = false)
        {
            InitializeComponent();
            this.isEditingPeriod = isEditingPeriod;
            if (isEditingPeriod)
            {
                this.period = period;
                PeriodNameEntry.Text = period.PeriodName;
                StartTimeEntry.Time = period.PeriodStartTime;
                EndTimeEntry.Time = period.PeriodEndTime;
            }
            else
            {
                period = new Period();
            }
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            PeriodNameEntry.Focus ();
            DoneButton.IsEnabled = false;
        }

        void PeriodEntryCompleted(object sender, EventArgs e) {
            period.PeriodName = PeriodNameEntry.Text;
            period.PeriodStartTime = StartTimeEntry.Time.Duration();
            period.PeriodEndTime = EndTimeEntry.Time.Duration();

            if (!isEditingPeriod)
            {
                Periods.Add(period);
            }
   
            Periods.Sort (Period.GetSortPreference());
            Navigation.PopAsync (false);
        }

        void CancelAddPeriod (object sender, EventArgs e)
        {
            Navigation.PopAsync (false);
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

