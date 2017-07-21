using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColdCutsNS
{
    public partial class ImageForm : Form
    {
        private Pen penGreen = new Pen(Color.LimeGreen, 1);
        private Pen penRed = new Pen(Color.Red, 1);

        private PictureBox soundwavePictureBox;

        public ImageForm()
        {
            InitializeComponent();
        }

        public void ShowSound(List<int> volumeSamples)
        {
            double maxVolume = 0;
            int waveHeight = 130;
            int sampleCount = volumeSamples.Count;
            panel.Controls.Clear();

            for (int i = 0; i < sampleCount; i++)
            {
                if (volumeSamples[i] > maxVolume)
                {
                    maxVolume = volumeSamples[i];
                }
            }

            Bitmap bitmap = new Bitmap(sampleCount, waveHeight);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                for (int i = 1; i < sampleCount; i++)
                {
                   graphics.DrawLine(penGreen, i, waveHeight, i, waveHeight - (int)Math.Round((volumeSamples[i] / maxVolume) * waveHeight));
                   if (i % 50 == 0)
                   {
                       graphics.DrawLine(penRed, i, 0, i, 10);
                       if (i % 5000 == 0)
                       {
                           graphics.DrawLine(penRed, i - 1, 0, i - 1, 6);
                           graphics.DrawLine(penRed, i - 2, 0, i - 2, 2);
                       }
                   }
               }
           }

            soundwavePictureBox = new SoundPicture(bitmap, sampleCount, waveHeight);
            soundwavePictureBox.MouseClick += WaveFormPictureBoxClicked;

            panel.Controls.Add(soundwavePictureBox);
        }

        public void WaveFormPictureBoxClicked(Object sender, MouseEventArgs e)
        {

            int mouseX = e.X;
            int mouseY = e.Y;

            Bitmap bitmap = (Bitmap)soundwavePictureBox.Image;

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {

               // graphics.DrawLine(penRed, mouseX, 0, mouseX, mouseY + bitmap.Height);
            }
            panel.Refresh();

            if (e.Button == MouseButtons.Right)
            {

                Console.WriteLine("let's make a context menu");
            }                       
        }
    }
}
