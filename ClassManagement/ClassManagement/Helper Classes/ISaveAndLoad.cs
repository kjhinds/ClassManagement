using System;
using System.Threading.Tasks;
namespace ClassManagement
{
    public interface ISaveAndLoad
    {
        void SaveText (string filename, string text);
        string LoadText (string filename);
        bool FileExists (string filename);
    }
}

