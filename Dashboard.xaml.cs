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
        public Dashboard()
        {
            InitializeComponent();
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
    }
}
