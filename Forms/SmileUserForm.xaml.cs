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
using smileUp.DataModel;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for SmileUserForm.xaml
    /// </summary>
    public partial class SmileUserForm : Window
    {
        DentalSmileDB DB;
        List<Dentist> dentist;

        public SmileUserForm()
        {
            InitializeComponent();
            DB = new DentalSmileDB();
            dentist = new List<Dentist>();

            LoadDentist();
        }

        private void LoadDentist()
        {
            dentist = DB.SelectAllDentists();
            cbDentist.ItemsSource = dentist;


            if (App.user.Admin)
            {
                cbDentist.IsEnabled = true;
            }
            else
            {
                cbDentist.IsEnabled = false;
                UserButton(true);
            }
            
            cbDentist.SelectedValue = App.user.Dentist.UserId;
        }

        private void UserButton(bool b)
        {
            btnDelete.IsEnabled = false;
            btnAddUser.IsEnabled = false;
            btnUpdate.IsEnabled = b;
        }

        private void AdminButton(bool b)
        {
            btnDelete.IsEnabled = b;
            btnUpdate.IsEnabled = b;
            btnAddUser.IsEnabled = !b;
        }

        private void cbDentist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dentist d = cbDentist.SelectedItem as Dentist;
            if (d != null)
            {
                if (d.IsUser)
                {
                    AdminButton(true);
                }
                else
                {
                    AdminButton(false);
                }
                txtFullname.Text = d.FullName;
            }
            else
            {
                txtFullname.Text = "";
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Dentist d = cbDentist.SelectedItem as Dentist;
            if (d == null)
            {
                MessageBox.Show("Please select a Dentist first.");
                return;
            }
            if (txtPasswd.Text.Equals(string.Empty))
            {
                MessageBox.Show("Type your password to updated.");
                return;
            }
            if (!txtPasswd.Text.Equals(txtConfirm.Text))
            {
                MessageBox.Show("Password didn't match. Please try again.");
                return;
            }

            SmileUser u = new SmileUser();
            u.UserId = d.UserId;
            u.Password = DB.CalculateMD5Hash(txtConfirm.Text);
            DB.InsertUser(u);            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Dentist d = cbDentist.SelectedItem as Dentist;
            if (d == null)
            {
                MessageBox.Show("Please select a Dentist first.");
                return;
            }

            
            if (txtPasswd.Text.Equals(string.Empty))
            {
                MessageBox.Show("Type your password to updated.");
                return;
            }
            if (!txtPasswd.Text.Equals(txtConfirm.Text))
            {
                MessageBox.Show("Password didn't match. Please try again.");
                return;
            }

            DB.SetPassword(DB.CalculateMD5Hash(txtConfirm.Text), d.UserId);
            MessageBox.Show("Password updated successfully.");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Dentist d = cbDentist.SelectedItem as Dentist;
            if (d == null)
            {
                MessageBox.Show("Please select a Dentist first.");
                return;
            }

            DB.DeleteUserOnly(d.UserId);

        }
    }
}
