using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class IncidentDetailPage : ContentPage
    {
        #region Private fields
        private Student _student;
        private Incident _incident;
        private bool isEditingIncident;
        #endregion

        #region Constructor
        /// <summary>
        /// Construct page for adding or editing incident.
        /// </summary>
        /// <param name="incident"></param>
        /// <param name="isEditingIncident"></param>
        public IncidentDetailPage (Student student, Incident incident = null, bool isEditingIncident = false)
        {
            InitializeComponent();
            _student = student;
            Title = _student.FullName;
            BindingContext = _student;
            this.isEditingIncident = isEditingIncident;
            if (isEditingIncident)
            {
                _incident = incident;
                DateEntry.Date = _incident.Date.Date;
                TimeEntry.Time = _incident.Date.TimeOfDay;
                CommentsEntry.Text = _incident.Comment;
                BehaviorListView.SelectedItem = _incident.Behavior;
                InterventionListView.SelectedItem = _incident.Intervention;
            }
            else
            {
                _incident = new Incident(DateTime.Now); 
                DateEntry.Date = DateTime.Now.Date;
                TimeEntry.Time = DateTime.Now.TimeOfDay;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Called when incident entry is completed to finish adding/editing data.
        /// </summary>
        private void IncidentEntryCompleted ()
        {
            DateTime fullDateTime = new DateTime (DateEntry.Date.Ticks + TimeEntry.Time.Ticks);

            _incident.Date = fullDateTime;
            _incident.Behavior = BehaviorListView.SelectedItem.ToString();
            _incident.Intervention = InterventionListView.SelectedItem.ToString();
            _incident.Comment = CommentsEntry.Text;

            if (!isEditingIncident)  // New incident, need to add to collection
            {
                _student.Incidents.Add(_incident);
            }
    
            _student.Incidents.Sort();
            _student.UpdateWorstBehavior();
            Navigation.PopAsync (false);
        }

        /// <summary>
        /// Done button clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDoneButtonClicked (object sender, EventArgs e)
        {
            IncidentEntryCompleted ();
        }

        /// <summary>
        /// Cancel button clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancelButtonClicked (object sender, EventArgs e)
        {
            Navigation.PopAsync (false);
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
                IncidentEntryCompleted ();
            }
        }
        #endregion
    }
}

