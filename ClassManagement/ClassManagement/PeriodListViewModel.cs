﻿using Xamarin.Forms;
using System.Windows.Input;

namespace ClassManagement
{
    class PeriodListViewModel : ViewModelBase
    {
        #region Private Fields
        private SortableObservableCollection<Period> periods;
        private bool editMode;
        #endregion

        #region Public Properties
        public SortableObservableCollection<Period> Periods
        {
            get { return periods; }
            set { SetProperty(ref periods, value); }
        }

        public bool EditMode
        {
            get { return editMode; }
            set { SetProperty(ref editMode, value); }
        }
        #endregion


        #region Constructor
        public PeriodListViewModel(SortableObservableCollection<Period> periods)
        {
            Periods = periods;
            EditMode = false;
            DeleteCommand = new Command<string>((arg) => DeleteItem(arg));
            EditCommand = new Command(() => ToggleEditMode());
            ButtonCommand = new Command<string>((arg) => SendMessageToView(arg));
        }
        #endregion

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
        private void DeleteItem(string arg)
        {
            foreach (var period in Periods)
            {
                if (period.PeriodName == arg)
                {
                    Periods.Remove(period);
                    break;
                }
            }
        }
    }
}
