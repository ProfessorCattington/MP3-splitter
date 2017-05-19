using System.IO;
using System.Windows.Forms;


namespace ColdCutsNS
{
    public class SourceFileBrowser
    {
        public static string Show()
        {
            string file = null;
            Stream stream;
            OpenFileDialog openSourceFileDialog = new OpenFileDialog();

            openSourceFileDialog.InitialDirectory = "c:\\";
            openSourceFileDialog.Filter = "MP3 files (*.mp3)|*.mp3";
            openSourceFileDialog.FilterIndex = 1;
            openSourceFileDialog.RestoreDirectory = true;

            if (openSourceFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = openSourceFileDialog.OpenFile()) != null)
                {
                    file = openSourceFileDialog.FileName;
                    stream.Close();
                }
            }
            return file;
        }
    }
}
