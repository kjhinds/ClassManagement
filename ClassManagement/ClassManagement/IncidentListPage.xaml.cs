using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class IncidentListPage : ContentPage
    {
        Student student;

        public IncidentListPage (String title, Student student)
        {
            InitializeComponent ();
            Title = title;
            this.student = student;
            IncidentListView.ItemsSource = student.Incidents;
        }

        public void OnDelete (object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var incident = mi.CommandParameter as Incident;
            student.Incidents.Remove (incident);
        }

        public void OnSelection (object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
            //Navigation.PushAsync (new IncidentListPage ((e.SelectedItem as Student).LastFirstName, (e.SelectedItem as Student).Incidents), false);
            DisplayAlert ("Incident Selected", (e.SelectedItem as Incident).Comment, "OK");
        }

        void AddIncident (object sender, EventArgs e)
        {
            Navigation.PushModalAsync (new AddIncidentPage (Title, student), false);
        }
    }
}

