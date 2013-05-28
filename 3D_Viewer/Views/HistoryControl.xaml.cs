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

namespace smileUp.Views
{
    /// <summary>
    /// Interaction logic for HistoryControl.xaml
    /// </summary>
    public partial class HistoryControl : UserControl
    {
        DentalSmileDB DB;
        List<Treatment> Treatments;
        App app;

        public HistoryControl()
        {
            InitializeComponent();
            DB = DentalSmileDBFactory.GetInstance();
            app = Application.Current as App;

            if (App.patient != null)
            {
                Treatments = DB.findTreatmentsByPatientId(App.patient.Id);
            }
            else
            {
                Treatments = DB.findTreatments();
            }

            DataContext = Treatments;
            navigateButton(Smile.REGISTERED);
        }



        private void btnStartManipulation_Click(object sender, RoutedEventArgs e)
        {
            if (filesDataGrid.SelectedItem != null)
            {
                Treatment t = treatmentsDataGrid.SelectedItem as Treatment;
                SmileFile file = filesDataGrid.SelectedItem as SmileFile;

                App.patient = t.Patient;

                MainWindow m = new MainWindow(t, file, true);
                m.ShowDialog();
                //this.Close();
            }
        }

        private void treatmentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Treatment t = treatmentsDataGrid.SelectedItem as Treatment;
            if (t != null) filesDataGrid.DataContext = t.Files;
            navigateButton(Smile.REGISTERED);
        }

        private void filesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SmileFile f = filesDataGrid.SelectedItem as SmileFile;
            if (f != null) navigateButton(f.Type);
        }

        private void navigateButton(int type)
        {
            if (type == Smile.REGISTERED)
            {
                btnNewScan.IsEnabled = true;
                btnStartManipulation.IsEnabled = false;
                btnContinueManipulation.IsEnabled = false;
            }
            else if (type == Smile.SCANNING)
            {
                btnNewScan.IsEnabled = true;
                btnStartManipulation.IsEnabled = true;
                btnContinueManipulation.IsEnabled = false;
            }
            else if (type == Smile.MANIPULATION)
            {
                btnNewScan.IsEnabled = true;
                btnStartManipulation.IsEnabled = true;
                btnContinueManipulation.IsEnabled = true;
            }
            else
            {
                btnNewScan.IsEnabled = false;
                btnStartManipulation.IsEnabled = false;
                btnContinueManipulation.IsEnabled = false;
            }
        }

        private void btnNewScan_Click(object sender, RoutedEventArgs e)
        {
            ScanningForm s = new ScanningForm();
            s.Show();
            s.Close();
        }

        private void btnContinueManipulation_Click(object sender, RoutedEventArgs e)
        {
            if (filesDataGrid.SelectedItem != null)
            {
                Treatment t = treatmentsDataGrid.SelectedItem as Treatment;
                SmileFile file = filesDataGrid.SelectedItem as SmileFile;
                App.patient = t.Patient;
                MainWindow m = new MainWindow(t, file, true);
                m.Show();
                //this.Close();
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //TreatmentForm f = new TreatmentForm();
            //f.ShowDialog();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            string id = null;
            string tdateStr = null;
            string patient = null;
            string dentist = null;

            if (idChk.IsChecked.Value)
                id = keywordTxt.Text.ToLower();
            if (tDateChk.IsChecked.Value)
                tdateStr = keywordTxt.Text.ToLower();
            if (patientChk.IsChecked.Value)
                patient = keywordTxt.Text.ToLower();
            if (dentistChk.IsChecked.Value)
                dentist = keywordTxt.Text.ToLower();

            DateTime tdate = DateTime.Now;
            bool err = false;
            try
            {
                tdate = DateTime.Parse(tdateStr);
            }
            catch (Exception ex) { err = true; }

            Treatments = DB.findTreatmentsByOr(id, (err ? null : tdateStr), patient, dentist);

            DataContext = Treatments;
        }
    }
}
