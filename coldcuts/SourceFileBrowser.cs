using System;
using System.IO;
using System.Windows.Forms;
using Un4seen.Bass.AddOn.Tags;

namespace ColdCutsNS
{
    public class SourceFileBrowser{

        public SourceFileBrowser(MainForm mainForm, MainFormHelper mainFormHelper){

            Stream stream;
            OpenFileDialog openSourceFileDialog = new OpenFileDialog();
            TAG_INFO inputFileTags;

            openSourceFileDialog.InitialDirectory = "c:\\";
            openSourceFileDialog.Filter = "MP3 files (*.mp3)|*.mp3";
            openSourceFileDialog.FilterIndex = 1;
            openSourceFileDialog.RestoreDirectory = true;

            if (openSourceFileDialog.ShowDialog() == DialogResult.OK) {
                Cursor.Current = Cursors.WaitCursor;
                if ((stream = openSourceFileDialog.OpenFile()) != null){

                    mainForm.sourceFilePathTextBox.Text = openSourceFileDialog.FileName;

                    inputFileTags = mainForm.GetOutputFileController().FillInputFileTags(mainForm.sourceFilePathTextBox.Text);

                    mainForm.artistInputLabel.Text = inputFileTags.artist;
                    mainForm.titleInputLabel.Text = inputFileTags.title;
                    mainForm.lengthInputLabel.Text = Math.Round(inputFileTags.duration, 0).ToString() + " seconds";

                    stream.Close();
                    mainForm.destinationBrowseButton.Enabled = true;
                    mainForm.destinationFilePathTextBox.Enabled = true;
                    mainForm.btnAutoSplit.Enabled = true;
                }

                if (mainFormHelper.AreSourceAndDestinationFilled()){

                   mainFormHelper.EnableTheEditingControls();
                }
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
