namespace smileUp
{
    partial class PatientRecordForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_PatientID = new System.Windows.Forms.TextBox();
            this.txt_PatientName = new System.Windows.Forms.TextBox();
            this.txt_PatientPlaceBirth = new System.Windows.Forms.TextBox();
            this.txt_PatientPhone = new System.Windows.Forms.TextBox();
            this.dt_PatientDateBirth = new System.Windows.Forms.DateTimePicker();
            this.ddl_PatientSex = new System.Windows.Forms.ComboBox();
            this.txt_PatientAddress = new System.Windows.Forms.TextBox();
            this.btn_SavePatient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Patient ID :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Place/Date of Birth :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sex :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Address :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Phone :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Patient Name :";
            // 
            // txt_PatientID
            // 
            this.txt_PatientID.Location = new System.Drawing.Point(132, 14);
            this.txt_PatientID.Name = "txt_PatientID";
            this.txt_PatientID.Size = new System.Drawing.Size(100, 20);
            this.txt_PatientID.TabIndex = 6;
            // 
            // txt_PatientName
            // 
            this.txt_PatientName.Location = new System.Drawing.Point(132, 40);
            this.txt_PatientName.Name = "txt_PatientName";
            this.txt_PatientName.Size = new System.Drawing.Size(100, 20);
            this.txt_PatientName.TabIndex = 7;
            // 
            // txt_PatientPlaceBirth
            // 
            this.txt_PatientPlaceBirth.Location = new System.Drawing.Point(132, 66);
            this.txt_PatientPlaceBirth.Name = "txt_PatientPlaceBirth";
            this.txt_PatientPlaceBirth.Size = new System.Drawing.Size(100, 20);
            this.txt_PatientPlaceBirth.TabIndex = 8;
            // 
            // txt_PatientPhone
            // 
            this.txt_PatientPhone.Location = new System.Drawing.Point(132, 151);
            this.txt_PatientPhone.Name = "txt_PatientPhone";
            this.txt_PatientPhone.Size = new System.Drawing.Size(100, 20);
            this.txt_PatientPhone.TabIndex = 9;
            // 
            // dt_PatientDateBirth
            // 
            this.dt_PatientDateBirth.Location = new System.Drawing.Point(240, 66);
            this.dt_PatientDateBirth.Name = "dt_PatientDateBirth";
            this.dt_PatientDateBirth.Size = new System.Drawing.Size(123, 20);
            this.dt_PatientDateBirth.TabIndex = 10;
            // 
            // ddl_PatientSex
            // 
            this.ddl_PatientSex.FormattingEnabled = true;
            this.ddl_PatientSex.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.ddl_PatientSex.Location = new System.Drawing.Point(132, 93);
            this.ddl_PatientSex.Name = "ddl_PatientSex";
            this.ddl_PatientSex.Size = new System.Drawing.Size(100, 21);
            this.ddl_PatientSex.TabIndex = 11;
            // 
            // txt_PatientAddress
            // 
            this.txt_PatientAddress.Location = new System.Drawing.Point(132, 121);
            this.txt_PatientAddress.Name = "txt_PatientAddress";
            this.txt_PatientAddress.Size = new System.Drawing.Size(100, 20);
            this.txt_PatientAddress.TabIndex = 12;
            // 
            // btn_SavePatient
            // 
            this.btn_SavePatient.Location = new System.Drawing.Point(132, 189);
            this.btn_SavePatient.Name = "btn_SavePatient";
            this.btn_SavePatient.Size = new System.Drawing.Size(75, 23);
            this.btn_SavePatient.TabIndex = 13;
            this.btn_SavePatient.Text = "Save";
            this.btn_SavePatient.UseVisualStyleBackColor = true;
            this.btn_SavePatient.Click += new System.EventHandler(this.btn_SavePatient_Click);
            // 
            // PatientRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(398, 252);
            this.Controls.Add(this.btn_SavePatient);
            this.Controls.Add(this.txt_PatientAddress);
            this.Controls.Add(this.ddl_PatientSex);
            this.Controls.Add(this.dt_PatientDateBirth);
            this.Controls.Add(this.txt_PatientPhone);
            this.Controls.Add(this.txt_PatientPlaceBirth);
            this.Controls.Add(this.txt_PatientName);
            this.Controls.Add(this.txt_PatientID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PatientRecordForm";
            this.Text = "PatientRecordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_PatientID;
        private System.Windows.Forms.TextBox txt_PatientName;
        private System.Windows.Forms.TextBox txt_PatientPlaceBirth;
        private System.Windows.Forms.TextBox txt_PatientPhone;
        private System.Windows.Forms.DateTimePicker dt_PatientDateBirth;
        private System.Windows.Forms.ComboBox ddl_PatientSex;
        private System.Windows.Forms.TextBox txt_PatientAddress;
        private System.Windows.Forms.Button btn_SavePatient;


        public System.EventHandler PatientRecordForm_Load { get; set; }
    }
}