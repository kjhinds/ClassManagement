using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class SettingsPage : ContentPage
    {
        private DataModel dataModel;

        public SettingsPage (DataModel dataModel)
        {
            InitializeComponent ();

            SubscribeToMessages();

            BindingContext = new SettingsViewModel(dataModel);

            this.dataModel = dataModel;
        }

        private async void ResetDefaults()
        {
            bool choice = await DisplayAlert("Reset Defaults", 
                    "'Accept' will reset defaults, but not student data.", 
                    "Accept", "Cancel");
            if (choice)
            {
                Settings.ResetSettings();

                BindingContext = new SettingsViewModel(dataModel);
            }
        }

        private async void DeleteStudentData()
        {
            bool choice = await DisplayAlert("Delete Student Data",
                    "'Accept' will delete all student data, but keep your settings.",
                    "Accept", "Cancel");
            if (choice)
            {
                dataModel.LoadDefaults();

                BindingContext = new SettingsViewModel(dataModel);
            }
        }

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<SettingsViewModel, string>(this, "Message",
                (sender, msg) => HandleMessage(msg));
        }

        private void HandleMessage(string msg)
        {
            switch (msg)
            {
                case "Done":
                    CloseSettingsPage();
                    break;
                case "Credits":
                    ShowCredits();
                    break;
                case "DeleteStudentData":
                    DeleteStudentData();
                    break;
                case "ResetDefaults":
                    ResetDefaults();
                    break;
                case "EditBehaviorList":
                case "EditInterventionList":
                    ShowEditListPage(msg);
                    break;
                case "ThresholdError":
                    ShowThresholdError();
                    break;
                default:
                    break;
            }
        }

        private void CloseSettingsPage()
        {
            MessagingCenter.Unsubscribe<SettingsViewModel, string>(this, "Message");
            Navigation.PopModalAsync(false);
        }

        private void ShowCredits()
        {
            DisplayAlert("Credits", "Icons by: pixel-mixer.com", "Close");
        }

        private void ShowEditListPage(string list)
        {
            Navigation.PushModalAsync(new EditListPage(list), false);
        }

        private void ShowThresholdError()
        {
            DisplayAlert("Error", "Threshold must be a number", "Close");
        }
    }
}

