namespace Parsley {
  partial class ScanningSlide {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanningSlide));
        this._btn_take_texture_image = new Parsley.UI.ParsleyButtonSmall();
        this._btn_clear_points = new Parsley.UI.ParsleyButtonSmall();
        this._btn_save_points = new Parsley.UI.ParsleyButtonSmall();
        this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        this.btn_update_transformation = new Parsley.UI.ParsleyButtonSmall();
        this.StartScanning = new Parsley.UI.ParsleyButtonSmall();
        this._btn_load = new Parsley.UI.ParsleyButtonSmall();
        this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.SuspendLayout();
        // 
        // growLabel1
        // 
        this.growLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.growLabel1.ForeColor = System.Drawing.Color.DarkGray;
        this.growLabel1.Text = "3D Scanning";
        // 
        // _btn_take_texture_image
        // 
        this._btn_take_texture_image.BackColor = System.Drawing.Color.Transparent;
        this._btn_take_texture_image.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
        this._btn_take_texture_image.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this._btn_take_texture_image.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._btn_take_texture_image.ForeColor = System.Drawing.Color.Silver;
        this._btn_take_texture_image.Image = ((System.Drawing.Image)(resources.GetObject("_btn_take_texture_image.Image")));
        this._btn_take_texture_image.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this._btn_take_texture_image.Location = new System.Drawing.Point(4, 148);
        this._btn_take_texture_image.Name = "_btn_take_texture_image";
        this._btn_take_texture_image.Size = new System.Drawing.Size(142, 27);
        this._btn_take_texture_image.TabIndex = 14;
        this._btn_take_texture_image.Text = "Take Texture Image";
        this._btn_take_texture_image.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this._btn_take_texture_image.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this._btn_take_texture_image.UseVisualStyleBackColor = false;
        this._btn_take_texture_image.Click += new System.EventHandler(this._btn_take_texture_image_Click);
        // 
        // _btn_clear_points
        // 
        this._btn_clear_points.BackColor = System.Drawing.Color.Transparent;
        this._btn_clear_points.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
        this._btn_clear_points.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this._btn_clear_points.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._btn_clear_points.ForeColor = System.Drawing.Color.Silver;
        this._btn_clear_points.Image = ((System.Drawing.Image)(resources.GetObject("_btn_clear_points.Image")));
        this._btn_clear_points.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this._btn_clear_points.Location = new System.Drawing.Point(4, 181);
        this._btn_clear_points.Name = "_btn_clear_points";
        this._btn_clear_points.Size = new System.Drawing.Size(142, 27);
        this._btn_clear_points.TabIndex = 15;
        this._btn_clear_points.Text = "Clear Points";
        this._btn_clear_points.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this._btn_clear_points.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this._btn_clear_points.UseVisualStyleBackColor = false;
        this._btn_clear_points.Click += new System.EventHandler(this._btn_clear_points_Click);
        // 
        // _btn_save_points
        // 
        this._btn_save_points.BackColor = System.Drawing.Color.Transparent;
        this._btn_save_points.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
        this._btn_save_points.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this._btn_save_points.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._btn_save_points.ForeColor = System.Drawing.Color.Silver;
        this._btn_save_points.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save_points.Image")));
        this._btn_save_points.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this._btn_save_points.Location = new System.Drawing.Point(4, 214);
        this._btn_save_points.Name = "_btn_save_points";
        this._btn_save_points.Size = new System.Drawing.Size(142, 27);
        this._btn_save_points.TabIndex = 16;
        this._btn_save_points.Text = "Save Points";
        this._btn_save_points.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this._btn_save_points.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this._btn_save_points.UseVisualStyleBackColor = false;
        this._btn_save_points.Click += new System.EventHandler(this._btn_save_points_Click);
        // 
        // saveFileDialog1
        // 
        this.saveFileDialog1.Filter = "CSV files|*.csv";
        this.saveFileDialog1.Title = "Select CSV File Destination";
        this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
        // 
        // btn_update_transformation
        // 
        this.btn_update_transformation.BackColor = System.Drawing.Color.Transparent;
        this.btn_update_transformation.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
        this.btn_update_transformation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btn_update_transformation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btn_update_transformation.ForeColor = System.Drawing.Color.Silver;
        this.btn_update_transformation.Image = ((System.Drawing.Image)(resources.GetObject("btn_update_transformation.Image")));
        this.btn_update_transformation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btn_update_transformation.Location = new System.Drawing.Point(4, 102);
        this.btn_update_transformation.Name = "btn_update_transformation";
        this.btn_update_transformation.Size = new System.Drawing.Size(142, 40);
        this.btn_update_transformation.TabIndex = 17;
        this.btn_update_transformation.Text = "Update Positioner Transformation";
        this.btn_update_transformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btn_update_transformation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this.btn_update_transformation.UseVisualStyleBackColor = false;
        this.btn_update_transformation.Click += new System.EventHandler(this.btn_update_transformation_Click);
        // 
        // StartScanning
        // 
        this.StartScanning.BackColor = System.Drawing.Color.Transparent;
        this.StartScanning.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
        this.StartScanning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.StartScanning.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.StartScanning.ForeColor = System.Drawing.Color.Silver;
        this.StartScanning.Image = ((System.Drawing.Image)(resources.GetObject("StartScanning.Image")));
        this.StartScanning.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.StartScanning.Location = new System.Drawing.Point(4, 304);
        this.StartScanning.Name = "StartScanning";
        this.StartScanning.Size = new System.Drawing.Size(142, 27);
        this.StartScanning.TabIndex = 18;
        this.StartScanning.Text = "Start Scanning";
        this.StartScanning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.StartScanning.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this.StartScanning.UseVisualStyleBackColor = false;
        this.StartScanning.Click += new System.EventHandler(this.StartScanning_Click);
        // 
        // _btn_load
        // 
        this._btn_load.BackColor = System.Drawing.Color.Transparent;
        this._btn_load.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
        this._btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this._btn_load.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this._btn_load.ForeColor = System.Drawing.Color.Silver;
        this._btn_load.Image = ((System.Drawing.Image)(resources.GetObject("_btn_load.Image")));
        this._btn_load.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this._btn_load.Location = new System.Drawing.Point(4, 257);
        this._btn_load.Name = "_btn_load";
        this._btn_load.Size = new System.Drawing.Size(142, 27);
        this._btn_load.TabIndex = 19;
        this._btn_load.Text = "Load Points";
        this._btn_load.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this._btn_load.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this._btn_load.UseVisualStyleBackColor = false;
        this._btn_load.Click += new System.EventHandler(this._btn_load_Click);
        // 
        // openFileDialog1
        // 
        this.openFileDialog1.FileName = "openFileDialog1";
        this.openFileDialog1.Filter = "point cloud|*.csv";
        // 
        // ScanningSlide
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.Controls.Add(this._btn_load);
        this.Controls.Add(this.btn_update_transformation);
        this.Controls.Add(this.StartScanning);
        this.Controls.Add(this._btn_save_points);
        this.Controls.Add(this._btn_clear_points);
        this.Controls.Add(this._btn_take_texture_image);
        this.Name = "ScanningSlide";
        this.Size = new System.Drawing.Size(550, 435);
        this.Controls.SetChildIndex(this._btn_take_texture_image, 0);
        this.Controls.SetChildIndex(this._btn_clear_points, 0);
        this.Controls.SetChildIndex(this._btn_save_points, 0);
        this.Controls.SetChildIndex(this.StartScanning, 0);
        this.Controls.SetChildIndex(this.btn_update_transformation, 0);
        this.Controls.SetChildIndex(this.growLabel1, 0);
        this.Controls.SetChildIndex(this._btn_load, 0);
        this.ResumeLayout(false);

    }

    #endregion

    private Parsley.UI.ParsleyButtonSmall _btn_take_texture_image;
    private Parsley.UI.ParsleyButtonSmall _btn_clear_points;
    private Parsley.UI.ParsleyButtonSmall _btn_save_points;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private Parsley.UI.ParsleyButtonSmall btn_update_transformation;
    private Parsley.UI.ParsleyButtonSmall StartScanning;
    private Parsley.UI.ParsleyButtonSmall _btn_load;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
  }
}
