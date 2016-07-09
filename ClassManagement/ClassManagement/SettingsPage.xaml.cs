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
    }
}

