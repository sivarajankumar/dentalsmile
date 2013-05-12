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
using System.Windows.Media.Animation;

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
            
            if (App.user != null && App.user.Admin == true)
            {
                menuDashboard.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                menuDashboard.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void LoadSettings()
        {
            app = Application.Current as App;
            DB = new DentalSmileDB();

                //if (Smile.INSTALL)
            if(!DB.TestConnectionString())
            {
                SettingsDB dbForm = new SettingsDB();
                dbForm.ShowDialog();
                DB = new DentalSmileDB();
            }

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

        private void WelcomeUserControl_NewButtonClick(object sender, RoutedEventArgs e)
        {
        }

        private void WelcomeUserControl_OpenButtonClick(object sender, RoutedEventArgs e)
        {
        }

        private void WelcomeUserControl_ImportButtonClick(object sender, RoutedEventArgs e)
        {
        }

        private void WelcomeUserControl_OpenRecentFileButtonClick(object sender, RoutedEventArgs e)
        {
        }


        private void HistoryControl_HistoryDataClick(object sender, RoutedEventArgs e)
        {
            //HistoryControl.Refresh();

            ((Storyboard)this.Resources["ShowHistoryData"]).Begin(this);
        }

        private void ShowHistoryData_StoryboardCompleted(object sender, EventArgs e)
        {
            //FirstNameEditTextBox.Focus();
        }

        private void Dentist_Click(object sender, RoutedEventArgs e)
        {
            DentistList f = new DentistList();
            f.ShowDialog();
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsForm f = new SettingsForm();
            f.ShowDialog();
        }
        private void DBSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsDB f = new SettingsDB();
            f.ShowDialog();
        }

        private void ChangeSkin(object sender, ExecutedRoutedEventArgs e)
        {
            ResourceDictionary rd = new ResourceDictionary();
            rd.MergedDictionaries.Add(Application.LoadComponent(new Uri(e.Parameter as string, UriKind.Relative)) as ResourceDictionary);
            Application.Current.Resources = rd;

            // save the skin setting
            Smile.Skin = Properties.Settings.Default.Skin = e.Parameter as string;
            Properties.Settings.Default.Save();            
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            SmileUserForm f = new SmileUserForm();
            f.ShowDialog();
        }
        
    }
}
