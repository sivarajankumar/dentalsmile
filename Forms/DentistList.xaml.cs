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
    /// Interaction logic for DoctorList.xaml
    /// </summary>
    public partial class DentistList : Window
    {
        DentalSmileDB db;
        List<Dentist> dentists;
        public DentistList()
        {
            InitializeComponent();

            db = new DentalSmileDB();
            dentists = new List<Dentist>();
            
            dentists = db.SelectAllDentists();

            this.Loaded += new RoutedEventHandler(DentistList_Loaded);
        }

        void DentistList_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = dentists;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            string userid = null ;
            string fname = null ;
            string lname = null;

            if (useridChk.IsChecked.Value)
                userid = keywordTxt.Text.ToLower();
            if (fnameChk.IsChecked.Value)
                fname = keywordTxt.Text.ToLower();
            if (lnameChk.IsChecked.Value)
                lname = keywordTxt.Text.ToLower();
            dentists = db.findDentistsByOr(userid, fname, lname);
            
            DataContext = dentists;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            DentistForm d = new DentistForm();
            d.ShowDialog();
        }

        private void dentistDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("" + dentistDataGrid.SelectedValue);
        }


    }
}
