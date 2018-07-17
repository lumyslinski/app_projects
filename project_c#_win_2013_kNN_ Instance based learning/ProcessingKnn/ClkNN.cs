using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace kNN
{
    public class ClkNN
    {
        Color[] colors = { new Color(0x00, 0x00, 0x00), 
                             new Color(0xFF, 0x00, 0x00), 
                             new Color(0x00, 0xFF, 0x00),
                             new Color(0x00, 0x00, 0xFF), 
                             new Color(0xFF, 0xFF, 0x00), 
                             new Color(0x00, 0xFF, 0xFF),
                             new Color(0xFF, 0x00, 0xFF)};

        public int w_bmp = 0;
        public int h_bmp = 0;
        int width;
        int height;
        int stride;
        InputData data;
        int length;
        public int k = 1;
        bool knnWeightedChecked = false;
        public BackgroundWorker bw;
        private byte[] originalBytes = null;

        public bool KnnWeightedChecked
        {
            get { return knnWeightedChecked; }
            set { knnWeightedChecked = value; }
        }

        public delegate void Complete(Image bm);
        public event Complete Completed;

        public delegate void BwWork(int percent);
        public event BwWork BwWorking;

        public ClkNN()
        {
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Bitmap bm = new Bitmap(w_bmp, h_bmp, PixelFormat.Format24bppRgb);
            try
            {
                BitmapData outbd = bm.LockBits(new Rectangle(0, 0, w_bmp, h_bmp), ImageLockMode.WriteOnly, bm.PixelFormat);
                byte[] outData = null;

                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, e.UserState);
                    outData = ms.ToArray();
                }
                
                Marshal.Copy(outData, 0, outbd.Scan0, outData.Length);
                bm.UnlockBits(outbd);
            }
            catch (Exception)
            {
                throw new Exception("bw_ProgressChanged");
            }
            this.Completed(bm);
            BwWorking(e.ProgressPercentage);
            
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            ClassifyAgain(worker);
        }

        public Bitmap DrawPoints()
        {
            Bitmap bm = new Bitmap(w_bmp, h_bmp, PixelFormat.Format24bppRgb);

            try
            {    
                BitmapData outbd = bm.LockBits(new Rectangle(0, 0, w_bmp, h_bmp), ImageLockMode.WriteOnly, bm.PixelFormat);

                byte[] outData = new byte[outbd.Stride * outbd.Height];
                this.width = outbd.Width;
                this.height = outbd.Height;
                this.stride = outbd.Stride;

                data.picturePoints = new Point[length];
                for (int i = 0; i < length; i++)
                {
                    data.picturePoints[i] = new Point((int)(data.normalizedPoints[i].x * (width - 1)), (int)(data.normalizedPoints[i].y * (height - 1)), data.normalizedPoints[i].c);
                }

                for (int i = 0; i < data.picturePoints.Length; i++)
                {
                    SetOutput(ref outData,data.picturePoints[i].iX, data.picturePoints[i].iY, colors[data.picturePoints[i].c]);
                }
 
                Marshal.Copy(outData, 0, outbd.Scan0, outData.Length);
                bm.UnlockBits(outbd);
                originalBytes = outData;
            }
            catch (Exception)
            {
                throw new Exception("DrawPoints");
            }

            return bm;
        }

        public void StartClassifyKnn()
        {
            bw.RunWorkerAsync();
        }

        public void ClassifyAgain(BackgroundWorker bw=null)
        {
            Bitmap bm = new Bitmap(w_bmp, h_bmp, PixelFormat.Format24bppRgb);
                try
                {
                    BitmapData outbd = bm.LockBits(new Rectangle(0, 0, w_bmp, h_bmp), ImageLockMode.WriteOnly, bm.PixelFormat);
                    byte[] outData = new byte[outbd.Stride * outbd.Height];

                    if (originalBytes != null)
                    {
                        originalBytes.CopyTo(outData, 0);
                    }

                    Classification(bw,ref outData);
                    Marshal.Copy(outData, 0, outbd.Scan0, outData.Length);
                    bm.UnlockBits(outbd);
                }
                catch (Exception)
                {
                    throw new Exception("ClassifyAgain");
                }
            this.Completed(bm);
        }

        public void Classification(BackgroundWorker worker, ref byte[] outData)
        {
            int pixel = 0;
            double len = width * height;

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                        {
                            if ((worker != null && worker.CancellationPending == true))
                            {
                                //e.Cancel = true;
                                break;
                            }
                            else
                            {
                                // Perform a time consuming operation and report progress.
                                pixel++;
                                double percent = (double)(100 * pixel) / len;
                                System.Threading.Thread.Sleep(2);

                                Point p = new Point(((double)i) / width, ((double)j) / height);
                                Color color = Knn(p, k);
                                p.iX = (int)(p.x * (width - 1));
                                p.iY = (int)(p.y * (height - 1));
                                
                                SetOutput(ref outData, p.iX, p.iY, color);

                                if (worker != null) worker.ReportProgress((int)percent,outData);
                            }
                        }

        }

        private Color Knn(Point p, int k)
        {
            Dictionary<Point, double> dict = new Dictionary<Point, double>();

            for (int i = 0; i < data.normalizedPoints.Length; i++)
            {
                dict.Add(data.normalizedPoints[i], Distance(p, data.normalizedPoints[i]));
            }

            double sumR = 0.0;
            double sumG = 0.0;
            double sumB = 0.0;

            double weightSum = 0.0;
            int n = 1;

            foreach (KeyValuePair<Point,double> pair in dict.OrderBy(x => x.Value))
            {
                if (n <= k)
                {
                    if (!this.KnnWeightedChecked)
                    {
                        sumR += colors[pair.Key.c].r;
                        sumG += colors[pair.Key.c].g;
                        sumB += colors[pair.Key.c].b;
                    }
                    else
                    {
                        double weight = 1 / (pair.Value * pair.Value);
                        weightSum += weight;
                        sumR += colors[pair.Key.c].r * weight;
                        sumG += colors[pair.Key.c].g * weight;
                        sumB += colors[pair.Key.c].b * weight;

                    }
                }
                else
                {
                    break;
                }
                n++;
            }

            if (!this.KnnWeightedChecked)
            {
                return new Color((byte)(sumR / k), (byte)(sumG / k), (byte)(sumB / k));
            }
            else
            {
                return new Color((byte)(sumR / weightSum), (byte)(sumG / weightSum), (byte)(sumB / weightSum));
            }
        }

        protected void SetOutput(ref byte[] outData,int x, int y, Color color)
        {
            outData[x * 3 + y * stride + 0] = (byte)color.b;
            outData[x * 3 + y * stride + 1] = (byte)color.g;
            outData[x * 3 + y * stride + 2] = (byte)color.r;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
        }

        public void SortDataByClass()
        {
            if (data != null)
            {
                if (data.filePoints != null) data.filePoints = data.filePoints.OrderBy(x => x.c).ToArray<Point>();
                if (data.normalizedPoints != null) data.normalizedPoints = data.normalizedPoints.OrderBy(x => x.c).ToArray<Point>();
                this.Completed(DrawPoints());
            }
        }

        public void RandomizeData()
        {
            if (data != null)
            {
                Random rnd = new Random();
                if (data.filePoints != null) data.filePoints.Shuffle();
                if (data.normalizedPoints != null) data.normalizedPoints.Shuffle();
                this.Completed(DrawPoints());
            }
        }

        public void CopyData(double[] xs, double[] ys, byte[] cs)
        {
            data = new InputData();
            length = xs.Length;
            data.filePoints = new Point[length];
            for (int i = 0; i < length; i++)
                data.filePoints[i] = new Point(xs[i], ys[i], cs[i]);
            data.normalizedPoints = new Point[length];
            double maxX = xs.Max();
            double maxY = ys.Max();
            double minX = xs.Min();
            double minY = ys.Min();
            if (maxX == 0 || maxY == 0)
                return;
            HashSet<byte> hashset = new HashSet<byte>();
            for (int i = 0; i < length; i++)
                hashset.Add(data.filePoints[i].c);
            int different = hashset.Count;
            if (different > 7)
            {
                throw new Exception("Klas jest więcej niż siedem");
            }
            List<byte> list = hashset.ToList<byte>();

            Dictionary<byte, byte> dictionary = new Dictionary<byte, byte>();
            for (int i = 0; i < list.Count; i++)
                dictionary.Add(list[i], (byte)i);
            for (int i = 0; i < length; i++)
            {
                byte value;
                dictionary.TryGetValue(cs[i], out value);
                data.normalizedPoints[i] = new Point((xs[i] - minX) / (maxX - minX), (ys[i] - minY) / (maxY - minY), value);
            }

        }
    }
}
