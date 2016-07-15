using System;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class SettingsPage : ContentPage
    {
        private DataModel _data;

        public SettingsPage (DataModel data)
        {
            InitializeComponent ();
            _data = data;
            Title = "Settings";
        }

        private void OnSortPreferenceChanged(object sender, EventArgs e)
        {
            Settings.LastNameFirstSetting = SortOrderSwitchCell.On;
            foreach (var period in _data.Periods) {
                period.Students.Sort(Student.GetSortPreference());
            }
        }

        private void OnQuickAddChanged(object sender, EventArgs e)
        {
            Settings.QuickAddMode = QuickAddSwitchCell.On;
        }

        private void OnExportCSVTapped(object sender, EventArgs e)
        {
            string csv = _data.csvSerialize();
            DependencyService.Get<ISaveAndLoad>().SaveText("ClassManagement.csv", csv);
            DependencyService.Get<IExportCSVFile>().ExportCSVFile("ClassManagement.csv");
        }

        private void OnReferralThresholdCompleted(object sender, EventArgs e)
        {
            int threshold;
            if (int.TryParse(ReferralThresholdCell.Text, out threshold))
            {
                Settings.ODRThreshold = threshold;
            }
            else
            {
                DisplayAlert("Error", "Please enter a whole number", "OK");
            }
        }

        private void OnEditBehaviorListTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditListPage("Behavior"), false);
        }

        private void OnEditInterventionListTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditListPage("Intervention"), false);
        }

        private async void OnResetDefaultsTapped(object sender, EventArgs e)
        {
            bool choice = await DisplayAlert("Reset Defaults", 
                    "'Accept' will reset defaults, but not student data.", 
                    "Accept", "Cancel");
            if (choice)
            {
                Settings.ResetSettings();
                //TODO: Need to change implementation to MVVM and reassign viewmodel
                Navigation.PopAsync();
                Navigation.PushAsync(new SettingsPage(_data));
            }
        }

        private void OnCreditsTapped(object sender, EventArgs e)
        {
            DisplayAlert("Credits", "Icons by: pixel-mixer.com", "Close");
        }
    }
}

