using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for AdminPasswordDialog.xaml
    /// </summary>
    public partial class AdminPasswordDialog : Window
    {
        DentalSmileDB DB;
        public AdminPasswordDialog()
        {
            InitializeComponent();
            DB = new DentalSmileDB();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (txtConfirmPasswd.Password != null && txtPasswd.Password.Equals(txtConfirmPasswd.Password))
            {
                //if admin is NULL
                if (DB.selectDefaultAdmin() == null)
                {
                    //insert
                    DB.InsertDefaultAdmin(txtConfirmPasswd.Password);
                }
                else
                {
                    //update
                    DB.updateAdmin(txtConfirmPasswd.Password);
                }
                MessageBox.Show("Successfully changes.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Password is Invalid.");
                txtPasswd.Focus();
            }
        }
    }
}
