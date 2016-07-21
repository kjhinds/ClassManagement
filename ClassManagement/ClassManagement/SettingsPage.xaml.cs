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
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new SettingsViewModel(dataModel);

            this.dataModel = dataModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<SettingsViewModel, string>(this, "Message",
                (sender, msg) => HandleMessage(msg));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SettingsViewModel, string>(this, "Message");
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
                case "Behavior":
                case "Intervention":
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
            Navigation.PopAsync(false);
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

