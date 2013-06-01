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
using System.Windows.Threading;
using smileUp.Forms;

namespace smileUp
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        DispatcherTimer _timer = new DispatcherTimer();
        int cont = 0;

        public SplashScreen()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(OnLoaded);
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            _timer.Interval = new System.TimeSpan(0, 0, 0, 0, 1000);
            _timer.Tick += new System.EventHandler(OnTimerEvent);

            _timer.Start();

        }

        void OnTimerEvent(object sender, System.EventArgs args)
        {
            cont = cont + 1;

            if (cont == 1)
            {
                LoadSettings();
            }

            if (cont > 2)
            {
                //MainWindow m = new MainWindow();
                //m.Show();
                Dashboard d = new Dashboard();
                d.Show();
                this.Close();
                _timer.Stop();
            }
        }

        private void LoadSettings()
        {
            //Smile.DbDatabase = Properties.Settings.Default.DbDatabase = "xx";
            //Smile.INSTALL = Properties.Settings.Default.InstallationMode = true;

            DentalSmileDB DB = DentalSmileDBFactory.GetInstance();
            ///TODO
            //test DB connection, if fail changes configuration
            //if (true)
            if (Smile.INSTALL)
            {
                SettingsDB dbForm = new SettingsDB();
                dbForm.ShowDialog();
                DB = DentalSmileDBFactory.GetInstance();
            }

            //detect if Admin  is NULL
            if (DB.selectDefaultAdmin() == null)
            {
                AdminPasswordDialog dlg = new AdminPasswordDialog();
                dlg.ShowDialog();
            }
        }

    }
}
