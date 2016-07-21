using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class IncidentDetailPage : ContentPage
    {
        #region Private fields
        private bool isEditingIncident;
        #endregion

        #region Constructor
        /// <summary>
        /// Construct page for adding or editing incident.
        /// </summary>
        /// <param name="incident"></param>
        /// <param name="isEditingIncident"></param>
        public IncidentDetailPage (Student student, Incident incident = null)
        {
            InitializeComponent();
            BindingContext = new IncidentDetailViewModel(student, incident);
            isEditingIncident = (incident != null) ? true : false;
            if (isEditingIncident)
            {
                InterventionListView.SelectedItem = incident.Intervention;
                BehaviorListView.SelectedItem = incident.Behavior;
            }
        }
        #endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<IncidentDetailViewModel, string>(this, "Message",
                (sender, arg) => HandleMessage(arg));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<IncidentDetailViewModel, string>(this, "Message");
        }

        /// <summary>
        /// Comments entry "Done" pressed, unfocus field to hide keyboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCommentsEntryCompleted (object sender, EventArgs e)
        {
            CommentsEntry.Unfocus ();
        }

        /// <summary>
        /// ListView item selected, find out if both are selected, quick-add is on, and you
        /// aren't editing an existing incident to auto-complete the entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelection(object sender, SelectedItemChangedEventArgs e) {
            CommentsEntry.Unfocus ();
            if (e.SelectedItem == null) {
                return;
            }
            // TODO: Add check for quick-add setting
            if (Settings.QuickAddMode &&
                BehaviorListView.SelectedItem != null &&
                InterventionListView.SelectedItem != null &&
                !isEditingIncident)
            {
                DoneButton.Command.Execute("Done");
            }
        }

        private void HandleMessage(string arg)
        {
            Navigation.PopModalAsync(false);
        }
        
    }
}

