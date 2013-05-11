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
    /// Interaction logic for AppointmentForm.xaml
    /// </summary>
    public partial class AppointmentForm : Window
    {
        public AppointmentForm()
        {
            InitializeComponent();
        }

        private void AptCalendar_DisplayMonthChanged(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AptCalendar_DisplayMonthChanged");
        }

        private void AptCalendar_DayBoxDoubleClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AptCalendar_DayBoxDoubleClicked");

        }

        private void AptCalendar_AppointmentDblClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AptCalendar_AppointmentDblClicked");

        }
    }
}
