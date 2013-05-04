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
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : Window
    {
        DentalSmileDB DB;
        public ChangePasswordDialog()
        {
            InitializeComponent();

            DB = new DentalSmileDB();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            txtOldPassword.Text =  DB.CalculateMD5Hash(txtConfirmNewPassword.Text);
        }
    }
}
