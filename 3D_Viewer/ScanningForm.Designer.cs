namespace smileUp
{
    partial class ScanningForm
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
            this.txt_FileOpen = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenFile = new System.Windows.Forms.Button();
            this.txt_FileResults = new System.Windows.Forms.TextBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // txt_FileOpen
            // 
            this.txt_FileOpen.Location = new System.Drawing.Point(13, 13);
            this.txt_FileOpen.Name = "txt_FileOpen";
            this.txt_FileOpen.Size = new System.Drawing.Size(129, 20);
            this.txt_FileOpen.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OpenFile
            // 
            this.OpenFile.Location = new System.Drawing.Point(149, 13);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(75, 23);
            this.OpenFile.TabIndex = 1;
            this.OpenFile.Text = "Open File";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // txt_FileResults
            // 
            this.txt_FileResults.Location = new System.Drawing.Point(13, 40);
            this.txt_FileResults.Name = "txt_FileResults";
            this.txt_FileResults.Size = new System.Drawing.Size(211, 20);
            this.txt_FileResults.TabIndex = 2;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(231, 36);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "Save File";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // ScanningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 219);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txt_FileResults);
            this.Controls.Add(this.OpenFile);
            this.Controls.Add(this.txt_FileOpen);
            this.Name = "ScanningForm";
            this.Text = "ScanningForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_FileOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.TextBox txt_FileResults;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}