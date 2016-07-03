using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class IncidentListPage : ContentPage
    {
        public Student Student;

        public IncidentListPage ()
        {
            InitializeComponent ();
        }

        public void OnDelete (object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var incident = mi.CommandParameter as Incident;
            Student.Incidents.Remove (incident);
            Student.UpdateWorstBehavior();
        }

        public void OnSelection (object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
            Navigation.PushAsync(new AddIncidentPage(e.SelectedItem as Incident, true)
            {
                Title = Student.FullName,
                BindingContext = Student,
                Student = Student
            }, false);
        }

        void AddIncident (object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddIncidentPage
            {
                Title = Student.FullName,
                BindingContext = Student,
                Student = Student
            }, false);
        }
    }
}

