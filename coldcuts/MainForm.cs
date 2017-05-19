using System;
using System.Windows.Forms;
using Un4seen.Bass;

namespace ColdCutsNS{

    public partial class MainForm : Form {

        protected OutputFileController m_outputFileController;
        protected MainFormHelper m_mainFormHelper;

        public MainForm() {

           InitializeComponent();
           InitializeFields();

            m_mainFormHelper = new MainFormHelper(this);
        }

        private void InitializeFields(){

            m_outputFileController = new OutputFileController();
            artistInputLabel.Text = "";
            titleInputLabel.Text = "";
            lengthInputLabel.Text = "";
        }

        private void browseButton_Click(object sender, EventArgs e){

            new SourceFileBrowser(this, m_mainFormHelper);
        }

        private void destinationBrowseButton_Click(object sender, EventArgs e){

            new DestinationFileBrowser(this, m_mainFormHelper);
        }
        private void encodeButton_Click(object sender, EventArgs e){

            Leave(sender, e);
            DataGridViewLeave(sender, e);
            m_mainFormHelper.PerformEncodingTasks();
        }

        public new void Leave(object sender, EventArgs e){

            if (startMinTextBox.Text == "") { startMinTextBox.Text = "0"; }
            if (startSecTextBox.Text == "") { startSecTextBox.Text = "0"; }
            if (endMinTextBox.Text == "") { endMinTextBox.Text = "0"; }
            if (endSecTextBox.Text == "") { endSecTextBox.Text = "0"; }

            if (m_mainFormHelper.StartAndEndTimesInEditFieldsAreValid()){

                m_mainFormHelper.SaveFieldsToFileObject();
                m_mainFormHelper.UpdateDataGrid();
            }
        }

        public void DataGridViewLeave(object sender, EventArgs e){

            //inserting a new row into the DGV also calls leave, which can cause an exception since we end up trying to add stuff to the row before it's initialized
            bool wasARowJustAddedToDGV = dataGridView1.RowCount == m_outputFileController.GetNumberOfSoundFiles() ? false : true;

            if (m_mainFormHelper.StartAndEndTimesInDGVAreValid(dataGridView1) && !wasARowJustAddedToDGV){

                m_mainFormHelper.SaveDataGridToFileObject();
                m_mainFormHelper.UpdateTextBoxesFromDataGridLeave();
            }
        }

        public void DataGridViewClickedNewRow(object sender, DataGridViewCellEventArgs e){

            //get the file index we are switching to
            int index = e.RowIndex;

            if (index <= 0) index = 0;

            m_outputFileController.GotoIndex(index);
            m_mainFormHelper.UpdateEditingPosition();
            m_mainFormHelper.LeftAndRightButtonsEnableDisable();
            m_mainFormHelper.UpdateTextBoxesFromDataGridLeave();
        }

        private void addFileButton_Click(object sender, EventArgs e){

            m_outputFileController.AddANewSoundFile();
            m_mainFormHelper.AddRowToDataGridView();
            m_mainFormHelper.UpdateDGVRowNumbers();

            if (m_outputFileController.GetNumberOfSoundFiles() > 1){

                deleteButton.Enabled = true;
            }

            m_mainFormHelper.LeftAndRightButtonsEnableDisable();
            m_mainFormHelper.UpdateEditingPosition();
        }

        private void deleteButton_Click(object sender, EventArgs e){

            m_mainFormHelper.DeleteRowFromDataGridView();//DGV doesn't get updated properly if you modify the editing position before it
            m_outputFileController.RemoveASoundFile();

            m_mainFormHelper.UpdateDGVRowNumbers();

            m_mainFormHelper.FillFieldsFromFileObject();

            m_mainFormHelper.LeftAndRightButtonsEnableDisable();
            m_mainFormHelper.UpdateEditingPosition();

            if (m_outputFileController.GetNumberOfSoundFiles() == 1){

                deleteButton.Enabled = false;
            }
        }

        private void fileLeftButton_Click(object sender, EventArgs e){

            if (m_outputFileController.GetCurrentFileIndex() > 0){

                m_mainFormHelper.SaveFieldsToFileObject();
                m_outputFileController.DecreaseIndex();

                m_mainFormHelper.LeftAndRightButtonsEnableDisable();
                m_mainFormHelper.FillFieldsFromFileObject();
                m_mainFormHelper.UpdateEditingPosition();
            }
        }

        private void fileRightButton_Click(object sender, EventArgs e){

            if (m_outputFileController.GetCurrentFileIndex() < m_outputFileController.GetNumberOfSoundFiles()-1){

                m_mainFormHelper.SaveFieldsToFileObject();
                m_outputFileController.IncreaseIndex();

                m_mainFormHelper.LeftAndRightButtonsEnableDisable();
                m_mainFormHelper.FillFieldsFromFileObject();
                m_mainFormHelper.UpdateEditingPosition();
            }
        }
        public OutputFileController GetOutputFileController(){

            return m_outputFileController;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Bass.BASS_Free();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                MessageBox.Show("Error loading Un4seen.Bass", "Un4seen.Bass", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
