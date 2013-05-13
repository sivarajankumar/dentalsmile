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
using smileUp.Calendar;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for AppointmentForm.xaml
    /// </summary>
    public partial class AppointmentForm : Window
    {
        DentalSmileDB DB;
        public AppointmentForm()
        {
            InitializeComponent();
            DB = new DentalSmileDB();
            Loaded += new RoutedEventHandler(AppointmentForm_Loaded);
        }

        void AppointmentForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.patient != null)
            {
                Changed();
            }
        }

        private void Changed()
        {
            int month = AptCalendar.DisplayStartDate.Month;
            AptCalendar.Appointments = DB.findAppointmentsByPatient(App.patient.Id, month);
            AptCalendar.Changed();
        }

        private void AptCalendar_DisplayMonthChanged(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("AptCalendar_DisplayMonthChanged");
            MonthChangedEventArgs a= e as MonthChangedEventArgs;
            //AptCalendar.DisplayStartDate = a.NewDisplayStartDate;
            Changed();
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
