using System;
using Xamarin.Forms;
using ClassManagement.Droid;
using System.IO;
using System.Threading.Tasks;
using ClassManagement;

[assembly: Dependency (typeof (SaveAndLoad_Android))]

namespace ClassManagement.Droid
{
    public class SaveAndLoad_Android : ISaveAndLoad
    {
        #region ISaveAndLoad implementation

        public void SaveText (string filename, string text) {
            var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            var filePath = Path.Combine (documentsPath, filename);
            System.IO.File.WriteAllText (filePath, text);
        }

        public string LoadText (string filename) {
            var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            var filePath = Path.Combine (documentsPath, filename);
            return System.IO.File.ReadAllText (filePath);
        }

        public bool FileExists (string filename)
        {
            return File.Exists (CreatePathToFile (filename));
        }

        #endregion

        string CreatePathToFile (string filename)
        {
            var docsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            return Path.Combine (docsPath, filename);
        }
    }
}