using System;
using Xamarin.Forms;
using ClassManagement.iOS;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using System.Linq;

[assembly: Dependency (typeof (SaveAndLoad_iOS))]

namespace ClassManagement.iOS
{
    public class SaveAndLoad_iOS : ISaveAndLoad
    {
        public static string DocumentsPath {
            get {
                var documentsDirUrl = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).Last ();
                return documentsDirUrl.Path;
            }
        }

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

        static string CreatePathToFile (string fileName)
        {
            return Path.Combine (DocumentsPath, fileName);
        }
    }
}