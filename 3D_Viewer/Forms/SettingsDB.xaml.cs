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
using System.IO;
using System.Windows.Forms;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsDB : Window
    {
        public SettingsDB()
        {
            InitializeComponent();
            cbDbType.ItemsSource = getDbItems();

            txtHostname.Text = Smile.DbHost;
            txtPort.Text = Smile.DbPort;
            txtUserId.Text = Smile.DbUserId;
            txtPassword.Text = Smile.DbPassword;
            txtDatabase.Text = Smile.DbDatabase;
            cbDbType.SelectedItem = Smile.DbType;
        }
        private List<string> getDbItems()
        {
            List<string> dbitems = new List<string>();
                dbitems.Add("MySQL");
                //dbitems.Add("SQLLite");
                dbitems.Add("Oracle");
            return dbitems;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //TODO: save to dictionary
            Smile.DbHost = Properties.Settings.Default.DbHost = txtHostname.Text;
            Smile.DbPort = Properties.Settings.Default.DbPort = txtPort.Text;
            Smile.DbUserId = Properties.Settings.Default.DbUserId = txtUserId.Text;
            Smile.DbPassword = Properties.Settings.Default.DbPassword = txtPassword.Text;
            Smile.DbDatabase = Properties.Settings.Default.DbDatabase = txtDatabase.Text;
            Smile.DbType = Properties.Settings.Default.DbType = cbDbType.SelectedValue.ToString();
            Smile.INSTALL= Properties.Settings.Default.InstallationMode = false;
            //System.Windows.MessageBox.Show(cbDbType.SelectedItem + "-" + cbDbType.SelectedValue+ " -"+Smile.DbType);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            DentalSmileDBFactory.ReInstantiate();
            System.Windows.MessageBox.Show("Setting saved successfully. Do you want to close this dialog?", "Configuration Changed");
            this.Close();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reload();
        }
    }
}
