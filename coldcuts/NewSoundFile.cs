using Un4seen.Bass.AddOn.Tags;

namespace ColdCutsNS
{
    public class NewSoundFile
    {
        public string fileName;
        public long startTimeSeconds;
        public long endTimeSeconds;
        public TAG_INFO tag;

        public NewSoundFile()
        {
            fileName = "<blank>";
            startTimeSeconds = 0;
            endTimeSeconds = 0;
            tag = new TAG_INFO();
            tag.artist = "<blank>";
            tag.title = "<blank>";
            tag.album = "<blank>";
            tag.comment = "<blank>";
        }
    }
}
