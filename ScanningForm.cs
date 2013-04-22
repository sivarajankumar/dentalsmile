using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;



namespace smileUp
{
    public partial class ScanningForm : Form
    {
        private DBConnect dbConnect;
        private MainViewModel vm;

        public ScanningForm()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Open Scaning File";
            fDialog.Filter = "Obj Files |*obj|Image Files|*.png";

            fDialog.InitialDirectory = @"D:\KULIAH TMDG\Thesis 2013- Ortho\References\3D models";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                txt_FileOpen.Text = fDialog.FileName.ToString();
                txt_FileResults.Text = fDialog.FileName.ToString();
                MessageBox.Show(fDialog.FileName.ToString());

            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            int patient_id = 2;
            int doctor_id = 1;
            string file_name = txt_FileOpen.Text;
            string file_desc = txt_FileResults.Text;

            if (file_name != "")
            {
                dbConnect.InsertTeethFiles(patient_id, file_name, file_desc);
                dbConnect.InsertPatientTreatment(patient_id, doctor_id, 1);

                DialogResult result = MessageBox.Show("Data File Scanning is saved successfully", "Do you want to save the result?",
            MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Obj Files|*.obj";
                    saveFileDialog1.ShowDialog();

                    string filename = System.IO.Path.GetFileName(saveFileDialog1.FileName);
                    string path = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);

                    if (filename != "")
                    { 
                        //savefile
                    }
                    else { return; }
                }
            }
            else { MessageBox.Show("There is no file to be saved"); return; }
        }
    }
}
