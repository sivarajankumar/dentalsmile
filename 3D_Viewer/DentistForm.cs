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
    public partial class DentistForm : Form
    {
        private DentalSmileDB dbConnect;

        public DentistForm()
        {
            InitializeComponent();
            dbConnect = new DentalSmileDB();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            string userid = txt_userid.Text;
            string fname = txt_fname.Text;
            string lname = txt_lname.Text;
            DateTime birthdate = DateTime.Parse(dt_birthdate.Text);
            string birthplace = txt_birthplace.Text;
            string gender;
            string address1 = txt_address1.Text;
            string address2 = txt_address2.Text;
            string city = txt_city.Text;
            string phone = txt_phone.Text;
            string created;
            string createdBy;

            dbConnect.InsertDentist(userid, fname, lname, birthdate, birthplace, address1, address2, city, phone);

            //MessageBox.Show("Data Patient is saved!");
            DialogResult result = MessageBox.Show("Data Dentist is saved successfully", "Continue to Scanning?",
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
