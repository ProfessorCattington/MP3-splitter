using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;

namespace ColdCutsNS{

    public partial class MainForm : Form {

        public OutputFileController outputFiles;
        private TAG_INFO inputFileTags;
        private ImageForm imageForm = new ImageForm();

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
            menu.Hide();
            string file = FileBrowser.Show();

            if (!string.IsNullOrEmpty(file))
            {
                UpdateFormWithSource(file);
                UpdateTheImageForm();
            }
        }

        private void destinationBrowseButton_Click(object sender, EventArgs e)
        {
            menu.Hide();
            string dir = FolderBrowser.Show();
            if (!string.IsNullOrEmpty(dir))
                UpdateFormWithDestination(dir);
        }
        private void encodeButton_Click(object sender, EventArgs e){

            if (EndTimesArentZero(dataGridView1))
            {
                menu.Hide();
                Leave(sender, e);
                DataGridViewLeave(sender, e);
                PerformEncodingTasks();
            }
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
                    {
                        UpdateFormWithSource(item);
                        UpdateTheImageForm();
                    }
                    else if (Directory.Exists(item))
                        UpdateFormWithDestination(item);
                }
            }
        }

        public void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SoundSplit.FindSilence(sourceFilePathTextBox.Text, silence:silence, minGap: minGap, bgWorker: backgroundWorker);
        }

        public void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            feedBackLabel2.Visible = true;
            feedBackLabel2.Text = $" {Math.Round((e.ProgressPercentage/inputFileTags.duration) * 100, 2)}%";
            if (e.UserState != null)
            {
                if (e.UserState.GetType() == typeof(SoundFile))
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
        }

        public void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            feedBackLabel2.Text = "";
            feedBackLabel2.Visible = false;
            Cursor.Current = Cursors.Default;
            EnableObjects(true);
        }

        private void btnAutoSplit_Click(object sender, EventArgs e)
        {
            menu.Hide();
            menu.Visible = false;
            feedBackLabel2.Text = "";
            EnableObjects(false);
            Cursor.Current = Cursors.WaitCursor;
            backgroundWorker.RunWorkerAsync();
        }

        private void btnAutoSplit_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var b = btnAutoSplit.Location;
                menu.Location = new Point(b.X + e.X, b.Y + e.Y);
                menu.Visible = !menu.Visible;
                if (menu.Visible)
                {
                    menu.Show();
                    ToolStripMenuItemOptions.ShowDropDown();
                }
            }
        }
        private void MainForm_Click(object sender, EventArgs e)
        {
            menu.Hide();
        }
    }
}