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

            m_outputFiles.AddANewSoundFile();
        }

        public void RemoveASoundFile(){

            m_outputFiles.RemoveASoundFile(m_currentFileIndex);

            //make sure we aren't going outside the bounds of the list
            int numberOfFiles = m_outputFiles.soundFiles.Count;

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

            int numberOfFiles = m_outputFiles.soundFiles.Count;

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

            return m_outputFiles.soundFiles.Count;
        }

        public void UpdateStartAndEndTimes(){

            m_outputFiles.UpdateStartTime(m_currentFileIndex, 0);
            m_outputFiles.UpdateEndTime(m_currentFileIndex, 0);
        }

        public void UpdateStartAndEndTimes(string startMin, string startSec, string endMin, string endSec){

            long newStartMin = long.Parse(startMin);
            long newStartSec = long.Parse(startSec);
            long newEndMin = long.Parse(endMin);
            long newEndSec = long.Parse(endSec);

            m_outputFiles.UpdateStartTime(m_currentFileIndex, ((newStartMin * 60) + newStartSec));
            m_outputFiles.UpdateEndTime(m_currentFileIndex, ((newEndMin * 60) + newEndSec));
        }

        public void UpdateInputTags(string fileName, string artist, string title, string album, string comment){

            m_outputFiles.soundFiles[m_currentFileIndex].UpdateArtist(artist);
            m_outputFiles.soundFiles[m_currentFileIndex].UpdateTitle(title);
            m_outputFiles.soundFiles[m_currentFileIndex].UpdateAlbum(album);
            m_outputFiles.soundFiles[m_currentFileIndex].UpdateComment(comment);
            m_outputFiles.soundFiles[m_currentFileIndex].fileName = fileName;
        }

        public string GetStartMinString(){

           return (m_outputFiles.soundFiles[m_currentFileIndex].startTimeSeconds / 60).ToString();
        }

        public string GetStartSecString(){

            return (m_outputFiles.soundFiles[m_currentFileIndex].startTimeSeconds % 60).ToString();
        }

        public string GetEndMinString(){

            return (m_outputFiles.soundFiles[m_currentFileIndex].endTimeSeconds / 60).ToString();
        }

        public string GetEndSecString(){

            return (m_outputFiles.soundFiles[m_currentFileIndex].endTimeSeconds % 60).ToString();
        }

        public string GetFileName(){

            return m_outputFiles.soundFiles[m_currentFileIndex].fileName;
        }

        public string GetArtist(){

            return m_outputFiles.soundFiles[m_currentFileIndex].outputFileTags.artist;
        }

        public string GetTitle(){

            return m_outputFiles.soundFiles[m_currentFileIndex].outputFileTags.title;
        }

        public string GetAlbum(){

            return m_outputFiles.soundFiles[m_currentFileIndex].outputFileTags.album;
        }

        public string GetComment(){

            return m_outputFiles.soundFiles[m_currentFileIndex].outputFileTags.comment;
        }
        public long GetStartTime(){

            return m_outputFiles.soundFiles[m_currentFileIndex].startTimeSeconds;
        }

        public long GetEndTime(){

            return m_outputFiles.soundFiles[m_currentFileIndex].endTimeSeconds;
        }
    }
}
