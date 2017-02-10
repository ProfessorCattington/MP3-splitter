using System;
using System.Windows.Forms;

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

            if (m_mainFormHelper.StartAndEndTimesInDGVAreValid(dataGridView1)){

                m_mainFormHelper.UpdateFromDataGridLeave();
                m_mainFormHelper.SaveFieldsToFileObject();
            }
        }

        private void addFileButton_Click(object sender, EventArgs e){

            m_outputFileController.AddANewSoundFile();

            if (m_outputFileController.GetNumberOfSoundFiles() > 1){

                deleteButton.Enabled = true;
            }

            m_mainFormHelper.LeftAndRightButtonsEnableDisable();
            m_mainFormHelper.UpdateEditingPosition();

            m_mainFormHelper.AddRowToDataGridView();
        }

        private void deleteButton_Click(object sender, EventArgs e){

            m_outputFileController.RemoveASoundFile();

            m_mainFormHelper.FillFieldsFromFileObject();

            m_mainFormHelper.LeftAndRightButtonsEnableDisable();
            m_mainFormHelper.UpdateEditingPosition();

            m_mainFormHelper.DeleteRowFromDataGridView();

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
    }
}
