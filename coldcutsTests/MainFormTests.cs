using System;
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
                var sounds = SoundSplit.FindSilence(@"C:\Users\Public\Documents\Audible\Downloads\AlgorithmstoLiveByTheComputerScienceofHumanDecisions.mp3");
                Assert.IsTrue(sounds.Count > 0);
            }
        }
    }
}