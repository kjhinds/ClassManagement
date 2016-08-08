using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class PeriodDetailPage : ContentPage
    {
        #region Constructor
        /// <summary>
        /// Create page to enter new period or edit existing period.
        /// </summary>
        /// <param name="periods"></param>
        /// <param name="period"></param>
        /// <param name="isEditingPeriod"></param>
        public PeriodDetailPage(SortableObservableCollection<Period> periods, 
                                Period period = null)
        {
            InitializeComponent();

            SubscribeToMessages();

            BindingContext = new PeriodDetailViewModel(periods, period);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Make sure period name entry has focus
        /// </summary>
        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            PeriodNameEntry.Focus();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Period name entry box done clicked, unfocus and hide keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PeriodNameEntryCompleted(object sender, EventArgs e)
        {
            PeriodNameEntry.Unfocus();
        }

        /// <summary>
        /// Text entered, so update done button enable status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if (PeriodNameEntry.Text != "") {
                DoneButton.IsEnabled = true;
            } else {
                DoneButton.IsEnabled = false;
            }
        }

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<PeriodDetailViewModel>(this, "Duplicate Period",
                (sender) => DuplicatePeriodAlert());

            MessagingCenter.Subscribe<PeriodDetailViewModel>(this, "Duplicate Period",
                (sender) => NoPeriodNameAlert());

            MessagingCenter.Subscribe<PeriodDetailViewModel>(this, "Close Period Detail Page",
                (sender) => ClosePeriodDetailPage());
        }

        private void DuplicatePeriodAlert()
        {
            DisplayAlert("Duplicate Period", "Period name already in use", "OK");
        }

        private void NoPeriodNameAlert()
        {
            DisplayAlert("Enter Period Name", "Please enter a period name", "OK");
        }

        private void ClosePeriodDetailPage()
        {
            Navigation.PopModalAsync(false);
            MessagingCenter.Unsubscribe<PeriodDetailViewModel>(this, "Duplicate Period");
            MessagingCenter.Unsubscribe<PeriodDetailViewModel>(this, "Close Period Detail Page");
        }

        private void OnAdvancedSwitchToggled(object sender, EventArgs e)
        {
            bool switchState = ((Switch)sender).IsToggled;
            DailyTimesStack.IsVisible = !switchState;
            CustomTimesGrid.IsVisible = switchState;            
        }
        #endregion
    }
}

