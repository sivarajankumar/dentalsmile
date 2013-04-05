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

namespace smileUp
{
    /// <summary>
    /// Interaction logic for ProgressDialog.xaml
    /// </summary>
    public partial class ProgressDialog : Window
    {
        public ProgressDialog()
        {
            InitializeComponent();
        }

        public string ProgressText
        {
            set
            {
                this.lblProgress.Content = value;
            }
        }

        public int ProgressValue
        {
            set
            {
                this.progress.Value = value;
            }
        }

        public event EventHandler Cancel = delegate { };

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Cancel(sender, e);
        }
    }
}
