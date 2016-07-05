using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class IncidentListPage : ContentPage
    {
        #region Private fields
        private Student _student;
        #endregion

        #region Constructor
        /// <summary>
        /// Create page to list student incidents
        /// </summary>
        /// <param name="student"></param>
        public IncidentListPage(Student student)
        {
            InitializeComponent ();
            _student = student;
            Title = _student.FullName;
            BindingContext = _student;
        }
        #endregion

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
            _student.Incidents.Remove(selectedIncident);
            _student.UpdateWorstBehavior();
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
            Navigation.PushAsync(new IncidentDetailPage(_student, selectedIncident, true), false);
        }

        /// <summary>
        /// Add button clicked, go to incident detail page to add new incident.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddToolbarItemClicked (object sender, EventArgs e)
        {
            Navigation.PushAsync(new IncidentDetailPage(_student), false);
        }
        #endregion
    }
}

