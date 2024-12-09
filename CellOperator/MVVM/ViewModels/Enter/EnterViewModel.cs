using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Controls;

using BLL.Services;
using BLL.Models;
using CellOperator.MVVM.Services;

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

            Username = "";
            Password = "";
        }
        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            try
            {
                AuthService authService = new AuthService();
                switch (authService.AuthCheck(Username, clearTextPassword)) {
                    
                    case 1:
                        {
                            AdministatorWindows taskWindow = new AdministatorWindows();
                            taskWindow.Show();
                        }
                        break;
                    case 2:
                        {
                            var Database = new DataBase_service();
                            ClientWindow taskWindow = new ClientWindow(Database.FindClient(Username, clearTextPassword));
                            taskWindow.Show();
                        }
                        break;
                    default:
                    case 0:                        
                            Status = "Введеные логин и пароль или не совпадают или не существуют.";
                        return;
                }
                NotifyPropertyChanged("IsAuthenticated");
                _loginCommand.RaiseCanExecuteChanged();
                WindowManager.CloseWindow(ViewID); //Close();                
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
