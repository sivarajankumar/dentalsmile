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
using smileUp.DataModel;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for DentistForm.xaml
    /// </summary>
    public partial class TreatmentForm : Window
    {
        DentalSmileDB db;
        List<Phase> phases;

        public TreatmentForm()
        {
            InitializeComponent();

            db = new DentalSmileDB();
            phases = Smile.Phases = db.SelectAllPhases();
            //phases = Smile.GetPhases();
            //phases.Remove(Smile.GetPhase(Smile.SCANNING));
            //phases.Remove(Smile.GetPhase(Smile.MANIPULATION));

            phaseCombo.ItemsSource = phases;

            roomTextBox.Text = Smile.Room;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (phaseCombo.SelectedValue == null)
            {
                Treatment d = new Treatment();
                d.Phase = phases.ElementAt(phaseCombo.SelectedIndex);
                d.Patient = App.patient;
                d.Dentist = App.user.Dentist;
                d.Room = roomTextBox.Text.ToLower();
                d.TreatmentDate = DateTime.Now;
                d.TreatmentTime = DateTime.Now.ToString(Smile.TIME_FORMAT);

                if (db.InsertTreatment(d))
                {
                    MessageBox.Show("Success inserted");
                    clear();
                }
            }
        }

        private void clear()
        {
            phaseCombo.SelectedIndex = 0;
            roomTextBox.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clear();
        }
    }
}
