using System;
using System.Windows.Forms;

namespace ColdCutsNS
{
    public class FolderBrowser
    {
        public static string Show()
        {
            string dir = null;
            var folderDialog = new FolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = "c:\\",
                Description = "Select Destination Folder"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                dir = folderDialog.SelectedPath;
            }
            return dir;
        }
    }
}
