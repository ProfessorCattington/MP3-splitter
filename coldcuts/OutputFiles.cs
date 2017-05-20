using System.Collections.Generic;

namespace ColdCutsNS
{
    public class OutputFiles: List<SoundFile>
    {
        public OutputFiles()
        {
            Insert(0, new SoundFile());
        }

        public void Add(int index, SoundFile sound)
        {
            Insert(index, sound);
        }

        public void UpdateStartTime(int index, double startTime)
        {
            this[index].startTimeSeconds = startTime;
        }

        public void UpdateEndTime(int index, double endTime)
        {
            this[index].endTimeSeconds = endTime;
        }
    }
}
