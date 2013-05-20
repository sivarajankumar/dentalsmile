using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using smileUp.DataModel;
using System.Data;

namespace smileUp
{
    public partial class PatientForm : Form
    {
        private DentalSmileDB dbConnect;

        public PatientForm()
        {
            InitializeComponent();
            dbConnect = new DentalSmileDB();
            
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            
            Patient p = new Patient(); 

            p.Id= txt_id.Text;
            p.Name = txt_fname.Text;
            p.LastName = txt_lname.Text;
            p.BirthDate = DateTime.Parse(dt_birthdate.Text);
            p.BirthPlace= txt_birthplace.Text;
            p.Gender = "";
            p.Address1 = txt_address1.Text;
            p.Address2 = txt_address2.Text;
            p.City= txt_city.Text;
            p.Phone = txt_phone.Text;
            // created;
            //string createdBy;

            dbConnect.InsertPatient(p);

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
