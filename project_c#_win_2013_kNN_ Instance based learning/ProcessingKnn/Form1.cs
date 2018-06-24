using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KnnLib;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace kNN
{
    public partial class Form1 : Form
    {
        Bitmap data;
        ClkNN knn = new ClkNN();
        private delegate void UpdateDelegate(Image bm);
      
        public Form1()
        {
            InitializeComponent();
            knn.h_bmp = pictureBox.Height;
            knn.w_bmp = pictureBox.Width;
            knn.Completed += new ClkNN.Complete(Knn_Completed);
            knn.BwWorking += new ClkNN.BwWork(Knn_BwWorking);
        }

        void Knn_BwWorking(int percent)
        {
            this.progressBar1.Value = percent;
        }

        void UpdateImageControl(Image bm)
        {
           if (pictureBox.InvokeRequired)
           {
            pictureBox.Invoke(new UpdateDelegate(UpdateImageControl), bm);
           }
           else
           {
             // This is the UI thread so perform the task.
             pictureBox.Image = bm;
             pictureBox.Invalidate();
           }
        }

        void Knn_Completed(Image bm)
        {
            try
            {
                UpdateImageControl(bm);
                this.Invoke(new Action(() =>
                {
                    Cursor = Cursors.Default;
                    kUpDown.Enabled = true;
                    chkWeightedKnn.Enabled = true;
                }));
            }
            catch (Exception)
            {
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
                openFileDialog.Filter = "Pliki tekstowe (.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    double[] xs;
                    double[] ys;
                    byte[] cs;
                    DataParser.LoadData(openFileDialog.FileName, out xs, out ys, out cs);
                    knn.CopyData(xs, ys, cs);
                    int k = 0;
                    int.TryParse(kUpDown.Value.ToString(), out k);
                    knn.k = k;
                    knn.KnnWeightedChecked = chkWeightedKnn.Checked;
                    data = knn.DrawPoints();
                    Knn_Completed(data);
                }
        }

        private void KUpDown_ValueChanged(object sender, EventArgs e)
        {
            RunKnn();
        }

        private void RunKnn()
        {
            Cursor = Cursors.WaitCursor;
            kUpDown.Enabled = false;
            chkWeightedKnn.Enabled = false;
            int k = 0;
            int.TryParse(kUpDown.Value.ToString(), out k);
            knn.k = k;
            knn.KnnWeightedChecked = chkWeightedKnn.Checked;

            if (!knn.bw.IsBusy)
            {
                knn.StartClassifyKnn();
            }
            else
            {
                ShowDialogWhenRunning();
            }
        }

        private void ChkWeightedKnn_CheckedChanged(object sender, EventArgs e)
        {
          if (chkWeightedKnn.Checked)
                RunKnn();
          else
              Knn_Completed(data);
        }

        private void SortujPoKlasieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            knn.SortDataByClass();
        }

        private void MieszajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            knn.RandomizeData();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Jpeg|*.jpg";
            saveFileDialog1.Title = "Save";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
        }

        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filename = saveFileDialog1.FileName;
            // If the file name is not an empty string open it for saving.
            if (filename != "" && !e.Cancel)
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile())
                {
                    // Saves the Image in the appropriate ImageFormat based upon the
                    pictureBox.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fs.Close();
                }
            }

            saveFileDialog1.Dispose();
            MessageBox.Show("Saved " + filename);
        }

        private void KNNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunKnn();
        }

        private void BtnStopKnn_Click(object sender, EventArgs e)
        {
            ShowDialogWhenRunning();
        }

        private void WyczyscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDialogWhenRunning();
            Bitmap bm = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bm;
            pictureBox.Invalidate();
        }

        private void PrzywrocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDialogWhenRunning();
            Knn_Completed(data);
        }

        private void ShowDialogWhenRunning()
        {
            if (knn.bw.IsBusy)
            {
                if (MessageBox.Show("Czy chcesz przerwać kNN? \n Po potwierdzeniu uruchum dodatkową akcje w razie potrzeby.", "Przerwanie pracy kNN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    knn.bw.CancelAsync();
                }
            }
        }
       
    }

    
       

    
}
