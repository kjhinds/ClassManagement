using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class StudentListPage : ContentPage
    {
        public SortableObservableCollection<Student> Students;

        public StudentListPage()
        {
            InitializeComponent();
        }

        void OnSelection (object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            var student = e.SelectedItem as Student;
            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
            Navigation.PushAsync(new AddIncidentPage
            {
                Title = student.FullName,
                BindingContext = student,
                Student = student
            }, false);
        }

        public void OnDetails (object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var student = mi.BindingContext as Student;
            Navigation.PushAsync(new IncidentListPage
            {
                Title = student.FullName,
                BindingContext = student,
                Student = student
            }, false);
        }

        public void OnDelete (object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var student = mi.BindingContext as Student;
            Students.Remove(student);
        }

        void OnPreviousPageButtonClicked (object sender, EventArgs e)
        {
            // Page appearance not animated
            Navigation.PopAsync (false);
        }

        void AddStudent(object sender, EventArgs e) {
            Navigation.PushModalAsync(new AddStudentPage { Students = Students }, false);
        }
    }
}

