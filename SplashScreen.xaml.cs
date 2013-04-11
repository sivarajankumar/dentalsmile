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

            if (cont > 5)
            {
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
                _timer.Stop();
            }
        }

    }
}
