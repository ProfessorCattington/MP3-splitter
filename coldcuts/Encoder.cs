using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace ColdCutsNS
{
    public class Encoder
    {
        public Encoder(MainForm mainForm, OutputFileController outputFiles)
        {
            EncoderLAME lameEncoder = new EncoderLAME(0);
            lameEncoder.LAME_UseVBR = true;

            bool overwriteOutput = true;
            bool deleteInput = false;
            bool useInputFileTags = false;

            string inputFile = mainForm.sourceFilePathTextBox.Text;

            for (int i = 0; i < outputFiles.CountOfSoundFiles; i++)
            {
                outputFiles.GoToIndex(i);
                string fileName = mainForm.destinationFilePathTextBox.Text + outputFiles.GetFileName();

                double startPoint = outputFiles.GetStartTime();
                double endPoint = outputFiles.GetEndTime();

                TAG_INFO tempTags = new TAG_INFO();
                tempTags.artist = outputFiles.GetArtist();
                tempTags.title = outputFiles.GetTitle();
                tempTags.album = outputFiles.GetAlbum();
                tempTags.comment = outputFiles.GetComment();

                lameEncoder.TAGs = tempTags;

                BaseEncoder.EncodeFile(inputFile, fileName + ".mp3", lameEncoder,
                      new BaseEncoder.ENCODEFILEPROC(mainForm.FileEncodingNotification),
                      overwriteOutput, deleteInput, useInputFileTags,
                      startPoint + 0.0f, endPoint + 0.0f);

                mainForm.ColorDataGrid(i);
            }
        }
    }
}
