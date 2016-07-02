using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class AddIncidentPage : ContentPage
    {
        Student student;

        public AddIncidentPage (string title, Student student)
        {
            String [] BehaviorList = { "One", "Two", "Three" };
            String [] InterventionList = { "A", "B", "C", "D", "E","F","G","H","I" };
            InitializeComponent ();
            TitleText.Text = title;
            DateEntry.Date = DateTime.Now.Date;
            TimeEntry.Time = DateTime.Now.TimeOfDay;
            this.student = student;
            BehaviorListView.ItemsSource = BehaviorList;
            InterventionListView.ItemsSource = InterventionList;
        }

        void IncidentEntryCompleted ()
        {
            DateTime currentDateTime = new DateTime (DateEntry.Date.Ticks + TimeEntry.Time.Ticks);
            if (CommentsEntry.Text != "") {
                student.Incidents.Add (new Incident (currentDateTime, 
                                             BehaviorListView.SelectedItem.ToString(), 
                                             InterventionListView.SelectedItem.ToString(), 
                                             CommentsEntry.Text));
                student.Incidents.Sort ();
                student.UpdateWorstBehavior();
                Navigation.PopModalAsync (false);
            }
        }

        void OnAddButtonClicked (object sender, EventArgs e)
        {
            IncidentEntryCompleted ();
        }

        void OnCancelButtonClicked (object sender, EventArgs e)
        {
            Navigation.PopModalAsync (false);
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
            if (BehaviorListView.SelectedItem != null && InterventionListView.SelectedItem != null) {
                IncidentEntryCompleted ();
            }
        }
    }
}

