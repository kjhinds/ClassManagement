using System;
using System.Linq;
using Xamarin.Forms;
using System.Windows.Input;

namespace ClassManagement
{
    class EditListViewModel : ViewModelBase
    {
        private SortableObservableCollection<ItemToEdit> listToEdit;
        private string listType;
        private string newItemText;

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

        public string NewItemText
        {
            get { return newItemText; }
            set { SetProperty(ref newItemText, value); }
        }

        public string ListType
        {
            get { return listType; }
            set { SetProperty(ref listType, value); }
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
            ListType = listType;
            listToEdit = BuildList();
            TapDeleteCommand = new Command(arg => DeleteItem(arg));
            TapDownCommand = new Command(arg => MoveItemDown(arg));
            TapUpCommand = new Command(arg => MoveItemUp(arg));
            AddItemCommand = new Command(() => AddItem());
            DoneButton = new Command(() => SendMessageToView());
        }

        public ICommand AddItemCommand { protected set; get; }
        public ICommand DoneButton { protected set; get; }
        public ICommand TapDownCommand { protected set; get; }
        public ICommand TapDeleteCommand { protected set; get; }
        public ICommand TapUpCommand { protected set; get; }

        private SortableObservableCollection<ItemToEdit> BuildList()
        {
            SortableObservableCollection<string> currentList;
            if (ListType == "Behavior")
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
            if (ListType == "Behavior")
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

        private void AddItem()
        {
            ItemToEdit item = new ItemToEdit() { ListItem = NewItemText, UpOpacity = 1, DownOpacity = 0 };
            ListToEdit.Add(item);
            UpdateItemOpacities();
            SetList();
            NewItemText = "";
        }

        private void SendMessageToView()
        {
            MessagingCenter.Send(this, "Done");
        }
    }
}
