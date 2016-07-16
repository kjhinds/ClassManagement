using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class PeriodDetailView : ContentPage
    {
        #region Private fields
        private SortableObservableCollection<Period> _periods;
        private Period _period;
        private bool isEditingPeriod;
        #endregion

        #region Constructor
        /// <summary>
        /// Create page to enter new period or edit existing period.
        /// </summary>
        /// <param name="periods"></param>
        /// <param name="period"></param>
        /// <param name="isEditingPeriod"></param>
        public PeriodDetailView(SortableObservableCollection<Period> periods, 
                                Period period = null, 
                                bool isEditingPeriod = false)
        {
            InitializeComponent();
            this.isEditingPeriod = isEditingPeriod;
            _periods = periods;
            if (isEditingPeriod)
            {
                _period = period;
                PeriodNameEntry.Text = _period.PeriodName;
                StartTimeEntry.Time = _period.PeriodStartTime;
                EndTimeEntry.Time = _period.PeriodEndTime;
            }
            else
            {
                _period = new Period();
            }
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Make sure period name entry has focus and done button is disabled to start
        /// </summary>
        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            PeriodNameEntry.Focus();
            if (!isEditingPeriod)
            {
                DoneButton.IsEnabled = false;
            }
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
        /// Finalize period entry, add to collection and pop page from stack.
        /// </summary>
        private void PeriodEntryCompleted()
        {
            _period.PeriodName = PeriodNameEntry.Text;
            _period.PeriodStartTime = StartTimeEntry.Time.Duration();
            _period.PeriodEndTime = EndTimeEntry.Time.Duration();

            if (!isEditingPeriod)
            {
                _periods.Add(_period);
            }

            _periods.Sort(Period.GetSortPreference());
            Navigation.PopAsync(false);
        }

        /// <summary>
        /// Done button clicked, finalize period entry information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDoneButtonClicked(object sender, EventArgs e)
        {
            PeriodEntryCompleted();
        }

        /// <summary>
        /// Cancel button clicked, pop page from stack.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync (false);
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
        #endregion
    }
}

