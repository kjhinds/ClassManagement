using System;
using Xamarin.Forms;
using ClassManagement.iOS;
using System.IO;
using Foundation;
using System.Linq;
using UIKit;

[assembly: Dependency(typeof(ExportCSVFile_iOS))]

namespace ClassManagement.iOS
{
    public class ExportCSVFile_iOS : IExportCSVFile
    {
        public static string DocumentsPath
        {
            get
            {
                var documentsDirUrl = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).Last();
                return documentsDirUrl.Path;
            }
        }


        public void OpenOptionsMenu(string _filePath)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //Get view
                var window = UIApplication.SharedApplication.KeyWindow;
                var subviews = window.Subviews;
                var view = subviews.First();

                //Get navigation controller
                var firstController = UIApplication.SharedApplication.KeyWindow.RootViewController.ChildViewControllers.First().ChildViewControllers.Last().ChildViewControllers.First();
                var navcontroller = firstController as UINavigationController;

                //Create Document Interaction Controller
                var uidic = UIDocumentInteractionController.FromUrl(new NSUrl(_filePath, true));
                var uidicDel = new UIDocumentInteractionControllerDelegate();
                uidic.Delegate = uidicDel;
                uidic.PresentOptionsMenu(CoreGraphics.CGRect.Empty, view, true);
            });
        }

        public void ExportCSVFile(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            OpenOptionsMenu(filePath);
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