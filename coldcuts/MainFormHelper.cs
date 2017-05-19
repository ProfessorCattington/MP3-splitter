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

        public void EnableTheEditingControls() {

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

            UpdateEditingPosition();
            LeftAndRightButtonsEnableDisable();
            FillFieldsFromFileObject();
        }

        public void InitializeDGV(){

            this.dataGridView1.Rows.Add();

            this.dataGridView1.Rows[0].Cells[0].Value = 0;
            this.dataGridView1.Rows[0].Cells[1].Value = "<blank>";
            this.dataGridView1.Rows[0].Cells[2].Value = 0;
            this.dataGridView1.Rows[0].Cells[3].Value = 0;

            foreach(DataGridViewColumn column in this.dataGridView1.Columns){

                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        public void UpdateEditingPosition()
        {
            this.editPositionLabel.Text = m_editLabelString + (OutputFileController.GetCurrentFileIndex() + 1).ToString() +
                " / " + OutputFileController.GetNumberOfSoundFiles().ToString();
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
        }

        public void SaveFieldsToFileObject()
        {

            OutputFileController.UpdateStartAndEndTimes(this.startMinTextBox.Text, this.startSecTextBox.Text,
                                                              this.endMinTextBox.Text, this.endSecTextBox.Text);

            OutputFileController.UpdateInputTags(this.fileNameOutputBox.Text,
                                                this.artistOutputTextBox.Text,
                                                 this.titleOutputTextBox.Text,
                                                 this.albumOutputTextBox.Text,
                                                this.commentOutputTextBox.Text);
        }

        public void SaveDataGridToFileObject()
        {
            List<NewSoundFile> soundFiles = OutputFileController.GetOutputFiles();

            for(int i = 0; i < soundFiles.Count; i++){

                soundFiles[i].fileName = this.dataGridView1.Rows[i].Cells[1].Value.ToString();

                string stringValue = this.dataGridView1.Rows[i].Cells[2].Value.ToString();
                soundFiles[i].startTimeSeconds = Convert.ToDouble(stringValue);

                stringValue = this.dataGridView1.Rows[i].Cells[3].Value.ToString();
                soundFiles[i].endTimeSeconds = Convert.ToDouble(stringValue);
            }
        }

        public void UpdateDGVRowNumbers()
        {
            List<NewSoundFile> soundFiles = OutputFileController.GetOutputFiles();

            for (int i = 0; i < soundFiles.Count; i++){

                this.dataGridView1.Rows[i].Cells[0].Value = i;
            }
        }

        public void UpdateDataGrid()
        {
            List<NewSoundFile> soundFiles = OutputFileController.GetOutputFiles();

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

        public void UpdateTextBoxesFromDataGridLeave()
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++){

                if(i == OutputFileController.GetCurrentFileIndex()){

                    this.fileNameOutputBox.Text = this.dataGridView1.Rows[i].Cells[1].Value.ToString();

                    int secondsToMins = (int)Math.Round(double.Parse(this.dataGridView1.Rows[i].Cells[2].Value.ToString()) / 60);

                    this.startMinTextBox.Text = secondsToMins.ToString();

                    double remainingSecs = double.Parse(this.dataGridView1.Rows[i].Cells[2].Value.ToString()) % 60;

                    this.startSecTextBox.Text = remainingSecs.ToString();

                    secondsToMins = (int)Math.Round(double.Parse(this.dataGridView1.Rows[i].Cells[3].Value.ToString()) / 60);

                    this.endMinTextBox.Text = secondsToMins.ToString();

                    remainingSecs = double.Parse(this.dataGridView1.Rows[i].Cells[3].Value.ToString()) % 60;

                    this.endSecTextBox.Text = remainingSecs.ToString();

                }
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
            int index = OutputFileController.GetCurrentFileIndex();
            this.dataGridView1.Rows.RemoveAt(index);
        }

        public void AddRowToDataGridView()
        {
            int index = OutputFileController.GetCurrentFileIndex() + 1;
            this.dataGridView1.Rows.Insert(index);
            this.dataGridView1.Rows[index].Cells[1].Value = "new";
            this.dataGridView1.Rows[index].Cells[2].Value = 0;
            this.dataGridView1.Rows[index].Cells[3].Value = 0;
        }

        public void FillFieldsFromFileObject()
        {
            this.startMinTextBox.Text = OutputFileController.GetStartMinString();
            this.startSecTextBox.Text = OutputFileController.GetStartSecString();
            this.endMinTextBox.Text = OutputFileController.GetEndMinString();
            this.endSecTextBox.Text = OutputFileController.GetEndSecString();
            this.fileNameOutputBox.Text = OutputFileController.GetFileName();
            this.artistOutputTextBox.Text = OutputFileController.GetArtist();
            this.titleOutputTextBox.Text = OutputFileController.GetTitle();
            this.albumOutputTextBox.Text = OutputFileController.GetAlbum();
            this.commentOutputTextBox.Text = OutputFileController.GetComment();

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
                    if (dataGridView.Rows[i].Cells[1].Value == null) dataGridView.Rows[i].Cells[1].Value = "<blank>";
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
            if (OutputFileController.GetCurrentFileIndex() == 0)
            {
                this.fileLeftButton.Enabled = false;
            }
            else
            {
                this.fileLeftButton.Enabled = true;
            }

            if (OutputFileController.GetCurrentFileIndex() == OutputFileController.GetNumberOfSoundFiles() - 1)
            {
                this.fileRightButton.Enabled = false;
            }
            else
            {
                this.fileRightButton.Enabled = true;
            }
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
            return new Encoder(this, OutputFileController);
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
