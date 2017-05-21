using Un4seen.Bass.AddOn.Tags;

namespace ColdCutsNS
{
    public class SoundFile
    {
        public string fileName;
        public double startTimeSeconds;
        public double endTimeSeconds;
        public TAG_INFO tag;

        public SoundFile(string name = "new", double start = 0, double end = 0, TAG_INFO tag = null)
        {
            fileName = name;
            startTimeSeconds = start;
            endTimeSeconds = end;
            if (tag != null)
                AddTagInfo(tag);
        }

        public void AddTagInfo(TAG_INFO tag)
        {
            this.tag = new TAG_INFO()
            {
                artist = tag.artist,
                title = tag.title,
                album = tag.album,
                comment = tag.comment
            };
        }
    }
}
