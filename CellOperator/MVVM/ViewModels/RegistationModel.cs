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
    class RegistationModel : INotifyPropertyChanged
    {
        DataBase_service DB;
        private readonly RelayCommand _RegistationAction;
        private string SUsername, SPassword,
            _FirstName, _LastName, _Patronymic, _Passport,
            _OrganizationName, _IdividualTaxpayerNumber, _Adress;
        private string _status;
        public RelayCommand RegistationAction { get { return _RegistationAction; } }


        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; NotifyPropertyChanged("FirstName"); }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; NotifyPropertyChanged("LastName"); }
        }
        public string Patronymic
        {
            get { return _Patronymic; }
            set { _Patronymic = value; NotifyPropertyChanged("Patronymic"); }
        }
        public string Passport
        {
            get { return _Passport; }
            set { _Passport = value; NotifyPropertyChanged("Passport"); }
        }
        ///

        public string OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; NotifyPropertyChanged("OrganizationName"); }
        }
        public string IdividualTaxpayerNumber
        {
            get { return _IdividualTaxpayerNumber; }
            set { _IdividualTaxpayerNumber = value; NotifyPropertyChanged("IdividualTaxpayerNumber"); }
        }
        public string Adress
        {
            get { return _Adress; }
            set { _Adress = value; NotifyPropertyChanged("Adress"); }
        }

        /// 
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

        /// 
        private bool _IsIndividual;
        public bool IsIndividual
        {
            get { return _IsIndividual; }
            set { _IsIndividual = value;
                NotifyPropertyChanged("IsIndividual");
                NotifyPropertyChanged("IsLegal");
            }
        }
        public bool IsLegal
        {
            get { return !_IsIndividual; }
            set { _IsIndividual = !value;
                NotifyPropertyChanged("IsIndividual");
                NotifyPropertyChanged("IsLegal");
            }
        }
        /// 


        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }
        public RegistationModel()
        {
            _viewId = Guid.NewGuid();
            _RegistationAction = new RelayCommand(Registration, i => true);

            DB = new DataBase_service();
        }

        private void Registration(object parameter)
        {
            bool IsCorrect = true;
            if (IsIndividual)
            {
                IndividualDTO Client = new IndividualDTO();
                Client.FirstName = FirstName;
                Client.LastName = LastName;
                Client.Patronymic = Patronymic;
                Client.Passport = Passport;

                IsCorrect = DB.AddIndividualClient(Client, Password, Username);
            }
            else
            {
                LegalEntityDTO Client = new LegalEntityDTO();
                Client.OrganizationName = OrganizationName;
                Client.IdividualTaxpayerNumber = IdividualTaxpayerNumber;
                Client.Adress = Adress;

                IsCorrect = DB.AddLegalEntityClient(Client, Password, Username);
            }
            if (!IsCorrect) Status = "Придумайте иной логин"; else
            {
                ClientWindow taskWindow = new ClientWindow(DB.FindClient(Username, Password));
                taskWindow.Show();

                WindowManager.CloseWindow(ViewID);
            }
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
}
