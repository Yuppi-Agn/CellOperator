using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Controls;

using BLL.Services;
using BLL.Models;

namespace CellOperator.MVVM.ViewModels
{
    class EnterViewModel : INotifyPropertyChanged
    {
        private readonly RelayCommand _loginCommand;
        private string SUsername, SPassword;
        private string _status;

        private readonly RelayCommand _RegirCommand;
        public RelayCommand LoginCommand { get { return _loginCommand; } }
        public RelayCommand RegirCommand { get { return _RegirCommand; } }
        public string Username
        {
            get { return SUsername; }
            set { SUsername = value; NotifyPropertyChanged("Username"); }
        }
        public string Password
        {
            get { return SPassword; }
            set { SPassword = value; NotifyPropertyChanged("Password"); }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged("Status"); }
        }
        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }

        public EnterViewModel()
        {
            _viewId = Guid.NewGuid();
            _loginCommand = new RelayCommand(Login, i => true);
            _RegirCommand = new RelayCommand(Registretion, i => true);

            Username = "200516135";
            Password = "12913507";
        }
        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            try
            {
                //User user = _authenticationService.AuthenticateUser(Username, clearTextPassword);

                //Update UI

                if (AdministratorAuth_check(Username, clearTextPassword))
                {
                    AdministatorWindows taskWindow = new AdministatorWindows();
                    taskWindow.Show();

                    //NotifyPropertyChanged("AuthenticatedUser");
                    NotifyPropertyChanged("IsAuthenticated");
                    _loginCommand.RaiseCanExecuteChanged();

                    WindowManager.CloseWindow(ViewID); //Close();
                }
                else if (UserAuth_check(Username, clearTextPassword))
                {
                    DataBase_service Database;
                    Database = new DataBase_service();
                    ClientWindow taskWindow = new ClientWindow(Database.FindClient(Username, passwordBox.Password));
                    taskWindow.Show();

                    //NotifyPropertyChanged("AuthenticatedUser");
                    NotifyPropertyChanged("IsAuthenticated");
                    _loginCommand.RaiseCanExecuteChanged();

                    WindowManager.CloseWindow(ViewID);//Close();
                }
                else
                {
                    Status = "НЕПРАВИЛЬНО";
                }
                
                //_logoutCommand.RaiseCanExecuteChanged();
                //Username = string.Empty; //reset
                //passwordBox.Password = string.Empty; //reset
                //Status = string.Empty;
                //_IsAuthenticated = true;
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Login failed! Please provide some valid credentials.";
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
            }
        }
        private void Registretion (object parameter)
        {
            RegistationWindows taskWindow = new RegistationWindows();
            taskWindow.Show();

            WindowManager.CloseWindow(ViewID);
        }

       /* private void Button_Auth(object sender, RoutedEventArgs e)
        {
            
        }*/

        private bool AdministratorAuth_check(string Login, string Password)
        {
            if (Login == "1234" && Password == "1234")
                return true;
            else return false;
        }
        private bool UserAuth_check(string Login, string Password)
        {
            DataBase_service Database;
            //Methods_service methods;

            Database = new DataBase_service();
            ClientDTO Client = Database.FindClient(Login, Password);
            if (Client != null)
                return true;
            else return false;
        }
        
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    /*public class PropertyChangeVar<T> : INotifyPropertyChanged 
    {
        private T _status;
        public T Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged(nameof(Status)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }*/
}
