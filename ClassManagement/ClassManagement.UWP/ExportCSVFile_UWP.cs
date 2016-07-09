using System;
using Xamarin.Forms;
using ClassManagement.UWP;
using System.IO;
using Windows.Storage;
using System.Threading.Tasks;

[assembly: Dependency(typeof(ExportCSVFile_UWP))]

namespace ClassManagement.UWP
{
    public class ExportCSVFile_UWP : IExportCSVFile
    {
        #region ISaveAndLoad implementation

        public void ExportCSVFile(string filename)
        {
            var task = LoadTextAsync(filename);
            task.Wait(); // HACK: to keep Interface return types simple (sorry!)
            //return task.Result;
        }
        async Task<string> LoadTextAsync(string filename)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            if (local != null)
            {
                var file = await local.GetItemAsync(filename);
                using (StreamReader streamReader = new StreamReader(new FileStream(file.Path, FileMode.OpenOrCreate, FileAccess.Read)))
                {
                    var text = streamReader.ReadToEnd();
                    return text;
                }
            }
            return "";
        }

        public bool FileExists(string filename)
        {
            return File.Exists(CreatePathToFile(filename));
        }

        #endregion

        string CreatePathToFile(string filename)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            return Path.Combine(local.ToString(), filename);
        }
    }
}