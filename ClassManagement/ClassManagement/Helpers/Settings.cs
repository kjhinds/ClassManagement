// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Newtonsoft.Json;
using System.Linq;

namespace ClassManagement
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
          get
          {
            return CrossSettings.Current;
          }
        }

        #region Setting Constants

        private const string behaviorListKey = "Behavior_List";
        private static readonly string behaviorListDefault = JsonConvert.SerializeObject(new SortableObservableCollection<string>
                    { "Academic Dishonesty", "Bullying", "Disrespect", "Disruption", "Inappropriate Lang",
                      "Physical Aggression", "Technology Violation", "Other" });

        private const string interventionListKey = "Intervention_List";
        private static readonly string interventionListDefault = JsonConvert.SerializeObject(new SortableObservableCollection<string>
                    { "Apology", "Campus Beautification", "Classroom Detention" ,"Loss of Privilege",
                      "Lunch Detention", "Parent Contact", "Prompting", "Proximity", "Redirection",
                      "Restitution", "Seating Change", "Student Conference", "Other" });

        private const string odrThresholdKey = "ODR_Threshold";
        private static readonly int odrThresholdDefault = 3;

        private const string quickAddModeKey = "Quick_Add_Mode";
        private static readonly bool quickAddModeDefault = true;

        private const string lastNameFirstKey = "Last_Name_First";
        private static readonly bool lastNameFirstDefault = true;

        #endregion

        public static SortableObservableCollection<string> BehaviorList
        {
            get
            {
                string csvString = AppSettings.GetValueOrDefault(behaviorListKey, behaviorListDefault);
                return JsonConvert.DeserializeObject<SortableObservableCollection<string>>(csvString);
            }
            set
            {
                string jsonString = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(behaviorListKey, jsonString);
            }
        }

        public static SortableObservableCollection<string> InterventionList
        {
            get
            {
                string csvString = AppSettings.GetValueOrDefault(interventionListKey, interventionListDefault);
                return JsonConvert.DeserializeObject<SortableObservableCollection<string>>(csvString);
            }
            set
            {
                string jsonString = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(interventionListKey, jsonString);
            }
        }

        public static int ODRThreshold
        {
            get
            {
                return AppSettings.GetValueOrDefault(odrThresholdKey, odrThresholdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(odrThresholdKey, value);
            }
        }

        public static string ODRThresholdString
        {
            get { return ODRThreshold.ToString(); }
        }

        public static string ODRThresholdCompare
        {
            get { return ">" + ODRThreshold; }
        }

        public static bool QuickAddMode
        {
            get
            {
                return AppSettings.GetValueOrDefault(quickAddModeKey, quickAddModeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(quickAddModeKey, value);
            }
        }

        public static bool LastNameFirstSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault(lastNameFirstKey, lastNameFirstDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(lastNameFirstKey, value);
            }
        }

        public static void ResetSettings()
        {
            AppSettings.AddOrUpdateValue(behaviorListKey, behaviorListDefault);
            AppSettings.AddOrUpdateValue(interventionListKey, interventionListDefault);
            AppSettings.AddOrUpdateValue(odrThresholdKey, odrThresholdDefault);
            AppSettings.AddOrUpdateValue(quickAddModeKey, quickAddModeDefault);
            AppSettings.AddOrUpdateValue(lastNameFirstKey, lastNameFirstDefault);
        }
    }
}