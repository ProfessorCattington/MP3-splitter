using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Un4seen.Bass;

namespace ColdCutsNS{

    public partial class MainForm : Form {

        protected OutputFileController m_outputFileController;

        public MainForm()
        {
           InitializeComponent();
           InitializeFields();
        }

        private void InitializeFields(){

            m_outputFileController = new OutputFileController();
            artistInputLabel.Text = "";
            titleInputLabel.Text = "";
            lengthInputLabel.Text = "";
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            string file = SourceFileBrowser.Show();
            if (!string.IsNullOrEmpty(file))
                UpdateFormWithSource(file);
        }

        private void destinationBrowseButton_Click(object sender, EventArgs e)
        {
            new DestinationFileBrowser(this);
        }
        private void encodeButton_Click(object sender, EventArgs e){

            Leave(sender, e);
            DataGridViewLeave(sender, e);
            this.PerformEncodingTasks();
        }

        public new void Leave(object sender, EventArgs e){

            if (startMinTextBox.Text == "") { startMinTextBox.Text = "0"; }
            if (startSecTextBox.Text == "") { startSecTextBox.Text = "0"; }
            if (endMinTextBox.Text == "") { endMinTextBox.Text = "0"; }
            if (endSecTextBox.Text == "") { endSecTextBox.Text = "0"; }

            if (this.StartAndEndTimesInEditFieldsAreValid()){

                this.SaveFieldsToFileObject();
                this.UpdateDataGrid();
            }
        }

        public void DataGridViewLeave(object sender, EventArgs e){

            //inserting a new row into the DGV also calls leave, which can cause an exception since we end up trying to add stuff to the row before it's initialized
            bool wasARowJustAddedToDGV = dataGridView1.RowCount == m_outputFileController.GetNumberOfSoundFiles() ? false : true;

            if (this.StartAndEndTimesInDGVAreValid(dataGridView1) && !wasARowJustAddedToDGV){

                this.SaveDataGridToFileObject();
                this.UpdateTextBoxesFromDataGridLeave();
            }
        }

        public void DataGridViewClickedNewRow(object sender, DataGridViewCellEventArgs e){

            //get the file index we are switching to
            int index = e.RowIndex;

            if (index <= 0) index = 0;

            m_outputFileController.GotoIndex(index);
            this.UpdateEditingPosition();
            this.LeftAndRightButtonsEnableDisable();
            this.UpdateTextBoxesFromDataGridLeave();
        }

        private void addFileButton_Click(object sender, EventArgs e){

            m_outputFileController.AddANewSoundFile();
            this.AddRowToDataGridView();
            this.UpdateDGVRowNumbers();

            if (m_outputFileController.GetNumberOfSoundFiles() > 1){

                deleteButton.Enabled = true;
            }

            this.LeftAndRightButtonsEnableDisable();
            this.UpdateEditingPosition();
        }

        private void deleteButton_Click(object sender, EventArgs e){

            this.DeleteRowFromDataGridView();//DGV doesn't get updated properly if you modify the editing position before it
            m_outputFileController.RemoveASoundFile();

            this.UpdateDGVRowNumbers();

            this.FillFieldsFromFileObject();

            this.LeftAndRightButtonsEnableDisable();
            this.UpdateEditingPosition();

            if (m_outputFileController.GetNumberOfSoundFiles() == 1){

                deleteButton.Enabled = false;
            }
        }

        private void fileLeftButton_Click(object sender, EventArgs e){

            if (m_outputFileController.GetCurrentFileIndex() > 0){

                this.SaveFieldsToFileObject();
                m_outputFileController.DecreaseIndex();

                this.LeftAndRightButtonsEnableDisable();
                this.FillFieldsFromFileObject();
                this.UpdateEditingPosition();
            }
        }

        private void fileRightButton_Click(object sender, EventArgs e){

            if (m_outputFileController.GetCurrentFileIndex() < m_outputFileController.GetNumberOfSoundFiles()-1){

                this.SaveFieldsToFileObject();
                m_outputFileController.IncreaseIndex();

                this.LeftAndRightButtonsEnableDisable();
                this.FillFieldsFromFileObject();
                this.UpdateEditingPosition();
            }
        }

        public OutputFileController OutputFileController
        {
            get { return m_outputFileController; }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Bass.BASS_Free();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                MessageBox.Show("Error loading Un4seen.Bass", "Un4seen.Bass", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (var item in Environment.GetCommandLineArgs())
                {
                    if (File.Exists(item) && item.ToLower().EndsWith(".mp3"))
                    {
                        UpdateFormWithSource(item);
                        break;
                    }
                }
            }
        }

        #region AutoSplit
        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            float block = 20;
            var chan = Bass.BASS_StreamCreateFile(sourceFilePathTextBox.Text, 0, 0, BASSFlag.BASS_STREAM_DECODE);
            var len = Bass.BASS_ChannelSeconds2Bytes(chan, block / (float)1000 - (float)0.02);

            int level = 0;
            int count = 0;
            float position = block;
            var buffer = new IntPtr();
            while (-1 != (level = Bass.BASS_ChannelGetLevel(chan)))
            {
                if ((count = (level < 600) ? count++ : 0) == 50)
                    backgroundWorker.ReportProgress(level, null);
                Bass.BASS_ChannelGetData(chan, buffer, (int)len);
                position += block;
            }
            Bass.BASS_StreamFree(chan);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            feedBackLabel2.Visible = true;
            feedBackLabel2.Text += ".";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            feedBackLabel2.Text = "";
            feedBackLabel2.Visible = false;
            Cursor.Current = Cursors.Default;
            btnAutoSplit.Enabled = true;
        }

        private void btnAutoSplit_Click(object sender, EventArgs e)
        {
            feedBackLabel2.Text = "";
            btnAutoSplit.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            backgroundWorker.RunWorkerAsync();
        }

        private void UpdateFormWithSource(string FileName)
        {
            Cursor.Current = Cursors.WaitCursor;
            sourceFilePathTextBox.Text = FileName;
            var inputFileTags = OutputFileController.FillInputFileTags(sourceFilePathTextBox.Text);

            artistInputLabel.Text = inputFileTags.artist;
            titleInputLabel.Text = inputFileTags.title;
            lengthInputLabel.Text = Math.Round(inputFileTags.duration, 0).ToString() + " seconds";

            destinationBrowseButton.Enabled = true;
            destinationFilePathTextBox.Enabled = true;
            btnAutoSplit.Enabled = true;

            if (this.AreSourceAndDestinationFilled())
                this.EnableTheEditingControls();
            Cursor.Current = Cursors.Default;
        }
        #endregion AutoSplit
    }
}
