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
    /// Interaction logic for ManipulationWindow.xaml
    /// </summary>
    public partial class ManipulationWindow : Window
    {
        public ManipulationWindow()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //save the manipulation file to disk
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //show patient form and select a patient to app.pasient
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            //finish manipulation file and update the db
        }
    }
}
