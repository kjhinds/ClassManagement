using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;
using ClassManagement.UWP;

[assembly: Dependency(typeof(SaveAndLoad_UWP))]
namespace ClassManagement.UWP
{
    public class SaveAndLoad_UWP : ISaveAndLoad {
    public string LoadText(string filename)
    {
        var task = LoadTextAsync(filename);
        task.Wait(); // HACK: to keep Interface return types simple (sorry!)
        return task.Result;
    }
        public async Task<string> LoadTextAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            IStorageFile file = await localFolder.GetFileAsync(filename);
            string text;

            using (StreamReader streamReader = new StreamReader(file.Path))
            {
                text = await streamReader.ReadToEndAsync();
            }
            return text;
        }
        public async void SaveText(string filename, string text)
    {
        StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
        var file = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
        using (StreamWriter writer = new StreamWriter(await file.OpenStreamForWriteAsync()))
        {
            writer.Write(text);
        }
    }

    public bool FileExists(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                localFolder.GetFileAsync(filename).AsTask().Wait();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}