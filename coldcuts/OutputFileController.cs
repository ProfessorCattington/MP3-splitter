using System;
using Un4seen.Bass.AddOn.Tags;

namespace ColdCutsNS{

    public class OutputFileController{

        private TAG_INFO m_inputFileTags;
        private OutputFiles m_outputFiles;
        private int index;

        public OutputFileController()
        {
            index = 0;
        }

        public void AddSoundFile(SoundFile sound)
        {
            if (sound.tag == null)
                sound.AddTagInfo(m_inputFileTags);
            m_outputFiles.Add(index, sound);
        }

        public void RemoveASoundFile(){

            m_outputFiles.RemoveAt(index);

            //make sure we aren't going outside the bounds of the list
            int numberOfFiles = m_outputFiles.Count;

            if (index >= numberOfFiles)
                index = numberOfFiles - 1;

            if (index < 0)
                index = 0;
        }

        public void GoToIndex(int index)
        {
            this.index = index;
        }

        public void IncreaseIndex()
        {
            index++;
            int numberOfFiles = m_outputFiles.Count;
            if (index >= numberOfFiles)
                index = numberOfFiles - 1;
        }

        public void DecreaseIndex()
        {
            index--;
            if(index < 0)
                index = 0;
        }

        public TAG_INFO FillInputFileTags(string inputFileText)
        {
            m_inputFileTags = BassTags.BASS_TAG_GetFromFile(inputFileText);
            if (m_outputFiles == null)
            {
                var sound = new SoundFile(tag: m_inputFileTags);
                m_outputFiles = new OutputFiles(sound);
            }
            return m_inputFileTags;
        }

        public int GetCurrentFileIndex(){

            return index;
        }

        public OutputFiles GetOutputFiles(){

            return m_outputFiles;
        }
        public int CountOfSoundFiles
        {
            get { return m_outputFiles.Count; }
        }

        public void UpdateStartAndEndTimes(string startMin, string startSec, string endMin, string endSec){

            long newStartMin = long.Parse(startMin);
            double newStartSec = double.Parse(startSec);
            long newEndMin = long.Parse(endMin);
            double newEndSec = double.Parse(endSec);

            m_outputFiles.UpdateStartTime(index, ((newStartMin * 60) + newStartSec));
            m_outputFiles.UpdateEndTime(index, ((newEndMin * 60) + newEndSec));
        }

        public void UpdateInputTags(string fileName, string artist, string title, string album, string comment){

            m_outputFiles[index].tag.artist = artist;
            m_outputFiles[index].tag.title = title;
            m_outputFiles[index].tag.album = album;
            m_outputFiles[index].tag.comment = comment;
            m_outputFiles[index].fileName = fileName;
        }

        public string GetStartMinString(){

           return ((int)Math.Round(m_outputFiles[index].startTimeSeconds / 60)).ToString();
        }

        public string GetStartSecString(){

            return (m_outputFiles[index].startTimeSeconds % 60).ToString();
        }

        public string GetEndMinString(){

            return ((int)Math.Round(m_outputFiles[index].endTimeSeconds / 60)).ToString();
        }

        public string GetEndSecString(){

            return (m_outputFiles[index].endTimeSeconds % 60).ToString();
        }

        public string GetFileName(){

            return m_outputFiles[index].fileName;
        }

        public TAG_INFO TagInfo { get { return m_outputFiles[index].tag; } }

        public string GetArtist(){

            return m_outputFiles[index].tag.artist;
        }

        public string GetTitle(){

            return m_outputFiles[index].tag.title;
        }

        public string GetAlbum(){

            return m_outputFiles[index].tag.album;
        }

        public string GetComment(){

            return m_outputFiles[index].tag.comment;
        }
        public double GetStartTime(){

            return m_outputFiles[index].startTimeSeconds;
        }

        public double GetEndTime(){

            return m_outputFiles[index].endTimeSeconds;
        }
    }
}
