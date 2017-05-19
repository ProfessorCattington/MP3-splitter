using System.Collections.Generic;

namespace ColdCutsNS
{
    public class OutputFiles: List<NewSoundFile>
    {
        public OutputFiles()
        {
            Insert(0, new NewSoundFile());
        }

        public void Add(int index)
        {
            Insert(index, new NewSoundFile());
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
