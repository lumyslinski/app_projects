//
// Autor:
//   Łukasz Myśliński, 109178
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace Lab2
{
    public partial class Out : Form
    {
        /// <summary>
        /// The red values in an image
        /// </summary>
        long[] Red = new long[256];

        /// <summary>
        /// The green values in an image
        /// </summary>
        long[] Green = new long[256];

        /// <summary>
        /// The blue values in an image
        /// </summary>
        long[] Blue = new long[256];

        /// <summary>
        /// The brightness values in an image
        /// </summary>
        long[] Brightness = new long[256];

        int[,] pixelDataOut;
        int[,] greyedPixelData;

        public Out()
        {
            InitializeComponent();
            pictureBoxLoaded.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxModified.SizeMode = PictureBoxSizeMode.StretchImage;
            comboBoxModifyList.SelectedIndex = 0;
        }

        private Bitmap DrawHistograms(Image bin)
        {
            ResetRGBValues();

            GetHistograms(bin);

            return VisualizeHistogram(Brightness, Color.Gray, Pens.Gray);
        }

        private void ResetRGBValues()
        {
            for (int i = 0; i < 256; i++)
            {
                Brightness[i] = 0;
                Red[i] = 0;
                Green[i] = 0;
                Blue[i] = 0;
            }
        }

        private void GetHistograms(Image bin)
        {
            Bitmap bm = new Bitmap(bin);
            BitmapData data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            try
            {
                byte[] pixelData = new Byte[data.Stride];


                for (int scanline = 0; scanline < data.Height; scanline++)
                {
                    IntPtr source = data.Scan0 + (scanline * data.Stride);
                    Marshal.Copy(source, pixelData, 0, data.Stride);

                    for (int p = 0; p < data.Width; p++)
                    {
                        byte r = pixelData[(3 * p) + 2];
                        byte g = pixelData[(3 * p) + 0];
                        byte b = pixelData[(3 * p) + 1];
                        int lum = (int)Math.Round((decimal)((r + g + b) / 3.0));

                        ++Red[r];
                        ++Green[g];
                        ++Blue[b];
                        ++Brightness[lum];
                    }

                    Marshal.Copy(pixelData, 0, source, pixelData.Length);
                }
            }
            catch (Exception e)
            {
                bm.Dispose();
            }
            finally
            {
                bm.UnlockBits(data);
            }
        }


        private float GetMax(long[] data)
        {
            float max = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > max)
                    max = data[i];
            }
            return max;

        }

        public Bitmap VisualizeHistogram(long[] data, Color c, Pen p)
        {
            float oneColorHeight = 100;
            float margin = 10;
            float maxValue = GetMax(data);

            Bitmap histogramBitmap = new Bitmap(276, 200);
            using (Graphics g = Graphics.FromImage(histogramBitmap))
            {
                g.FillRectangle(Brushes.White, 0, 0, histogramBitmap.Width, histogramBitmap.Height);
                float yOffset = margin + oneColorHeight;

                for (int i = 0; i < 256; i++)
                {
                    g.DrawLine(p, margin + i, yOffset, margin + i, yOffset - (data[i] / maxValue) * oneColorHeight);
                }
            }

            return histogramBitmap;
        }


        private void buttonLoad_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Open6";
            openFileDialog1.Filter = "Jpeg|*.jpg";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowDialog();
        }

        private void DrawConvertedToGray(Image outr, bool weighted)
        {
            Image convertedToGray = ConvertToGray(outr, weighted);

            if (convertedToGray != null)
            {
                pictureBoxModified.Image = convertedToGray;
                pictureBoxHistogramModified.Image = DrawHistograms(convertedToGray);
            }
        }

        private void DrawUsingFilter(Image image, FilterType filterType)
        {
            Image converted = null;
            decimal[,] filter = new decimal[3, 3];

            switch (filterType)
            {
                case FilterType.Average:

                    for(int i=0;i<3;i++) {
                        for(int j=0;j<3;j++) {
                            filter[i, j] = (decimal)1 / 9;
                        }
                    }

                    converted = ConvertUsingFilter(image,filter);
                    break;

                case FilterType.Laplacian:

                    filter[0, 0] = 0;
                    filter[1, 0] = -1;
                    filter[2, 0] = 0;

                    filter[0, 1] = -1;
                    filter[1, 1] = 4;
                    filter[2, 1] = -1;

                    filter[0, 2] = 0;
                    filter[1, 2] = -1;
                    filter[2, 2] = 0;

                    converted = ConvertUsingFilter(image, filter);
                    break;

                case FilterType.Sobel:

                    filter[0, 0] = -1;
                    filter[1, 0] = 0;
                    filter[2, 0] = 1;

                    filter[0, 1] = -2;
                    filter[1, 1] = 0;
                    filter[2, 1] = 2;

                    filter[0, 2] = -1;
                    filter[1, 2] = 0;
                    filter[2, 2] = 1;

                    converted = ConvertUsingFilter(image, filter);
                    break;
            }
            

            if (converted != null)
            {
                pictureBoxModified.Image = converted;
                pictureBoxHistogramModified.Image = DrawHistograms(converted);
            }
        }

        private Image ConvertUsingFilter(Image bin, decimal[,] filter)
        {
            int x = 0;
            int y = 0;

            Bitmap bm = new Bitmap(bin);

            MessageBox.Show("Converting using selected filter. Please wait...");

            #region multiple filter mask by avgPixelData and save into pixelDataOut
            
            try
            {
                for (y = 0; y < bm.Height; y++)
                {
                    for (x = 0; x < bm.Width; x++)
                    {
                        int sWindX = 1;
                        int sWindY = 1;
                        int result = 0;

                        result = (int)(filter[sWindX, sWindY] * greyedPixelData[x, y]);

                        #region calculate left column
                        if ((x - 1) >= 0) // index inside image
                        {
                            result += (int)(filter[sWindX - 1, sWindY] * greyedPixelData[x - 1, y]);
                        }

                        if ((x - 1) >= 0 && (y - 1) >= 0) // index inside image
                        {
                            result += (int)(filter[sWindX - 1, sWindY - 1] * greyedPixelData[x - 1, y - 1]);
                        }

                        if ((x - 1) >= 0 && (y + 1) < bm.Height) // index inside image
                        {
                            result += (int)(filter[sWindX - 1, sWindY + 1] * greyedPixelData[x - 1, y + 1]);
                        }
                        #endregion

                        #region calculate middle column
                        if ((y - 1) >= 0) // index inside image
                        {
                            result += (int)(filter[sWindX, sWindY - 1] * greyedPixelData[x, y - 1]);
                        }

                        if ((y + 1) < bm.Height) // index inside image
                        {
                            result += (int)(filter[sWindX, sWindY + 1] * greyedPixelData[x, y + 1]);
                        }
                        #endregion

                        #region calculate right column
                        if ((x + 1) < bm.Width) // index inside image
                        {
                            result += (int)(filter[sWindX + 1, sWindY] * greyedPixelData[x + 1, y]);
                        }

                        if ((x + 1) < bm.Width && (y + 1) < bm.Height) // index inside image
                        {
                            result += (int)(filter[sWindX + 1, sWindY + 1] * greyedPixelData[x + 1, y + 1]);
                        }

                        if ((x + 1) < bm.Width && (y - 1) >= 0) // index inside image
                        {
                            result += (int)(filter[sWindX + 1, sWindY - 1] * greyedPixelData[x + 1, y - 1]);
                        }
                        #endregion

                        // save result
                        if (result > 255) { result = 255; }
                        if (result < 0) { result = 0; }

                        pixelDataOut[x, y] = result;
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                MessageBox.Show("Range exception, x=" + x + ",y=" + y);
            }
            catch (Exception e)
            {

            }
            #endregion

            BitmapData data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            try
            {
                byte[] pixelData = new Byte[data.Stride];
                x = 0;
                y = 0;

                for (int scanline = 0; scanline < data.Height; scanline++)
                {
                    IntPtr source = data.Scan0 + (scanline * data.Stride);
                    Marshal.Copy(source, pixelData, 0, data.Stride);
                    x = 0;

                    for (int p = 0; p < data.Width; p++)
                    {
                        pixelData[(3 * p) + 2] = (byte)pixelDataOut[x, y];
                        pixelData[(3 * p) + 0] = (byte)pixelDataOut[x, y];
                        pixelData[(3 * p) + 1] = (byte)pixelDataOut[x, y];
                        x++;
                    }

                    Marshal.Copy(pixelData, 0, source, pixelData.Length);
                    y++;
                }
            }
            catch (Exception e)
            {
                bm.Dispose();
            }
            finally
            {
                bm.UnlockBits(data);
            }

            //MessageBox.Show("Converted!");

            return bm;
        }

        private Image ConvertToGray(Image bin, bool weighted)
        {
            Bitmap bm = new Bitmap(bin);
            BitmapData data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                byte[] pixelData = new Byte[data.Stride];
                pixelDataOut = new int[bm.Height, bm.Width];
                greyedPixelData = new int[bm.Height, bm.Width];
                int x = 0;
                int y = 0;

                for (int i = 0; i < bm.Height; i++)
                {
                    for (int j = 0; j < bm.Width; j++)
                    {
                        pixelDataOut[i, j] = 0;
                        greyedPixelData[i, j] = 0;
                    }
                }

                for (int scanline = 0; scanline < data.Height; scanline++)
                {
                    IntPtr source = data.Scan0 + (scanline * data.Stride);
                    Marshal.Copy(source, pixelData, 0, data.Stride);
                    x = 0;

                    for (int p = 0; p < data.Width; p++)
                    {
                        byte r = pixelData[(3 * p) + 2];
                        byte g = pixelData[(3 * p) + 0];
                        byte b = pixelData[(3 * p) + 1];
                        int lum = 0;

                        if (weighted)
                        {
                            lum = (int)Math.Round((decimal)(( (0.64 * r) + (0.32 * g) + (0.04 * b) ) / (0.64 + 0.32 + 0.04)));
                        }
                        else
                        {
                            lum = (int)Math.Round((decimal)( (r + g + b) / 3.0) );
                        }

                            pixelData[(3 * p) + 0] = (byte)lum;
                            pixelData[(3 * p) + 1] = (byte)lum;
                            pixelData[(3 * p) + 2] = (byte)lum;

                       greyedPixelData[x, y] = lum;
                       x++;
                    }

                    y++;
                    Marshal.Copy(pixelData, 0, source, pixelData.Length);
                }
            }
            catch (Exception e)
            {
                bm.Dispose();
            }
            finally
            {
                bm.UnlockBits(data);
            }

            return bm;
        }

        private void comboBoxModifyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            RunAction(cb);
        }

        private void RunAction(ComboBox cb)
        {
            if (pictureBoxLoaded.Image != null)
            {
                switch (cb.SelectedIndex)
                {
                    case 0: DrawConvertedToGray(pictureBoxLoaded.Image, false); break;
                    case 1: DrawConvertedToGray(pictureBoxLoaded.Image, true); break;
                    case 2: DrawUsingFilter(pictureBoxLoaded.Image, FilterType.Average); break;
                    case 3: DrawUsingFilter(pictureBoxLoaded.Image, FilterType.Laplacian); break;
                    case 4: DrawUsingFilter(pictureBoxLoaded.Image, FilterType.Sobel); break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Jpeg|*.jpg";
            saveFileDialog1.Title = "Save";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filename = saveFileDialog1.FileName;
            // If the file name is not an empty string open it for saving.
            if (filename != "" && !e.Cancel)
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile())
                {
                    // Saves the Image in the appropriate ImageFormat based upon the
                    pictureBoxModified.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fs.Close();
                }
            }

            saveFileDialog1.Dispose();
            MessageBox.Show("Saved " + filename);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filename = openFileDialog1.FileName;
            Image outr = null;

            //if (filename != "" && !e.Cancel && (openFileDialog1.ShowDialog() == DialogResult.OK))
            if (filename != "" && !e.Cancel)
            {
                using (FileStream fs = (System.IO.FileStream)openFileDialog1.OpenFile())
                {
                    outr = Image.FromFile(openFileDialog1.FileName);
                    fs.Close();
                }
            }

            pictureBoxLoaded.Image = outr;
            pictureBoxHistogramLoaded.Image = DrawHistograms(outr);

            comboBoxModifyList.SelectedIndex = 0;

            RunAction(comboBoxModifyList);
        }

        
    }

        [Flags]
        enum FilterType
        {
            Average,
            Laplacian,
            Sobel
        }
}
