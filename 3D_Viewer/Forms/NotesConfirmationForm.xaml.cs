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
    /// Interaction logic for NotesConfirmationForm.xaml
    /// </summary>
    public partial class NotesConfirmationForm : Window
    {
        public string Notes {get;set;}
        public string MedicalResume { get; set; }

        public NotesConfirmationForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Notes = txtNotes.Text;
            MedicalResume = txtResumeMedic.Text;

            this.Hide();
        }
    }
}
