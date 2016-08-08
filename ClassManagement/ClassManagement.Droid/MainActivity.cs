using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using System.IO;
using Android.Provider;

namespace ClassManagement.Droid
{
    [Activity(Label = "Class Management", 
            Icon = "@drawable/icon", 
            MainLauncher = true,
            ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new string[] { Intent.ActionView },
       Categories = new string[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
       DataScheme = "file",
       DataHost = "*",
       DataMimeType = "text/csv")]

    [IntentFilter(new string[] { Intent.ActionView },
        Categories = new string[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "content",
        DataHost = "*",
        DataMimeType = "text/csv")]

    [IntentFilter(new string[] { Intent.ActionView },
        Categories = new string[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "http",
        DataHost = "*",
        DataMimeType = "text/csv")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        App mainApp;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            mainApp = new App();

            string action = Intent.Action;
            string type = Intent.Type;

            if (Intent.ActionView.Equals(action) && !String.IsNullOrEmpty(type))
            {
                Android.Net.Uri fileUri = Intent.Data;
                try
                {
                    Stream stream = ContentResolver.OpenInputStream(fileUri);
                    TextReader reader = new StreamReader(stream);
                    string studentData = reader.ReadToEnd();

                    mainApp.ImportStudentList(studentData);
                }
                catch
                {
                    RunOnUiThread(() =>
                    {
                        AlertDialog.Builder builder;
                        builder = new AlertDialog.Builder(this);
                        builder.SetTitle("Error");
                        builder.SetMessage("Download file and open from downloads folder \n\nSorry, Android is weird");
                        builder.SetCancelable(false);
                        builder.SetPositiveButton("OK", delegate { Finish(); });
                        builder.Show();
                    });
                }
            }


            LoadApplication(mainApp);
        }
    }
}

