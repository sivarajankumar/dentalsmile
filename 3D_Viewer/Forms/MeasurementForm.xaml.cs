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

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for MeasurementForm.xaml
    /// </summary>
    public partial class MeasurementForm : Window
    {
        Measurement measurement;
        List<MeasurementTeeth> tooth;
        DentalSmileDB DB;
        App app;

        public MeasurementForm(Treatment treatment, SmileFile file)
        {
            InitializeComponent();
            app = Application.Current as App;
            tooth = new List<MeasurementTeeth>();
            DB = new DentalSmileDB();
            
            //TODO: DB.User = app.user.UserId;

            measurement = new Measurement();
            measurement.Treatment = treatment.Id;
            measurement.Patient = treatment.Patient.Id;
            measurement.Pfile = file.Id;
        }

        public void addRowTeeth(TeethVisual3D teeth, string type)
        {
            measurement.Type = Smile.TEETH;

            string spoint = teeth.Model.StartPosition;
            string epoint = teeth.Model.EndPosition;
            MeasurementTeeth m = new MeasurementTeeth(teeth.Id, teeth.Model.Length, spoint, epoint,type);
            tooth.Add(m);
            resultDataGrid.ItemsSource = null;
            resultDataGrid.ItemsSource = tooth;
            //DataContext = tooth;
        }
        
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            DB.insertMeasurement(measurement);
            if (measurement.Type.Equals(Smile.TEETH))
            {
                DB.insertMeasurementTeeth(measurement, tooth);
            }

            this.Close();
        }

        internal void Clear()
        {
            tooth.Clear();
            DataContext = tooth;
        }
    }
}
