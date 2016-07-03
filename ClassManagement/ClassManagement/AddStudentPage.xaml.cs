using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class AddStudentPage : ContentPage
    {
        public SortableObservableCollection<Student> Students;

        public AddStudentPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            FirstNameEntry.Focus();
            DoneButton.IsEnabled = false;
        }

        void StudentEntryCompleted(object sender, EventArgs e) {
            if (FirstNameEntry.Text != "" && LastNameEntry.Text != "") {
                Students.Add (new Student (LastNameEntry.Text, FirstNameEntry.Text));
                Students.Sort (Student.GetSortPreference());
                Navigation.PopModalAsync (false);
            }
        }

        void FirstNameCompleted(object sender, EventArgs e) {
            LastNameEntry.Focus();
        }

        void CancelAddStudent(object sender, EventArgs e) {
            Navigation.PopModalAsync (false);
        }

        void Handle_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (FirstNameEntry.Text != "" && LastNameEntry.Text != "") {
                DoneButton.IsEnabled = true;
            } else {
                DoneButton.IsEnabled = false;
            }
        }
    }
}

