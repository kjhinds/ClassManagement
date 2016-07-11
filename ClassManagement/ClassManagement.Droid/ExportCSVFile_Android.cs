using System;
using Xamarin.Forms;
using ClassManagement.Droid;
using System.IO;
using Android.Content;

[assembly: Dependency(typeof(ExportCSVFile_Android))]

namespace ClassManagement.Droid
{
    public class ExportCSVFile_Android : IExportCSVFile
    {
        #region IExportCSVFile implementation

        public void ExportCSVFile(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            SendFile(filePath);
        }

        public void SendFile(String filePath)
        {
            Java.IO.File file = new Java.IO.File(filePath);
            Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
            file.SetReadable(true, false); 

            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraStream, uri);

            Forms.Context.StartActivity(Intent.CreateChooser(intent, "Send Data"));
        }

        public bool FileExists(string filename)
        {
            return File.Exists(CreatePathToFile(filename));
        }

        #endregion

        string CreatePathToFile(string filename)
        {
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(docsPath, filename);
        }
    }
}