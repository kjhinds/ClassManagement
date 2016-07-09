using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class StudentListPage : ContentPage
    {
        #region Private fields
        private SortableObservableCollection<Student> _students;
        #endregion

        #region Constructor
        /// <summary>
        /// Create page to list students in period
        /// </summary>
        /// <param name="period"></param>
        public StudentListPage(Period period)
        {
            InitializeComponent();
            Title = period.PeriodName;
            BindingContext = period;
            _students = period.Students;
            _students.Sort(Student.GetSortPreference());
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Student selected, push page to add new incident to the stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelection (object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) {
                return;
            }
            var student = e.SelectedItem as Student;
            ((ListView)sender).SelectedItem = null;
            Navigation.PushAsync(new IncidentDetailPage(student), false);
        }

        /// <summary>
        /// Details button pushed, go to list of student's incidents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDetailsMenuItemClicked (object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var selectedStudent = mi.BindingContext as Student;
            Navigation.PushAsync(new IncidentListPage(selectedStudent), false);
        }

        /// <summary>
        /// Delete button pushed, delete that student from the period.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleteMenuItemClicked (object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var student = mi.BindingContext as Student;
            _students.Remove(student);
        }

        /// <summary>
        /// Add button pushed, push page to add a new student to the stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddToolbarItemClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new StudentDetailPage(_students), false);
        }
        #endregion
    }
}

