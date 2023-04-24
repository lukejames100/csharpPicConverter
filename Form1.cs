using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using Imazen.WebP;
using System.Windows.Media;
//using 

/*using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using Aspose.Imaging;
using Aspose.Imaging.FileFormats.Png;
using Aspose.Imaging.ImageOptions;*/
namespace csharpPicConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //webp转jpg
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            filedialog.Filter = "webp图片(*.webp)|*.webp";
            filedialog.FilterIndex = 0;
            filedialog.RestoreDirectory = true;
            filedialog.Title = "选择webp文件";
            string filepath;
            if (filedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filepath = filedialog.FileName;
            }
            else
            {
                return;
            }
            textBox1.Text = filepath;
            //在同一个目录下生成同样文件名的jpg文件
            int pos = filepath.IndexOf('.');
            if (pos < 0)
            {
                MessageBox.Show("没有文件扩展名webp");
                return;
            }
            int lpos = filepath.LastIndexOf('.');
            int len = filepath.Length;
            if (lpos + 1 >= len)
            {
                MessageBox.Show("没有扩展名");
                return;
            }
            string secstr = filepath.Substring(0, lpos + 1);
            string newfilestr = secstr + "jpg";  //jpg文件名，包括目录

            //Bitmap bitmap = (Bitmap)Image.FromFile(filepath);
            //var stream = new FileStream(filepath, FileMode.Open);
            //var bitmap = new Bitmap(stream);
           // var image = Image.FromStream(stream);
            //bitmap.Save(newfilestr, ImageFormat.Jpeg);
            //Process.Start("explorer.exe", "/select, " + newfilestr);

            /*var bitmap = ReadWebpFile(filepath);
            bitmap.Save(newfilestr, System.Drawing.Imaging.ImageFormat.Jpeg);
            bitmap.Clone();*/
            //var imagebytes=await File.ReadAllBytesAsync

            //使用imageprocess，可能.net版本不夠高，對應的imageprocessor版本也不是最高
            /*using (var stream = new FileStream(filepath, FileMode.Open))
            {
                using (var imagefactory = new ImageFactory())
                {
                    imagefactory.Load(stream).Format(new JpegFormat { Quality = 100 }).Save(newfilestr);
                }
            }*/
            //用了Imazen
            byte[] bytes = System.IO.File.ReadAllBytes(filepath);
            Imazen.WebP.Extern.LoadLibrary.LoadWebPOrFail();
            SimpleDecoder simpleDecoder = new SimpleDecoder();
            Bitmap bitmap = simpleDecoder.DecodeFromBytes(bytes, bytes.LongLength);
            bitmap.Save(newfilestr, System.Drawing.Imaging.ImageFormat.Jpeg);

            //用文件浏览器，打开生成的文件
            System.Diagnostics.Process.Start("Explorer.exe", "/select," + @newfilestr);

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            filedialog.Filter = "png图片(*.png)|*.png";
            filedialog.FilterIndex = 0;
            filedialog.RestoreDirectory = true;
            filedialog.Title = "选择png文件";
            string filepath;
            if (filedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filepath = filedialog.FileName;
            }
            else
            {
                return;
            }
            textBox2.Text = filepath;
            //在同一个目录下生成同样文件名的jpg文件
            int pos = filepath.IndexOf('.');
            if (pos < 0)
            {
                MessageBox.Show("没有文件扩展名png");
                return;
            }
            int lpos = filepath.LastIndexOf('.');
            int len = filepath.Length;
            if (lpos + 1 >= len)
            {
                MessageBox.Show("没有扩展名");
                return;
            }
            string secstr = filepath.Substring(0, lpos + 1);
            string newfilestr = secstr + "jpg";  //jpg文件名，包括目录

            Image img = Image.FromFile(filepath);
            using (var b = new Bitmap(img.Width, img.Height))
            {
                b.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                using (var g = Graphics.FromImage(b))
                {
                    //g.Clear(Color.White);
                    g.Clear(System.Drawing.Color.White);
                    g.DrawImageUnscaled(img, 0, 0);
                }
                b.Save(@newfilestr, System.Drawing.Imaging.ImageFormat.Jpeg);
                //用文件浏览器，打开生成的文件
                System.Diagnostics.Process.Start("Explorer.exe", "/select," + @newfilestr);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            filedialog.Filter = "tiff图片(*.tif)|*.tif";
            filedialog.FilterIndex = 0;
            filedialog.RestoreDirectory = true;
            filedialog.Title = "选择tiff文件";
            string filepath;
            if (filedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filepath = filedialog.FileName;
            }
            else
            {
                return;
            }
            textBox3.Text = filepath;
            //在同一个目录下生成同样文件名的jpg文件
            int pos = filepath.IndexOf('.');
            if (pos < 0)
            {
                MessageBox.Show("没有文件扩展名tiff");
                return;
            }
            int lpos = filepath.LastIndexOf('.');
            int len = filepath.Length;
            if (lpos + 1 >= len)
            {
                MessageBox.Show("没有扩展名");
                return;
            }
            string secstr = filepath.Substring(0, lpos + 1);
            string newfilestr = secstr + "jpg";  //jpg文件名，包括目录

            byte[] bytes = System.IO.File.ReadAllBytes(filepath);
            Stream stream = new MemoryStream(bytes);

            using(FileStream fs=new FileStream(newfilestr,FileMode.Create))
            {
                System.Windows.Media.Imaging.TiffBitmapDecoder tiffdecoder = new System.Windows.Media.Imaging.TiffBitmapDecoder(stream, System.Windows.Media.Imaging.BitmapCreateOptions.PreservePixelFormat, System.Windows.Media.Imaging.BitmapCacheOption.Default);
                System.Windows.Media.Imaging.BitmapSource bitmapsource= tiffdecoder.Frames[0];
                System.Windows.Media.Imaging.JpegBitmapEncoder jpgencoder = new System.Windows.Media.Imaging.JpegBitmapEncoder();
                jpgencoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmapsource));
                jpgencoder.Save(fs);
                //用文件浏览器，打开生成的文件
                System.Diagnostics.Process.Start("Explorer.exe", "/select," + @newfilestr);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            filedialog.Filter = "bmp图片(*.bmp)|*.bmp";
            filedialog.FilterIndex = 0;
            filedialog.RestoreDirectory = true;
            filedialog.Title = "选择bmp文件";
            string filepath;
            if (filedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filepath = filedialog.FileName;
            }
            else
            {
                return;
            }
            textBox4.Text = filepath;
            //在同一个目录下生成同样文件名的jpg文件
            int pos = filepath.IndexOf('.');
            if (pos < 0)
            {
                MessageBox.Show("没有文件扩展名bmp");
                return;
            }
            int lpos = filepath.LastIndexOf('.');
            int len = filepath.Length;
            if (lpos + 1 >= len)
            {
                MessageBox.Show("没有扩展名");
                return;
            }
            string secstr = filepath.Substring(0, lpos + 1);
            string newfilestr = secstr + "jpg";  //jpg文件名，包括目录

            System.Drawing.Image isource=System.Drawing.Image.FromFile(filepath);
            System.Drawing.Imaging.ImageFormat tformat=isource.RawFormat;

            var qualityencoder = System.Drawing.Imaging.Encoder.Quality;
            //var quality=(long)<desired quality>;
            var ratio = new System.Drawing.Imaging.EncoderParameter(qualityencoder, 100L);
            var codecparams = new System.Drawing.Imaging.EncoderParameters(1);
            codecparams.Param[0] = ratio;
            //varjpgcodeinfo=
            //System.Drawing.Image isource = Image.FromFile(filepath);
            System.Drawing.Imaging.ImageCodecInfo[] arrayici = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            System.Drawing.Imaging.ImageCodecInfo jpgiciinfo = null;
            for (int x = 0; x < arrayici.Length; x++)
            {
                if (arrayici[x].FormatDescription.Equals("JPEG"))
                {
                    jpgiciinfo = arrayici[x];
                    break;
                }
            }
            if (jpgiciinfo != null)
            {
                isource.Save(newfilestr, jpgiciinfo, codecparams);
            }
            else
            {
                isource.Save(newfilestr, tformat);
            }
            //isource
            //用文件浏览器，打开生成的文件
            System.Diagnostics.Process.Start("Explorer.exe", "/select," + @newfilestr);
        }
    }
}
