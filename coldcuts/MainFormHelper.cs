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

        public void InitializeDGV(){

            this.dataGridView1.Rows.Add();

            this.dataGridView1.Rows[0].Cells[0].Value = 0;
            this.dataGridView1.Rows[0].Cells[1].Value = "new";
            this.dataGridView1.Rows[0].Cells[2].Value = 0;
            this.dataGridView1.Rows[0].Cells[3].Value = 0;

            foreach(DataGridViewColumn column in this.dataGridView1.Columns){

                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        public void UpdateEditingPosition()
        {
            this.editPositionLabel.Text = m_editLabelString + (outputFiles.GetCurrentFileIndex() + 1).ToString() +
                " / " + outputFiles.CountOfSoundFiles.ToString();
        }

        public void DisableTheEditingControls() {

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

        public void UpdateDataGrid()
        {
            var soundFiles = outputFiles.GetOutputFiles();

            for (int i = 0; i < soundFiles.Count; i++) {

                this.dataGridView1.Rows[i].Cells[0].ReadOnly = true;

                this.dataGridView1.Rows[i].Cells[1].Value = soundFiles[i].fileName;
                this.dataGridView1.Rows[i].Cells[1].ReadOnly = false;

                this.dataGridView1.Rows[i].Cells[2].Value = soundFiles[i].startTimeSeconds;
                this.dataGridView1.Rows[i].Cells[2].ReadOnly = false;

                this.dataGridView1.Rows[i].Cells[3].Value = soundFiles[i].endTimeSeconds;
                this.dataGridView1.Rows[i].Cells[3].ReadOnly = false;
            }
        }

        public void UpdateTextBoxesFromDataGridLeave(int RowIndex)
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

        public void ColorDataGrid(int rowNumber) {

            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            cellStyle.BackColor = Color.Green;

            int numberOfCells = 4;

            for (int i = 0; i < numberOfCells; i++) {

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
                index = outputFiles.GetCurrentFileIndex() + 1;
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
            try {
                if (int.Parse(startMinTextBox.Text) >= 0 &&
                    double.Parse(startSecTextBox.Text) >= 0 &&
                    int.Parse(endMinTextBox.Text) >= 0 &&
                    double.Parse(endSecTextBox.Text) >= 0) {
                    return true;
                }
            } catch { }

            MessageBox.Show(INVALID_TIMES);
            return false;
        }

        public bool StartAndEndTimesInDGVAreValid(DataGridView dataGridView)
        {
            bool goodToGo = true;
            try
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    //DGV Leave gets called after you call Add on it. This makes sure the important cells aren't null after you press the + button
                    if (dataGridView.Rows[i].Cells[1].Value == null) dataGridView.Rows[i].Cells[1].Value = "new";
                    if (dataGridView.Rows[i].Cells[2].Value == null) dataGridView.Rows[i].Cells[2].Value = 0;
                    if (dataGridView.Rows[i].Cells[3].Value == null) dataGridView.Rows[i].Cells[3].Value = 0;

                    if (!(double.Parse(dataGridView.Rows[i].Cells[2].Value.ToString()) >= 0) ||
                        !(double.Parse(dataGridView.Rows[i].Cells[3].Value.ToString()) >= 0)){
                        goodToGo = false;
                    }
                }
            }
            catch
            {
                goodToGo = false;
            }
            if (!goodToGo)
                MessageBox.Show(INVALID_TIMES);
            return goodToGo;
        }

        public void LeftAndRightButtonsEnableDisable()
        {
            fileLeftButton.Enabled = (outputFiles.GetCurrentFileIndex() != 0);
            fileRightButton.Enabled = (outputFiles.GetCurrentFileIndex() != outputFiles.CountOfSoundFiles - 1);
        }

        public async void PerformEncodingTasks(){

            DisableTheEditingControls();

            this.feedBackLabel.Visible = true;
            this.feedBackLabel.Text = "Encoding...";

            await EncodeFilesAsync();

            this.sourceBrowseButton.Enabled = true;
            this.destinationBrowseButton.Enabled = true;
            EnableTheEditingControls();

            this.feedBackLabel.Text = "Done!";
            this.feedBackLabel2.Visible = false;
        }

        public Task<Encoder> EncodeFilesAsync(){

            return Task.Factory.StartNew(() => MakeAnEncoder());
        }

        public Encoder MakeAnEncoder()
        {
            return new Encoder(this, outputFiles);
        }

        public bool AreSourceAndDestinationFilled(){

            return (this.sourceFilePathTextBox.Text != "" && this.destinationFilePathTextBox.Text != "");
        }

        public void FileEncodingNotification(long bytesDone, long bytesTotal){

            //Console.Write("Encoding: {0:P}\r", Math.Round((double)bytesDone / (double)bytesTotal, 2));
            //feedBackLabel2.Text = Math.Round((double)bytesDone / (double)bytesTotal).ToString();
        }
    }
}
