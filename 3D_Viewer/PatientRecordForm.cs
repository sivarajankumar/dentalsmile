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
    public partial class PatientRecordForm : Form
    {
        private DBConnect dbConnect;

        public PatientRecordForm()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
        }

        private void btn_SavePatient_Click(object sender, EventArgs e)
        {
           
           string name = txt_PatientName.Text;
           string pob = txt_PatientPlaceBirth.Text;
           DateTime dob = DateTime.Parse(dt_PatientDateBirth.Text);
           char sex = Convert.ToChar(ddl_PatientSex.SelectedValue);
           string phone =  txt_PatientPhone.Text;
           string address =  txt_PatientAddress.Text;

           dbConnect.Insert(name,dob,pob,sex,address, phone);
           //MessageBox.Show("Data Patient is saved!");
           DialogResult result = MessageBox.Show("Data Patient is saved successfully", "Continue to Scanning?",
           MessageBoxButtons.OKCancel);
           switch (result)
           {
               case DialogResult.OK:
                   {
                       //this.Text = "[OK]";
                       this.Close();
                       break;
                   }
               case DialogResult.Cancel:
                   {
                       this.Text = "[Cancel]";
                       break;
                   }
           }

           
  
        }

       
       
    }
}
