using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WebCamLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace ImageProcessing1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        Bitmap imageA;
        Bitmap imageB;
        Bitmap processed;    

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = imageA;
        }
        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
            pictureBox3.Image = imageB;
        }
        

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(imageA.Width, imageA.Height);
            for (int i = 0; i < imageA.Width; i++)
            {
                for (int j = 0; j < imageA.Height; j++)
                {
                    Color pix = imageA.GetPixel(i, j);
                    processed.SetPixel(i, j, pix);
                }
            }
            pictureBox2.Image = processed;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(imageA.Width, imageA.Height);
            for (int i = 0; i < imageA.Width; i++)
            {
                for (int j = 0; j < imageA.Height; j++)
                {
                    Color pix = imageA.GetPixel(i, j);
                    int gray = (pix.R + pix.G + pix.B) / 3;
                    processed.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            pictureBox2.Image = processed;
        }

        private void invertionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(imageA.Width, imageA.Height);
            for (int i = 0; i < imageA.Width; i++)
            {
                for (int j = 0; j < imageA.Height; j++)
                {
                    Color pixel = imageA.GetPixel(i, j);
                    processed.SetPixel(i, j, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
            pictureBox2.Image = processed;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(imageA.Width, imageA.Height);

            for (int i = 0; i < imageA.Width; i++)
            {
                for (int j = 0; j < imageA.Height; j++)
                {
                    Color pixel = imageA.GetPixel(i, j);
                    int gray = (pixel.R + pixel.G + pixel.B) / 3;
                    processed.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }

            Color sample;
            int[] histData = new int[256];

            for (int i = 0; i < imageA.Width; i++)
            {
                for (int j = 0; j < imageA.Height; j++)
                {
                    sample = imageA.GetPixel(i, j);
                    histData[sample.R] = histData[sample.R] + 1;
                }
            }

            Bitmap myData = new Bitmap(256, 800);
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 800; j++)
                {
                    myData.SetPixel(i, j, Color.White);
                }
            }

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < Math.Min(histData[i] / 5, 800); j++)
                {
                    myData.SetPixel(i, 799 - j, Color.Black);
                }
            }

            pictureBox2.Image = myData;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(imageA.Width, imageA.Height);
            for (int i = 0; i < imageA.Width; i++)
            {
                for (int j = 0; j < imageA.Height; j++)
                {
                    Color pixel = imageA.GetPixel(i, j);

                    int sepiaR = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int sepiaG = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int sepiaB = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    processed.SetPixel(i, j, Color.FromArgb(Math.Min(255, sepiaR), Math.Min(255, sepiaG), Math.Min(255, sepiaB)));
                }
            }
            pictureBox2.Image = processed;
        }

 

        //subtraction
        private void button9_Click(object sender, EventArgs e)
        {
            Color mygreen = Color.FromArgb(0, 0, 255);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            processed = new Bitmap(imageA.Width, imageA.Height);
            int threshold = 5;
            for (int x = 0; x < imageA.Width; x++)
            {
                for (int y = 0; y < imageA.Height; y++)
                {
                    Color pixel = imageA.GetPixel(x, y);
                    Color backpixel = imageB.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int sval = Math.Abs(grey - greygreen);
                    if (sval > threshold)
                    {
                        processed.SetPixel(x, y, pixel);
                    }
                    else
                    {
                        processed.SetPixel(x, y, backpixel);
                    }
                }
            }
            pictureBox2.Image = processed;
        }

        Device[] devices = DeviceManager.GetAllDevices();
        Device webcam = DeviceManager.GetDevice(0);

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webcam.ShowWindow(pictureBox1);
            Console.WriteLine(webcam.ToString());
        }

        
    }
}
