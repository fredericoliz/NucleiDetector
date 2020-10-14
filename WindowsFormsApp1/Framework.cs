﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Flann;
using Emgu.CV.Cuda;
using System.Runtime.InteropServices;
using Emgu.CV.CvEnum;
using Emgu.CV.OCR;
using Emgu.CV.XFeatures2D;

namespace WindowsFormsApp1
{
    public partial class Framework : Form
    {
        public string imagePath;
        public int CelulasIgnoradas;
        public int CelulasNormais;
        public int CelulasMicronucleadas;
        public int Background;
        Image<Hsv, Byte> img;
        Image<Hsv, Byte> Camera;
        Image<Hsv, Byte> CameraResult;
        Image<Hsv, Byte> imgH;

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

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                X_pos.Text = "X: " + e.X.ToString();
                Y_pos.Text = "Y: " + e.Y.ToString();
            }
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null) return;
                CelulasMicronucleadas = 0;
                CelulasNormais = 0;
                CelulasIgnoradas = 0;
                Background = 0;
                SimpleBlobDetector param = new SimpleBlobDetector();
                VectorOfKeyPoint keypoint = new VectorOfKeyPoint();
                img = new Image<Hsv, Byte>(imagePath);
                Camera = new Image<Hsv, Byte>(imagePath);
                CameraResult = new Image<Hsv, Byte>(imagePath);
                Matrix<byte> kernel1 = new Matrix<byte>(new Byte[3, 3] { { 1, 1, 1}, { 1, 1, 1}, { 1, 1, 1} });
                var mask = img[0].MorphologyEx(MorphOp.Open, kernel1, new Point(-1, -1), 1, BorderType.Default, new MCvScalar())
                    .MorphologyEx(MorphOp.Close, kernel1, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                CvInvoke.Threshold(mask, mask, 50, 255, ThresholdType.Otsu);
                Mat distanceTransform = new Mat();
                CvInvoke.DistanceTransform(mask, distanceTransform, null, Emgu.CV.CvEnum.DistType.L2, 5);
                CvInvoke.Normalize(distanceTransform, distanceTransform, 0, 255, NormType.MinMax);
                var markers = distanceTransform.ToImage<Gray, byte>()
                    .ThresholdBinary(new Gray(90), new Gray(255));
                CvInvoke.ConnectedComponents(markers, markers);
                var finalMarkers = markers.Convert<Gray, Int32>();
                CvInvoke.Watershed(img, finalMarkers);
                Image<Gray, byte> boundaries = finalMarkers.Convert<byte>(delegate (Int32 x)
                {
                    return (byte)(x == -1 ? 255 : 0);
                });

                boundaries._Dilate(1);
                CvInvoke.Threshold(boundaries, boundaries, 0, 255, ThresholdType.BinaryInv);
                Image<Gray, Byte> CellsBoundaries = new Image<Gray, Byte>(img.Cols, img.Rows);
                CellsBoundaries.SetZero();
                CellsBoundaries = boundaries & mask;
                // criando a mascara dos nucleos:
                imgH = new Image<Hsv, Byte>(imagePath);
                var nucleimask = imgH[0];
                CvInvoke.Threshold(nucleimask, nucleimask, 100, 255, ThresholdType.Binary);
                CvInvoke.MorphologyEx(nucleimask,nucleimask,MorphOp.Close, kernel1, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

                ////////////////////////////////////////
                // aqui cria o contorno do watershed e separa o nucleo de cada celula encontrada
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Mat hierarchy = new Mat();
                CvInvoke.FindContours(CellsBoundaries, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxNone);
                for (int i = 0; i < contours.Size; i++)
                {
                    Image<Hsv, Byte> separateCell = new Image<Hsv, Byte>(img.Cols, img.Rows);
                    Image<Hsv, Byte> separateNuclei = new Image<Hsv, Byte>(img.Cols, img.Rows);
                    Image<Hsv, Byte> maskCell = new Image<Hsv, Byte>(img.Cols, img.Rows);
                    //Image<Hsv, Byte> ImageSave = new Image<Gray, Byte>(img.Cols, img.Rows);
                    //MessageBox.Show(contours.Size.ToString());
                    maskCell.SetZero();
                    separateCell.SetZero();
                    separateNuclei.SetZero();
                    double area = CvInvoke.ContourArea(contours[i]);
                    //cria a mascara para separar cada célula e seus núcleos
                    //caso o contorno for a area total da foto, ignora
                    if (area < 500)
                    {
                        Background += 1;
                    }
                    //se a celula for muito grande é uma célula a ser ignorada
                    else if (area > 4000 || area < 1000) 
                    {
                        CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(255, 100, 255));
                        CelulasIgnoradas += 1;

                    }
                    //se tudo estiver correto, realiza a análise do núcleo da célula
                    else
                    {
                        CvInvoke.DrawContours(maskCell, contours, i, new MCvScalar(255, 255, 255), -1);
                        separateCell = Camera & maskCell;
                        separateNuclei = separateCell & nucleimask.Convert<Hsv, Byte>();
                        VectorOfVectorOfPoint Nucleicontours = new VectorOfVectorOfPoint();
                        Mat Nucleihierarchy = new Mat();
                        CvInvoke.FindContours(separateNuclei.Convert<Gray, Byte>(), Nucleicontours, Nucleihierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
                        //se encontrar 2 núcleos dentro da célula
                        if (Nucleicontours.Size == 2)
                        {
                            double AreaFirstNuclei = CvInvoke.ContourArea(Nucleicontours[0]);
                            double AreaSecondNuclei = CvInvoke.ContourArea(Nucleicontours[1]);
                            //ignora os nucleos gigantes por conta do threshold
                            if (AreaFirstNuclei > 50 || AreaSecondNuclei > 50) 
                            {
                                CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(255, 100, 255));
                                CelulasIgnoradas += 1;
                            }
                            //se for muito menor (1/5 do valor) que o primeiro nucleo ou vice versa, é um micronucleo
                            else if ((AreaFirstNuclei < (AreaSecondNuclei / 2)) || (AreaSecondNuclei < (AreaFirstNuclei / 2)))
                            {
                                CelulasMicronucleadas += 1;
                                CvInvoke.DrawContours(CameraResult, Nucleicontours, -1, new MCvScalar(60, 255, 255), -1);
                                CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(60, 255, 255));
                            }
                        }
                        //se tiver apenas um núcleo dentro da célula
                        else if (Nucleicontours.Size == 1)
                        {
                            CelulasNormais += 1;
                            CvInvoke.DrawContours(CameraResult, Nucleicontours, -1, new MCvScalar(128, 128, 128), -1);
                            CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(128, 128, 128));
                        }
                        else
                        {
                            CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(0, 0, 255));
                            CvInvoke.DrawContours(CameraResult, Nucleicontours, -1, new MCvScalar(0, 0, 255),-1);
                        }
                    }
                }
                HealthyCells.Text = "Células Normais: " + CelulasNormais.ToString();
                MononucleadasCells.Text = "Micronucleadas: " + CelulasMicronucleadas.ToString();
                CellsQuantity.Text = "Ignoradas: " + CelulasIgnoradas.ToString();
                pictureBox1.Image = CameraResult.ToBitmap();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
