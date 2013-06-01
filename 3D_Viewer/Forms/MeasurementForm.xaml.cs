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
using System.Windows.Media.Media3D;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for MeasurementForm.xaml
    /// </summary>
    public partial class MeasurementForm : Window
    {
        Measurement measurement;
        List<MeasurementTeeth> Mantooth,Autotooth;
        DentalSmileDB DB;
        App app;
        MainWindow mw;

        public MeasurementForm(Treatment treatment, SmileFile file, MainWindow m)
        {
            InitializeComponent();
            app = System.Windows.Application.Current as App;
            Mantooth = new List<MeasurementTeeth>();
            Autotooth = new List<MeasurementTeeth>();
            DB = DentalSmileDBFactory.GetInstance();
            this.mw = m;
            
            
            //TODO: DB.User = app.user.UserId;

            measurement = new Measurement();
            string treatment_id = treatment.Id;
            if (treatment_id == null)
            {
                treatment_id = treatment.RefId;
            }
            measurement.Treatment = treatment_id;
            measurement.Patient = treatment.Patient.Id;
            measurement.Pfile = file.Id;

            string measurement_id = checkPreviousData(file.Id) ;
            if(measurement_id !=null)
            { LoadMeasurementGrid(measurement_id); }

        }

    
        public void addRowTeeth(TeethVisual3D teeth, string type)
        {
            measurement.Type = Smile.TEETH;        
            string modified_date = DateTime.Today.ToString("dd-MM-yyyy");
                
            if (type == "man")
            {
                string[] separators = { ";" };
                string[] _startpoint = teeth.Model.StartPosition.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string[] _endpoint = teeth.Model.EndPosition.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                //string spoint = String.Concat(String.Format("{0:0.00}", _startpoint[0]), ";", String.Format("{0:0.00}", _startpoint[1]), ";", String.Format("{0:0.00}", _startpoint[2]));
                //string epoint = String.Concat(String.Format("{0:0.00}", _endpoint[0]), ";", String.Format("{0:0.00}", _endpoint[1]), ";", String.Format("{0:0.00}", _endpoint[2]));
                Point3D spoint = new Point3D(Math.Round(Convert.ToDouble(_startpoint[0]), 2), Math.Round(Convert.ToDouble(_startpoint[1]), 2), Math.Round(Convert.ToDouble(_startpoint[2]), 2));
                Point3D epoint = new Point3D(Math.Round(Convert.ToDouble(_endpoint[0]), 2), Math.Round(Convert.ToDouble(_endpoint[1]), 2), Math.Round(Convert.ToDouble(_endpoint[2]), 2));

                MeasurementTeeth m = new MeasurementTeeth(teeth.Id, Math.Round(teeth.Model.Length, 2), spoint.ToString(), epoint.ToString(), type, modified_date, false);

                Mantooth.Add(m);
                resultDataGridMan.ItemsSource = null;
                resultDataGridMan.ItemsSource = Mantooth;
 
                resultDataGridMan.Columns[1].IsReadOnly = false;
            }
            else
            {
                string spoint = null; string epoint = null;
                MeasurementTeeth m = new MeasurementTeeth(teeth.Id, Math.Round(teeth.Model.Length, 2), spoint, epoint, type, modified_date, false);
                Autotooth.Add(m);
                resultDataGridAuto.ItemsSource = null;
                resultDataGridAuto.ItemsSource = Autotooth;
 
           
            }
            this.Show();
            return;

        }
        
        private void saveManBtn_Click(object sender, RoutedEventArgs e)
        {

            if (measurement.Id == null)
            {
                DB.insertMeasurement(measurement);
            }
          
           if (measurement.Type.Equals(Smile.TEETH))
            {
                DB.insertMeasurementTeeth(measurement, Mantooth);
                System.Windows.MessageBox.Show("Data saved successfully.", "Successfully");
            }

           MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to continue measuring?", "Measurement Process Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
           if (result == MessageBoxResult.No)
           {
               this.Close();
           }

        }

        private void selectAllMeasurement(object sender, RoutedEventArgs e)
        { 
            

        }

        private void LoadMeasurementGrid(string measurement_id)
        {
            List<MeasurementTeeth> t = DB.SelectTeethById(measurement_id);

            MainWindow mw = new MainWindow();
            for (int i = 0; i < t.Count; i++)
            {

               
                
                if (t[i].Type == "man")
                {
                    string[] separators = { ";" };
                    string[] _startpoint = t[i].SPoint.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string[] _endpoint = t[i].EPoint.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    Point3D spoint = new Point3D(Convert.ToDouble(_startpoint[0]), Convert.ToDouble(_startpoint[1]), Convert.ToDouble(_startpoint[2]));
                    Point3D epoint = new Point3D(Convert.ToDouble(_endpoint[0]), Convert.ToDouble(_endpoint[1]), Convert.ToDouble(_endpoint[2]));

                    Mantooth.Add(new MeasurementTeeth(t[i].Identity, Math.Round(t[i].Length, 2), spoint.ToString(), epoint.ToString(), t[i].Type,t[i].ModifiedDate.ToString(),true));

                }
                else 
                {
                    string spoint = null; string epoint = null;
                    Autotooth.Add(new MeasurementTeeth(t[i].Identity, Math.Round(t[i].Length, 2), spoint, epoint, t[i].Type, t[i].ModifiedDate.ToString(), true));
                }
                
                //mw.createLine(spoint,epoint);
            }

            resultDataGridMan.ItemsSource = null;
            resultDataGridMan.ItemsSource = Mantooth;

            resultDataGridAuto.ItemsSource = null;
            resultDataGridAuto.ItemsSource = Autotooth;

        }


        internal void ClearMan()
        {
            Mantooth.Clear();
            DataContext = Mantooth;
        }

        internal void ClearAuto()
        {
            Autotooth.Clear();
            DataContext = Autotooth;

        }

        private void btnSaveAuto_Click(object sender, RoutedEventArgs e)
        {
            DB.insertMeasurement(measurement);
            if (measurement.Type.Equals(Smile.TEETH))
            {
                DB.insertMeasurementTeeth(measurement, Autotooth);
                System.Windows.MessageBox.Show("Data saved successfully.", "Successfully");
            }

            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to continue measuring?", "Measurement Process Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                this.Close();
            }
        }

        private string checkPreviousData(string file_id)
        {
            Measurement measurement_result = DB.findMeasurementByFileId(file_id);
            if (measurement_result == null)
            { return null; }
            else 
            { 
                return measurement_result.Id.ToString();
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MeasurementTeeth t= resultDataGridMan.SelectedItem as MeasurementTeeth;
            if (t != null)
            {
                string[] separators = { ";" };
                string[] _startpoint = t.SPoint.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string[] _endpoint = t.EPoint.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                Point3D spoint = new Point3D(Math.Round(Convert.ToDouble(_startpoint[0]), 2), Math.Round(Convert.ToDouble(_startpoint[1]), 2), Math.Round(Convert.ToDouble(_startpoint[2]), 2));
                Point3D epoint = new Point3D(Math.Round(Convert.ToDouble(_endpoint[0]), 2), Math.Round(Convert.ToDouble(_endpoint[1]), 2), Math.Round(Convert.ToDouble(_endpoint[2]), 2));
                mw.createLine(spoint, epoint);

            }
        }
        private void UncheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MeasurementTeeth t = resultDataGridMan.SelectedItem as MeasurementTeeth;
            if (t != null)
            {
                string[] separators = { ";" };
                string[] _startpoint = t.SPoint.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string[] _endpoint = t.EPoint.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                Point3D spoint = new Point3D(Math.Round(Convert.ToDouble(_startpoint[0]), 2), Math.Round(Convert.ToDouble(_startpoint[1]), 2), Math.Round(Convert.ToDouble(_startpoint[2]), 2));
                Point3D epoint = new Point3D(Math.Round(Convert.ToDouble(_endpoint[0]), 2), Math.Round(Convert.ToDouble(_endpoint[1]), 2), Math.Round(Convert.ToDouble(_endpoint[2]), 2));
                mw.removeLine(spoint, epoint);

            }
        }


       
    }
}
