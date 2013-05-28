using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using SoftArcs.WPFSmartLibrary.MVVMCommands;
using SoftArcs.WPFSmartLibrary.MVVMCore;
using SoftArcs.WPFSmartLibrary.SmartUserControls;
using smileUp.DataModel;
using smileUp.Forms;

namespace smileUp.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		#region Fields

        DentalSmileDB DB;
        
		List<SmileUser> userList;
		private readonly string userImagesPath = @"\Images";

		#endregion // Fields

		#region Constructors

        LoginDialog dialog;

		public LoginViewModel(LoginDialog dialog)
		{
			if (ViewModelHelper.IsInDesignModeStatic == false)
			{
				this.initializeAllCommands();
                this.dialog = dialog;

				//+ This is only neccessary if you want to display the appropriate image while typing the user name.
				//+ If you want a higher security level you wouldn't do this here !
				//! Remember : ONLY for demonstration purposes I have used a local Collection
				//this.getAllUser();
			}
		}

		#endregion // Constructors

		#region Public Properties

		public string UserName
		{
			get { return GetValue( () => UserName ); }
			set
			{
				SetValue( () => UserName, value );

				//this.UserImageSource = this.getUserImagePath();
			}
		}

		public string Password
		{
			get { return GetValue( () => Password ); }
			set { SetValue( () => Password, value ); }
		}

		public string UserImageSource
		{
			get { return GetValue( () => UserImageSource ); }
			set { SetValue( () => UserImageSource, value ); }
		}

		#endregion // Public Properties

		#region Submit Command Handler

		public ICommand SubmitCommand { get; private set; }

		private void ExecuteSubmit(object commandParameter)
		{
			var accessControlSystem = commandParameter as SmartLoginOverlay;

			if (accessControlSystem != null)
			{
				if (this.validateUser( this.UserName, this.Password ) == true)
				{
					accessControlSystem.Unlock();
                    dialog.Close();
				}
				else
				{
					accessControlSystem.ShowWrongCredentialsMessage();
				}
			}
		}

		private bool CanExecuteSubmit(object commandParameter)
		{
			return !string.IsNullOrEmpty( this.Password );
		}

		#endregion // Submit Command Handler

		#region Private Methods

		private void initializeAllCommands()
		{
			this.SubmitCommand = new ActionCommand( this.ExecuteSubmit, this.CanExecuteSubmit );
            DB = DentalSmileDBFactory.GetInstance();
		}

		private void getAllUser()
		{
			//+ Here you would implement code, which will get all user from a database,
			//+ a webservice or from somewhere else (if you want to display the right image)
			//! Remember : ONLY for demonstration purposes I have used a local Collection
			this.userList = new List<SmileUser>()
								 {
									new SmileUser() { UserId="gingerbreadman", Password="gingy1" },
									new SmileUser() { UserId="bluehairbeauty", Password="blue1" },
								 };
		}

		private bool validateUser(string username, string password)
		{
			//+ Here you would implement code, which will get the validation for the given credentials
			//+ from a database, a webservice or from somewhere else
			//! Remember : ONLY for demonstration purposes I have used a local Collection
            SmileUser validatedUser = null;// this.userList.FirstOrDefault(user => user.UserId.Equals(username) && user.Password.Equals(password));
            if (DB.login(username, password, ref validatedUser)){
                App.user = validatedUser;
                return validatedUser != null;
            }
			return validatedUser != null;
		}

		private string getUserImagePath()
		{
			SmileUser currentUser = this.userList.FirstOrDefault( user => user.UserId.Equals( this.UserName ) );

			return String.Empty;
		}

		#endregion
	}
}
