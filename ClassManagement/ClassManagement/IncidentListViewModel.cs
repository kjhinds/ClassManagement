using System.Windows.Input;
using Xamarin.Forms;

namespace ClassManagement
{
    class IncidentListViewModel : ViewModelBase
    {
        private SortableObservableCollection<Incident> incidents;
        private string fullName;
        private bool editMode;

        public bool EditMode
        {
            get { return editMode; }
            set { SetProperty(ref editMode, value); }
        }

        public SortableObservableCollection<Incident> Incidents
        {
            get { return incidents; }
            set { SetProperty(ref incidents, value); }
        }

        public string FullName
        {
            get { return fullName; }
            set { SetProperty(ref fullName, value); }
        }

        public IncidentListViewModel(Student student)
        {
            EditMode = false;
            FullName = student.FullName;
            Incidents = student.Incidents;
            EditCommand = new Command(() => ToggleEditMode());
            ButtonCommand = new Command<string>((arg) => SendMessageToView(arg));
            DetailsCommand = new Command<Incident>((arg) => SendIncidentToView(arg));
            DeleteCommand = new Command<Incident>((arg) => DeleteIncident(arg));
        }

        public ICommand ButtonCommand { protected set; get; }
        private void SendMessageToView(string arg)
        {
            MessagingCenter.Send(this, "Message", arg);
        }

        public ICommand DetailsCommand { protected set; get; }
        private void SendIncidentToView(Incident arg)
        {
            MessagingCenter.Send(this, "Incident", arg);
        }

        public ICommand EditCommand { protected set; get; }
        private void ToggleEditMode()
        {
            EditMode = EditMode ? false : true;
        }

        public ICommand DeleteCommand { protected set; get; }
        private void DeleteIncident(Incident incident)
        {

        }

    }
}
