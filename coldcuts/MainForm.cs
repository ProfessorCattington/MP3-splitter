using System;
using System.IO;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;

namespace ColdCutsNS{

    public partial class MainForm : Form {

        public OutputFileController outputFiles;
        private TAG_INFO inputFileTags;

        public MainForm()
        {
           InitializeComponent();
           InitializeFields();
        }

        private void InitializeFields()
        {
            outputFiles = new OutputFileController();
            artistInputLabel.Text = "";
            titleInputLabel.Text = "";
            lengthInputLabel.Text = "";
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            string file = FileBrowser.Show();
            if (!string.IsNullOrEmpty(file))
                UpdateFormWithSource(file);
        }

        private void destinationBrowseButton_Click(object sender, EventArgs e)
        {
            string dir = FolderBrowser.Show();
            if (!string.IsNullOrEmpty(dir))
                UpdateFormWithDestination(dir);
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

        public void DataGridViewLeave(object sender, EventArgs e)
        {
            //inserting a new row into the DGV also calls leave, which can cause an exception since we end up trying to add stuff to the row before it's initialized
            bool wasARowJustAddedToDGV = dataGridView1.RowCount == outputFiles.CountOfSoundFiles ? false : true;

            if (StartAndEndTimesInDGVAreValid(dataGridView1) && !wasARowJustAddedToDGV)
            {
                if (e.GetType() == typeof(DataGridViewCellEventArgs))
                {
                    int RowIndex = ((DataGridViewCellEventArgs)e).RowIndex;
                    if (RowIndex <= 0) RowIndex = 0;
                    SaveDataGridToFileObject(RowIndex);
                }
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            outputFiles.GoToIndex(e.RowIndex);
            UpdateEditingPosition();
            LeftAndRightButtonsEnableDisable();
            outputFiles.GoToIndex(e.RowIndex);
            UpdateTextBoxesFromDataGridLeave(e.RowIndex);
            FillFieldsBottom();
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            addSoundFile(new SoundFile());
        }

        private void addSoundFile(SoundFile sound)
        {
            outputFiles.AddSoundFile(sound);
            AddRowToDataGridView(sound);
            UpdateDGVRowNumbers();

            if (outputFiles.CountOfSoundFiles > 1)
                deleteButton.Enabled = true;

            LeftAndRightButtonsEnableDisable();
            UpdateEditingPosition();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //DGV doesn't get updated properly if you modify the editing position before it
            this.DeleteRowFromDataGridView();
            outputFiles.RemoveASoundFile();

            this.UpdateDGVRowNumbers();
            if (outputFiles.CountOfSoundFiles > 0)
                this.FillFieldsFromFileObject();

            this.LeftAndRightButtonsEnableDisable();
            this.UpdateEditingPosition();

            deleteButton.Enabled = (outputFiles.CountOfSoundFiles > 1);
        }

        private void fileLeftButton_Click(object sender, EventArgs e){

            if (outputFiles.GetCurrentFileIndex() > 0){

                this.SaveFieldsToFileObject();
                outputFiles.DecreaseIndex();

                this.LeftAndRightButtonsEnableDisable();
                this.FillFieldsFromFileObject();
                this.UpdateEditingPosition();
            }
        }

        private void fileRightButton_Click(object sender, EventArgs e)
        {
            if (outputFiles.GetCurrentFileIndex() < outputFiles.CountOfSoundFiles -1)
            {
                this.SaveFieldsToFileObject();
                outputFiles.IncreaseIndex();

                this.LeftAndRightButtonsEnableDisable();
                this.FillFieldsFromFileObject();
                this.UpdateEditingPosition();
            }
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
                this.Refresh();
                foreach (var item in Environment.GetCommandLineArgs())
                {
                    if (File.Exists(item) && item.ToLower().EndsWith(".mp3"))
                        UpdateFormWithSource(item);
                    else if(Directory.Exists(item))
                        UpdateFormWithDestination(item);
                }
            }
        }

        #region AutoSplit
        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            const float block = 100;
            const float minGap = 480000;

            int level = 0;
            int count = 0;
            float gap = 0;
            float start = 0;
            float position = block;
            var buffer = new IntPtr();

            var chan = Bass.BASS_StreamCreateFile(sourceFilePathTextBox.Text, 0, 0, BASSFlag.BASS_STREAM_DECODE);
            var len = Bass.BASS_ChannelSeconds2Bytes(chan, block / (float)1000 - (float)0.02);

            while ((level = Bass.BASS_ChannelGetLevel(chan)) != -1)
            {
                int left = Utils.LowWord32(level);
                int right = Utils.HighWord32(level);
                if (((count = ((left + right) < 40000) ? count+1 : 0) == 200) && (gap > minGap))
                {
                    var pos = (int)Math.Round(position / 1000);
                    var sound = new SoundFile($"File_{pos}", start/1000.0, position/1000.0);
                    backgroundWorker.ReportProgress(pos, sound);
                    start = position + 1;
                    gap = 0;
                }
                else if (position % 50000 == 0)
                {
                    backgroundWorker.ReportProgress((int)Math.Round(position / 1000), null);
                }
                Bass.BASS_ChannelGetData(chan, buffer, (int)len);
                position += block;
                gap += block;
            }
            Bass.BASS_StreamFree(chan);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            feedBackLabel2.Visible = true;
            feedBackLabel2.Text = $" {Math.Round((e.ProgressPercentage/inputFileTags.duration) * 100, 2)}%";
            if (e.UserState != null)
            {
                if (outputFiles.CountOfSoundFiles == 1)
                {
                    var outFiles = outputFiles.GetOutputFiles();
                    if (outFiles[0].endTimeSeconds == 0 && outFiles[0].startTimeSeconds == 0)
                        deleteButton_Click(null, null);
                }
                addSoundFile((SoundFile)e.UserState);
                outputFiles.IncreaseIndex();
                EnableObjects(false);
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            feedBackLabel2.Text = "";
            feedBackLabel2.Visible = false;
            Cursor.Current = Cursors.Default;
            EnableObjects(true);
        }

        private void btnAutoSplit_Click(object sender, EventArgs e)
        {
            feedBackLabel2.Text = "";
            EnableObjects(false);
            Cursor.Current = Cursors.WaitCursor;
            backgroundWorker.RunWorkerAsync();
        }

        private void EnableObjects(bool enabled)
        {
            btnAutoSplit.Enabled = enabled;
            encodeButton.Enabled = enabled;
            fileLeftButton.Enabled = enabled;
            fileRightButton.Enabled = enabled;
            addFileButton.Enabled = enabled;
            deleteButton.Enabled = enabled;
            dataGridView1.Enabled = enabled;
            EnableTextBox(Controls, enabled);
        }

        private void EnableTextBox(Control.ControlCollection cControls, bool enabled)
        {
            foreach (Control c in cControls)
            {
                if (c.GetType() == typeof(TextBox))
                    c.Enabled = enabled;
                else if (c.Controls != null)
                    EnableTextBox(c.Controls, enabled);
            }
        }

        private void UpdateFormWithDestination(string dir)
        {
            destinationFilePathTextBox.Text = dir + "\\";
            if (AreSourceAndDestinationFilled())
            {
                EnableTheEditingControls();
                InitializeDGV();
            }
        }

        private void UpdateFormWithSource(string FileName)
        {
            Cursor.Current = Cursors.WaitCursor;
            sourceFilePathTextBox.Text = FileName;
            inputFileTags = outputFiles.FillInputFileTags(sourceFilePathTextBox.Text);

            artistInputLabel.Text = inputFileTags.artist;
            titleInputLabel.Text = inputFileTags.title;
            lengthInputLabel.Text = Math.Round(inputFileTags.duration, 0).ToString() + " seconds";

            destinationBrowseButton.Enabled = true;
            destinationFilePathTextBox.Enabled = true;

            if (AreSourceAndDestinationFilled())
                EnableTheEditingControls();
            Cursor.Current = Cursors.Default;
        }
        #endregion AutoSplit
    }
}
