using System;
using Xamarin.Forms;
using ClassManagement.Droid;
using System.IO;

[assembly: Dependency (typeof (SaveAndLoad_Android))]

namespace ClassManagement.Droid
{
    public class SaveAndLoad_Android : ISaveAndLoad
    {
        public static string DocumentsPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
        }

        public void SaveText(string filename, string text)
        {
            File.WriteAllText(CreatePathToFile(filename), text);
        }

        public string LoadText(string filename)
        {
            if (FileExists(filename))
            {
                return File.ReadAllText(CreatePathToFile(filename));
            }
            else
            {
                return "";
            }
        }

        public bool FileExists(string filename)
        {
            return File.Exists(CreatePathToFile(filename));
        }

        static string CreatePathToFile(string fileName)
        {
            return Path.Combine(DocumentsPath, fileName);
        }
    }
}