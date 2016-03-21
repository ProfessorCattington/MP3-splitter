using Un4seen.Bass.AddOn.Tags;

namespace ColdCutsNS
{
    public class NewSoundFile
    {

        public string fileName;
        public long startTimeSeconds;
        public long endTimeSeconds;
        public TAG_INFO outputFileTags;

        public NewSoundFile(){

            fileName = "<blank>";
            startTimeSeconds = 0;
            endTimeSeconds = 0;
            outputFileTags = new TAG_INFO();
            outputFileTags.artist = "<blank>";
            outputFileTags.title = "<blank>";
            outputFileTags.album = "<blank>";
            outputFileTags.comment = "<blank>";
        }

        public void UpdateFileName(string fileName){

            this.fileName = fileName;
        }

        public void UpdateStartPoint(long startPoint){

            this.startTimeSeconds = startPoint;
        }

        public void UpdateEndPoint(long endPoint){

            this.endTimeSeconds = endPoint;
        }

        public void UpdateArtist(string artist){

            outputFileTags.artist = artist;
        }

        public void UpdateTitle(string title){

            outputFileTags.title = title;
        }

        public void UpdateAlbum(string album){

            outputFileTags.album = album;
        }

        public void UpdateComment(string comment){

            outputFileTags.comment = comment;
        }
    }
}
