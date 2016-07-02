using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class AddStudentPage : ContentPage
    {
        SortableObservableCollection<Student> students;

        public AddStudentPage(SortableObservableCollection<Student> students)
        {
            InitializeComponent();
            this.students = students;
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            FirstNameEntry.Focus();
            DoneButton.IsEnabled = false;
        }

        void StudentEntryCompleted(object sender, EventArgs e) {
            if (FirstNameEntry.Text != "" && LastNameEntry.Text != "") {
                students.Add (new Student (LastNameEntry.Text, FirstNameEntry.Text));
                students.Sort (Student.GetSortPreference());
                Navigation.PopModalAsync (false);
            }
        }

        void FirstNameCompleted(object sender, EventArgs e) {
            LastNameEntry.Focus();
        }

        void CancelAddStudent(object sender, EventArgs e) {
            Navigation.PopModalAsync (false);
        }

        void Handle_TextChanged (object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (FirstNameEntry.Text != "" && LastNameEntry.Text != "") {
                DoneButton.IsEnabled = true;
            } else {
                DoneButton.IsEnabled = false;
            }
        }
    }
}

