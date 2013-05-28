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
using System.Windows.Navigation;
using System.Windows.Shapes;
using smileUp.DataModel;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for DentistForm.xaml
    /// </summary>
    public partial class DentistForm : Window
    {
        DentalSmileDB db;
        Dentist doctor;

        public DentistForm()
        {
            InitializeComponent();
             
            db = DentalSmileDBFactory.GetInstance();
            DataContext = doctor;
        }

        void SetDoctor(Dentist d)
        {
            this.doctor = d;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dentist d = new Dentist();
            d.UserId = useridTextBox.Text.ToString();
            d.FirstName = fnameTextBox.Text.ToString();
            d.LastName = lnameTextBox.Text.ToString();
            d.BirthDate = birthdateDatePicker.DisplayDate;
            d.BirthPlace= birthplaceTextBox.Text.ToString();
            d.Gender = (genderMaleRadioButton.IsChecked.Value ? "M" : "F");
            d.Address1 = address1TextBox.Text.ToString();
            d.Address2 = address2TextBox.Text.ToString();
            d.City = cityTextBox.Text.ToString();
            d.Phone = phoneTextBox.Text.ToString();

            if (db.InsertDentist(d))
            {
                MessageBox.Show("Success inserted");
                clear();
            }
        }

        private void clear()
        {
            useridTextBox.Text = "";
            fnameTextBox.Text = "";
            lnameTextBox.Text = "";
            birthdateDatePicker.Text = "";
            birthplaceTextBox.Text = "";
            genderMaleRadioButton.IsChecked = false;
            genderFemaleRadioButton.IsChecked = false;
            address1TextBox.Text = "";
            address2TextBox.Text = "";
            cityTextBox.Text = "";
            phoneTextBox.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clear();
        }
    }
}
