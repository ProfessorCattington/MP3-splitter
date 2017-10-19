using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColdCutsNS
{
    public partial class ImageForm : Form
    {
        private Pen m_penGreen = new Pen(Color.LimeGreen, 1);
        private Pen m_penRed = new Pen(Color.Red, 1);
        private Pen m_penBlue = new Pen(Color.Blue, 1);

        private Font m_font = new Font("Arial", 7, FontStyle.Regular);
        private Brush m_brush = new SolidBrush(Color.Black);

        private PictureBox m_soundwavePictureBox;

        private List<int> m_volumeSamples;

        private double m_maxVolume = 0;
        private const int m_waveHeight = 130;

        private const int m_redMarkerModifier = 50;
        private int m_resolutionScale = 1;
        private Control m_parent;

        public ImageForm(Control parent)
        {
            InitializeComponent();
            m_parent = parent;
        }

        public void ShowSound(List<int> volumeSamples)
        {
            m_volumeSamples = volumeSamples;
            int sampleCount = m_volumeSamples.Count;

            for (int i = 0; i < sampleCount; i++)
            {
                if (m_volumeSamples[i] > m_maxVolume)
                {
                    m_maxVolume = m_volumeSamples[i];
                }
            }

            RedrawSound(sampleCount);
        }

        private void RedrawSound(int sampleCount)
        {
            panel.Controls.Clear();

            List<int> adjustedSamples = new List<int>();

            for(int i = 0; i < m_volumeSamples.Count; i++)
            {
                if(i % m_resolutionScale == 0)
                {
                    adjustedSamples.Add(m_volumeSamples[i]);
                }
            }

            //if we have too many samples we need to split them up to properly display large sound files
            // max width determined by 64bit int
            int totalDisplaySamples = sampleCount / m_resolutionScale;
            List<Bitmap> bitmaps = new List<Bitmap>();

            if (totalDisplaySamples > 65535)
            {

                int numberOfBitmaps = (int)Math.Ceiling(totalDisplaySamples / 65535f);

                for (int i = 0; i < numberOfBitmaps; i++)
                {
                    //we don't want to just add max width bitmaps for the number of bitmaps we need
                    if(i == numberOfBitmaps - 1)
                    {
                        //last bitmap in the list needs to be trimmed down
                        int remainingPixels = totalDisplaySamples % 65535;
                        Bitmap lastBitmap = new Bitmap(remainingPixels, m_waveHeight);
                        bitmaps.Add(lastBitmap);
                    }
                    else
                    {
                        bitmaps.Add(new Bitmap(65535, m_waveHeight));
                    }
                }
            }
            else
            {
                bitmaps.Add(new Bitmap(totalDisplaySamples, m_waveHeight));
            }

            int sampleLocation = 0;

            for (int j = 0; j < bitmaps.Count; j++)
            {
                int currentImageSamples = bitmaps[j].Width;

                using (Graphics graphics = Graphics.FromImage(bitmaps[j]))
                {
                    for (int i = 1; i < currentImageSamples; i++)
                    {

                        int lineHeight = m_waveHeight - (int)Math.Round((adjustedSamples[sampleLocation] / m_maxVolume) * m_waveHeight);

                        graphics.DrawLine(m_penGreen, i, m_waveHeight, i, lineHeight);

                        if (i % m_redMarkerModifier == 0)
                        {
                            graphics.DrawLine(m_penRed, i, 0, i, 10);

                            PointF timeStampPoint = new PointF(i, 0);
                            string timeStamp = ((sampleLocation + 1) * m_resolutionScale / 3000).ToString() + ":" + string.Format("{0:00}", ((sampleLocation + 1)  * m_resolutionScale/ m_redMarkerModifier) % 60);

                            graphics.DrawString(timeStamp, m_font, m_brush, timeStampPoint);

                            if (i % (m_redMarkerModifier * 10) == 0)
                            {
                                graphics.DrawLine(m_penRed, i - 1, 0, i - 1, 6);
                                graphics.DrawLine(m_penRed, i - 2, 0, i - 2, 2);
                            }
                        }

                        sampleLocation++;
                    }
                }

                Point currentImagePosition = new Point(0, m_waveHeight * j);

                m_soundwavePictureBox = new SoundPicture(bitmaps[j], currentImageSamples, m_waveHeight, currentImagePosition);
                m_soundwavePictureBox.MouseClick += WaveFormPictureBoxClicked;

                panel.Controls.Add(m_soundwavePictureBox);
            }
        }

        private void WaveFormPictureBoxClicked(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(Cursor.Position);
            }
            else
            {
                AddMarker(e.X, e.Y);
            }
        }

        private void AddMarker(int x, int y)
        {
            Bitmap bitmap = (Bitmap)m_soundwavePictureBox.Image;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawLine(m_penBlue, x, 0, x, y + bitmap.Height);
            }
            panel.Refresh();
        }

        public void DecreaseSoundwaveResolution(object sender, EventArgs e)
        {
            decreaseResolutionButton.Enabled = true;
            m_resolutionScale++;
            if(m_resolutionScale >= 3)
            {
                m_resolutionScale = 3;
                increaseResolutionButton.Enabled = false;
            }
            RedrawSound(m_volumeSamples.Count);
        }

        public void IncreaseSoundwaveResolution(object sender, EventArgs e)
        {
            increaseResolutionButton.Enabled = true;
            m_resolutionScale--;
            if(m_resolutionScale < 1)
            {
                m_resolutionScale = 1;
                decreaseResolutionButton.Enabled = false;
            }
            RedrawSound(m_volumeSamples.Count);
        }

        private void ImageForm_DoubleClick(object sender, EventArgs e)
        {
            Location = new Point(m_parent.Location.X, m_parent.Location.Y + m_parent.Height);
        }
    }
}
