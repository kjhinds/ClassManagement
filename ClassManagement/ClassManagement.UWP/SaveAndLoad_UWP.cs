using System;
using Xamarin.Forms;
using ClassManagement.UWP;
using System.IO;
using Windows.Storage;
using System.Threading.Tasks;

[assembly: Dependency(typeof(SaveAndLoad_UWP))]

namespace ClassManagement.UWP
{
    public class SaveAndLoad_UWP : ISaveAndLoad
    {
        #region ISaveAndLoad implementation

        public void SaveText(string filename, string text)
        {
            var task = SaveTextAsync(filename, text);
            task.Wait();
        }

        public async Task<bool> SaveTextAsync(string filename, string text)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            var file = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            using (StreamWriter writer = new StreamWriter(await file.OpenStreamForWriteAsync()))
            {
                writer.Write(text);
            }
            return true;
        }

        public string LoadText(string filename)
        {
            if (FileExists(filename))
            {
                var task = LoadTextAsync(filename);
                task.Wait(); // HACK: to keep Interface return types simple (sorry!)
                return task.Result;
            }
            return "";
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