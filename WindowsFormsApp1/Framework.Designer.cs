using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsFormsApp1
{
    partial class Framework
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButtonCancelar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.AnalisarTudo = new System.Windows.Forms.Button();
            this.CheckBoxAnalisar = new System.Windows.Forms.CheckBox();
            this.ButtonAbrirDiretorio = new System.Windows.Forms.Button();
            this.TextBoxCaminho = new System.Windows.Forms.TextBox();
            this.ListBoxImages = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LabelMicronucleadas = new System.Windows.Forms.Label();
            this.LabelCariolise = new System.Windows.Forms.Label();
            this.LabelBinucleadas = new System.Windows.Forms.Label();
            this.LabelNormais = new System.Windows.Forms.Label();
            this.LabelTotalCelulas = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Y_pos = new System.Windows.Forms.Label();
            this.X_pos = new System.Windows.Forms.Label();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.ButtonCancelar);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.AnalisarTudo);
            this.groupBox1.Controls.Add(this.CheckBoxAnalisar);
            this.groupBox1.Controls.Add(this.ButtonAbrirDiretorio);
            this.groupBox1.Controls.Add(this.TextBoxCaminho);
            this.groupBox1.Controls.Add(this.ListBoxImages);
            this.groupBox1.Location = new System.Drawing.Point(8, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 608);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // ButtonCancelar
            // 
            this.ButtonCancelar.Location = new System.Drawing.Point(6, 444);
            this.ButtonCancelar.Name = "ButtonCancelar";
            this.ButtonCancelar.Size = new System.Drawing.Size(101, 38);
            this.ButtonCancelar.TabIndex = 7;
            this.ButtonCancelar.Text = "Cancelar";
            this.ButtonCancelar.UseVisualStyleBackColor = true;
            this.ButtonCancelar.Click += new System.EventHandler(this.ButtonCancelar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 559);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(343, 31);
            this.progressBar1.TabIndex = 2;
            // 
            // AnalisarTudo
            // 
            this.AnalisarTudo.Location = new System.Drawing.Point(6, 367);
            this.AnalisarTudo.Name = "AnalisarTudo";
            this.AnalisarTudo.Size = new System.Drawing.Size(178, 62);
            this.AnalisarTudo.TabIndex = 6;
            this.AnalisarTudo.Text = "Analisar Pasta";
            this.AnalisarTudo.UseVisualStyleBackColor = true;
            this.AnalisarTudo.Click += new System.EventHandler(this.AnalisarTudo_Click);
            // 
            // CheckBoxAnalisar
            // 
            this.CheckBoxAnalisar.AutoSize = true;
            this.CheckBoxAnalisar.Location = new System.Drawing.Point(6, 499);
            this.CheckBoxAnalisar.Name = "CheckBoxAnalisar";
            this.CheckBoxAnalisar.Size = new System.Drawing.Size(123, 24);
            this.CheckBoxAnalisar.TabIndex = 5;
            this.CheckBoxAnalisar.Text = "Modo Análise";
            this.CheckBoxAnalisar.UseVisualStyleBackColor = true;
            this.CheckBoxAnalisar.CheckedChanged += new System.EventHandler(this.CheckBoxAnalisar_CheckedChanged);
            // 
            // ButtonAbrirDiretorio
            // 
            this.ButtonAbrirDiretorio.Location = new System.Drawing.Point(308, 24);
            this.ButtonAbrirDiretorio.Name = "ButtonAbrirDiretorio";
            this.ButtonAbrirDiretorio.Size = new System.Drawing.Size(43, 27);
            this.ButtonAbrirDiretorio.TabIndex = 4;
            this.ButtonAbrirDiretorio.Text = "...";
            this.ButtonAbrirDiretorio.UseVisualStyleBackColor = true;
            this.ButtonAbrirDiretorio.Click += new System.EventHandler(this.ButtonAbrirDiretorio_Click);
            // 
            // TextBoxCaminho
            // 
            this.TextBoxCaminho.Location = new System.Drawing.Point(4, 24);
            this.TextBoxCaminho.Name = "TextBoxCaminho";
            this.TextBoxCaminho.Size = new System.Drawing.Size(298, 27);
            this.TextBoxCaminho.TabIndex = 3;
            this.TextBoxCaminho.TextChanged += new System.EventHandler(this.TextBoxCaminho_TextChanged);
            // 
            // ListBoxImages
            // 
            this.ListBoxImages.FormattingEnabled = true;
            this.ListBoxImages.ItemHeight = 20;
            this.ListBoxImages.Location = new System.Drawing.Point(6, 54);
            this.ListBoxImages.Name = "ListBoxImages";
            this.ListBoxImages.Size = new System.Drawing.Size(343, 284);
            this.ListBoxImages.TabIndex = 2;
            this.ListBoxImages.SelectedIndexChanged += new System.EventHandler(this.ListBoxImages_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.LabelMicronucleadas);
            this.groupBox2.Controls.Add(this.LabelCariolise);
            this.groupBox2.Controls.Add(this.LabelBinucleadas);
            this.groupBox2.Controls.Add(this.LabelNormais);
            this.groupBox2.Controls.Add(this.LabelTotalCelulas);
            this.groupBox2.Location = new System.Drawing.Point(1078, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 608);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // LabelMicronucleadas
            // 
            this.LabelMicronucleadas.AutoSize = true;
            this.LabelMicronucleadas.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelMicronucleadas.ForeColor = System.Drawing.Color.Orange;
            this.LabelMicronucleadas.Location = new System.Drawing.Point(6, 257);
            this.LabelMicronucleadas.Name = "LabelMicronucleadas";
            this.LabelMicronucleadas.Size = new System.Drawing.Size(241, 41);
            this.LabelMicronucleadas.TabIndex = 2;
            this.LabelMicronucleadas.Text = "Micronucleadas: ";
            // 
            // LabelCariolise
            // 
            this.LabelCariolise.AutoSize = true;
            this.LabelCariolise.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelCariolise.ForeColor = System.Drawing.Color.DarkViolet;
            this.LabelCariolise.Location = new System.Drawing.Point(6, 330);
            this.LabelCariolise.Name = "LabelCariolise";
            this.LabelCariolise.Size = new System.Drawing.Size(145, 41);
            this.LabelCariolise.TabIndex = 2;
            this.LabelCariolise.Text = "Cariólise: ";
            // 
            // LabelBinucleadas
            // 
            this.LabelBinucleadas.AutoSize = true;
            this.LabelBinucleadas.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelBinucleadas.ForeColor = System.Drawing.Color.Red;
            this.LabelBinucleadas.Location = new System.Drawing.Point(6, 182);
            this.LabelBinucleadas.Name = "LabelBinucleadas";
            this.LabelBinucleadas.Size = new System.Drawing.Size(189, 41);
            this.LabelBinucleadas.TabIndex = 2;
            this.LabelBinucleadas.Text = "Binucleadas: ";
            // 
            // LabelNormais
            // 
            this.LabelNormais.AutoSize = true;
            this.LabelNormais.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelNormais.ForeColor = System.Drawing.Color.Green;
            this.LabelNormais.Location = new System.Drawing.Point(6, 107);
            this.LabelNormais.Name = "LabelNormais";
            this.LabelNormais.Size = new System.Drawing.Size(144, 41);
            this.LabelNormais.TabIndex = 1;
            this.LabelNormais.Text = "Normais: ";
            // 
            // LabelTotalCelulas
            // 
            this.LabelTotalCelulas.AutoSize = true;
            this.LabelTotalCelulas.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelTotalCelulas.Location = new System.Drawing.Point(6, 33);
            this.LabelTotalCelulas.Name = "LabelTotalCelulas";
            this.LabelTotalCelulas.Size = new System.Drawing.Size(240, 41);
            this.LabelTotalCelulas.TabIndex = 0;
            this.LabelTotalCelulas.Text = "Total de Células: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Location = new System.Drawing.Point(371, 77);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(698, 608);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(692, 582);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1449, 66);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(487, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(475, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "Analisador de Núcleos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Y_pos);
            this.panel2.Controls.Add(this.X_pos);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 706);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1469, 31);
            this.panel2.TabIndex = 6;
            // 
            // Y_pos
            // 
            this.Y_pos.AutoSize = true;
            this.Y_pos.Location = new System.Drawing.Point(68, 11);
            this.Y_pos.Name = "Y_pos";
            this.Y_pos.Size = new System.Drawing.Size(20, 20);
            this.Y_pos.TabIndex = 1;
            this.Y_pos.Text = "Y:";
            // 
            // X_pos
            // 
            this.X_pos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.X_pos.AutoSize = true;
            this.X_pos.Location = new System.Drawing.Point(0, 11);
            this.X_pos.Name = "X_pos";
            this.X_pos.Size = new System.Drawing.Size(21, 20);
            this.X_pos.TabIndex = 0;
            this.X_pos.Text = "X:";
            this.X_pos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Framework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1469, 737);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Framework";
            this.Text = "Nuclei Detector";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Y_pos;
        private System.Windows.Forms.Label X_pos;
        private System.Windows.Forms.Label LabelBinucleadas;
        private System.Windows.Forms.Label LabelNormais;
        private System.Windows.Forms.Label LabelTotalCelulas;
        private System.Windows.Forms.Label LabelCariolise;
        private System.Windows.Forms.Label LabelMicronucleadas;
        private System.Windows.Forms.Button ButtonAbrirDiretorio;
        private System.Windows.Forms.TextBox TextBoxCaminho; 
        private System.Windows.Forms.ListBox ListBoxImages;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.CheckBox CheckBoxAnalisar;
        private System.Windows.Forms.Button AnalisarTudo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button ButtonCancelar;
    }
}

