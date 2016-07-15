using System;
using System.Linq;
using Xamarin.Forms;

namespace ClassManagement
{
    class EditListViewModel : ViewModelBase
    {
        private SortableObservableCollection<string> listToEdit;
        private string listType;

        public static Command TapDownCommand;
        public static Command TapDeleteCommand;
        public static Command TapUpCommand;
            
        public SortableObservableCollection<string> ListToEdit
        {
            get { return listToEdit; }
            set { SetProperty(ref listToEdit, value); }
        }

        public EditListViewModel(string listType)
        {
            this.listType = listType;
            listToEdit = GetList();
            TapDeleteCommand = new Command(arg => DeleteItem(arg));
            TapDownCommand = new Command(arg => MoveItemDown(arg));
            TapUpCommand = new Command(arg => MoveItemUp(arg));
        }

        private SortableObservableCollection<string> GetList()
        {
            if (listType == "Behavior")
            {
                return Settings.BehaviorList;
            }
            else
            {
                return Settings.InterventionList;
            }
        }

        private void SetList()
        {
            if (listType == "Behavior")
            {
                Settings.BehaviorList = listToEdit;
            }
            else
            {
                Settings.InterventionList = listToEdit;
            }
        }

        private void DeleteItem(object arg)
        {
            ListToEdit.Remove(arg.ToString());
            SetList();
        }

        private void MoveItemDown(object arg)
        {
            if (ListToEdit.Last() != arg.ToString())
            {
                int oldIndex = ListToEdit.IndexOf(arg.ToString());
                ListToEdit.Move(oldIndex, oldIndex + 1);
                SetList();
            }
        }

        private void MoveItemUp(object arg)
        {
            if (ListToEdit.First() != arg.ToString())
            {
                int oldIndex = ListToEdit.IndexOf(arg.ToString());
                ListToEdit.Move(oldIndex, oldIndex - 1);
                SetList();
            }
        }

        public void AddItem(object sender, EventArgs e)
        {

            ListToEdit.Add(((Entry)sender).Text);
            SetList();

        }
    }
}
