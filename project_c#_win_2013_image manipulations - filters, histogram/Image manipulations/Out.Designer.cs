namespace Lab2
{
    partial class Out
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.pictureBoxLoaded = new System.Windows.Forms.PictureBox();
            this.pictureBoxModified = new System.Windows.Forms.PictureBox();
            this.groupBoxHistograms = new System.Windows.Forms.GroupBox();
            this.pictureBoxHistogramModified = new System.Windows.Forms.PictureBox();
            this.pictureBoxHistogramLoaded = new System.Windows.Forms.PictureBox();
            this.comboBoxModifyList = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoaded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxModified)).BeginInit();
            this.groupBoxHistograms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHistogramModified)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHistogramLoaded)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 693);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(512, 73);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Load new image";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // pictureBoxLoaded
            // 
            this.pictureBoxLoaded.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxLoaded.Name = "pictureBoxLoaded";
            this.pictureBoxLoaded.Size = new System.Drawing.Size(512, 512);
            this.pictureBoxLoaded.TabIndex = 1;
            this.pictureBoxLoaded.TabStop = false;
            // 
            // pictureBoxModified
            // 
            this.pictureBoxModified.Location = new System.Drawing.Point(530, 12);
            this.pictureBoxModified.Name = "pictureBoxModified";
            this.pictureBoxModified.Size = new System.Drawing.Size(512, 512);
            this.pictureBoxModified.TabIndex = 2;
            this.pictureBoxModified.TabStop = false;
            // 
            // groupBoxHistograms
            // 
            this.groupBoxHistograms.Controls.Add(this.pictureBoxHistogramModified);
            this.groupBoxHistograms.Controls.Add(this.pictureBoxHistogramLoaded);
            this.groupBoxHistograms.Location = new System.Drawing.Point(12, 530);
            this.groupBoxHistograms.Name = "groupBoxHistograms";
            this.groupBoxHistograms.Size = new System.Drawing.Size(1030, 157);
            this.groupBoxHistograms.TabIndex = 3;
            this.groupBoxHistograms.TabStop = false;
            this.groupBoxHistograms.Text = "Histograms";
            // 
            // pictureBoxHistogramModified
            // 
            this.pictureBoxHistogramModified.Location = new System.Drawing.Point(627, 15);
            this.pictureBoxHistogramModified.Name = "pictureBoxHistogramModified";
            this.pictureBoxHistogramModified.Size = new System.Drawing.Size(309, 132);
            this.pictureBoxHistogramModified.TabIndex = 1;
            this.pictureBoxHistogramModified.TabStop = false;
            // 
            // pictureBoxHistogramLoaded
            // 
            this.pictureBoxHistogramLoaded.Location = new System.Drawing.Point(78, 15);
            this.pictureBoxHistogramLoaded.Name = "pictureBoxHistogramLoaded";
            this.pictureBoxHistogramLoaded.Size = new System.Drawing.Size(309, 132);
            this.pictureBoxHistogramLoaded.TabIndex = 0;
            this.pictureBoxHistogramLoaded.TabStop = false;
            // 
            // comboBoxModifyList
            // 
            this.comboBoxModifyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModifyList.FormattingEnabled = true;
            this.comboBoxModifyList.Items.AddRange(new object[] {
            "Convert to Grey",
            "Convert to Grey (weights)",
            "Average filter",
            "Laplacian filter",
            "Sobel filter"});
            this.comboBoxModifyList.Location = new System.Drawing.Point(530, 695);
            this.comboBoxModifyList.Name = "comboBoxModifyList";
            this.comboBoxModifyList.Size = new System.Drawing.Size(512, 21);
            this.comboBoxModifyList.TabIndex = 4;
            this.comboBoxModifyList.SelectedIndexChanged += new System.EventHandler(this.comboBoxModifyList_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(530, 722);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(512, 44);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save modified image";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // Out
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 778);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.comboBoxModifyList);
            this.Controls.Add(this.groupBoxHistograms);
            this.Controls.Add(this.pictureBoxModified);
            this.Controls.Add(this.pictureBoxLoaded);
            this.Controls.Add(this.buttonLoad);
            this.Name = "Out";
            this.Text = "Out";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoaded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxModified)).EndInit();
            this.groupBoxHistograms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHistogramModified)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHistogramLoaded)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.PictureBox pictureBoxLoaded;
        private System.Windows.Forms.PictureBox pictureBoxModified;
        private System.Windows.Forms.GroupBox groupBoxHistograms;
        private System.Windows.Forms.PictureBox pictureBoxHistogramModified;
        private System.Windows.Forms.PictureBox pictureBoxHistogramLoaded;
        private System.Windows.Forms.ComboBox comboBoxModifyList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

        

    }
}