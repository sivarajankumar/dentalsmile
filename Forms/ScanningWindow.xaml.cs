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
    /// Interaction logic for ScanningWindow.xaml
    /// </summary>
    public partial class ScanningWindow : Window
    {
        App app;
        DentalSmileDB db;
        Treatment treatment;

        public ScanningWindow()
        {
            InitializeComponent();
            
            app = Application.Current as App;
            db = new DentalSmileDB();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //show patient form to select another patient name and set to app.patient variable

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //save the progress file to disk

        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            //make a treatment

            treatment = new Treatment();
            treatment.Id = db.getTreatmentNewId(App.patient.Id);//generated: patient+sequence
            treatment.Patient = App.patient;
            treatment.Dentist = App.user.Dentist;
            treatment.Phase = new Phase();// Smile.SCANNING;
            treatment.Room = "R212";//Setting Default Room
            treatment.TreatmentDate = DateTime.Now;
            treatment.TreatmentTime = DateTime.Now.ToString(Smile.TIME_FORMAT);

            if (db.InsertTreatment(treatment))
            {
                MessageBox.Show("Success inserted");
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //finish the treatment 

            //and store the file to disk 

            //and to table
            SmileFile file = new SmileFile();
            file.Id = db.getSmileFileNewId(App.patient.Id);
            file.Type = Smile.SCANNING;
            file.FileName = "SCAN"+file.Id+".obj";
            file.Screenshot = "SCAN" + file.Id + ".png";
            file.Patient = App.patient;
            file.Description = textBox1.Text.ToString();//"Describe about some thing by USER";

            if (db.InsertFileInfo(file))
            {
                db.insertTreatmentFiles(treatment, file);
            }

        }


    }
}
