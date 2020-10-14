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
            this.buttonProcess = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MononucleadasCells = new System.Windows.Forms.Label();
            this.HealthyCells = new System.Windows.Forms.Label();
            this.CellsQuantity = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Y_pos = new System.Windows.Forms.Label();
            this.X_pos = new System.Windows.Forms.Label();
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
            this.groupBox1.Controls.Add(this.buttonProcess);
            this.groupBox1.Controls.Add(this.buttonLoad);
            this.groupBox1.Location = new System.Drawing.Point(8, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 592);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // buttonProcess
            // 
            this.buttonProcess.Location = new System.Drawing.Point(6, 135);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(344, 56);
            this.buttonProcess.TabIndex = 1;
            this.buttonProcess.Text = "Analisar Imagem";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(6, 41);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(344, 57);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Carregar Imagem";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.MononucleadasCells);
            this.groupBox2.Controls.Add(this.HealthyCells);
            this.groupBox2.Controls.Add(this.CellsQuantity);
            this.groupBox2.Location = new System.Drawing.Point(1074, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 595);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // MononucleadasCells
            // 
            this.MononucleadasCells.AutoSize = true;
            this.MononucleadasCells.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MononucleadasCells.Location = new System.Drawing.Point(6, 150);
            this.MononucleadasCells.Name = "MononucleadasCells";
            this.MononucleadasCells.Size = new System.Drawing.Size(237, 41);
            this.MononucleadasCells.TabIndex = 2;
            this.MononucleadasCells.Text = "Mononucleadas:";
            // 
            // HealthyCells
            // 
            this.HealthyCells.AutoSize = true;
            this.HealthyCells.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.HealthyCells.Location = new System.Drawing.Point(6, 87);
            this.HealthyCells.Name = "HealthyCells";
            this.HealthyCells.Size = new System.Drawing.Size(156, 41);
            this.HealthyCells.TabIndex = 1;
            this.HealthyCells.Text = "Saudáveis:";
            // 
            // CellsQuantity
            // 
            this.CellsQuantity.AutoSize = true;
            this.CellsQuantity.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CellsQuantity.Location = new System.Drawing.Point(6, 25);
            this.CellsQuantity.Name = "CellsQuantity";
            this.CellsQuantity.Size = new System.Drawing.Size(232, 41);
            this.CellsQuantity.TabIndex = 0;
            this.CellsQuantity.Text = "Total de Células:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Location = new System.Drawing.Point(371, 77);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(694, 643);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Location = new System.Drawing.Point(3, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(688, 592);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
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
            this.panel1.Size = new System.Drawing.Size(1445, 66);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(485, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(475, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "Analisador de Núcleos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.Y_pos);
            this.panel2.Controls.Add(this.X_pos);
            this.panel2.Location = new System.Drawing.Point(8, 678);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1445, 31);
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
            this.ClientSize = new System.Drawing.Size(1465, 721);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Framework";
            this.Text = "Nuclei Detector";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Y_pos;
        private System.Windows.Forms.Label X_pos;
        private System.Windows.Forms.Label MononucleadasCells;
        private System.Windows.Forms.Label HealthyCells;
        private System.Windows.Forms.Label CellsQuantity;
    }
}

