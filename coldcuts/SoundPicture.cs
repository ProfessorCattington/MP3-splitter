using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColdCutsNS
{
    class SoundPicture : PictureBox
    {
        public SoundPicture(Bitmap b, int width, int height)
        {
            Image = b;
            Width = width;
            Height = height;
            Location = new Point(0, 0);
            Margin = new Padding(0);
        }
    }
}
