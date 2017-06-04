using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColdCutsNS
{
    public partial class ImageForm : Form
    {
        Pen pen = new Pen(Color.LimeGreen, 1);
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
            int count = sound.Count;
            for (int i = 0; i < count; i++)
                if (sound[i] > max) max = sound[i];
            var b = new Bitmap(count, pictureBox.Height);
            using (var g = Graphics.FromImage(b))
            {
                for (int i = 0; i < count; i++)
                {
                    g.DrawLine(pen, i, 0, i, (int)Math.Round((sound[i]/max) * pictureBox.Height));
                }
            }
            pictureBox.Image = b;
            pictureBox.Width = count;
            busy = false;
        }
    }
}
