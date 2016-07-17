using Xamarin.Forms;
using System.Windows.Input;

namespace ClassManagement
{
    class PeriodListViewModel : ViewModelBase
    {
        #region Private Fields
        private SortableObservableCollection<Period> periods;
        private bool editMode;
        private INavigation navigation;
        #endregion

        #region Public Properties

        public static Command TapDeleteCommand;
        public static Command TapDetailsCommand;

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
        public PeriodListViewModel(SortableObservableCollection<Period> periods, INavigation navigation)
        {
            Periods = periods;
            this.navigation = navigation;
            EditMode = false;
            TapDeleteCommand = new Command(arg => DeleteItem(arg));
            TapDetailsCommand = new Command<string>(arg => ItemDetails(arg));
        }
        #endregion

        private void DeleteItem(object arg)
        {
            foreach (var item in Periods)
            {
                if (item.PeriodName == arg.ToString())
                {
                    Periods.Remove(item);
                    break;
                }
            }
        }

        private void ItemDetails(object arg)
        {
            foreach (var period in Periods)
            {
                if (period.PeriodName == arg.ToString())
                {
                    navigation.PushAsync(new PeriodDetailPage(Periods, period, true),false);
                }
            }
        }

        public void ToggleEditMode()
        {
            EditMode = EditMode ? false : true;
        }
    }
}
