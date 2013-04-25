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
    public partial class SearchPatient : Form
    {
        private DBConnect dbConnect;

        public SearchPatient()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
        }

        private void Search_Click(object sender, EventArgs e)
        {
             
            List<string>[] list;
            list = dbConnect.Select();

           // dgDisplay.Rows.Clear();

            for(int i = 0; i < list[0].Count; i++)
            {
                int number = dgDisplay.Rows.Add();
                dgDisplay.Rows[number].Cells[0].Value = list[0][i];
                dgDisplay.Rows[number].Cells[1].Value = list[1][i];
                dgDisplay.Rows[number].Cells[2].Value = list[2][i];
                dgDisplay.Rows[number].Cells[3].Value = "View";
                dgDisplay.Rows[number].Cells[3].Value = "Scan"; 
            }
        }

        private void dgDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = (string)dgDisplay.CurrentRow.Cells[1].Value.ToString();
            Patient patient = new Patient();
            patient.Name = name;
            ScanningForm scanform = new ScanningForm();
            scanform.Show();

            this.Close();



        }

    }
}
