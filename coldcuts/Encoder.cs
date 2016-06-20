using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace ColdCutsNS
{
    public class Encoder{

        public Encoder(MainForm mainForm, MainFormHelper mainFormHelper, OutputFileController outputFileController){

            EncoderLAME lameEncoder = new EncoderLAME(0);
            lameEncoder.LAME_UseVBR = true;

            bool overwriteOutput = true;
            bool deleteInput = false;
            bool useInputFileTags = false;

            string inputFile = mainForm.sourceFilePathTextBox.Text;

            int numberOfSoundFiles = outputFileController.GetNumberOfSoundFiles();

            for (int i = 0; i < numberOfSoundFiles; i++){

                outputFileController.GoToIndex(i);

                string fileName = mainForm.destinationFilePathTextBox.Text + outputFileController.GetFileName();

                long startPoint = outputFileController.GetStartTime();
                long endPoint = outputFileController.GetEndTime();

                TAG_INFO tempTags = new TAG_INFO();
                tempTags.artist = outputFileController.GetArtist();
                tempTags.title = outputFileController.GetTitle();
                tempTags.album = outputFileController.GetAlbum();
                tempTags.comment = outputFileController.GetComment();

                lameEncoder.TAGs = tempTags;

                BaseEncoder.EncodeFile(inputFile, fileName + ".mp3", lameEncoder,
                      new BaseEncoder.ENCODEFILEPROC(mainFormHelper.FileEncodingNotification),
                      overwriteOutput, deleteInput, useInputFileTags,
                      startPoint + 0.0f, endPoint + 0.0f);

                mainFormHelper.ColorDataGrid(i);
            }
        }
    }
}
