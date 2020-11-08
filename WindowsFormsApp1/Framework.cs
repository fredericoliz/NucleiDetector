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
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Flann;
using Emgu.CV.Cuda;
using System.Runtime.InteropServices;
using Emgu.CV.CvEnum;
using Emgu.CV.XFeatures2D;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;
using CsvHelper;

namespace WindowsFormsApp1
{
    public partial class Framework : Form
    {
        private List<string> ListFormatosValidos = new List<string>(new string[]
        {
            ".png",
            ".jpeg",
            ".jpg",
            ".bmp",
            ".Jpeg",
            ".Png",
            ".Jpg",
            ".Bmp"
        });
        private BackgroundWorker BackgroundWorker = new BackgroundWorker();
        public string imagePath;
        public string NomeImagem;
        public string Caminho;
        public string NomeImagemSaida;
        public int CelulasIgnoradas2;
        public int CelulasNormais2;
        public int CelulasMicronucleadas2;
        public int CelulasBinucleadas2;
        public int CelulasCariolise2;
        public int CelulasTotais2;
        public int CelulasIgnoradas;
        public int CelulasNormais;
        public int CelulasMicronucleadas;
        public int CelulasBinucleadas;
        public int CelulasCariolise;
        public int CelulasTotais;
        public int Background;
        public int thresh;
        public int analisar;
        public StringBuilder csv;
        Image<Hsv, Byte> Camera;
        Image<Rgb, Byte> CameraResult;

        public Framework()
        {
            InitializeComponent();
            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            BackgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.WorkerSupportsCancellation = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                X_pos.Text = "X: " + e.X.ToString();
                Y_pos.Text = "Y: " + e.Y.ToString();
            }
        }

        private void CheckBoxAnalisar_CheckedChanged(object sender, EventArgs e)
        {
            string ImagemSelecionada = this.ListBoxImages.SelectedItem.ToString();
            string ImagemCaminho = Path.Combine(this.imagePath, ImagemSelecionada);
            if (System.IO.File.Exists(ImagemCaminho))
            {
                if (CheckBoxAnalisar.Checked)
                {
                    Caminho = ImagemCaminho;
                    pictureBox1.Image = Processar(Caminho, NomeImagem);
                    CelulasTotais = CelulasNormais + CelulasBinucleadas + CelulasCariolise + CelulasMicronucleadas;
                    LabelTotalCelulas.Text = "Total de células: " + CelulasTotais.ToString();
                    LabelNormais.Text = "Normais: " + CelulasNormais.ToString();
                    LabelBinucleadas.Text = "Binucleadas: " + CelulasBinucleadas.ToString();
                    LabelMicronucleadas.Text = "Micronucleadas: " + CelulasMicronucleadas.ToString();
                    LabelCariolise.Text = "Cariólise: " + CelulasCariolise.ToString();
                }
                else
                {
                    Caminho = ImagemCaminho;
                    pictureBox1.ImageLocation = ImagemCaminho;
                }
            }
            else
            {
                
            }
            
        }
        private Bitmap Processar(string Caminho, string NomeImagem)
        {
            if (pictureBox1.Image == null) return null;
            CelulasMicronucleadas = 0;
            CelulasCariolise = 0;
            CelulasBinucleadas = 0;
            CelulasNormais = 0;
            CelulasIgnoradas = 0;
            CelulasTotais = 0;
            Background = 0;
            int PosicaoThresh2 = 0;
            int PosicaoNucleo2 = 0;

            SimpleBlobDetector param = new SimpleBlobDetector();
            VectorOfKeyPoint keypoint = new VectorOfKeyPoint();
            Camera = new Image<Hsv, Byte>(Caminho);
            CameraResult = new Image<Rgb, Byte>(Caminho);

            Image<Gray, Byte> ImageSave = new Image<Gray, Byte>(Camera.Cols, Camera.Rows);
            Image<Rgb, Byte> ImageSave2 = new Image<Rgb, Byte>(Camera.Cols, Camera.Rows);

            Image<Gray, Byte> Cellthresh = new Image<Gray, Byte>(Camera.Cols, Camera.Rows);
            Image<Gray, Byte> ThreshSet = new Image<Gray, Byte>(Camera.Cols, Camera.Rows);
            Image<Gray, Byte> ThreshSet2 = new Image<Gray, Byte>(Camera.Cols, Camera.Rows);
            Image<Rgb, Byte> separateCellRGB = new Image<Rgb, Byte>(Camera.Cols, Camera.Rows);
            Image<Hsv, Byte> separateCell = new Image<Hsv, Byte>(Camera.Cols, Camera.Rows);
            Image<Hsv, Byte> separateNuclei = new Image<Hsv, Byte>(Camera.Cols, Camera.Rows);
            Image<Gray, Byte> MaskContourNuclei = new Image<Gray, Byte>(Camera.Cols, Camera.Rows);
            Image<Gray, Byte> MaskContourCell = new Image<Gray, Byte>(Camera.Cols, Camera.Rows);
            Image<Hsv, Byte> maskCell = new Image<Hsv, Byte>(Camera.Cols, Camera.Rows);
            Image<Gray, Byte> ContourCompare = new Image<Gray, Byte>(Camera.Cols, Camera.Rows);
            Image<Gray, Byte> Cellthresh2 = new Image<Gray, Byte>(Camera.Cols, Camera.Rows);
            Image<Hsv, Byte> nothing = new Image<Hsv, Byte>(Camera.Cols, Camera.Rows);

            Mat EllipseKernel = new Mat();
            Matrix<byte> kernel1 = new Matrix<byte>(new Byte[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } });
            var mask = Camera[0];
            //ImageSave = mask;
            //ImageSave.Save("met_hsv.png");
            EllipseKernel = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(5, 5), new Point(-1, -1));
            CvInvoke.MorphologyEx(mask, mask, MorphOp.Open, EllipseKernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
            CvInvoke.MorphologyEx(mask, mask, MorphOp.Close, EllipseKernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
            //ImageSave = mask;
            //ImageSave.Save("met_openclose.png");
            CvInvoke.Threshold(mask, mask, 50, 255, ThresholdType.Otsu);
            //ImageSave = mask;
            //ImageSave.Save("met_otsu.png");
            Mat distanceTransform = new Mat();
            CvInvoke.DistanceTransform(mask, distanceTransform, null, Emgu.CV.CvEnum.DistType.L2, 5);
            CvInvoke.Normalize(distanceTransform, distanceTransform, 0, 255, NormType.MinMax);
            var markers = distanceTransform.ToImage<Gray, byte>();
            //ImageSave = markers;
            //ImageSave.Save("met_distancetransform.png");
            markers = markers.ThresholdBinary(new Gray(100), new Gray(255));
            CvInvoke.ConnectedComponents(markers, markers);
            var finalMarkers = markers.Convert<Gray, Int32>();
            CvInvoke.Watershed(Camera, finalMarkers);
            Image<Gray, byte> boundaries = finalMarkers.Convert<byte>(delegate (Int32 x)
            {
                return (byte)(x == -1 ? 255 : 0);
            });

            boundaries._Dilate(1);
            //ImageSave = boundaries;
            //ImageSave.Save("met_watershed.png");
            CvInvoke.Threshold(boundaries, boundaries, 0, 255, ThresholdType.BinaryInv);
            Image<Gray, Byte> CellsBoundaries = new Image<Gray, Byte>(Camera.Cols, Camera.Rows);
            CellsBoundaries.SetZero();
            CellsBoundaries = boundaries & mask;
            //ImageSave = CellsBoundaries;
            //ImageSave.Save("met_segmentado.png");

            ////////////////////////////////////////
            // aqui cria o contorno do watershed e separa o nucleo de cada celula encontrada
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(CellsBoundaries, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxNone);
            for (int i = 0; i < contours.Size; i++)
            {
                maskCell.SetZero();
                separateCell.SetZero();
                separateCellRGB.SetZero();
                separateNuclei.SetZero();
                MaskContourNuclei.SetZero();
                MaskContourCell.SetZero();
                nothing.SetZero();
                double area = CvInvoke.ContourArea(contours[i]);
                double perimeter = CvInvoke.ArcLength(contours[i], true);
                //cria a mascara para separar cada célula e seus núcleos
                //caso o contorno for a area total da foto, ignora
                if (area < 400)
                {
                    Background += 1;
                    continue;
                }
                //se a celula for muito grande é uma célula a ser ignorada
                else if (area > 4000 || area < 1000 || perimeter > 250 || perimeter < 100)
                {
                    CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(255, 255, 255));
                    CelulasIgnoradas += 1;
                    continue;

                }
                //se tudo estiver correto, realiza a análise do núcleo da célula
                else
                {
                    CvInvoke.DrawContours(maskCell, contours, i, new MCvScalar(255, 255, 255), -1);
                    separateCell = Camera & maskCell;
                    //ImageSave = separateCell.Convert<Gray,Byte>();
                    //ImageSave.Save("met_mascaracelula.png");
                    var teste2 = separateCell.Convert<Gray, Byte>();
                    Double intensity = 0.0;
                    double pixelintensity = 0;
                    for (int cols = 0; cols < teste2.Cols; cols++)
                    {
                        for (int rows = 0; rows < teste2.Rows; rows++)
                        {
                            intensity += teste2.Data[rows, cols, 0];
                            if (teste2.Data[rows, cols, 0] > 0) 
                            {
                                pixelintensity++;
                            }
                        }
                    }
                    intensity = intensity / pixelintensity;
                    // verifica a intensidade de pixels na celula
                    if (intensity < 40)
                    {
                        CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(255, 0, 255));
                        CelulasCariolise++;
                        break;
                    }
                    else if (intensity < 55)
                    {
                        thresh = 120;
                    }
                    else
                    {
                        thresh = 110;
                    }

                    int AchouNucleo = 0;
                    int PosicaoThresh = 0;
                    int PosicaoNucleo = 0;
                    int Nucleos = 0;
                    int Micronucleada = 0;
                    int Binucleada = 0;
                    int Normal = 0;
                    double AreaNucleo1 = 0;
                    double AreaNucleo2 = 0;
                    int OutroNucleo = 0;
                    double AreaOutroNucleo = 0;
                    int PosicaoOutroNucleo = 0;
                    VectorOfVectorOfPoint Nucleithresh = new VectorOfVectorOfPoint();
                    for (int k = thresh; k > 95; k--)
                    {
                        Mat NucleihierarchyThresh = new Mat();
                        Mat MorphStructuring = new Mat();
                        Cellthresh = separateCell[0];
                        MorphStructuring = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(5, 5), new Point(-1, -1));
                        CvInvoke.Threshold(Cellthresh, Cellthresh, k, 255, ThresholdType.Binary);
                        CvInvoke.MorphologyEx(Cellthresh, Cellthresh, MorphOp.Dilate, MorphStructuring, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                        CvInvoke.FindContours(Cellthresh, Nucleithresh, NucleihierarchyThresh, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
                        Nucleos = 0;
                        AchouNucleo = 0;
                        AreaNucleo1 = 0;
                        AreaNucleo2 = 0;
                        OutroNucleo = 0;
                        if (Nucleithresh.Size > 0 && Nucleithresh.Size < 3)
                        {

                            for (int j = 0; j < Nucleithresh.Size; j++)
                            {
                                ContourCompare.SetZero();
                                MaskContourCell.SetZero();
                                MaskContourNuclei.SetZero();
                                double AreaNucleiThresh = CvInvoke.ContourArea(Nucleithresh[j]);
                                double PerimeterNucleiThresh = CvInvoke.ArcLength(Nucleithresh[j], true);
                                CvInvoke.DrawContours(MaskContourCell, contours, i, new MCvScalar(255, 255, 255));
                                CvInvoke.DrawContours(MaskContourNuclei, Nucleithresh, j, new MCvScalar(255, 255, 255));
                                ContourCompare = MaskContourCell & MaskContourNuclei;
                                int pixel = 0;
                                for (int cols = 0; cols < ContourCompare.Cols; cols++)
                                {
                                    for (int rows = 0; rows < ContourCompare.Rows; rows++)
                                    {
                                        pixel += ContourCompare.Data[rows, cols, 0];
                                    }
                                }

                                if (AreaNucleiThresh > 100 || pixel > 0)
                                {
                                    break;
                                }
                             
                                if (AreaNucleiThresh < 100 && AreaNucleiThresh > 25 && pixel == 0 && AchouNucleo == 0 && PerimeterNucleiThresh < 31)
                                {
                                    AchouNucleo = 1;
                                    PosicaoThresh = k;
                                    PosicaoNucleo = j;
                                    ThreshSet = Cellthresh;
                                    AreaNucleo1 = AreaNucleiThresh;
                                    MessageBox.Show(PerimeterNucleiThresh.ToString());
                                }
                                if (AreaNucleiThresh < 25 && pixel == 0 && AchouNucleo == 0 && OutroNucleo == 0)
                                {
                                    OutroNucleo = 1;
                                    AreaOutroNucleo = AreaNucleiThresh;
                                    PosicaoOutroNucleo = j;
                                }

                                if (AchouNucleo == 1 && OutroNucleo == 1 && pixel == 0)
                                {
                                    Nucleos++;
                                    if (Math.Abs(AreaNucleo1 - AreaOutroNucleo) > 20)
                                    {
                                        Micronucleada = 1;
                                    }
                                    else
                                    {
                                        Binucleada = 1;
                                    }
                                    continue;
                                }
                                
                                if ((AchouNucleo == 1 && OutroNucleo == 0 && pixel == 0))
                                {
                                    Nucleos++;
                                    AreaNucleo2 = AreaNucleiThresh;
                                    if (Nucleos > 1)
                                    {
                                        if (Math.Abs(AreaNucleo1 - AreaNucleo2) > 20)
                                        {
                                            Micronucleada = 1;
                                        }
                                        else
                                        {
                                            Binucleada = 1;
                                        }
                                        continue;
                                    }
                                    else if (Nucleos == 1)
                                    {
                                        Normal = 1;
                                    }
                                }
                            }
                        }
                        if (AchouNucleo == 1)
                        {
                            break;
                        }
                    }
                    if (AchouNucleo == 1)
                    {
                        
                        Mat NucleihierarchyThresh = new Mat();
                        Mat MorphStructuring = new Mat();
                        Cellthresh = separateCell[0];
                        MorphStructuring = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(5, 5), new Point(-1, -1));
                        CvInvoke.Threshold(Cellthresh, Cellthresh, PosicaoThresh, 255, ThresholdType.Binary);
                        CvInvoke.MorphologyEx(Cellthresh, Cellthresh, MorphOp.Dilate, MorphStructuring, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                        CvInvoke.FindContours(Cellthresh, Nucleithresh, NucleihierarchyThresh, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
                        int check = 0;

                        for (int j = 0; j < Nucleithresh.Size; j++)
                        {
                            ContourCompare.SetZero();
                            MaskContourCell.SetZero();
                            MaskContourNuclei.SetZero();
                            double AreaNucleiThresh = CvInvoke.ContourArea(Nucleithresh[j]);
                            CvInvoke.DrawContours(MaskContourCell, contours, i, new MCvScalar(255, 255, 255));
                            CvInvoke.DrawContours(MaskContourNuclei, Nucleithresh, j, new MCvScalar(255, 255, 255));
                            ContourCompare = MaskContourCell & MaskContourNuclei;
                            int pixel = 0;
                            for (int cols = 0; cols < ContourCompare.Cols; cols++)
                            {
                                for (int rows = 0; rows < ContourCompare.Rows; rows++)
                                {
                                    pixel += ContourCompare.Data[rows, cols, 0];
                                }
                            }
                            if (Nucleos == 2 && Binucleada == 1)
                            {
                                if(check == 0) // conta somente uma vez!
                                {
                                    check = 1;
                                    CelulasBinucleadas++;
                                }
                                CvInvoke.DrawContours(CameraResult, Nucleithresh, j, new MCvScalar(255, 0, 0));
                                CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(255, 0, 0));
                            }
                            else if (Nucleos == 2 && Micronucleada == 1)
                            {
                                if(check == 0) //checa somente uma vez!
                                {
                                    check = 1;
                                    CelulasMicronucleadas++;
                                }
                                CvInvoke.DrawContours(CameraResult, Nucleithresh, j, new MCvScalar(255, 128, 0));
                                CvInvoke.DrawContours(CameraResult, Nucleithresh, PosicaoOutroNucleo, new MCvScalar(255, 128, 0));
                                CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(255, 128, 0));
                            }
                            else if (Nucleos == 1 && Normal == 1)
                            {
                                VectorOfVectorOfPoint NucleithreshFinal = new VectorOfVectorOfPoint();
                                //VERIFICA A PRESENÇA DE OUTRO NUCLEO DENTRO DA CELULA!
                                for (int k = PosicaoThresh; k > 95; k--)
                                {
                                    VectorOfVectorOfPoint Nucleithresh2 = new VectorOfVectorOfPoint();
                                    int AchouNucleo2 = 0;
                                    Mat NucleihierarchyThresh2 = new Mat();
                                    Mat MorphStructuring2 = new Mat();
                                    Cellthresh = separateCell[0];
                                    Cellthresh2 = separateCell[0];
                                    MorphStructuring2 = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(9, 9), new Point(-1, -1));
                                    CvInvoke.Threshold(Cellthresh, Cellthresh, PosicaoThresh, 255, ThresholdType.Binary);
                                    //MessageBox.Show(PosicaoThresh.ToString());
                                    CvInvoke.Threshold(Cellthresh2, Cellthresh2, k, 255, ThresholdType.Binary);
                                    CvInvoke.MorphologyEx(Cellthresh, Cellthresh, MorphOp.Dilate, MorphStructuring2, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                                    CvInvoke.MorphologyEx(Cellthresh2, Cellthresh2, MorphOp.Dilate, MorphStructuring, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                                    CvInvoke.MorphologyEx(Cellthresh2, Cellthresh2, MorphOp.Erode, MorphStructuring, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                                    Cellthresh2 = Cellthresh2 - Cellthresh;
                                    CvInvoke.FindContours(Cellthresh2, Nucleithresh2, NucleihierarchyThresh2, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
                                    if (Nucleithresh2.Size > 0 && Nucleithresh2.Size < 10)
                                    {
                                        
                                        for (int z = 0; z < Nucleithresh2.Size; z++)
                                        {
                                            ContourCompare.SetZero();
                                            MaskContourCell.SetZero();
                                            MaskContourNuclei.SetZero();
                                            double perimeter2 = CvInvoke.ArcLength(Nucleithresh2[z], true);
                                            double AreaNucleiThresh2 = CvInvoke.ContourArea(Nucleithresh2[z]);
                                            CvInvoke.DrawContours(MaskContourCell, contours, i, new MCvScalar(255, 255, 255));
                                            CvInvoke.DrawContours(MaskContourNuclei, Nucleithresh2, z, new MCvScalar(255, 255, 255));
                                            ContourCompare = MaskContourCell & MaskContourNuclei;
                                            int pixel2 = 0;

                                            for (int cols = 0; cols < ContourCompare.Cols; cols++)
                                            {
                                                for (int rows = 0; rows < ContourCompare.Rows; rows++)
                                                {
                                                    pixel2 += ContourCompare.Data[rows, cols, 0];
                                                }
                                            }

                                            if (AreaNucleiThresh2 < 50 && AreaNucleiThresh2 > 30 && pixel2 == 0 && AchouNucleo2 == 0 && perimeter2 < 27)
                                            {
                                                AchouNucleo2 = 1;
                                                PosicaoThresh2 = k;
                                                PosicaoNucleo2 = z;
                                                //MessageBox.Show(perimeter2.ToString());
                                                ThreshSet2 = Cellthresh;
                                                NucleithreshFinal = Nucleithresh2;
                                                AreaNucleo2 = AreaNucleiThresh2;
                                                break;
                                            }
                                        }
                                    }
                                    if (AchouNucleo2 == 1)
                                    {
                                        Nucleos++;
                                        Binucleada = 1;
                                        Normal = 0;
                                        break;
                                    }
                                }

                                if (Normal == 1)
                                {
                                    if (check == 0) //checa somente uma vez!
                                    {
                                        check = 1;
                                        CelulasNormais++;
                                    }
                                    CvInvoke.DrawContours(CameraResult, Nucleithresh, j, new MCvScalar(0, 255, 0));
                                    CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(0, 255, 0));
                                    break;
                                }
                                else if (Binucleada == 1)
                                {
                                    if (check == 0) //checa somente uma vez!
                                    {
                                        check = 1;
                                        CelulasBinucleadas++;
                                    }
                                    CvInvoke.DrawContours(CameraResult, Nucleithresh, PosicaoNucleo, new MCvScalar(255, 0, 0));
                                    CvInvoke.DrawContours(CameraResult, NucleithreshFinal, PosicaoNucleo2, new MCvScalar(255, 0, 0));
                                    CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(255, 0, 0));
                                    break;
                                }
                                    
                            }
                        }
                    }
                    if (AchouNucleo == 0)
                        //provavelmente a célula não possui núcleo -- cariólise
                    {
                        CvInvoke.DrawContours(CameraResult, contours, i, new MCvScalar(255, 0, 255));
                        CelulasCariolise++;
                    }
                }

            }
            NomeImagemSaida = Path.GetFileName(Caminho);
            CelulasNormais2 = CelulasNormais;
            CelulasMicronucleadas2 = CelulasMicronucleadas;
            CelulasBinucleadas2 = CelulasBinucleadas;
            CelulasCariolise2 = CelulasCariolise;
            CelulasTotais2 = CelulasNormais2 + CelulasBinucleadas2 + CelulasCariolise2 + CelulasMicronucleadas2;
            return CameraResult.ToBitmap();
        }

        private void ButtonAbrirDiretorio_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(this.TextBoxCaminho.Text))
                {
                    this.FolderBrowserDialog.SelectedPath = this.TextBoxCaminho.Text;
                }
                else
                {
                    this.FolderBrowserDialog.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
                }
                
                DialogResult result = this.FolderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.TextBoxCaminho.Text = this.FolderBrowserDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadImages()
        {
            this.ListBoxImages.Items.Clear();
            this.imagePath = TextBoxCaminho.Text;

            if (Directory.Exists(imagePath))
            {
                string[] images = Directory.GetFiles(imagePath);

                foreach (string image in images)
                {
                    if (this.ListFormatosValidos.Contains(Path.GetExtension(image)))
                    {
                        this.ListBoxImages.Items.Add(Path.GetFileName(image));
                    }
                }
            }

            this.ListBoxImages.SelectedIndex = 0;
        }

        private void TextBoxCaminho_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadImages();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ImagemSelecionada = this.ListBoxImages.SelectedItem.ToString();
                string ImagemCaminho = Path.Combine(this.imagePath, ImagemSelecionada);
                if (System.IO.File.Exists(ImagemCaminho))
                {
                    if (CheckBoxAnalisar.Checked)
                    {
                        Caminho = ImagemCaminho;
                        pictureBox1.Image = Processar(Caminho, NomeImagem);
                        CelulasTotais = CelulasNormais + CelulasBinucleadas + CelulasCariolise + CelulasMicronucleadas;
                        LabelTotalCelulas.Text = "Total de células: " + CelulasTotais.ToString();
                        LabelNormais.Text = "Normais: " + CelulasNormais.ToString();
                        LabelBinucleadas.Text = "Binucleadas: " + CelulasBinucleadas.ToString();
                        LabelMicronucleadas.Text = "Micronucleadas: " + CelulasMicronucleadas.ToString();
                        LabelCariolise.Text = "Cariólise: " + CelulasCariolise.ToString();
                    }
                    else
                    {
                        Caminho = ImagemCaminho;
                        pictureBox1.ImageLocation = ImagemCaminho;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AnalisarTudo_Click(object sender, EventArgs e)
        {
            try
            {
                BackgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.imagePath = TextBoxCaminho.Text;
            int count = 0;
            csv = new StringBuilder();
            var newLine = string.Format("{0},{1},{2},{3},{4},{5}", "imagem", "total de celulas", "normais", "binucleadas", "micronucleadas", "cariolise");
            csv.AppendLine(newLine);
            foreach (string path in this.ListBoxImages.Items)
            {
                string ImagemSelecionada = path;
                string ImagemCaminho = Path.Combine(this.imagePath, ImagemSelecionada);

                if (System.IO.File.Exists(ImagemCaminho))
                {
                    NomeImagem = ImagemSelecionada;
                    Caminho = ImagemCaminho;
                    pictureBox1.Image = Processar(Caminho, NomeImagem);
                    count++;
                    BackgroundWorker.ReportProgress((count * 100) / this.ListBoxImages.Items.Count);
                }
                if (BackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    BackgroundWorker.ReportProgress(0);
                    return;
                }
            }
            BackgroundWorker.ReportProgress(100);
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            CameraResult.Save("resultados/result - " + NomeImagemSaida);
            

            var normais = CelulasNormais2.ToString();
            var binuc = CelulasBinucleadas2.ToString();
            var micron = CelulasMicronucleadas2.ToString();
            var cario = CelulasCariolise2.ToString();
            var total = CelulasTotais2.ToString();
            var newLine = string.Format("{0},{1},{2},{3},{4},{5}", NomeImagemSaida, total, normais, binuc, micron, cario);
            csv.AppendLine(newLine);

            LabelTotalCelulas.Text = "Total de células: " + CelulasTotais2.ToString();
            LabelNormais.Text = "Normais: " + CelulasNormais2.ToString();
            LabelBinucleadas.Text = "Binucleadas: " + CelulasBinucleadas2.ToString();
            LabelMicronucleadas.Text = "Micronucleadas: " + CelulasMicronucleadas2.ToString();
            LabelCariolise.Text = "Cariólise: " + CelulasCariolise2.ToString();
            progressBar1.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                File.WriteAllText("resultados/result.csv", csv.ToString());
                MessageBox.Show("Análise Cancelada");
            }
            else if (e.Error != null)
            {
                File.WriteAllText("resultados/result.csv", csv.ToString());
                MessageBox.Show("Ocorreu um erro durante a análise. A thread foi abortada.");
            }
            else
            {
                File.WriteAllText("resultados/result.csv", csv.ToString());
                MessageBox.Show("Análise Concluída");
            }
        }

        private void ButtonCancelar_Click(object sender, EventArgs e)
        {
            if (BackgroundWorker.IsBusy)
            {
                BackgroundWorker.CancelAsync();
            }
        }
    }
}
