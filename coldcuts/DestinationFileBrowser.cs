using System;
using System.Windows.Forms;

namespace ColdCutsNS
{
    public class DestinationFileBrowser{

        public DestinationFileBrowser(MainForm mainForm, MainFormHelper mainFormHelper){

            FolderBrowserDialog openDestinationFolderBrowserDialog = new FolderBrowserDialog();

            openDestinationFolderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            openDestinationFolderBrowserDialog.SelectedPath = "c:\\";
            openDestinationFolderBrowserDialog.Description = "Select Destination Folder";

            if (openDestinationFolderBrowserDialog.ShowDialog() ==
                System.Windows.Forms.DialogResult.OK){

                mainForm.destinationFilePathTextBox.Text = openDestinationFolderBrowserDialog.SelectedPath + "\\";
            }

            if (mainFormHelper.AreSourceAndDestinationFilled()){

                mainFormHelper.EnableTheEditingControls();
            }
        }
    }
}
