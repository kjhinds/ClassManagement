using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class StudentAddPage : ContentPage
    {
        #region Constructor
        /// <summary>
        /// Create page for adding student first and last name
        /// </summary>
        /// <param name="students"></param>
        public StudentAddPage(SortableObservableCollection<Student> students)
        {
            InitializeComponent();

            BindingContext = new StudentAddViewModel(students);
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
            MessagingCenter.Subscribe<StudentAddViewModel, string>(this, "Message",
                (sender, arg) => HandleMessage(arg));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<StudentAddViewModel, string>(this, "Message");
        }
        #endregion

        #region Private methods
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
            if (DoneButton.IsEnabled)
            {
                DoneButton.Command.Execute(null);
            }
            else
            {
                FirstNameEntry.Focus();
            }
        }

        /// <summary>
        /// Cancel button clicked, return to previous screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancelButtonClicked()
        {
            Navigation.PopModalAsync(false);
        }

        /// <summary>
        /// Done button clicked, try to finalize student entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDoneButtonClicked()
        {
            Navigation.PopModalAsync(false);
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

        private void HandleMessage(string arg)
        {
            switch (arg)
            {
                case "Cancel":
                    OnCancelButtonClicked();
                    break;
                case "Done":
                    OnDoneButtonClicked();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}