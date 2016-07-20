using System.Windows.Input;
using Xamarin.Forms;

namespace ClassManagement
{
    class StudentAddViewModel : ViewModelBase
    {
        private SortableObservableCollection<Student> students;
        private string firstName;
        private string lastName;

        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        public SortableObservableCollection<Student> Students
        {
            get { return students; }
            set { SetProperty(ref students, value); }
        }

        public StudentAddViewModel(SortableObservableCollection<Student> students)
        {
            Students = students;
            CancelButton = new Command(() => OnCancel());
            DoneButton = new Command(() => OnDone());
        }

        public ICommand CancelButton { protected set; get; }
        private void OnCancel()
        {
            MessagingCenter.Send(this, "Message", "Cancel");
        }

        public ICommand DoneButton { protected set; get; }
        private void OnDone()
        {
            Student student = new Student(LastName, FirstName);
            students.Add(student);
            students.Sort(Student.GetSortPreference());
            MessagingCenter.Send(this, "Message", "Done");
        }
    }
}
