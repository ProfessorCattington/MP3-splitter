using Un4seen.Bass.AddOn.Tags;

namespace ColdCutsNS
{
    public class SoundFile
    {
        public string fileName;
        public double startTimeSeconds;
        public double endTimeSeconds;
        public TAG_INFO tag;

        public SoundFile(string name = "new", double start = 0, double end = 0)
        {
            fileName = name;
            startTimeSeconds = start;
            endTimeSeconds = end;
            tag = new TAG_INFO()
            {
                artist = "<blank>",
                title = "<blank>",
                album = "<blank>",
                comment = "<blank>"
            };
        }
    }
}
