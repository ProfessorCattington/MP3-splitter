using System.IO;
using System.Windows.Forms;

namespace ColdCutsNS
{
    public class FileBrowser
    {
        public static string Show()
        {
            string file = null;
            Stream stream;
            var fileDialog = new OpenFileDialog()
            {
                InitialDirectory = "c:\\",
                Filter = "MP3 files (*.mp3)|*.mp3",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = fileDialog.OpenFile()) != null)
                {
                    file = fileDialog.FileName;
                    stream.Close();
                }
            }
            return file;
        }
    }
}
