using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace smileUp
{
    public partial class MeasurementForm : Form
    {
        public MeasurementForm()
        {
            InitializeComponent();
        }

        public void addRow(TeethVisual3D teeth, string type)
        {
            
            string teeth_name = teeth.Model.Id;
            double teeth_length = Math.Round(teeth.Model.Length, 2);

            if (type == "auto")
            { this.dgTMeasurement.Rows.Add(teeth.Model.TeethNumber,teeth_name, teeth_length); }
            else { this.dgTMeasurement.Rows.Add(teeth.Model.TeethNumber,teeth.Model.Id,teeth.Model.StartPosition,teeth.Model.EndPosition,teeth.Model.Length); }

            return;
        }
     

        private void MeasurementForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'orthoEntities.tooth_lookup' table. You can move, or remove it, as needed.
           // this.tooth_lookupTableAdapter.Fill(this.orthoEntities.tooth_lookup);
            // TODO: This line of code loads data into the 'orthoEntities.teeth_files' table. You can move, or remove it, as needed.
           // this.teeth_filesTableAdapter.Fill(this.orthoEntities.teeth_files);
        }

        private void btn_saveMeasurement_Click(object sender, EventArgs e)
        {
            //string patient_name = patient_name.ToString();

           //dbConnect.Insert(name, dob, pob, sex, address, phone);
            DialogResult result = MessageBox.Show("Data Measurement is saved successfully", "Continue?",
            MessageBoxButtons.OK);
            switch (result)
            {
                case DialogResult.OK:
                    {
                        this.Close();
                        break;
                    }
            }


        }

        public void AddColumnsManualMeasurement()
        {
            
            var col3 = new DataGridViewTextBoxColumn();
            var col4 = new DataGridViewTextBoxColumn();

            col3.HeaderText = "Start Position";
            col3.Name = "StartPosition";

            col4.HeaderText = "End Position";
            col4.Name = "EndPosition";

            dgTMeasurement.Columns.AddRange(new DataGridViewColumn[] { col3, col4 });
        }
    }
}
