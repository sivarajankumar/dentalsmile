﻿using System;
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
using System.IO;
using System.Windows.Forms;

namespace smileUp.Forms
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : Window
    {
        public SettingsForm()
        {
            InitializeComponent();
            txtManipulatedPath.Text = Smile.MANIPULATED_PATH;
            txtScannedPath.Text = Smile.SCANNED_PATH;
            txtPhotoPath.Text = Smile.PHOTOS_PATH;
        }

        private void btnBrowse1_Click(object sender, RoutedEventArgs e)
        {
            var d = new FolderBrowserDialog();
            d.ShowNewFolderButton = true;
            d.ShowDialog();
            if (d.SelectedPath != null && !d.SelectedPath.Equals(""))
                txtScannedPath.Text = System.IO.Path.GetFullPath(d.SelectedPath);
            d.Dispose();
        }

        private void btnBrowse2_Click(object sender, RoutedEventArgs e)
        {
            var d = new FolderBrowserDialog();
            d.ShowNewFolderButton = true;
            d.ShowDialog();
            if(d.SelectedPath != null && !d.SelectedPath.Equals(""))
                txtManipulatedPath.Text = System.IO.Path.GetFullPath(d.SelectedPath);
            d.Dispose();
        }

        private void btnBrowse3_Click(object sender, RoutedEventArgs e)
        {
            var d = new FolderBrowserDialog();
            d.ShowNewFolderButton = true;
            d.ShowDialog();
            if (d.SelectedPath != null && !d.SelectedPath.Equals(""))
                txtPhotoPath.Text = System.IO.Path.GetFullPath(d.SelectedPath);
            d.Dispose();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //TODO: save to dictionary
            Properties.Settings.Default.ManipulatedPath = txtManipulatedPath.Text;
            Properties.Settings.Default.ScannedPath = txtScannedPath.Text;
            Properties.Settings.Default.PhotoPath = txtPhotoPath.Text;
            
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            if (System.Windows.MessageBox.Show("Setting saved successfully. Do you want to close this dialog?", "Configuration Changed", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reload();
        }
    }
}
