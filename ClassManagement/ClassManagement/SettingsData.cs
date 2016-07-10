using Plugin.Settings;

namespace ClassManagement
{
    public class SettingsData : ViewModelBase
    {
        public static string[] BehaviorList
        {
            get
            {
                string csvBehaviorString = CrossSettings.Current.GetValueOrDefault("BehaviorList",
                    "Academic Dishonesty,Bullying,Disrespect,Disruption,Inappropriate Lang," +
                    "Physical Aggression,Technology Violation,Other");
                return csvBehaviorString.Split(',');
            }
            set
            {
                string behaviorList = string.Join(",", value);
                CrossSettings.Current.AddOrUpdateValue("BehaviorList", behaviorList);
            }
        }

        public static string[] InterventionList
        {
            get
            {
                string csvInterventionString = CrossSettings.Current.GetValueOrDefault("InterVentionList",
                    "Apology,Campus Beautification,Classroom Detention,Loss of Privilege,Lunch Detention," +
                    "Parent Contact,Prompting,Proximity,Redirection,Restitution,Seating Change," +
                    "Student Conference,Other");
                return csvInterventionString.Split(',');
            }
            set
            {
                string interventionList = string.Join(",", value);
                CrossSettings.Current.AddOrUpdateValue("InterventionList", interventionList);
            }
        }

        public static string ODRThreshold
        {
            get
            {
                return CrossSettings.Current.GetValueOrDefault("ODRThreshold", "3");
            }
            set
            {
                CrossSettings.Current.AddOrUpdateValue("ODRThreshold", value);
            }
        }

        public static string ODRThresholdCompare
        {
            get { return ">" + ODRThreshold; }
        }

        public static bool QuickAddMode {
            get {
                string quickAddModeString = CrossSettings.Current.GetValueOrDefault("QuickAddMode", "true");
                return bool.Parse(quickAddModeString);
            }
            set {
                CrossSettings.Current.AddOrUpdateValue("QuickAddMode", value.ToString());
            }
        } 

        public static bool LastNameFirstSetting {
            get 
            {
                string lastNameFirstString = CrossSettings.Current.GetValueOrDefault("LastNameFirst", "true");
                return bool.Parse(lastNameFirstString);
            }
            set 
            {
                CrossSettings.Current.AddOrUpdateValue("LastNameFirst", value.ToString());
            }
        }

        public static void SaveSettings()
        {

            string csvInterventionString = string.Join(",", InterventionList);
            CrossSettings.Current.AddOrUpdateValue("InterventionList", csvInterventionString);

        }
    }
}

