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
using smileUp.Forms;

namespace smileUp
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        App app;
        DentalSmileDB DB;

        public Dashboard()
        {
            InitializeComponent();
            LoadSettings();
            
            //detect if Admin  is NULL
            if (DB.selectDefaultAdmin() == null)
            {
                AdminPasswordDialog dlg = new AdminPasswordDialog();
                dlg.ShowDialog();
            }

            if (App.user == null)
            {
                //ChangePasswordDialog dlg = new ChangePasswordDialog();
                LoginDialog dlg = new LoginDialog();
                dlg.ShowDialog();
            }
        }

        private void LoadSettings()
        {
            app = Application.Current as App;
            DB = new DentalSmileDB();
            ///TODO
        }

        private void doctorBtn_Click(object sender, RoutedEventArgs e)
        {
            //DentistForm df = new DentistForm();
            DentistList df = new DentistList();
            df.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DentistForm df = new DentistForm();
            df.ShowDialog();

        }

        private void btnTreatmentList_Click(object sender, RoutedEventArgs e)
        {
            TreatmentList list = new TreatmentList();
            list.ShowDialog();
        }

        private void btnTreatmentAdd_Click(object sender, RoutedEventArgs e)
        {
            TreatmentForm form = new TreatmentForm();
            form.ShowDialog();
        }


        private void btnPatientAdd_Click(object sender, RoutedEventArgs e)
        {
            PatientForm form = new PatientForm();
            form.ShowDialog();
        } 

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsForm s = new SettingsForm();
            s.ShowDialog();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            app.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Title = DateTime.Now.ToString(Smile.LONG_DATE_FORMAT);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginDialog dlg = new LoginDialog();
            dlg.ShowDialog();
        }
    }
}
