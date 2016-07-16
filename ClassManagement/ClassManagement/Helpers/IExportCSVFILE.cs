
namespace ClassManagement
{ 
    public interface IExportCSVFile
    {
        void ExportCSVFile(string filename);
        bool FileExists(string filename);
    }
}
