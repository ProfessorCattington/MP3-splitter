using System;
using System.IO;
using System.Windows.Forms;
using Un4seen.Bass.AddOn.Tags;

namespace ColdCutsNS{

    public partial class MainForm : Form {

        protected OutputFileController m_outputFileController;
        private const string m_editLabelString = "Editing Output File: ";

        public MainForm() {

           InitializeComponent();
           InitializeFields();
        }

        private void InitializeFields(){

            m_outputFileController = new OutputFileController();
            artistInputLabel.Text = "";
            titleInputLabel.Text = "";
            lengthInputLabel.Text = "";
        }

        private void browseButton_Click(object sender, EventArgs e){

            Stream stream;
            OpenFileDialog openSourceFileDialog = new OpenFileDialog();
            TAG_INFO inputFileTags;

            openSourceFileDialog.InitialDirectory = "c:\\";
            openSourceFileDialog.Filter = "MP3 files (*.mp3)|*.mp3";
            openSourceFileDialog.FilterIndex = 1;
            openSourceFileDialog.RestoreDirectory = true;

            if (openSourceFileDialog.ShowDialog() == DialogResult.OK){

                if ((stream = openSourceFileDialog.OpenFile()) != null){

                    sourceFilePathTextBox.Text = openSourceFileDialog.FileName;

                    inputFileTags = m_outputFileController.FillInputFileTags(sourceFilePathTextBox.Text);

                    artistInputLabel.Text = inputFileTags.artist;
                    titleInputLabel.Text = inputFileTags.title;
                    lengthInputLabel.Text = Math.Round(inputFileTags.duration,0).ToString() + " seconds";

                    stream.Close();
                }

                if (AreSourceAndDestinationFilled()){

                    EnableTheEditingControls();
                }
            }
        }

        private void destinationBrowseButton_Click(object sender, EventArgs e){

            FolderBrowserDialog openDestinationFolderBrowserDialog = new FolderBrowserDialog();

            openDestinationFolderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            openDestinationFolderBrowserDialog.SelectedPath = "c:\\";
            openDestinationFolderBrowserDialog.Description = "Select Destination Folder";

            if (openDestinationFolderBrowserDialog.ShowDialog() ==
                System.Windows.Forms.DialogResult.OK){

                    destinationFilePathTextBox.Text = openDestinationFolderBrowserDialog.SelectedPath + "\\";
            }

            if (AreSourceAndDestinationFilled()){

                EnableTheEditingControls();
            }
        }
        private void encodeButton_Click(object sender, EventArgs e){

            DisableTheEditingControls();

            feedBackLabel.Visible = true;
            feedBackLabel2.Visible = true;
            feedBackLabel.Text = "Encoding...";

            new StartEncodingStrategy(this, m_outputFileController);

            sourceBrowseButton.Enabled = true;
            destinationBrowseButton.Enabled = true;
            EnableTheEditingControls();    

            feedBackLabel.Text = "Done!";
            feedBackLabel2.Visible = false;
        }

        public void FileEncodingNotification(long bytesTotal, long bytesDone){

            Console.Write("Encoding: {0:P}\r", Math.Round((double)bytesDone / (double)bytesTotal, 2));
            feedBackLabel2.Text = Math.Round((double)bytesDone / (double)bytesTotal).ToString();
        }

        public bool AreSourceAndDestinationFilled(){

            return (sourceFilePathTextBox.Text != "" && destinationFilePathTextBox.Text != "");
        }

        public new void Leave(object sender, EventArgs e){

            if (startMinTextBox.Text == "") { startMinTextBox.Text = "0"; }
            if (startSecTextBox.Text == "") { startSecTextBox.Text = "0"; }
            if (endMinTextBox.Text == "") { endMinTextBox.Text = "0"; }
            if (endSecTextBox.Text == "") { endSecTextBox.Text = "0"; }

            if (StartAndEndTimesAreValid()){

                SaveFieldsToFileObject();
            }
        }

        public void EnableTheEditingControls(){

            encodeButton.Enabled = true;
            addFileButton.Enabled = true;
            startMinTextBox.Enabled = true;
            startSecTextBox.Enabled = true;
            endMinTextBox.Enabled = true;
            endSecTextBox.Enabled = true;
            fileNameOutputBox.Enabled = true;
            artistOutputTextBox.Enabled = true;
            titleOutputTextBox.Enabled = true;
            albumOutputTextBox.Enabled = true;
            commentOutputTextBox.Enabled = true;

            UpdateEditingPosition();
            LeftAndRightButtonsEnableDisable();
        }

        public void UpdateEditingPosition() {

            editPositionLabel.Text = m_editLabelString + (m_outputFileController.GetCurrentFileIndex() + 1).ToString() +
                " / " + m_outputFileController.GetNumberOfSoundFiles().ToString();
        }

        public void DisableTheEditingControls(){

            fileLeftButton.Enabled = false;
            fileRightButton.Enabled = false;
            addFileButton.Enabled = false;
            startMinTextBox.Enabled = false;
            startSecTextBox.Enabled = false;
            endMinTextBox.Enabled = false;
            endSecTextBox.Enabled = false;
            deleteButton.Enabled = false;
            fileNameOutputBox.Enabled = false;
            artistOutputTextBox.Enabled = false;
            titleOutputTextBox.Enabled = false;
            albumOutputTextBox.Enabled = false;
            commentOutputTextBox.Enabled = false;
            encodeButton.Enabled = false;
        }

        public void SaveFieldsToFileObject(){

            m_outputFileController.UpdateStartAndEndTimes(startMinTextBox.Text, startSecTextBox.Text,
                                                              endMinTextBox.Text, endSecTextBox.Text);

            m_outputFileController.UpdateInputTags(fileNameOutputBox.Text,
                                                artistOutputTextBox.Text,
                                                 titleOutputTextBox.Text,
                                                 albumOutputTextBox.Text,
                                                 commentOutputTextBox.Text);
        }

        public void FillFieldsFromFileObject(){

            startMinTextBox.Text = m_outputFileController.GetStartMinString();
            startSecTextBox.Text = m_outputFileController.GetStartSecString();
            endMinTextBox.Text = m_outputFileController.GetEndMinString();
            endSecTextBox.Text = m_outputFileController.GetEndSecString();
            fileNameOutputBox.Text = m_outputFileController.GetFileName();
            artistOutputTextBox.Text = m_outputFileController.GetArtist();
            titleOutputTextBox.Text = m_outputFileController.GetTitle();
            albumOutputTextBox.Text = m_outputFileController.GetAlbum();
            commentOutputTextBox.Text = m_outputFileController.GetComment();

        }

        public bool StartAndEndTimesAreValid() {

            try { 

                if (int.Parse(startMinTextBox.Text) >= 0 &&
                    int.Parse(startSecTextBox.Text) >= 0 &&
                    int.Parse(endMinTextBox.Text) >= 0 &&
                    int.Parse(endSecTextBox.Text) >= 0){

                    return true;
                }
                else {

                    MessageBox.Show("Please enter valid start and end times.");
                    return false;
                }
            }
            catch{

                MessageBox.Show("Please enter valid start and end times.");
                return false;
            }
        }

        private void addFileButton_Click(object sender, EventArgs e){

            m_outputFileController.AddANewSoundFile();

            if (m_outputFileController.GetNumberOfSoundFiles() > 1){

                deleteButton.Enabled = true;
            }

            LeftAndRightButtonsEnableDisable();
            UpdateEditingPosition();
        }

        private void deleteButton_Click(object sender, EventArgs e){

            m_outputFileController.RemoveASoundFile();
            m_outputFileController.IncreaseIndex();

            FillFieldsFromFileObject();

            LeftAndRightButtonsEnableDisable();
            UpdateEditingPosition();

            if (m_outputFileController.GetNumberOfSoundFiles() == 1){

                deleteButton.Enabled = false;
            }
        }

        private void fileLeftButton_Click(object sender, EventArgs e){

            if (m_outputFileController.GetCurrentFileIndex() > 0){

                SaveFieldsToFileObject();
                m_outputFileController.DecreaseIndex();

                LeftAndRightButtonsEnableDisable();
                FillFieldsFromFileObject();
                UpdateEditingPosition();
            }
        }

        private void fileRightButton_Click(object sender, EventArgs e){

            if (m_outputFileController.GetCurrentFileIndex() < m_outputFileController.GetNumberOfSoundFiles()-1){

                SaveFieldsToFileObject();
                m_outputFileController.IncreaseIndex();

                LeftAndRightButtonsEnableDisable();
                FillFieldsFromFileObject();
                UpdateEditingPosition();
            }
        }

        private void LeftAndRightButtonsEnableDisable(){

            if (m_outputFileController.GetCurrentFileIndex() == 0){

                fileLeftButton.Enabled = false;
            }
            else{

                fileLeftButton.Enabled = true;
            }

            if (m_outputFileController.GetCurrentFileIndex() == m_outputFileController.GetNumberOfSoundFiles() - 1){

                fileRightButton.Enabled = false;
            }
            else{

                fileRightButton.Enabled = true;
            }
        }

        public OutputFileController GetOutputFileController(){

            return m_outputFileController;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
