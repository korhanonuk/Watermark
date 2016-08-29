using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Watermark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //rb1 seçildiğinde pb1 aktif olacak ve diğer pbler inaktif hale geçecek. Ayrıca pb1 in içindeki fotoğrafın opacity ayarı düşecek ve tekrar atanacak.
        static void PictureBoxActivate(RadioButton rb1, PictureBox pb1, PictureBox pb2, PictureBox pb3, PictureBox pb4, PictureBox pb5, PictureBox pb6)
        {
            pb1.Visible = true;
            pb2.Visible = false;
            pb3.Visible = false;
            pb4.Visible = false;
            pb5.Visible = false;

        }


        static public Image SetImageOpacity(Image image, float opacity)
        {

            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void rb5_CheckedChanged(object sender, EventArgs e)
        {
            PictureBoxActivate(rb5, pb5, pb2, pb3, pb4, pb1, pbMain);
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            PictureBoxActivate(rb1, pb1, pb2, pb3, pb4, pb5, pbMain);
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            PictureBoxActivate(rb3, pb3, pb2, pb5, pb4, pb1, pbMain);
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            PictureBoxActivate(rb2, pb2, pb5, pb3, pb4, pb1, pbMain);
        }

        private void rb4_CheckedChanged(object sender, EventArgs e)
        {
            PictureBoxActivate(rb4, pb4, pb2, pb3, pb5, pb1, pbMain);
        }


        static void SetPictureBox(PictureBox pb1, PictureBox pb2, OpenFileDialog ofd)
        {
            pb1.Image = new Bitmap(ofd.FileName);
            pb2.Controls.Add(pb1);
            Image img = SetImageOpacity(pb1.Image, 0.6f);
            pb1.Image = img;
            pb1.BackColor = Color.Transparent;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Resim Seç";
                dlg.Filter = "Resim Dosyaları (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    pbMain.Image = new Bitmap(dlg.FileName);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Resim Seçin";
                dlg.Filter = "PNG Files (*.png)|*.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    SetPictureBox(pb1, pbMain, dlg);
                    SetPictureBox(pb2, pbMain, dlg);
                    SetPictureBox(pb3, pbMain, dlg);
                    SetPictureBox(pb4, pbMain, dlg);
                    SetPictureBox(pb5, pbMain, dlg);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            Bitmap bitmap = new Bitmap(Convert.ToInt32(1024), Convert.ToInt32(1024), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            bitmap = (Bitmap)pbMain.Image;
            Bitmap bitmap2 = new Bitmap(bitmap, new Size(1600, 900));
            Graphics grfx = Graphics.FromImage(bitmap2);
            Image img = SetImageOpacity(pb1.Image, 0.6f);
            pb1.Image = img;
            pb1.BackColor = Color.Transparent;

            grfx.DrawImage(pb1.Image, 518, 29, 150, 79);
            // Add drawing commands here
            

            bitmap2.Save(@"C:\Users\Raven\Desktop\asdsf.jpeg", ImageFormat.Jpeg);





            //SaveFileDialog save = new SaveFileDialog();

            ////pb1.Parent = pbMain;
            ////pb1.ImageLocation = pb1.Location.ToString();
            ////Bitmap bmp = new Bitmap(pbMain.Image);
            ////pb1.DrawToBitmap(bmp, pb1.Bounds);
            ////pbBack.Image = bmp;
            //save.Filter = "Bitmap files (*.bmp)|*.bmp|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|PNG files (*.png)|*.png|TIF files (*.tif)|*.tif|All files (*.*)|*.*";
            //save.FilterIndex = 2;
            //save.RestoreDirectory = true;
            //save.OverwritePrompt = true;
            //save.ShowHelp = true;
            //save.AddExtension = true;

            //if ((save.ShowDialog() == DialogResult.OK))
            //    if (Path.GetExtension(save.FileName).ToLower() == ".bmp")
            //        pbBack.Image.Save(save.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            //    else if (Path.GetExtension(save.FileName).ToLower() == ".jpg")
            //        pbBack.Image.Save(save.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    else if (Path.GetExtension(save.FileName).ToLower() == ".gif")
            //        pbBack.Image.Save(save.FileName, System.Drawing.Imaging.ImageFormat.Gif);
            //    else if (Path.GetExtension(save.FileName).ToLower() == ".png")
            //        pbBack.Image.Save(save.FileName, System.Drawing.Imaging.ImageFormat.Png);
            //    else if (Path.GetExtension(save.FileName).ToLower() == ".tif")
            //        pbBack.Image.Save(save.FileName, System.Drawing.Imaging.ImageFormat.Tiff);

        }


    }

}
