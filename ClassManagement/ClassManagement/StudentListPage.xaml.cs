using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class StudentListPage : ContentPage
    {
        #region Private fields
        private SortableObservableCollection<Student> students;
        #endregion

        #region Constructor
        /// <summary>
        /// Create page to list students in period
        /// </summary>
        /// <param name="period"></param>
        public StudentListPage(Period period)
        {
            InitializeComponent();

            BindingContext = new StudentListViewModel(period.Students, period.PeriodName);
            students = period.Students;
            students.Sort(Student.GetSortPreference());
        }
        #endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<StudentListViewModel, string>(this, "Message",
                (sender, arg) => HandleMessage(arg));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<StudentListViewModel, string>(this, "Message");
        }

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
            Navigation.PushModalAsync(new IncidentDetailPage(student), false);
        }

        private void HandleMessage(string arg)
        {
            switch (arg)
            {
                case "_Add_":
                    ShowAddStudentView();
                    break;
                case "_Cancel_":
                    CloseStudentListPage();
                    break;
                default:
                    ShowStudentDetailView(arg);
                    break;
            }
        }

        private void ShowAddStudentView()
        {
            Navigation.PushModalAsync(new StudentAddPage(students), false);
        }

        private void CloseStudentListPage()
        {
            Navigation.PopModalAsync(false);
        }

        private void ShowStudentDetailView(string studentName)
        {
            foreach (var student in students)
            {
                if (student.FullName == studentName)
                {
                    Navigation.PushModalAsync(new IncidentListPage(student), false);
                    break;
                }
            }
        }

        #endregion
    }
}

