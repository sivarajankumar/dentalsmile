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
using smileUp.Calendar;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for AppointmentEditForm.xaml
    /// </summary>
    public partial class AppointmentEditForm : Window
    {
        List<Dentist> dentists;
        DentalSmileDB DB;

        public AppointmentEditForm()
        {
            InitializeComponent();
            DB = new DentalSmileDB();
            
            dentists = DB.SelectAllDentists();
            cbDentist.ItemsSource = dentists;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Appointment ap = DataContext as Appointment;
            if (ap == null || ap.Id == 0)
            {
                //NEW DATA
                ap = new Appointment();
                ap.Subject = txtSubject.Text;
                ap.Notes = txtNotes.Text;
                ap.ApDate = dtAppDate.DisplayDate.Date;
                ap.Aptime = txtTime.Text;
                Dentist dent = cbDentist.SelectedItem as Dentist;
                ap.Dentist = dent;
                ap.Patient = App.patient;
                ap.Room = (string)cbRoom.SelectedValue;
                DB.insertAppointment(ap);
            }
            else
            {
                ap.Subject = txtSubject.Text;
                ap.Notes = txtNotes.Text;
                ap.ApDate = dtAppDate.DisplayDate.Date;
                ap.Aptime = txtTime.Text;
                Dentist dent = cbDentist.SelectedItem as Dentist;
                ap.Dentist = dent;
                ap.Patient = App.patient;
                ap.Room = (string)cbRoom.SelectedValue;
                DB.updateAppointment(ap);
            }
            this.Close();
        }


    }
}
