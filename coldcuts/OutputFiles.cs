using System.Collections.Generic;

namespace ColdCutsNS
{
    public class OutputFiles{

        public List<NewSoundFile> soundFiles = new List<NewSoundFile>();
        private int m_numberOfFiles = 0;

        public OutputFiles(){

            AddANewSoundFile();
        }

        public void AddANewSoundFile(){

            soundFiles.Add(new NewSoundFile());
            m_numberOfFiles++;
        }

        public void RemoveASoundFile(int index){

            soundFiles.RemoveAt(index);
            m_numberOfFiles--;
        }

        public void UpdateStartTime(int index, long startTime){

            soundFiles[index].startTimeSeconds = startTime;
        }

        public void UpdateEndTime(int index, long endTime){

            soundFiles[index].endTimeSeconds = endTime;
        }

        public int GetNumberOfFiles(){

            return m_numberOfFiles;
        }

        public List<NewSoundFile> GetSoundFiles(){

            return soundFiles;
        }
    }
}
