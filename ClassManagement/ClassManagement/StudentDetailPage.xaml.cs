using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class StudentDetailPage : ContentPage
    {
        #region Private fields
        private SortableObservableCollection<Student> _students;
        #endregion

        #region Constructor
        /// <summary>
        /// Create page for adding student first and last name
        /// </summary>
        /// <param name="students"></param>
        public StudentDetailPage(SortableObservableCollection<Student> students)
        {
            InitializeComponent();
            _students = students;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Make sure FirstNameEntry is focused and Done Button is disabled at first.
        /// </summary>
        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            FirstNameEntry.Focus();
            DoneButton.IsEnabled = false;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Finalize student entry info if available and pop page from stack
        /// </summary>
        private void StudentEntryCompleted() {
            if (FirstNameEntry.Text != "" && LastNameEntry.Text != "") {
                _students.Add (new Student (LastNameEntry.Text, FirstNameEntry.Text));
                _students.Sort (Student.GetSortPreference());
                Navigation.PopAsync (false);
            }
            else
            {
                FirstNameEntry.Focus();
            }
        }

        /// <summary>
        /// User entered first name so move focus to LastNameEntry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstNameCompleted(object sender, EventArgs e) {
            LastNameEntry.Focus();
        }

        /// <summary>
        /// User entered last name, so try to finalized student entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastNameCompleted(object sender, EventArgs e)
        {
            StudentEntryCompleted();
        }

        /// <summary>
        /// Cancel button clicked, return to previous screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancelButtonClicked(object sender, EventArgs e) {
            Navigation.PopAsync (false);
        }

        /// <summary>
        /// Done button clicked, try to finalize student entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDoneButtonClicked(object sender, EventArgs e)
        {
            StudentEntryCompleted();
        }

        /// <summary>
        /// Text changed in fields so update done button to enabled if appropriate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleTextChanged (object sender, TextChangedEventArgs e)
        {
            if (FirstNameEntry.Text != "" && LastNameEntry.Text != "") {
                DoneButton.IsEnabled = true;
            } else {
                DoneButton.IsEnabled = false;
            }
        }
        #endregion
    }
}