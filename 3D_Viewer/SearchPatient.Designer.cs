namespace smileUp
{
    partial class SearchPatient
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_PatientID = new System.Windows.Forms.TextBox();
            this.txt_PatientName = new System.Windows.Forms.TextBox();
            this.ddl_Status = new System.Windows.Forms.ComboBox();
            this.Search = new System.Windows.Forms.Button();
            this.dgDisplay = new System.Windows.Forms.DataGridView();
            this.orthoprojectDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PatientId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Process = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orthoprojectDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Patient ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Patient Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Status";
            // 
            // txt_PatientID
            // 
            this.txt_PatientID.Location = new System.Drawing.Point(97, 13);
            this.txt_PatientID.Name = "txt_PatientID";
            this.txt_PatientID.Size = new System.Drawing.Size(150, 20);
            this.txt_PatientID.TabIndex = 3;
            // 
            // txt_PatientName
            // 
            this.txt_PatientName.Location = new System.Drawing.Point(97, 37);
            this.txt_PatientName.Name = "txt_PatientName";
            this.txt_PatientName.Size = new System.Drawing.Size(150, 20);
            this.txt_PatientName.TabIndex = 4;
            // 
            // ddl_Status
            // 
            this.ddl_Status.FormattingEnabled = true;
            this.ddl_Status.Items.AddRange(new object[] {
            "Recording",
            "Scanning",
            "Measuring",
            "Manipulating",
            "Treatment",
            "Closed"});
            this.ddl_Status.Location = new System.Drawing.Point(97, 64);
            this.ddl_Status.Name = "ddl_Status";
            this.ddl_Status.Size = new System.Drawing.Size(150, 21);
            this.ddl_Status.TabIndex = 5;
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(97, 108);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(75, 23);
            this.Search.TabIndex = 6;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // dgDisplay
            // 
            this.dgDisplay.AllowUserToOrderColumns = true;
            this.dgDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PatientId,
            this.PatientName,
            this.PatientStatus,
            this.DataAction,
            this.Process});
            this.dgDisplay.Location = new System.Drawing.Point(-2, 147);
            this.dgDisplay.Name = "dgDisplay";
            this.dgDisplay.Size = new System.Drawing.Size(547, 176);
            this.dgDisplay.TabIndex = 7;
            this.dgDisplay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDisplay_CellContentClick);
            // 
            // PatientId
            // 
            this.PatientId.HeaderText = "Patient Id";
            this.PatientId.Name = "PatientId";
            // 
            // PatientName
            // 
            this.PatientName.HeaderText = "Patient Name";
            this.PatientName.Name = "PatientName";
            // 
            // PatientStatus
            // 
            this.PatientStatus.HeaderText = "Patient Status";
            this.PatientStatus.Name = "PatientStatus";
            // 
            // DataAction
            // 
            this.DataAction.HeaderText = "Data Action";
            this.DataAction.Name = "DataAction";
            // 
            // Process
            // 
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            // 
            // SearchPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 309);
            this.Controls.Add(this.dgDisplay);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.ddl_Status);
            this.Controls.Add(this.txt_PatientName);
            this.Controls.Add(this.txt_PatientID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SearchPatient";
            this.Text = "SearchPatient";
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orthoprojectDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_PatientID;
        private System.Windows.Forms.TextBox txt_PatientName;
        private System.Windows.Forms.ComboBox ddl_Status;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.DataGridView dgDisplay;
        private System.Windows.Forms.BindingSource orthoprojectDataSetBindingSource;
        private orthoprojectDataSet orthoprojectDataSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientStatus;
        private System.Windows.Forms.DataGridViewButtonColumn DataAction;
        private System.Windows.Forms.DataGridViewButtonColumn Process;
    }
}