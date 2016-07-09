using Plugin.Settings;

namespace ClassManagement
{
    public class SettingsData : ViewModelBase
    {
        public static string[] BehaviorList;
        public static string[] InterventionList;
        private static bool _lastNameFirstSetting;
        private static bool _quickAddMode;

        public static bool QuickAddMode {
            get {
                string quickAddModeString = CrossSettings.Current.GetValueOrDefault("QuickAddMode", "true");
                _quickAddMode = bool.Parse(quickAddModeString);
                return _quickAddMode;
            }
            set {
                _quickAddMode = value;
                CrossSettings.Current.AddOrUpdateValue("QuickAddMode", value.ToString());
            }
        } 

        public static bool LastNameFirstSetting {
            get 
            {
                string lastNameFirstString = CrossSettings.Current.GetValueOrDefault("LastNameFirst", "true");
                _lastNameFirstSetting = bool.Parse(lastNameFirstString);
                return _lastNameFirstSetting;
            }
            set 
            {
                _lastNameFirstSetting = value;
                CrossSettings.Current.AddOrUpdateValue("LastNameFirst", value.ToString());
            }
        }

        public SettingsData(){
            LoadSettings();
        }

        public static void SaveSettings()
        {
            string csvBehaviorString = string.Join(",", BehaviorList);
            CrossSettings.Current.AddOrUpdateValue("BehaviorList", csvBehaviorString);

            string csvInterventionString = string.Join(",", InterventionList);
            CrossSettings.Current.AddOrUpdateValue("InterventionList", csvInterventionString);

        }

        public static void LoadSettings()
        {
            string csvBehaviorString = CrossSettings.Current.GetValueOrDefault("BehaviorList",
                    "Academic Dishonesty,Bullying,Disrespect,Disruption,Inappropriate Lang," +
                    "Physical Aggression,Technology Violation,Other");
            BehaviorList = csvBehaviorString.Split(',');

            string csvInterventionString = CrossSettings.Current.GetValueOrDefault("InterVentionList",
                    "Apology,Campus Beautification,Classroom Detention,Loss of Privilege,Lunch Detention," +
                    "Parent Contact,Prompting,Proximity,Redirection,Restitution,Seating Change," +
                    "Student Conference,Other");
            InterventionList = csvInterventionString.Split(',');

            //string lastNameFirstString = CrossSettings.Current.GetValueOrDefault("LastNameFirst", "true");
            //_lastNameFirstSetting = bool.TryParse(lastNameFirstString, out _lastNameFirstSetting);
        }
    }
}

