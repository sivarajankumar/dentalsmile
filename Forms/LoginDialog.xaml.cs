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
using smileUp.ViewModels;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public LoginViewModel ViewModel;
        App app;

        public LoginDialog()
        {
            InitializeComponent();
            app = Application.Current as App;

            this.ViewModel = new LoginViewModel(this);
            this.DataContext = this.ViewModel;
        }

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            this.SmartLoginOverlayControl.Lock();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (App.user == null)
            {
                app.Shutdown();
            }
        }
    }
}
