using System;
using Xamarin.Forms;
using ClassManagement.Droid;
using System.IO;
using System.Threading.Tasks;
using ClassManagement;

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
            //return System.IO.File.ReadAllText(filePath);
            
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