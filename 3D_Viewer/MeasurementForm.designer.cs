namespace smileUp
{
    partial class MeasurementForm
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
            this.components = new System.ComponentModel.Container();
            this.dgTMeasurement = new System.Windows.Forms.DataGridView();
            this.IdTeeth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeethName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeethLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toothlookupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.orthoEntitiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.orthoEntities = new smileUp.orthoprojectDataSet();
            this.btn_saveMeasurement = new System.Windows.Forms.Button();
            this.teethfilesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.teeth_filesTableAdapter = new smileUp.orthoprojectDataSetTableAdapters.teeth_filesTableAdapter();
            this.tooth_lookupTableAdapter = new smileUp.orthoprojectDataSetTableAdapters.tooth_lookupTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgTMeasurement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toothlookupBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orthoEntitiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orthoEntities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teethfilesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTMeasurement
            // 
            this.dgTMeasurement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgTMeasurement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTMeasurement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdTeeth,
            this.TeethName,
            this.TeethLength});
            this.dgTMeasurement.Location = new System.Drawing.Point(0, 32);
            this.dgTMeasurement.Name = "dgTMeasurement";
            this.dgTMeasurement.Size = new System.Drawing.Size(347, 150);
            this.dgTMeasurement.TabIndex = 0;
            // 
            // IdTeeth
            // 
            this.IdTeeth.HeaderText = "Teeth Id";
            this.IdTeeth.Name = "IdTeeth";
            // 
            // TeethName
            // 
            this.TeethName.HeaderText = "Teeth Name";
            this.TeethName.Name = "TeethName";
            // 
            // TeethLength
            // 
            this.TeethLength.HeaderText = "Length(mm)";
            this.TeethLength.Name = "TeethLength";
            // 
            // toothlookupBindingSource
            // 
            this.toothlookupBindingSource.DataMember = "tooth_lookup";
            this.toothlookupBindingSource.DataSource = this.orthoEntitiesBindingSource;
            // 
            // orthoEntitiesBindingSource
            // 
            this.orthoEntitiesBindingSource.DataSource = this.orthoEntities;
            this.orthoEntitiesBindingSource.Position = 0;
            // 
            // orthoEntities
            // 
            this.orthoEntities.DataSetName = "OrthoEntities";
            this.orthoEntities.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btn_saveMeasurement
            // 
            this.btn_saveMeasurement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_saveMeasurement.Location = new System.Drawing.Point(0, 0);
            this.btn_saveMeasurement.Name = "btn_saveMeasurement";
            this.btn_saveMeasurement.Size = new System.Drawing.Size(350, 23);
            this.btn_saveMeasurement.TabIndex = 1;
            this.btn_saveMeasurement.Text = "Save";
            this.btn_saveMeasurement.UseVisualStyleBackColor = true;
            this.btn_saveMeasurement.Click += new System.EventHandler(this.btn_saveMeasurement_Click);
            // 
            // teethfilesBindingSource
            // 
            this.teethfilesBindingSource.DataMember = "teeth_files";
            this.teethfilesBindingSource.DataSource = this.orthoEntitiesBindingSource;
            // 
            // teeth_filesTableAdapter
            // 
            this.teeth_filesTableAdapter.ClearBeforeFill = true;
            // 
            // tooth_lookupTableAdapter
            // 
            this.tooth_lookupTableAdapter.ClearBeforeFill = true;
            // 
            // MeasurementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 274);
            this.Controls.Add(this.btn_saveMeasurement);
            this.Controls.Add(this.dgTMeasurement);
            this.Name = "MeasurementForm";
            this.Text = "MeasurementForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MeasurementForm_FormClosed);
            this.Load += new System.EventHandler(this.MeasurementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTMeasurement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toothlookupBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orthoEntitiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orthoEntities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teethfilesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTMeasurement;
        private System.Windows.Forms.BindingSource orthoEntitiesBindingSource;
        private orthoprojectDataSet orthoEntities;
        private System.Windows.Forms.Button btn_saveMeasurement;
        private System.Windows.Forms.BindingSource teethfilesBindingSource;
        private orthoprojectDataSetTableAdapters.teeth_filesTableAdapter teeth_filesTableAdapter;
        private System.Windows.Forms.BindingSource toothlookupBindingSource;
        private orthoprojectDataSetTableAdapters.tooth_lookupTableAdapter tooth_lookupTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTeeth;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeethName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeethLength;
    }
}