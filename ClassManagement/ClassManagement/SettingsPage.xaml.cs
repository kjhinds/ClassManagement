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
            SettingsData.LastNameFirstSetting = SortOrderSwitchCell.On;
            foreach (var period in _data.Periods) {
                period.Students.Sort(Student.GetSortPreference());
            }
        }

        private void OnQuickAddChanged(object sender, EventArgs e)
        {
            SettingsData.QuickAddMode = QuickAddSwitchCell.On;
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
                SettingsData.ODRThreshold = ReferralThresholdCell.Text;
            }
            else
            {
                DisplayAlert("Error", "Please enter a whole number", "OK");
            }
        }

        private void OnEditBehaviorListTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditList("Edit Behavior List"));
        }

        private void OnEditInterventionListTapped(object sender, EventArgs e)
        {

        }

        private void OnCreditsTapped(object sender, EventArgs e)
        {
            DisplayAlert("Credits", "Icons by: pixel-mixer.com", "Close");
        }
    }
}

