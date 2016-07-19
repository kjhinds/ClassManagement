using Xamarin.Forms;
using System.Windows.Input;

namespace ClassManagement
{
    class SettingsViewModel : ViewModelBase
    {
        private DataModel dataModel;
        private int odrThreshold;
        private bool lastNameFirstSetting;
        private bool quickAddMode;

        public bool LastNameFirstSetting
        {
            get { return lastNameFirstSetting; }
            set
            {
                SetProperty(ref lastNameFirstSetting, value);
                Settings.LastNameFirstSetting = value;
            }
        }

        public bool QuickAddMode
        {
            get { return quickAddMode; }
            set
            {
                SetProperty(ref quickAddMode, value);
                Settings.QuickAddMode = value;
            }
        }

        public int ODRThreshold
        {
            get { return odrThreshold; }
            set
            {
                SetProperty(ref odrThreshold, value);
                Settings.ODRThreshold = value;
            }
        }

        public SettingsViewModel(DataModel dataModel)
        {
            this.dataModel = dataModel;
            ODRThreshold = Settings.ODRThreshold;
            LastNameFirstSetting = Settings.LastNameFirstSetting;
            QuickAddMode = Settings.QuickAddMode;
            ButtonCommand = new Command<string>((arg) => SendMessageToView(arg));
            ExportCommand = new Command(() => ExportDataAsCSV());
        }

        public ICommand ButtonCommand { protected set; get; }
        public ICommand ExportCommand { protected set; get; }

        private void SendMessageToView(string msg)
        {
            MessagingCenter.Send(this, "Message", msg);
        }

        private void UpdateThreshold(string value)
        {
            int thresholdInt;
            bool success = int.TryParse(value, out thresholdInt);
            if (success)
            {
                Settings.ODRThreshold = thresholdInt;
            }
            else
            {
                SendMessageToView("ThresholdError");
            }
        }

        private void ExportDataAsCSV()
        {
            string csv = dataModel.csvSerialize();
            DependencyService.Get<ISaveAndLoad>().SaveText("ClassManagement.csv", csv);
            DependencyService.Get<IExportCSVFile>().ExportCSVFile("ClassManagement.csv");
        }
    }

}
