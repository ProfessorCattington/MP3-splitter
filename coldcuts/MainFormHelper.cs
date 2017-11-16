using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColdCutsNS
{
    partial class MainForm
    {

        private const string INVALID_TIMES = "Please enter valid start and end times.";
        private const string m_endTimeIsZero = "End time on track set to '0' seconds. Please verify that this is correct. Setting track end time to 0 will encode to source file end time.";
        private const string m_editLabelString = "Editing Output File: ";

        public void EnableTheEditingControls()
        {
            this.encodeButton.Enabled = true;
            this.addFileButton.Enabled = true;
            this.startMinTextBox.Enabled = true;
            this.startSecTextBox.Enabled = true;
            this.endMinTextBox.Enabled = true;
            this.endSecTextBox.Enabled = true;
            this.fileNameOutputBox.Enabled = true;
            this.artistOutputTextBox.Enabled = true;
            this.titleOutputTextBox.Enabled = true;
            this.albumOutputTextBox.Enabled = true;
            this.commentOutputTextBox.Enabled = true;
            this.btnAutoSplit.Enabled = true;

            UpdateEditingPosition();
            LeftAndRightButtonsEnableDisable();
            FillFieldsFromFileObject();
        }

        public void InitializeDGV()
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Rows.Add();

            this.dataGridView1.Rows[0].Cells[0].Value = 0;
            this.dataGridView1.Rows[0].Cells[1].Value = "new";
            this.dataGridView1.Rows[0].Cells[2].Value = 0;
            this.dataGridView1.Rows[0].Cells[3].Value = 0;

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        public void UpdateEditingPosition()
        {
            this.editPositionLabel.Text = m_editLabelString + (outputFiles.GetCurrentFileIndex() + 1).ToString() +
                " / " + outputFiles.CountOfSoundFiles.ToString();
        }

        public void DisableTheEditingControls()
        {
            this.fileLeftButton.Enabled = false;
            this.fileRightButton.Enabled = false;
            this.addFileButton.Enabled = false;
            this.startMinTextBox.Enabled = false;
            this.startSecTextBox.Enabled = false;
            this.endMinTextBox.Enabled = false;
            this.endSecTextBox.Enabled = false;
            this.deleteButton.Enabled = false;
            this.fileNameOutputBox.Enabled = false;
            this.artistOutputTextBox.Enabled = false;
            this.titleOutputTextBox.Enabled = false;
            this.albumOutputTextBox.Enabled = false;
            this.commentOutputTextBox.Enabled = false;
            this.encodeButton.Enabled = false;
            this.btnAutoSplit.Enabled = false;
        }

        public void SaveFieldsToFileObject()
        {
            outputFiles.UpdateStartAndEndTimes(this.startMinTextBox.Text,
                                               this.startSecTextBox.Text,
                                               this.endMinTextBox.Text,
                                               this.endSecTextBox.Text);

            outputFiles.UpdateInputTags(this.fileNameOutputBox.Text,
                                        this.artistOutputTextBox.Text,
                                        this.titleOutputTextBox.Text,
                                        this.albumOutputTextBox.Text,
                                        this.commentOutputTextBox.Text);
        }

        public void SaveDataGridToFileObject(int RowIndex)
        {
            var soundFiles = outputFiles.GetOutputFiles();
            var row = dataGridView1.Rows[RowIndex];
            soundFiles[RowIndex].fileName = row.Cells[1].Value.ToString();
            soundFiles[RowIndex].startTimeSeconds = Convert.ToDouble(row.Cells[2].Value.ToString());
            soundFiles[RowIndex].endTimeSeconds = Convert.ToDouble(row.Cells[3].Value.ToString());
        }

        public void UpdateDGVRowNumbers()
        {
            for (int i = 0; i < outputFiles.CountOfSoundFiles; i++)
                this.dataGridView1.Rows[i].Cells[0].Value = i;
        }

        public void UpdateDataGrid(OutputFiles soundFiles)
        {
            for (int i = 0; i < soundFiles.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;

                dataGridView1.Rows[i].Cells[1].Value = soundFiles[i].fileName;
                dataGridView1.Rows[i].Cells[1].ReadOnly = false;

                dataGridView1.Rows[i].Cells[2].Value = soundFiles[i].startTimeSeconds;
                dataGridView1.Rows[i].Cells[2].ReadOnly = false;

                dataGridView1.Rows[i].Cells[3].Value = soundFiles[i].endTimeSeconds;
                dataGridView1.Rows[i].Cells[3].ReadOnly = false;
            }
        }

        public void UpdateTextBoxesFromDataGridLeave(int RowIndex)
        {
            try
            {
                var row = dataGridView1.Rows[RowIndex];
                if (row.Cells[0].Value != null)
                {
                    fileNameOutputBox.Text = row.Cells[1].Value.ToString();

                    int secondsToMins = (int)Math.Round(double.Parse(row.Cells[2].Value.ToString()) / 60);
                    startMinTextBox.Text = secondsToMins.ToString();

                    double remainingSecs = double.Parse(row.Cells[2].Value.ToString()) % 60;
                    startSecTextBox.Text = remainingSecs.ToString();

                    secondsToMins = (int)Math.Round(double.Parse(row.Cells[3].Value.ToString()) / 60);
                    endMinTextBox.Text = secondsToMins.ToString();

                    remainingSecs = double.Parse(row.Cells[3].Value.ToString()) % 60;
                    endSecTextBox.Text = remainingSecs.ToString();
                }
            }
            catch
            {

                MessageBox.Show(INVALID_TIMES);
            }
        }

        public void ColorDataGrid(int rowNumber)
        {

            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            cellStyle.BackColor = Color.Green;

            int numberOfCells = 4;

            for (int i = 0; i < numberOfCells; i++)
            {

                this.dataGridView1.Rows[rowNumber].Cells[i].Style = cellStyle;
            }
        }

        public void DeleteRowFromDataGridView()
        {
            int index = outputFiles.GetCurrentFileIndex();
            this.dataGridView1.Rows.RemoveAt(index);
        }

        public void AddRowToDataGridView(SoundFile sound)
        {
            int index = 0;
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Rows.Add();
            }
            else
            {
                index = outputFiles.GetCurrentFileIndex();
                dataGridView1.Rows.Insert(index);
            }
            dataGridView1.Rows[index].Cells[1].Value = sound.fileName;
            dataGridView1.Rows[index].Cells[2].Value = sound.startTimeSeconds;
            dataGridView1.Rows[index].Cells[3].Value = sound.endTimeSeconds;
        }

        public void FillFieldsFromFileObject()
        {
            FillFieldsTop();
            FillFieldsBottom();
        }

        public void FillFieldsTop()
        {
            this.startMinTextBox.Text = outputFiles.GetStartMinString();
            this.startSecTextBox.Text = outputFiles.GetStartSecString();
            this.endMinTextBox.Text = outputFiles.GetEndMinString();
            this.endSecTextBox.Text = outputFiles.GetEndSecString();
            this.fileNameOutputBox.Text = outputFiles.GetFileName();
        }

        public void FillFieldsBottom()
        {
            this.artistOutputTextBox.Text = outputFiles.GetArtist();
            this.titleOutputTextBox.Text = outputFiles.GetTitle();
            this.albumOutputTextBox.Text = outputFiles.GetAlbum();
            this.commentOutputTextBox.Text = outputFiles.GetComment();
        }

        public bool StartAndEndTimesInEditFieldsAreValid()
        {
            try
            {
                if (int.Parse(startMinTextBox.Text) >= 0 &&
                    double.Parse(startSecTextBox.Text) >= 0 &&
                    int.Parse(endMinTextBox.Text) >= 0 &&
                    double.Parse(endSecTextBox.Text) >= 0)
                {
                    return true;
                }
            }
            catch { }

            MessageBox.Show(INVALID_TIMES);
            return false;
        }

        //if the user has a '0' as their end time it will encode the entire file from their start location
        //this is to warn users
        public bool EndTimesArentZero(DataGridView dataGridView)
        {
            bool userContinues = true;

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {

                if (double.Parse(dataGridView.Rows[i].Cells[3].Value.ToString()) == 0)
                {

                    DialogResult errorMessageResult = MessageBox.Show(m_endTimeIsZero, "End Time 0 Seconds", MessageBoxButtons.OKCancel);

                    if (errorMessageResult == DialogResult.Cancel)
                    {

                        userContinues = false;
                    }
                    else
                    {

                        userContinues = true;
                    }
                }
                else
                {

                    userContinues = true;
                }
            }

            return userContinues;
        }

        public bool StartAndEndTimesInDGVAreValid(DataGridView dataGridView)
        {
            bool startAndEndTimesValid = true;
            try
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    //DGV Leave gets called after you call Add on it. This makes sure the important cells aren't null after you press the + button
                    if (dataGridView.Rows[i].Cells[1].Value == null) dataGridView.Rows[i].Cells[1].Value = "new";
                    if (dataGridView.Rows[i].Cells[2].Value == null) dataGridView.Rows[i].Cells[2].Value = 0;
                    if (dataGridView.Rows[i].Cells[3].Value == null) dataGridView.Rows[i].Cells[3].Value = 0;

                    if (!(double.Parse(dataGridView.Rows[i].Cells[2].Value.ToString()) >= 0) ||
                        !(double.Parse(dataGridView.Rows[i].Cells[3].Value.ToString()) >= 0))
                    {
                        startAndEndTimesValid = false;
                    }
                }
            }
            catch
            {
                startAndEndTimesValid = false;
            }
            if (!startAndEndTimesValid)
                MessageBox.Show(INVALID_TIMES);
            return startAndEndTimesValid;
        }

        public void LeftAndRightButtonsEnableDisable()
        {
            fileLeftButton.Enabled = (outputFiles.GetCurrentFileIndex() != 0);
            fileRightButton.Enabled = (outputFiles.GetCurrentFileIndex() != outputFiles.CountOfSoundFiles - 1);
        }

        #region AutoSplit
        float silence = 2000;
        float minGap = 480000;

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

        private void objectIntOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void minGapMenuItem_KeyUp(object sender, KeyEventArgs e)
        {
            minGap = float.Parse(minGapMenuItem.Text);
        }

        private void silenceMenuItem_KeyUp(object sender, KeyEventArgs e)
        {
            silence = float.Parse(silenceMenuItem.Text);
        }
        #endregion AutoSplit

        private void UpdateFormWithDestination(string dir)
        {
            destinationFilePathTextBox.Text = dir + "\\";
            if (SourceAndDestinationFilled())
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

            if (SourceAndDestinationFilled())
                EnableTheEditingControls();

            if (string.IsNullOrEmpty(destinationFilePathTextBox.Text))
                destinationFilePathTextBox.Focus();
            Cursor.Current = Cursors.Default;
        }

        public string Mp3File
        {
            get { return sourceFilePathTextBox.Text; }
        }

        public async void PerformEncodingTasks()
        {
            DisableTheEditingControls();

            this.feedBackLabel.Visible = true;
            this.feedBackLabel.Text = "Encoding...";

            await EncodeFilesAsync();

            this.sourceBrowseButton.Enabled = true;
            this.destinationBrowseButton.Enabled = true;
            EnableTheEditingControls();

            this.feedBackLabel.Text = "Done Encoding!";
            this.feedBackLabel2.Visible = false;
        }

        public Task<Encoder> EncodeFilesAsync()
        {

            return Task.Factory.StartNew(() => new Encoder(this, outputFiles));
            //return Task.Factory.StartNew(() => MakeAnEncoder());
        }

        //public Encoder MakeAnEncoder()
        //{
        //    return new Encoder(this, outputFiles);
        //}

        public bool SourceAndDestinationFilled()
        {

            return (this.sourceFilePathTextBox.Text != "" && this.destinationFilePathTextBox.Text != "");
        }

        public async void UpdateTheImageForm()
        {
            List<int> soundWave = new List<int>();
            soundWave = await LoadSoundWaveAsync(sourceFilePathTextBox.Text);

            if (!imageForm.IsDisposed)
            {
                if (!imageForm.Visible)
                {
                    imageForm.Show();
                    imageForm.Location = new Point(Location.X, Location.Y + Height);
                }

                imageForm.Text = sourceFilePathTextBox.Text;
                imageForm.ShowSound(soundWave);

                imageForm.increaseResolutionButton.Enabled = true;
                imageForm.decreaseResolutionButton.Enabled = true;
            }
        }

        public Task<List<int>> LoadSoundWaveAsync(string fileName)
        {
            return Task.Factory.StartNew(
                () => SoundSplit.GetSoundWave(fileName));
        }

        public void FileEncodingNotification(long bytesDone, long bytesTotal)
        {
            Console.Write("Encoding: {0:P}\r", Math.Round((double)bytesDone / (double)bytesTotal, 2));
            //feedBackLabel2.Text = Math.Round((double)bytesDone / (double)bytesTotal).ToString();
        }
    }
}
