using Un4seen.Bass.Misc;

namespace ColdCutsNS
{
    public class Encoder
    {
        public Encoder(MainForm mainForm, OutputFileController outputFiles)
        {
            string inputFile = mainForm.sourceFilePathTextBox.Text;
            for (int i = 0; i < outputFiles.CountOfSoundFiles; i++)
            {
                outputFiles.GoToIndex(i);
                var resp = BaseEncoder.EncodeFile(
                    inputFile: inputFile,
                    outputFile: mainForm.destinationFilePathTextBox.Text + outputFiles.GetFileName() + ".mp3",
                    encoder: new EncoderLAME(0) { LAME_UseVBR = true, TAGs = outputFiles.TagInfo },
                    proc: new BaseEncoder.ENCODEFILEPROC(mainForm.FileEncodingNotification),
                    overwriteOutput: true,
                    deleteInput: false,
                    updateTags: false,
                    fromPos: outputFiles.GetStartTime() + 0.0f,
                    toPos: outputFiles.GetEndTime() + 0.0f
                );
                mainForm.ColorDataGrid(i);
            }
        }
    }
}
