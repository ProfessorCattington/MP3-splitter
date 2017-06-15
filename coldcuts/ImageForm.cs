using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColdCutsNS
{
    public partial class ImageForm : Form
    {
        Pen penGreen = new Pen(Color.LimeGreen, 1);
        Pen penRed = new Pen(Color.Red, 1);
        bool busy = false;

        public ImageForm()
        {
            InitializeComponent();
        }

        public bool IsBusy { get { return busy; } }

        public void ShowSound(List<int> sound)
        {
            busy = true;
            double max = 0;
            int height = 100;
            int count = sound.Count;
            panel.Controls.Clear();
            if (count < 90000)
            {
                for (int i = 0; i < count; i++)
                    if (sound[i] > max) max = sound[i];
                var b = new Bitmap(count, height);
                using (var g = Graphics.FromImage(b))
                {
                    for (int i = 1; i < count; i++)
                    {
                        g.DrawLine(penGreen, i, 0, i, (int)Math.Round((sound[i] / max) * height));
                        if (i % 100 == 0)
                        {
                            g.DrawLine(penRed, i, 0, i, 10);
                            if (i % 500 == 0)
                            {
                                g.DrawLine(penRed, i - 1, 0, i - 1, 6);
                                g.DrawLine(penRed, i - 2, 0, i - 2, 2);
                            }
                        }
                    }
                }
                panel.Controls.Add(new SoundPicture(b, count, height));
            }
            busy = false;
        }

        private class SoundPicture: PictureBox
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
}
