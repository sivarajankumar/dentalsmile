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

namespace smileUp.Views
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        private TextBlock textBlockHint;
		private Brush savedBackgroundBrush;
		private DependencyObject visualRoot;


        public LoginControl()
        {
            InitializeComponent();
        }



         #region Password Box event handler

		/// <summary>
		/// If the user press the Return (resp. Enter) Key perform the "submit process"
		/// </summary>
		private void PasswordBoxControl_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return || e.Key == Key.Enter)
			{
				this.SubmitButton_Click( sender, new RoutedEventArgs() );
				return;
			}

			this.lblCapsLockInfo.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Hidden;
		}

		/// <summary>
		/// Check whether the CapsLock is active or not and display or hide the CapsLockInfo
		/// </summary>
		private void PasswordBoxControl_OnGotFocus(object sender, RoutedEventArgs e)
		{
			this.lblCapsLockInfo.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Hidden;
		}

		/// <summary>
		/// The CapsLockInfo is only visible when the PasswordBox Control got the focus
		/// </summary>
		private void PasswordBoxControl_OnLostFocus(object sender, RoutedEventArgs e)
		{
			this.lblCapsLockInfo.Visibility = Visibility.Hidden;
		}

		#endregion

		#region Event handler of all buttons

		/// <summary>
		/// Set the focus to the PasswordBox Control when the RevealButton lost mouse capture.
		/// </summary>
		void RevealButton_LostMouseCapture(object sender, MouseEventArgs e)
		{
			this.PasswordBoxControl.Focus();
		}

		

		/// <summary>
		/// Hide the Info Overlay an set the focus to the PasswordBox Control. 
		/// </summary>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			this.PasswordBoxControl.Visibility = Visibility.Visible;
			this.FaultMessagePanel.Visibility = Visibility.Hidden;

			this.PasswordBoxControl.Focus();
		}

		#endregion

		
		#region Private helper methods

		/// <summary>
		/// Assimilate the background from the visual root (window) and save the background brush
		/// </summary>
		protected void assimilateBackground()
		{
			if (this.savedBackgroundBrush != null)
			{
				this.Background = this.savedBackgroundBrush.Clone();
				return;
			}

			if (this.savedBackgroundBrush == null && this.Background != null)
			{
				this.savedBackgroundBrush = this.Background.Clone();
				return;
			}

			if (this.visualRoot is Window)
			{
				Window window = this.visualRoot as Window;

				this.savedBackgroundBrush = window.Background.Clone();
				this.Background = this.savedBackgroundBrush;
			}
        }
        #endregion

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(tbUserName.Text +" - "+PasswordBoxControl.Password+" - "+PasswordBoxControl.SecurePassword);
        }


    }
}
