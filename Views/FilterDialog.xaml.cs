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
using System.ComponentModel;

namespace smileUp.Views
{
    /// <summary>
    /// Interaction logic for FilterDialog.xaml
    /// </summary>
    public partial class FilterDialog : UserControl
    {
        bool ignoreSelection = true;
        List<Patient> family;
        DentalSmileDB DB;

        public FilterDialog()
        {
            InitializeComponent();
            DB = new DentalSmileDB();
            family = DB.SelectAllPatient();
            ICollectionView view = System.Windows.Data.CollectionViewSource.GetDefaultView(family);
            view.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));
            DataContext = family;
        }


        private void FamilyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ignoreSelection)
            {
                ignoreSelection = true;
                Patient selected = (Patient)((ListBox)sender).SelectedItem;
                if (selected != null)
                {
                    this.DataContext = selected;
                }

                ignoreSelection = false;
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FamilyListView.FilterList(FilterTextBox.Text);
        }
    }
}
