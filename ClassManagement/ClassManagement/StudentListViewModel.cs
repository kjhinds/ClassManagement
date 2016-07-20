using Xamarin.Forms;
using System.Windows.Input;

namespace ClassManagement
{
    class StudentListViewModel : ViewModelBase
    {
        private SortableObservableCollection<Student> students;
        private string periodName;
        private bool editMode;

        public string PeriodName
        {
            get { return periodName; }
            set { SetProperty(ref periodName, value); }
        }

        public bool EditMode
        {
            get { return editMode; }
            set { SetProperty(ref editMode, value); }
        }

        public SortableObservableCollection<Student> Students
        {
            get { return students; }
            set { SetProperty(ref students, value); }
        }

        public StudentListViewModel(SortableObservableCollection<Student> students, string periodName)
        {
            Students = students;
            PeriodName = periodName;
            editMode = false;
            EditCommand = new Command(() => ToggleEditMode());
            ButtonCommand = new Command<string>((arg) => SendMessageToView(arg));
            DeleteCommand = new Command<string>((arg) => DeleteStudent(arg));
        }

        public ICommand EditCommand { protected set; get; }
        private void ToggleEditMode()
        {
            EditMode = EditMode ? false : true;
        }

        public ICommand ButtonCommand { protected set; get; }
        private void SendMessageToView(string arg)
        {
            MessagingCenter.Send(this, "Message", arg);
        }

        public ICommand DeleteCommand { protected set; get; }
        private void DeleteStudent(string studentName)
        {
            foreach (var student in Students)
            {
                if (student.FullName == studentName)
                {
                    Students.Remove(student);
                    break;
                }
            }
        }

    }
}
