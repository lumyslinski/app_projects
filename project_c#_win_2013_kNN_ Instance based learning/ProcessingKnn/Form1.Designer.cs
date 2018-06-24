namespace kNN
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.daneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortujPoKlasieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mieszajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kUpDown = new System.Windows.Forms.NumericUpDown();
            this.KLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.chkWeightedKnn = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.operacjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kNNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wyczyśćToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.przywróćToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnStopKnn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.daneToolStripMenuItem,
            this.operacjeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(438, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.openToolStripMenuItem.Text = "Otwórz...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveToolStripMenuItem.Text = "Zapisz...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // daneToolStripMenuItem
            // 
            this.daneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortujPoKlasieToolStripMenuItem,
            this.mieszajToolStripMenuItem});
            this.daneToolStripMenuItem.Name = "daneToolStripMenuItem";
            this.daneToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.daneToolStripMenuItem.Text = "Dane";
            // 
            // sortujPoKlasieToolStripMenuItem
            // 
            this.sortujPoKlasieToolStripMenuItem.Name = "sortujPoKlasieToolStripMenuItem";
            this.sortujPoKlasieToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.sortujPoKlasieToolStripMenuItem.Text = "Sortuj po klasie";
            this.sortujPoKlasieToolStripMenuItem.Click += new System.EventHandler(this.SortujPoKlasieToolStripMenuItem_Click);
            // 
            // mieszajToolStripMenuItem
            // 
            this.mieszajToolStripMenuItem.Name = "mieszajToolStripMenuItem";
            this.mieszajToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.mieszajToolStripMenuItem.Text = "Mieszaj";
            this.mieszajToolStripMenuItem.Click += new System.EventHandler(this.MieszajToolStripMenuItem_Click);
            // 
            // kUpDown
            // 
            this.kUpDown.Location = new System.Drawing.Point(31, 27);
            this.kUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.kUpDown.Name = "kUpDown";
            this.kUpDown.Size = new System.Drawing.Size(116, 20);
            this.kUpDown.TabIndex = 1;
            this.kUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.kUpDown.ValueChanged += new System.EventHandler(this.KUpDown_ValueChanged);
            // 
            // KLabel
            // 
            this.KLabel.AutoSize = true;
            this.KLabel.Location = new System.Drawing.Point(12, 31);
            this.KLabel.Name = "KLabel";
            this.KLabel.Size = new System.Drawing.Size(17, 13);
            this.KLabel.TabIndex = 2;
            this.KLabel.Text = "K:";
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox.Location = new System.Drawing.Point(0, 58);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(438, 400);
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // chkWeightedKnn
            // 
            this.chkWeightedKnn.AutoSize = true;
            this.chkWeightedKnn.Location = new System.Drawing.Point(153, 29);
            this.chkWeightedKnn.Name = "chkWeightedKnn";
            this.chkWeightedKnn.Size = new System.Drawing.Size(65, 17);
            this.chkWeightedKnn.TabIndex = 7;
            this.chkWeightedKnn.Text = "Ważony";
            this.chkWeightedKnn.UseVisualStyleBackColor = true;
            this.chkWeightedKnn.CheckedChanged += new System.EventHandler(this.ChkWeightedKnn_CheckedChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk);
            // 
            // operacjeToolStripMenuItem
            // 
            this.operacjeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kNNToolStripMenuItem,
            this.wyczyśćToolStripMenuItem,
            this.przywróćToolStripMenuItem});
            this.operacjeToolStripMenuItem.Name = "operacjeToolStripMenuItem";
            this.operacjeToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.operacjeToolStripMenuItem.Text = "Operacje";
            // 
            // kNNToolStripMenuItem
            // 
            this.kNNToolStripMenuItem.Name = "kNNToolStripMenuItem";
            this.kNNToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.kNNToolStripMenuItem.Text = "kNN";
            this.kNNToolStripMenuItem.Click += new System.EventHandler(this.KNNToolStripMenuItem_Click);
            // 
            // wyczyśćToolStripMenuItem
            // 
            this.wyczyśćToolStripMenuItem.Name = "wyczyśćToolStripMenuItem";
            this.wyczyśćToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.wyczyśćToolStripMenuItem.Text = "Wyczyść";
            this.wyczyśćToolStripMenuItem.Click += new System.EventHandler(this.WyczyscToolStripMenuItem_Click);
            // 
            // przywróćToolStripMenuItem
            // 
            this.przywróćToolStripMenuItem.Name = "przywróćToolStripMenuItem";
            this.przywróćToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.przywróćToolStripMenuItem.Text = "Przywróć";
            this.przywróćToolStripMenuItem.Click += new System.EventHandler(this.PrzywrocToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(224, 27);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(185, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // btnStopKnn
            // 
            this.btnStopKnn.Location = new System.Drawing.Point(418, 27);
            this.btnStopKnn.Name = "btnStopKnn";
            this.btnStopKnn.Size = new System.Drawing.Size(17, 23);
            this.btnStopKnn.TabIndex = 9;
            this.btnStopKnn.Text = "X";
            this.btnStopKnn.UseVisualStyleBackColor = true;
            this.btnStopKnn.Click += new System.EventHandler(this.BtnStopKnn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 458);
            this.Controls.Add(this.btnStopKnn);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chkWeightedKnn);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.KLabel);
            this.Controls.Add(this.kUpDown);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Klasyfikator KNN";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem daneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortujPoKlasieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mieszajToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown kUpDown;
        private System.Windows.Forms.Label KLabel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.CheckBox chkWeightedKnn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem operacjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kNNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wyczyśćToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem przywróćToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnStopKnn;
    }
}

