using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace ClassManagement
{
    class IncidentDetailViewModel : ViewModelBase
    {
        private Student student;
        private Incident incident;
        private DateTime date;
        private string comments;
        private string behavior;
        private string intervention;
        private bool editingIncident;

        public Student Student
        {
            get { return student; }
            set { SetProperty(ref student, value); }
        }

        public SortableObservableCollection<string> BehaviorList
        {
            get { return Settings.BehaviorList; }
        }

        public SortableObservableCollection<string> InterventionList
        {
            get { return Settings.InterventionList; }
        }

        public DateTime Date
        {
            get { return date; }
            set { SetProperty(ref date, value); }
        }

        public string Comments
        {
            get { return comments; }
            set { SetProperty(ref comments, value); }
        }

        public string Behavior
        {
            get { return behavior; }
            set { SetProperty(ref behavior, value); }
        }

        public string Intervention
        {
            get { return intervention; }
            set { SetProperty(ref intervention, value); }
        }

        public Incident Incident
        {
            get { return incident; }
            set { SetProperty(ref incident, value); }
        }

        public IncidentDetailViewModel(Student student, Incident incident)
        {
            Student = student;
            if (incident != null)
            {
                Incident = incident;
                editingIncident = true;
            }
            else
            {
                Incident = new Incident(DateTime.Now);
                editingIncident = false;
            }
            Incident = (incident != null) ? incident : new Incident(DateTime.Now);
            Date = Incident.Date;
            Comments = Incident.Comment;
            Intervention = Incident.Intervention;
            Behavior = Incident.Behavior;

            CommandButton = new Command<string>((arg) => CommandButtonPressed(arg));
        }

        public ICommand CommandButton { protected set; get; }
        private void CommandButtonPressed(string arg)
        {
            switch (arg)
            {
                case "Done":
                    SaveIncident();
                    MessagingCenter.Send(this, "Message", arg);
                    break;
                case "Cancel":
                    MessagingCenter.Send(this, "Message", arg);
                    break;
                default:
                    break;
            }
        }

        private void SaveIncident()
        {
            if (editingIncident)
            {
                Incident.Date = Date;
                Incident.Comment = Comments;
                Incident.Intervention = Intervention;
                Incident.Behavior = Behavior;
                student.Incidents.Sort();
                student.UpdateWorstBehavior();
            }
            else
            {
                Incident = new Incident(Date, Behavior, Intervention, Comments);
                student.Incidents.Add(Incident);
                student.Incidents.Sort();
                student.UpdateWorstBehavior();
            }
        }
    }
}
