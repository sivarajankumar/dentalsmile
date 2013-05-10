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
using System.Windows.Media.Animation;
using System.ComponentModel;
using smileUp.Forms;

namespace smileUp.Views
{
    /// <summary>
    /// Interaction logic for PatientDetails.xaml
    /// </summary>
    public partial class PatientDetails : UserControl
    {
        List<Patient> patients;
        bool ignoreSelection = true;
        DentalSmileDB DB;
        Storyboard CollapseDetailsEdit;
        Storyboard CollapseDetailsAdd;
        App app;

        public PatientDetails()
        {
            InitializeComponent();
            app = Application.Current as App;

            DB = new DentalSmileDB();
            DetailsEdit.Visibility = Visibility.Collapsed;
            DetailsAdd.Visibility = Visibility.Collapsed;
            DetailsList.Visibility = Visibility.Collapsed;
            HistoryList.Visibility = Visibility.Collapsed;

            CollapseDetailsEdit = ((Storyboard)this.Resources["CollapseDetailsEdit"]);
            CollapseDetailsAdd = ((Storyboard)this.Resources["CollapseDetailsAdd"]);

            PatientListView.ItemsSource = patients;

            patients = DB.SelectAllPatient();
            ICollectionView view = System.Windows.Data.CollectionViewSource.GetDefaultView(patients);
            view.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));

            PatientListView.ItemsSource = patients;
            ignoreSelection = false;
            navigateButton();

            Storyboard ExpandDetailsList = ((Storyboard)this.Resources["ExpandDetailsList"]);
            ExpandDetailsList.Begin();
        }

        /// <summary>
        /// Handles Drop Event for setting the Avatar photo.
        /// </summary>
        private void AvatarPhoto_Drop(object sender, DragEventArgs e)
        {
            string[] fileNames = e.Data.GetData(DataFormats.FileDrop, true) as string[];

            if (fileNames.Length > 0)
            {
                Photo photo = new Photo(fileNames[0]);
                if (App.patient != null && App.patient.Photos != null)
                {
                    // Set IsAvatar to false for the existing photos
                    foreach (Photo existingPhoto in App.patient.Photos)
                    {
                        existingPhoto.IsAvatar = false;
                    }

                    // Make the dropped photo the  avatar photo
                    photo.IsAvatar = true;

                    // Add the avatar photo to the person photos
                    App.patient.Photos.Add(photo);

                    // Bitmap image for the avatar
                    BitmapImage bitmap = new BitmapImage(new Uri(photo.FullyQualifiedPath));

                    // Use BitmapCacheOption.OnLoad to prevent binding the source holding on to the photo file.
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;

                    // Show the avatar
                    AvatarPhoto.Source = bitmap;
                }
            }

            // Mark the event as handled, so the control's native Drop handler is not called.
            e.Handled = true;
        }


        /// <summary>
        /// Event handler for deleting people.  Note that not all people can be deleted.  See "IsDeletable" property on the person class.
        /// </summary>

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DoneEditButton_Click(object sender, RoutedEventArgs e)
        {
            // Make sure the data binding is updated for fields that update during LostFocus.
            // This is necessary since this method can be invoked when the Enter key is pressed,
            // but the text field has not lost focus yet, so it does not update binding. This
            // manually updates the binding for those fields.
            if (!validatePatient())
            {
                CollapseDetailsEdit.Stop();
                return;
            }

            // Let the collection know that it has been updated so that the diagram control will update.
            //patient.OnContentChanged();
            
            Patient patient = App.patient;
            //TODO: store to DB
            patient.FirstName = FirstNameEditTextBox.Text;
            patient.LastName = LastNameEditTextBox.Text;
            patient.Gender = GenderListBox.SelectedValue.ToString();
            patient.BirthPlace = BirthPlaceEditTextBox.Text;
            patient.BirthDate = BirthDateEditTextBox.SelectedDate.Value;
            patient.Address1 = Address1EditTextBox.Text;
            patient.Address2 = Address2EditTextBox.Text;
            patient.City = CityEditTextBox.Text;
            patient.Phone = PhoneEditTextBox.Text;
            DB.UpdatePatient(patient);
            CollapseDetailsEdit.Begin();
        }

        private bool validatePatient()
        {
            if (BirthDateEditTextBox.IsFocused)
                BirthDateEditTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (FirstNameEditTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("First Name can't be null");
                FirstNameEditTextBox.Focus();
                return false;
            }
            if (LastNameEditTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Last Name can't be null");
                LastNameEditTextBox.Focus();
                return false;
            }
            if (GenderListBox.SelectedItem.Equals(string.Empty))
            {
                MessageBox.Show("Gender can't be null");
                GenderListBox.Focus();
                return false;
            }
            if (BirthDateEditTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Birth Date can't be null");
                BirthDateEditTextBox.Focus();
                return false;
            }
            if (BirthPlaceEditTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Birth Place can't be null");
                BirthPlaceEditTextBox.Focus();
                return false;
            }
            if (Address1EditTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Address can't be null");
                Address1EditTextBox.Focus();
                return false;
            }
            if (PhoneEditTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Phone can't be null");
                PhoneEditTextBox.Focus();
                return false;
            }
            return true;
        }
        private bool validatePatientAdd()
        {
            if (BirthDateAddTextBox.IsFocused)
                BirthDateAddTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (FirstNameAddTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("First Name can't be null");
                FirstNameAddTextBox.Focus();
                return false;
            }
            if (LastNameAddTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Last Name can't be null");
                LastNameAddTextBox.Focus();
                return false;
            }
            if (GenderAddListBox.SelectedValue.Equals(string.Empty))
            {
                MessageBox.Show("Gender can't be null");
                GenderListBox.Focus();
                return false;
            }
            if (BirthDateAddTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Birth Date can't be null");
                BirthDateAddTextBox.Focus();
                return false;
            }
            if (BirthPlaceAddTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Birth Place can't be null");
                BirthPlaceAddTextBox.Focus();
                return false;
            }
            if (Address1AddTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Address can't be null");
                Address1AddTextBox.Focus();
                return false;
            }
            if (PhoneAddTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Phone can't be null");
                PhoneAddTextBox.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// The focus can be set only after the animation has stopped playing.
        /// </summary>
        private void ExpandDetailsEdit_StoryboardCompleted(object sender, EventArgs e)
        {
            FirstNameEditTextBox.Focus();
            navigateButton();

        }

        /// <summary>
        /// Make it easy to add new people to the tree by pressing return
        /// </summary>
        private void CollapseDetailsEdit_StoryboardCompleted(object sender, EventArgs e)
        {
            navigateButton();

        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryListView.DataContext = null;
            //RaiseEvent(new RoutedEventArgs(HistoryDataClickEvent));
            List<Treatment> t =  DB.findTreatmentsByPatientId(App.patient.Id);
            HistoryListView.ItemsSource = t;
            HistoryFileListView.ItemsSource = null;
            navigateButton(Smile.NONE);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HideFindButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// The focus can be set only after the animation has stopped playing.
        /// </summary>
        private void ExpandDetailsAdd_StoryboardCompleted(object sender, EventArgs e)
        {
            navigateButton();

        }

        /// <summary>
        /// Make it easy to add new people to the tree by pressing return
        /// </summary>
        private void CollapseDetailsAdd_StoryboardCompleted(object sender, EventArgs e)
        {
            if (App.patient != null)
            {
                DetailsList.Visibility = Visibility.Visible;
                DataContext = App.patient;
            }
            navigateButton();

        }


        private void PatientListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ignoreSelection)
            {
                ignoreSelection = true;
                Patient selected = (Patient)((ListBox)sender).SelectedItem;
                if (selected != null)
                {
                    this.DataContext = selected;
                    App.patient = selected;
                }

                ignoreSelection = false;
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PatientListView.FilterList(FilterTextBox.Text);
        }


        /// <summary>
        /// The focus can be set only after the animation has stopped playing.
        /// </summary>
        private void ExpandDetailsList_StoryboardCompleted(object sender, EventArgs e)
        {
            navigateButton();
        }

        /// <summary>
        /// Make it easy to add new people to the tree by pressing return
        /// </summary>
        private void CollapseDetailsList_StoryboardCompleted(object sender, EventArgs e)
        {
            navigateButton();
        }


        public static readonly RoutedEvent HistoryDataClickEvent = EventManager.RegisterRoutedEvent(
    "HistoryDataClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PatientDetails));

        public event RoutedEventHandler HistoryDataClick
        {
            add { AddHandler(HistoryDataClickEvent, value); }
            remove { RemoveHandler(HistoryDataClickEvent, value); }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!validatePatientAdd())
            {
                CollapseDetailsAdd.Stop();
                return;
            }

            // Let the collection know that it has been updated so that the diagram control will update.
            //patient.OnContentChanged();
            //Check Existing Patient
            if (!DB.findPatientByNameAndBirthDate(FirstNameAddTextBox.Text, LastNameAddTextBox.Text, BirthDateAddTextBox.Text))
            {
                Patient patient = new Patient();
                //TODO: store to DB
                patient.Id = DB.getPatientNewId();
                patient.FirstName = FirstNameAddTextBox.Text;
                patient.LastName = LastNameAddTextBox.Text;
                patient.Gender = GenderAddListBox.SelectedValue.ToString();
                patient.BirthPlace = BirthPlaceAddTextBox.Text;
                patient.BirthDate = BirthDateAddTextBox.SelectedDate.Value;
                patient.Address1 = Address1AddTextBox.Text;
                patient.Address2 = Address2AddTextBox.Text;
                patient.City = CityAddTextBox.Text;
                patient.Phone = PhoneAddTextBox.Text;

                DB.InsertPatient(patient);
                CollapseDetailsAdd.Begin();

                App.patient = patient;
                DataContext = patient;

                insertTreatment(Smile.REGISTERED);
            }
            else
            {
                MessageBox.Show("Patient already exist");
                //TODO Find
            }
        }

        private void insertTreatment(int p)
        {
            Treatment t = new Treatment();
            t.Phase = Smile.GetPhase(p);
            t.Patient = App.patient;
            t.Room = Smile.Room;
            t.Dentist = App.user.Dentist;
            DB.InsertTreatment(t);
        }

        private void navigateButton(){
            EditButton.IsEnabled = App.patient != null;
            InfoButton.IsEnabled = App.patient != null;
            AddTreatmentButton1.IsEnabled = App.patient != null;
        }

        private void HideHistoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterHistoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            HistoryListView.FilterList(FilterHistoryTextBox.Text);

        }

        private void HistoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ignoreSelection)
            {
                ignoreSelection = true;
                Treatment selected = (Treatment)((ListBox)sender).SelectedItem;
                if (selected != null)
                {
                    HistoryFileListView.ItemsSource = selected.Files;
                }
                ignoreSelection = false;
            }
            navigateButton(Smile.REGISTERED);
        }
        private void HistoryFileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ignoreSelection)
            {
                ignoreSelection = true;
                
                SmileFile selected = (SmileFile)((ListBox)sender).SelectedItem;

                if (selected != null)
                {
                    if (selected != null) navigateButton(selected.Type);
                }
                
                ignoreSelection = false;
            }

        }
        private void FilterHistoryFileTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            HistoryFileListView.FilterList(FilterHistoryFileTextBox.Text);

        }
        private void ExpandHistoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExpandHistoryList_StoryboardCompleted(object sender, EventArgs e)
        {
            navigateButton();
        }
        private void CollapseHistoryList_StoryboardCompleted(object sender, EventArgs e)
        {
            navigateButton();
        }

        private void AddTreatmentButton_Click(object sender, RoutedEventArgs e)
        {
            TreatmentForm f = new TreatmentForm();
            f.ShowDialog();
        }

        private void btnStartManipulation_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryFileListView.SelectedItem != null)
            {
                Treatment t = HistoryListView.SelectedItem as Treatment;
                SmileFile file = HistoryFileListView.SelectedItem as SmileFile;

                App.patient = t.Patient;

                MainWindow m = new MainWindow(t, file, true);
                m.ShowDialog();
                //this.Close();
            }
        }
        private void btnNewScan_Click(object sender, RoutedEventArgs e)
        {
            ScanningForm s = new ScanningForm();
            s.Show();
            //s.Close();
        }

        private void btnContinueManipulation_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryFileListView.SelectedItem != null)
            {
                Treatment t = HistoryListView.SelectedItem as Treatment;
                SmileFile file = HistoryFileListView.SelectedItem as SmileFile;
                App.patient = t.Patient;
                MainWindow m = new MainWindow(t, file, true);
                m.ShowDialog();
                //this.Close();
            }
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.patient;
        }
    }
}
