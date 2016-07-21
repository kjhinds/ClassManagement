using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class IncidentListPage : ContentPage
    {
        #region Private fields
        private Student student;
        #endregion

        #region Constructor
        /// <summary>
        /// Create page to list student incidents
        /// </summary>
        /// <param name="student"></param>
        public IncidentListPage(Student student)
        {
            InitializeComponent ();
            this.student = student;
            BindingContext = new IncidentListViewModel(student);
        }
        #endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<IncidentListViewModel, string>(this, "Message",
                (sender, arg) => HandleMessage(arg));
            MessagingCenter.Subscribe<IncidentListViewModel, Incident>(this, "Incident",
                (sender, arg) => HandleIncident(arg));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<IncidentListViewModel, string>(this, "Message");
            MessagingCenter.Unsubscribe<IncidentListViewModel, Incident>(this, "Incident");
        }

        #region Private methods
        /// <summary>
        /// Delete clicked, remove selected incident from collection and
        /// update collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleteMenuItemClicked (object sender, EventArgs e)
        {
            var selectedIncident = ((MenuItem)sender).BindingContext as Incident;
            student.Incidents.Remove(selectedIncident);
        }

        /// <summary>
        /// Incident selected, go to incident page to edit incident.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelection (object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) {
                return;
            }
            ((ListView)sender).SelectedItem = null;
            var selectedIncident = e.SelectedItem as Incident;
            Navigation.PushModalAsync(new IncidentDetailPage(student, selectedIncident), false);
        }

        /// <summary>
        /// Add button clicked, go to incident detail page to add new incident.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddToolbarItemClicked (object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new IncidentDetailPage(student), false);
        }

        private void HandleMessage(string arg)
        {
            switch (arg)
            {
                case "_Cancel_":
                    CloseIncidentListPage();
                    break;
                case "_Add_":
                    ShowIncidentDetailPage();
                    break;
                default:
                    break;
            }
        }

        private void HandleIncident(Incident incident)
        {
            Navigation.PushModalAsync(new IncidentDetailPage(student, incident), false);
        }

        private void CloseIncidentListPage()
        {
            Navigation.PopModalAsync(false);
        }

        private void ShowIncidentDetailPage()
        {
            Navigation.PushModalAsync(new IncidentDetailPage(student), false);
        }
        #endregion
    }
}

