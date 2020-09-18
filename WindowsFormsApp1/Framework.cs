using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.ImgHash;

namespace WindowsFormsApp1
{
    public partial class Framework : Form
    {
        public string imagePath;
        Image<Bgr, Byte> blueChannel;
        Image<Gray, Byte> img;
        Image<Gray, Byte> dst;

        public Framework()
        {
            InitializeComponent();
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Bitmaps|*.bmp|Jpegs|*.jpg|Pngs|*.png"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog.FileName;
                imagePath = openFileDialog.FileName;
            }
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            //chama a imagem carregada
            img = new Image<Gray, Byte>(imagePath);
            blueChannel = new Image<Bgr, Byte>(imagePath);
            Suppress(0);

            //converte para o canal azul da imagem
            //img = blueChannel.Convert<Gray, Byte>();
            dst = new Image<Gray, Byte>(imagePath);

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();

            //setando os filtros para encontrar as celulas
            CvInvoke.MedianBlur(img, dst, 21);
            CvInvoke.Threshold(dst, dst, 135, 255, ThresholdType.Binary);
            
            //encontro o contorno das celulas
            CvInvoke.FindContours(dst, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
            //CvInvoke.DrawContours(img, contours, -1, new MCvScalar(255, 0, 0));

            // criando uma mascara
            //Mat msk = new Mat(imagePath, ImreadModes.Grayscale);
            Image<Gray, Byte> mask = new Image<Gray, Byte>(img.Cols, img.Rows);
            Image<Gray, Byte> separateCell;
            mask.SetZero();
            CvInvoke.DrawContours(mask, contours, 1, new MCvScalar(255, 255, 255), -1);
            separateCell = img & mask;


            //RESULTADO NO OPENCV/
            String win1 = "Test Window"; //The name of the window
            CvInvoke.NamedWindow(win1); //Create the window using the specific name
            CvInvoke.Imshow(win1,separateCell); //Show the image
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(win1); //Destroy the window if key is pressed
        }


        private void Suppress(int spectrum)
        {

            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    blueChannel.Data[i, j, spectrum] = 0;
                }
            }
        }




    }
}
