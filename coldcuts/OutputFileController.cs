using System;
using Un4seen.Bass.AddOn.Tags;


namespace ColdCutsNS{

    public class OutputFileController{

        protected TAG_INFO m_inputFileTags;
        protected OutputFiles m_outputFiles;

        protected int m_currentFileIndex;

        public OutputFileController(){

            m_outputFiles = new OutputFiles();
            m_inputFileTags = new TAG_INFO();
            m_currentFileIndex = 0;
        }

        public void AddANewSoundFile(){

            m_outputFiles.Add(m_currentFileIndex);
        }

        public void RemoveASoundFile(){

            m_outputFiles.RemoveAt(m_currentFileIndex);

            //make sure we aren't going outside the bounds of the list
            int numberOfFiles = m_outputFiles.Count;

            if (m_currentFileIndex >= numberOfFiles){

                m_currentFileIndex = numberOfFiles - 1;
            }

            if (m_currentFileIndex < 0){

                m_currentFileIndex = 0;
            }
        }

        public void GoToIndex(int index){

            m_currentFileIndex = index;
        }

        public void IncreaseIndex(){

            m_currentFileIndex++;

            int numberOfFiles = m_outputFiles.Count;

            if (m_currentFileIndex >= numberOfFiles){

                m_currentFileIndex = numberOfFiles - 1;
            }
        }

        public void DecreaseIndex(){

            m_currentFileIndex--;

            if(m_currentFileIndex < 0){

                m_currentFileIndex = 0;
            }
        }

        public void GotoIndex(int index){

            m_currentFileIndex = index;
        }

        public TAG_INFO GetTagInfo(){

            return m_inputFileTags;
        }

        public TAG_INFO FillInputFileTags(string inputFileText){

            return m_inputFileTags = BassTags.BASS_TAG_GetFromFile(inputFileText);
        }

        public int GetCurrentFileIndex(){

            return m_currentFileIndex;
        }

        public OutputFiles GetOutputFiles(){

            return m_outputFiles;
        }
        public int GetNumberOfSoundFiles(){

            return m_outputFiles.Count;
        }

        public void UpdateStartAndEndTimes(){

            m_outputFiles.UpdateStartTime(m_currentFileIndex, 0);
            m_outputFiles.UpdateEndTime(m_currentFileIndex, 0);
        }

        public void UpdateStartAndEndTimes(string startMin, string startSec, string endMin, string endSec){

            long newStartMin = long.Parse(startMin);
            double newStartSec = double.Parse(startSec);
            long newEndMin = long.Parse(endMin);
            double newEndSec = double.Parse(endSec);

            m_outputFiles.UpdateStartTime(m_currentFileIndex, ((newStartMin * 60) + newStartSec));
            m_outputFiles.UpdateEndTime(m_currentFileIndex, ((newEndMin * 60) + newEndSec));
        }

        public void UpdateInputTags(string fileName, string artist, string title, string album, string comment){

            m_outputFiles[m_currentFileIndex].tag.artist = artist;
            m_outputFiles[m_currentFileIndex].tag.title = title;
            m_outputFiles[m_currentFileIndex].tag.album = album;
            m_outputFiles[m_currentFileIndex].tag.comment = comment;
            m_outputFiles[m_currentFileIndex].fileName = fileName;
        }

        public string GetStartMinString(){

           return (m_outputFiles[m_currentFileIndex].startTimeSeconds / 60).ToString();
        }

        public string GetStartSecString(){

            return (m_outputFiles[m_currentFileIndex].startTimeSeconds % 60).ToString();
        }

        public string GetEndMinString(){

            return (m_outputFiles[m_currentFileIndex].endTimeSeconds / 60).ToString();
        }

        public string GetEndSecString(){

            return (m_outputFiles[m_currentFileIndex].endTimeSeconds % 60).ToString();
        }

        public string GetFileName(){

            return m_outputFiles[m_currentFileIndex].fileName;
        }

        public string GetArtist(){

            return m_outputFiles[m_currentFileIndex].tag.artist;
        }

        public string GetTitle(){

            return m_outputFiles[m_currentFileIndex].tag.title;
        }

        public string GetAlbum(){

            return m_outputFiles[m_currentFileIndex].tag.album;
        }

        public string GetComment(){

            return m_outputFiles[m_currentFileIndex].tag.comment;
        }
        public double GetStartTime(){

            return m_outputFiles[m_currentFileIndex].startTimeSeconds;
        }

        public double GetEndTime(){

            return m_outputFiles[m_currentFileIndex].endTimeSeconds;
        }
    }
}
