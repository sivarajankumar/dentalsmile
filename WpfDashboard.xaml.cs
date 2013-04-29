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

namespace smileUp
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class WpfDashboard : Window
    {
        public WpfDashboard()
        {
            InitializeComponent();
            
            Patient patient = new Patient();
            DataContext = patient;

            ApplySkinFromMenuItem(".\\Resources\\Skins\\BlackSkin.xaml");
        }

        void ApplySkinFromMenuItem(String skinDictPath)
        {
            // Get a relative path to the ResourceDictionary which
            // contains the selected skin.
            Uri skinDictUri = new Uri(skinDictPath, UriKind.Relative);

            // Tell the Application to load the skin resources.
            App app = Application.Current as App;
            app.ApplySkin(skinDictUri);
        }

    }
}
