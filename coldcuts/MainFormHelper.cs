using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColdCutsNS
{
    public class MainFormHelper{

        protected MainForm m_mainForm;
        private const string m_editLabelString = "Editing Output File: ";
        public MainFormHelper(MainForm mainForm){

            m_mainForm = mainForm;
        }

        public void EnableTheEditingControls(){

            m_mainForm.encodeButton.Enabled = true;
            m_mainForm.addFileButton.Enabled = true;
            m_mainForm.startMinTextBox.Enabled = true;
            m_mainForm.startSecTextBox.Enabled = true;
            m_mainForm.endMinTextBox.Enabled = true;
            m_mainForm.endSecTextBox.Enabled = true;
            m_mainForm.fileNameOutputBox.Enabled = true;
            m_mainForm.artistOutputTextBox.Enabled = true;
            m_mainForm.titleOutputTextBox.Enabled = true;
            m_mainForm.albumOutputTextBox.Enabled = true;
            m_mainForm.commentOutputTextBox.Enabled = true;

            UpdateEditingPosition();
            LeftAndRightButtonsEnableDisable();
            FillFieldsFromFileObject();
        }

        public void UpdateEditingPosition(){

            OutputFileController outputFileController = m_mainForm.GetOutputFileController();

            m_mainForm.editPositionLabel.Text = m_editLabelString + (outputFileController.GetCurrentFileIndex() + 1).ToString() +
                " / " + outputFileController.GetNumberOfSoundFiles().ToString();
        }

        public void DisableTheEditingControls(){

            m_mainForm.fileLeftButton.Enabled = false;
            m_mainForm.fileRightButton.Enabled = false;
            m_mainForm.addFileButton.Enabled = false;
            m_mainForm.startMinTextBox.Enabled = false;
            m_mainForm.startSecTextBox.Enabled = false;
            m_mainForm.endMinTextBox.Enabled = false;
            m_mainForm.endSecTextBox.Enabled = false;
            m_mainForm.deleteButton.Enabled = false;
            m_mainForm.fileNameOutputBox.Enabled = false;
            m_mainForm.artistOutputTextBox.Enabled = false;
            m_mainForm.titleOutputTextBox.Enabled = false;
            m_mainForm.albumOutputTextBox.Enabled = false;
            m_mainForm.commentOutputTextBox.Enabled = false;
            m_mainForm.encodeButton.Enabled = false;
        }

        public void SaveFieldsToFileObject(){

            OutputFileController outputFileController = m_mainForm.GetOutputFileController();

            outputFileController.UpdateStartAndEndTimes(m_mainForm.startMinTextBox.Text, m_mainForm.startSecTextBox.Text,
                                                              m_mainForm.endMinTextBox.Text, m_mainForm.endSecTextBox.Text);

            outputFileController.UpdateInputTags(m_mainForm.fileNameOutputBox.Text,
                                                m_mainForm.artistOutputTextBox.Text,
                                                 m_mainForm.titleOutputTextBox.Text,
                                                 m_mainForm.albumOutputTextBox.Text,
                                                m_mainForm.commentOutputTextBox.Text);
        }

        public void UpdateDataGrid(){

            m_mainForm.dataGridView1.Rows.Clear();

            OutputFileController outputfileController = m_mainForm.GetOutputFileController();

            List<NewSoundFile> soundFiles = outputfileController.GetOutputFiles().GetSoundFiles();

            for (int i = 0; i < soundFiles.Count; i++){

                m_mainForm.dataGridView1.Rows.Add();

                m_mainForm.dataGridView1.Rows[i].Cells[0].Value = i;
                m_mainForm.dataGridView1.Rows[i].Cells[1].Value = soundFiles[i].fileName;
                m_mainForm.dataGridView1.Rows[i].Cells[2].Value = soundFiles[i].startTimeSeconds;
                m_mainForm.dataGridView1.Rows[i].Cells[3].Value = soundFiles[i].endTimeSeconds;
            }
        }

        public void ColorDataGrid(int rowNumber) {

            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            cellStyle.BackColor = Color.Green;

            int numberOfCells = 4;

            for (int i = 0; i < numberOfCells; i++){

                m_mainForm.dataGridView1.Rows[rowNumber].Cells[i].Style = cellStyle;
            }
        }

        public void FillFieldsFromFileObject(){

            OutputFileController outputFileController = m_mainForm.GetOutputFileController();

            m_mainForm.startMinTextBox.Text = outputFileController.GetStartMinString();
            m_mainForm.startSecTextBox.Text = outputFileController.GetStartSecString();
            m_mainForm.endMinTextBox.Text = outputFileController.GetEndMinString();
            m_mainForm.endSecTextBox.Text = outputFileController.GetEndSecString();
            m_mainForm.fileNameOutputBox.Text = outputFileController.GetFileName();
            m_mainForm.artistOutputTextBox.Text = outputFileController.GetArtist();
            m_mainForm.titleOutputTextBox.Text = outputFileController.GetTitle();
            m_mainForm.albumOutputTextBox.Text = outputFileController.GetAlbum();
            m_mainForm.commentOutputTextBox.Text = outputFileController.GetComment();

        }

        public bool StartAndEndTimesAreValid(){

            try{

                if (int.Parse(m_mainForm.startMinTextBox.Text) >= 0 &&
                    int.Parse(m_mainForm.startSecTextBox.Text) >= 0 &&
                    int.Parse(m_mainForm.endMinTextBox.Text) >= 0 &&
                    int.Parse(m_mainForm.endSecTextBox.Text) >= 0){

                    return true;
                }
                else{

                    MessageBox.Show("Please enter valid start and end times.");
                    return false;
                }
            }
            catch{

                MessageBox.Show("Please enter valid start and end times.");
                return false;
            }
        }

        public void LeftAndRightButtonsEnableDisable(){

            OutputFileController outputFileController = m_mainForm.GetOutputFileController();

            if (outputFileController.GetCurrentFileIndex() == 0){

                m_mainForm.fileLeftButton.Enabled = false;
            }
            else{

                m_mainForm.fileLeftButton.Enabled = true;
            }

            if (outputFileController.GetCurrentFileIndex() == outputFileController.GetNumberOfSoundFiles() - 1){

                m_mainForm.fileRightButton.Enabled = false;
            }
            else{

                m_mainForm.fileRightButton.Enabled = true;
            }
        }

        public async void PerformEncodingTasks(){

            DisableTheEditingControls();

            m_mainForm.feedBackLabel.Visible = true;
            m_mainForm.feedBackLabel.Text = "Encoding...";

            await EncodeFilesAsync();

            m_mainForm.sourceBrowseButton.Enabled = true;
            m_mainForm.destinationBrowseButton.Enabled = true;
            EnableTheEditingControls();

            m_mainForm.feedBackLabel.Text = "Done!";
            m_mainForm.feedBackLabel2.Visible = false;
        }

        public Task<Encoder> EncodeFilesAsync(){

            return Task.Factory.StartNew(() => MakeAnEncoder());
        } 

        public Encoder MakeAnEncoder(){

            OutputFileController outputFileController = m_mainForm.GetOutputFileController();

            return new Encoder(m_mainForm, this, outputFileController);
        }

        public bool AreSourceAndDestinationFilled(){

            return (m_mainForm.sourceFilePathTextBox.Text != "" && m_mainForm.destinationFilePathTextBox.Text != "");
        }

        public void FileEncodingNotification(long bytesDone, long bytesTotal){

            //Console.Write("Encoding: {0:P}\r", Math.Round((double)bytesDone / (double)bytesTotal, 2));
            //feedBackLabel2.Text = Math.Round((double)bytesDone / (double)bytesTotal).ToString(); 
        }
    }
}
