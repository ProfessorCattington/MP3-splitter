using System;
using Un4seen.Bass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColdCutsNS.Tests
{
    [TestClass()]
    public class MainFormTests
    {
        [TestMethod()]
        public void FindSilenceTest()
        {
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                Assert.Fail();
            }
            else
            {
                int loops = 10;
                var sound = SoundWave.CreateFile(Guid.NewGuid().ToString() + ".wav", loops);
                var sounds = SoundSplit.FindSilence(sound, 20, 1000, 2500);
                Assert.AreEqual(sounds.Count, loops);
            }
        }
    }
}