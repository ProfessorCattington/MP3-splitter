using System;
using System.Windows.Forms;
using Un4seen.Bass;

namespace ColdCutsNS{

    static class Program{
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //initialize bass.net. this seemed like the best place
           
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero)) {

                Application.Run(new MainForm());
            }
        }
    }
}
