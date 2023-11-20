using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap loaded;
        Bitmap processed;
        String file_name;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();

            file_name = op.FileName;
            try
            {
                loaded = (Bitmap)Image.FromFile(file_name);
            }
            catch (Exception ex){
                MessageBox.Show("No image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            pictureBox1.Image = loaded;
        }

        //basic copy
        private void button3_Click(object sender, EventArgs e) 
        {
            if (loaded == null)
            {
                MessageBox.Show("No image loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            processed = new Bitmap(loaded.Width, loaded.Height);
          
            for (int i = 0; i < loaded.Width; i++)
            {
                for(int j = 0; j < loaded.Height; j++)
                {
                    Color pix = loaded.GetPixel(i, j);  
                    processed.SetPixel(i, j, pix);
                }
            }
            pictureBox2.Image = processed;
        }

        //greyscale
        private void button4_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pix = loaded.GetPixel(i, j);
                    int gray = (pix.R + pix.G + pix.B) / 3;
                    processed.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            pictureBox2.Image = processed;
        }

        //color inversion
        private void button5_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pixel = loaded.GetPixel(i, j);
                    processed.SetPixel(i, j, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
            pictureBox2.Image = processed;
        }

        //histogram
        private void button6_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pixel = loaded.GetPixel(i, j);
                    int gray = (pixel.R + pixel.G + pixel.B) / 3;
                    processed.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
                
            Color sample;
            int[] histData = new int[256];

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    sample = loaded.GetPixel(i, j);
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

        private void button7_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pixel = loaded.GetPixel(i, j);

                    int sepiaR = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int sepiaG = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int sepiaB = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    processed.SetPixel(i, j, Color.FromArgb(Math.Min(255, sepiaR), Math.Min(255, sepiaG), Math.Min(255, sepiaB)));
                }
            }
            pictureBox2.Image = processed;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (processed != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    processed.Save(saveFileDialog.FileName);
                }
            }
            else
                MessageBox.Show("No image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }
}
