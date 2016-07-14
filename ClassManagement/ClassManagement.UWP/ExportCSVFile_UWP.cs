using System;
using Xamarin.Forms;
using ClassManagement.UWP;
using System.IO;
using Windows.Storage;
using Windows.ApplicationModel.Email;
using System.Threading.Tasks;

[assembly: Dependency(typeof(ExportCSVFile_UWP))]

namespace ClassManagement.UWP
{
    public class ExportCSVFile_UWP : IExportCSVFile
    {
        public void ExportCSVFile(string filename)
        {
            if (FileExists(filename))
            {
                var task = SendEmail(filename);
                task.Wait(); // HACK: to keep Interface return types simple (sorry!)
            }
            var exists = FileExists(filename);
        }

        async Task<StorageFile> GetFileAsync(string filename)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            if (local != null)
            {
                var file = await local.GetFileAsync(filename);
                //using (StreamReader streamReader = new StreamReader(new FileStream(file.Path, FileMode.OpenOrCreate, FileAccess.Read)))
                //{
                //    var text = streamReader.ReadToEnd();
                //    return text;
                //}
                return file;
            }
            return null;
        }

        private async Task<bool> SendEmail(string fileName)
        {
            EmailMessage emailMessage = new EmailMessage();
            //emailMessage.To.Add(new EmailRecipient("***@***.com"));
            //string messageBody = "Class Management Data";
            emailMessage.Body = "Class Management Data";
            StorageFolder MyFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFile attachmentFile = await MyFolder.GetFileAsync(fileName);
            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);
                var attachment = new EmailAttachment(
                         attachmentFile.Name,
                         stream);
                emailMessage.Attachments.Add(attachment);
            }
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
            return true;
        }

        public bool FileExists(string filename)
        {
            return File.Exists(CreatePathToFile(filename));
        }

        string CreatePathToFile(string filename)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            return Path.Combine(local.ToString(), filename);
        }
    }
}