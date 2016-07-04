using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class AddIncidentPage : ContentPage
    {
        public Student Student;
        private Incident incident;
        bool isEditingIncident;

        public AddIncidentPage (Incident incident = null, bool isEditingIncident = false)
        {
            InitializeComponent ();
            this.isEditingIncident = isEditingIncident;
            if (isEditingIncident)
            {
                this.incident = incident;
                DateEntry.Date = incident.Date.Date;
                TimeEntry.Time = incident.Date.TimeOfDay;
                CommentsEntry.Text = incident.Comment;
                BehaviorListView.SelectedItem = incident.Behavior;
                InterventionListView.SelectedItem = incident.Intervention;
            }
            else
            {
                incident = new Incident(); 
                DateEntry.Date = DateTime.Now.Date;
                TimeEntry.Time = DateTime.Now.TimeOfDay;
            }
        }

        void IncidentEntryCompleted ()
        {
            DateTime currentDateTime = new DateTime (DateEntry.Date.Ticks + TimeEntry.Time.Ticks);

            incident.Date = currentDateTime;
            incident.Behavior = BehaviorListView.SelectedItem.ToString();
            incident.Intervention = InterventionListView.SelectedItem.ToString();
            incident.Comment = CommentsEntry.Text;

            if (!isEditingIncident)
            {
                Student.Incidents.Add(incident);
            }
    
            Student.Incidents.Sort ();
            Student.UpdateWorstBehavior();
            Navigation.PopAsync (false);
        }

        void OnAddButtonClicked (object sender, EventArgs e)
        {
            IncidentEntryCompleted ();
        }

        void OnCancelButtonClicked (object sender, EventArgs e)
        {
            Navigation.PopAsync (false);
        }

        void DateEntryCompleted (object sender, EventArgs e)
        {
            CommentsEntry.Focus ();
        }

        void CommentsEntryCompleted (object sender, EventArgs e)
        {
            CommentsEntry.Unfocus ();
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e) {
            CommentsEntry.Unfocus ();
            if (e.SelectedItem == null) {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            if (BehaviorListView.SelectedItem != null && InterventionListView.SelectedItem != null && !isEditingIncident) {
                IncidentEntryCompleted ();
            }
        }
    }
}

