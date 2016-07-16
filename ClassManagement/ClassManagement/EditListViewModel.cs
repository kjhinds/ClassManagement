using System;
using System.Linq;
using Xamarin.Forms;

namespace ClassManagement
{
    class EditListViewModel : ViewModelBase
    {
        private SortableObservableCollection<ItemToEdit> listToEdit;
        private string listType;

        public static Command TapDownCommand;
        public static Command TapDeleteCommand;
        public static Command TapUpCommand;

        public class ItemToEdit : ViewModelBase
        {
            private string listItem;
            private double upOpacity;
            private double downOpacity;

            public string ListItem
            {
                get { return listItem; }
                set { SetProperty(ref listItem, value); }
            }

            public double UpOpacity
            {
                get { return upOpacity; }
                set { SetProperty(ref upOpacity, value); }
            }

            public double DownOpacity
            {
                get { return downOpacity; }
                set { SetProperty(ref downOpacity, value); }
            }
        }

        private void UpdateItemOpacities()
        {
            for (int i = 0; i < ListToEdit.Count; i++)
            {
                ListToEdit[i].UpOpacity = (i == 0) ? 0 : 1;
                ListToEdit[i].DownOpacity = (i == ListToEdit.Count - 1) ? 0 : 1;
            }
        }

        public SortableObservableCollection<ItemToEdit> ListToEdit
        {
            get { return listToEdit; }
            set { SetProperty(ref listToEdit, value); }
        }

        public EditListViewModel(string listType)
        {
            this.listType = listType;
            listToEdit = BuildList();
            TapDeleteCommand = new Command(arg => DeleteItem(arg));
            TapDownCommand = new Command(arg => MoveItemDown(arg));
            TapUpCommand = new Command(arg => MoveItemUp(arg));
        }

        private SortableObservableCollection<ItemToEdit> BuildList()
        {
            SortableObservableCollection<string> currentList;
            if (listType == "Behavior")
            {
                currentList = Settings.BehaviorList;
            }
            else
            {
                currentList = Settings.InterventionList;
            }
            SortableObservableCollection<ItemToEdit> listOfItems = new SortableObservableCollection<ItemToEdit>();
            for (int i = 0; i < currentList.Count; i++)
            {
                double upOpacity = (i == 0) ? 0 : 1;
                double downOpacity = (i == currentList.Count - 1) ? 0 : 1;
                ItemToEdit item = new ItemToEdit() { ListItem = currentList[i], UpOpacity = upOpacity, DownOpacity = downOpacity };
                listOfItems.Add(item);
            }
            return listOfItems;
        }

        private void SetList()
        {
            SortableObservableCollection<string> listOfItems = new SortableObservableCollection<string>();
            foreach (var item in ListToEdit)
            {
                listOfItems.Add(item.ListItem);
            }
            if (listType == "Behavior")
            {
                Settings.BehaviorList = listOfItems;
            }
            else
            {
                Settings.InterventionList = listOfItems;
            }
        }

        private void DeleteItem(object arg)
        {
            foreach (var item in ListToEdit)
            {
                if (item.ListItem == arg.ToString())
                {
                    ListToEdit.Remove(item);
                    UpdateItemOpacities();
                    break;
                }
            }
            SetList();
        }

        private void MoveItemDown(object arg)
        {
            if (ListToEdit.Last().ListItem != arg.ToString())
            {
                foreach (var item in ListToEdit)
                {
                    if (item.ListItem == arg.ToString())
                    {
                        int oldIndex = ListToEdit.IndexOf(item);
                        item.UpOpacity = 1;
                        ListToEdit.Move(oldIndex, oldIndex + 1);
                        UpdateItemOpacities();
                        break;
                    }
                }
                SetList();
            }
        }

        private void MoveItemUp(object arg)
        {
            if (ListToEdit.First().ListItem != arg.ToString())
            {
                foreach (var item in ListToEdit)
                {
                    if (item.ListItem == arg.ToString())
                    {
                        int oldIndex = ListToEdit.IndexOf(item);
                        ListToEdit.Move(oldIndex, oldIndex - 1);
                        UpdateItemOpacities();
                        break;
                    }
                }
                SetList();
            }
        }

        public void AddItem(object sender, EventArgs e)
        {
            ItemToEdit item = new ItemToEdit() { ListItem = ((Entry)sender).Text, UpOpacity = 1, DownOpacity = 0 };
            ListToEdit.Add(item);
            UpdateItemOpacities();
            SetList();

        }
    }
}
