using System;
using System.ComponentModel;
using System.Threading;
using Un4seen.Bass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColdCutsNS.Tests
{
    [TestClass()]
    public class MainFormTests
    {
        [TestMethod()]
        public void backgroundWorker_DoWorkTest()
        {
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                Assert.Fail();
            }
            else
            {
                int pauses = 0;
                var f = new MainForm();
                f.sourceFilePathTextBox.Text = @"C:\Users\Public\Music\Sample Music\Kalimba.mp3";
                f.backgroundWorker.ProgressChanged += delegate (object o, ProgressChangedEventArgs p){ pauses++; };
                f.backgroundWorker.RunWorkerAsync();
                while (f.backgroundWorker.IsBusy)
                    Thread.Sleep(100);

                Bass.BASS_Free();
                Assert.IsTrue(pauses > 0);
            }
        }
    }
}