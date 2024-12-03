﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using BLL.Services;
using BLL.Models;
using System.Collections.ObjectModel;

using System.ComponentModel;
using System.Windows.Controls;

using CellOperator.MVVM.Services;

namespace CellOperator.MVVM.ViewModels
{
    class ClientWindow_ReportCallingModel
    {
        private List<Methods.Report_Calling> MyTable;
        private RelayCommand _SaveCSVAction;
        public RelayCommand SaveCSVAction { get { return _SaveCSVAction; } }
        public ObservableCollection<Methods.Report_Calling> Table { get; set; }
        public ClientWindow_ReportCallingModel(List<Methods.Report_Calling> Table)
        {
            MyTable = Table;
            this.Table = new ObservableCollection<Methods.Report_Calling>();
            for (int i = 0; i < Table.Count; i++) this.Table.Add(Table[i]);
            _SaveCSVAction = new RelayCommand(SaveCSV, i => true);
        }
        void SaveCSV(object parameter)
        {
            var Service = new FileService_csv();
            Service.Save(MyTable);
        }
    }
    class ClientWindow_ReportSMSModel
    {
        private List<Methods.Report_SMS> MyTable;
        private RelayCommand _SaveCSVAction;
        public RelayCommand SaveCSVAction { get { return _SaveCSVAction; } }
        public ObservableCollection<Methods.Report_SMS> Table { get; set; }
        public ClientWindow_ReportSMSModel(List<Methods.Report_SMS> Table)
        {
            this.Table = new ObservableCollection<Methods.Report_SMS>();
            for (int i = 0; i < Table.Count; i++) this.Table.Add(Table[i]);
            _SaveCSVAction = new RelayCommand(SaveCSV, i => true);
        }
        void SaveCSV(object parameter)
        {
            var Service = new FileService_csv();
            Service.Save(MyTable);
        }
    }
    class ClientWindow_ReportExpensesModel
    {
        private List<ExpensesDTO> MyTable;
        private RelayCommand _SaveCSVAction;
        public RelayCommand SaveCSVAction { get { return _SaveCSVAction; } }
        public ObservableCollection<ExpensesDTO> Table { get; set; }
        public ClientWindow_ReportExpensesModel(List<ExpensesDTO> Table)
        {
            this.Table = new ObservableCollection<ExpensesDTO>();
            for (int i = 0; i < Table.Count; i++) this.Table.Add(Table[i]);
            _SaveCSVAction = new RelayCommand(SaveCSV, i => true);
        }
        void SaveCSV(object parameter)
        {
            var Service = new FileService_csv();
            Service.Save(MyTable);
        }
    }
    class ClientWindow_TarifChangeModel : INotifyPropertyChanged
    {
        ClientDTO client;
        NumberDTO number;

        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }

        private DataBase_service DB;
        public ObservableCollection<TarifDTO> Tarifs { get; set; }

        private RelayCommand _ChangeAction;
        public RelayCommand ChangeAction { get { return _ChangeAction; } }
        private RelayCommand _SelectAction;
        public RelayCommand SelectAction { get { return _SelectAction; } }

        private TarifDTO _SelectedTarif;
        public TarifDTO SelectedTarif
        {
            get { return _SelectedTarif; }
            set
            {
                _SelectedTarif = value;
                NotifyPropertyChanged("SelectedTarif");
            }
        }
        public ClientWindow_TarifChangeModel(ref DataBase_service db, ClientDTO client, NumberDTO number)
        {
            _viewId = Guid.NewGuid();
            this.Tarifs = new ObservableCollection<TarifDTO>();
            _ChangeAction = new RelayCommand(Change, i => true);
            DB = db;

            var Table = DB.GetAllTarifs(client);
            for (int i = 0; i < Table.Count; i++)
            {
                this.Tarifs.Add(Table[i]);
            }

            this.client = client;
            this.number = number;
        }
        public ClientWindow_TarifChangeModel(ref DataBase_service db, ClientDTO client)
        {
            _viewId = Guid.NewGuid();
            this.Tarifs = new ObservableCollection<TarifDTO>();
            _SelectAction = new RelayCommand(Select, i => true);
            DB = db;

            var Table = DB.GetAllTarifs(client);
            for (int i = 0; i < Table.Count; i++)
            {
                this.Tarifs.Add(Table[i]);
            }

            this.client = client;
            this.number = null;
        }
        public void Change(object parameter)
        {
            if (SelectedTarif == null) return;
            DB.ChangeTarif_NumByClient(client, _SelectedTarif.ID, number.ID);
            MessageBox.Show("Произошла успешно!", "Смена тарифа...", MessageBoxButton.OK);
            WindowManager.CloseWindow(ViewID);//Close();
        }
        public void Select(object parameter)
        {
            if (SelectedTarif == null) return;
            DB.BuyNumber(client, _SelectedTarif.ID);
            MessageBox.Show("Произошла успешно!", "Покупка номера...", MessageBoxButton.OK);
            WindowManager.CloseWindow(ViewID);//Close();
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
    class ClientWindow_ServiceChangeModel : INotifyPropertyChanged
    {
        ClientDTO client;
        NumberDTO number;

        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }

        private DataBase_service DB;
        public ObservableCollection<Methods.ServiceOutput> Table { get; set; }

        private RelayCommand _ChangeAction;
        public RelayCommand ChangeAction { get { return _ChangeAction; } }

        private Methods.ServiceOutput _Selected;
        public Methods.ServiceOutput Selected
        {
            get { return _Selected; }
            set
            {
                _Selected = value;
                NotifyPropertyChanged("Selected");
            }
        }
        public ClientWindow_ServiceChangeModel(ref DataBase_service db, ClientDTO client, NumberDTO number)
        {
            _viewId = Guid.NewGuid();
            this.Table = new ObservableCollection<Methods.ServiceOutput>();
            _ChangeAction = new RelayCommand(Change, i => true);
            DB = db;
            this.client = client;
            this.number = number;

            Update();
        }
        public void Change(object parameter)
        {
            if (Selected == null) return;

            if (Selected.Stat) DB.ServiceDisconnect(number.ID, Selected.ID);
            else DB.ServiceConnect(number.ID, Selected.ID);

            Update();
        }

        private void Update()
        {
            var Service = new Methods_service();

            var Table = Service.GetServices(number.ID);
            this.Table.Clear();
            for (int i = 0; i < Table.Count; i++)
            {
                this.Table.Add(Table[i]);
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
    class ClientWindow_SendSMSModel : INotifyPropertyChanged
    {
        ClientDTO client;
        NumberDTO number;

        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }

        private DataBase_service DB;

        private RelayCommand _BaseCommand;
        public RelayCommand BaseCommand { get { return _BaseCommand; } }

        private string _OtherNumber;
        public string OtherNumber
        {
            get { return _OtherNumber; }
            set
            {
                _OtherNumber = value;
                NotifyPropertyChanged("OtherNumber");
            }
        }
        private string _SMSData;
        public string SMSData
        {
            get { return _SMSData; }
            set
            {
                _SMSData = value;
                NotifyPropertyChanged("SMSData");
            }
        }
        public string _YourNumber;
        public string YourNumber
        {
            get { return _YourNumber; }
        }
        public ClientWindow_SendSMSModel(ref DataBase_service db, ClientDTO client, NumberDTO number)
        {
            _YourNumber = "Ваш номер: " + number.Number;
            _viewId = Guid.NewGuid();
            _BaseCommand = new RelayCommand(Action, i => true);
            DB = db;

            this.client = client;
            this.number = number;
        }
        public void Action(object parameter)
        {
            DB.UserSendSMS(number.ID, "+7980" + _OtherNumber, SMSData);
            MessageBox.Show("Произошла успешно!", "Отправка СМС...", MessageBoxButton.OK);
            WindowManager.CloseWindow(ViewID);//Close();
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
    class ClientWindow_MakeCallModel : INotifyPropertyChanged
    {
        ClientDTO client;
        NumberDTO number;

        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }

        private DataBase_service DB;

        private RelayCommand _BaseCommand;
        public RelayCommand BaseCommand { get { return _BaseCommand; } }

        private string _OtherNumber;
        public string OtherNumber
        {
            get { return _OtherNumber; }
            set
            {
                _OtherNumber = value;
                NotifyPropertyChanged("OtherNumber");
            }
        }
        private string _CallDuration;
        public string CallDuration
        {
            get { return _CallDuration; }
            set
            {
                _CallDuration = value;
                NotifyPropertyChanged("CallDuration");
            }
        }
        public string _YourNumber;
        public string YourNumber
        {
            get { return _YourNumber; }
        }
        public ClientWindow_MakeCallModel(ref DataBase_service db, ClientDTO client, NumberDTO number)
        {
            _YourNumber = "Ваш номер: " + number.Number;
            _viewId = Guid.NewGuid();
            _BaseCommand = new RelayCommand(Action, i => true);
            DB = db;

            this.client = client;
            this.number = number;
        }
        public void Action(object parameter)
        {
            DB.UserMakeCall(number.ID, "+7980" + _OtherNumber, int.Parse(_CallDuration));
            MessageBox.Show("Произошел успешно!", "Звонок...", MessageBoxButton.OK);
            WindowManager.CloseWindow(ViewID);//Close();
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
    class ClientWindow_SpentInternetModel : INotifyPropertyChanged
    {
        ClientDTO client;
        NumberDTO number;

        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }

        private DataBase_service DB;

        private RelayCommand _BaseCommand;
        public RelayCommand BaseCommand { get { return _BaseCommand; } }

        private string _InternetAmount;
        public string InternetAmount
        {
            get { return _InternetAmount; }
            set
            {
                _InternetAmount = value;
                NotifyPropertyChanged("InternetAmount");
            }
        }
        public string _YourNumber;
        public string YourNumber
        {
            get { return _YourNumber; }
        }
        public ClientWindow_SpentInternetModel(ref DataBase_service db, ClientDTO client, NumberDTO number)
        {
            _YourNumber = "Ваш номер: " + number.Number;
            _viewId = Guid.NewGuid();
            _BaseCommand = new RelayCommand(Action, i => true);
            DB = db;

            this.client = client;
            this.number = number;
        }
        public void Action(object parameter)
        {
            DB.UserSpentInternet(number.ID, int.Parse(_InternetAmount));
            MessageBox.Show("Произошла успешно!", "Трата интернета...", MessageBoxButton.OK);
            WindowManager.CloseWindow(ViewID);//Close();
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
    class ClientWindow_PasswordChangeModel : INotifyPropertyChanged
    {
        ClientDTO client;

        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private DataBase_service DB;

        private RelayCommand _BaseAction;
        public RelayCommand BaseAction { get { return _BaseAction; } }

        public ClientWindow_PasswordChangeModel(ref DataBase_service db, ClientDTO client)
        {
            this.client = client;
            _viewId = Guid.NewGuid();
            _BaseAction = new RelayCommand(Change, i => true);
            DB = db;
        }
        public void Change(object parameter)
        {
            if (_Password.Length > 0) {
                DB.PasswordChange(client.ID, _Password);

                MessageBox.Show("Произошла успешно!", "Смена пароля...", MessageBoxButton.OK);
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
