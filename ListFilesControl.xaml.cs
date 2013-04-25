using System;
using System.Collections.Generic;
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

namespace smileUp
{
	public partial class ListFilesControl : System.Windows.Controls.UserControl
	{
        public ListFilesControl()
		{
			InitializeComponent();
		}

        private void rdScanning_Checked(object sender, RoutedEventArgs e)
        {
            filter("scanning");
        }

        private void rdManipulation_Checked(object sender, RoutedEventArgs e)
        {
            filter("manipulation");
        }

        private void filter(string p)
        {
            Console.WriteLine("filtering" + p);
        }

	}
}