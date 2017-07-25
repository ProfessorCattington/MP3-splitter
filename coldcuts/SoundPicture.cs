using System.Drawing;
using System.Windows.Forms;

namespace ColdCutsNS
{
    class SoundPicture : PictureBox
    {
        public SoundPicture(Bitmap b, int width, int height, Point position)
        {
            Image = b;
            Width = width;
            Height = height;
            Location = position;
            Margin = new Padding(0);
        }
    }
}
