using System;
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

namespace CellOperator.MVVM.ViewModels
{
    class ClientWindow_ReportCallingModel
    {
        public ObservableCollection<Methods.Report_Calling> Table { get; set; }
        public ClientWindow_ReportCallingModel( List<Methods.Report_Calling> Table)
        {
            this.Table = new ObservableCollection<Methods.Report_Calling>();
            for (int i = 0; i < Table.Count; i++) this.Table.Add(Table[i]);
        }
    }
    class ClientWindow_ReportSMSModel
    {
        public ObservableCollection<Methods.Report_SMS> Table { get; set; }
        public ClientWindow_ReportSMSModel(List<Methods.Report_SMS> Table)
        {
            this.Table = new ObservableCollection<Methods.Report_SMS>();
            for (int i = 0; i < Table.Count; i++) this.Table.Add(Table[i]);
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
            for (int i = 0; i < Table.Count; i++) {
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
        public ClientWindow_SendSMSModel(ref DataBase_service db, ClientDTO client, NumberDTO number)
        {
            _viewId = Guid.NewGuid();
            _BaseCommand = new RelayCommand(Action, i => true);
            DB = db;

            this.client = client;
            this.number = number;
        }
        public void Action(object parameter)
        {
            DB.UserSendSMS(client.ID, "+7980"+_OtherNumber, SMSData);
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
        public ClientWindow_MakeCallModel(ref DataBase_service db, ClientDTO client, NumberDTO number)
        {
            _viewId = Guid.NewGuid();
            _BaseCommand = new RelayCommand(Action, i => true);
            DB = db;

            this.client = client;
            this.number = number;
        }
        public void Action(object parameter)
        {
            DB.UserMakeCall(client.ID, "+7980"+_OtherNumber, int.Parse(_CallDuration));
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
        public ClientWindow_SpentInternetModel(ref DataBase_service db, ClientDTO client, NumberDTO number)
        {
            _viewId = Guid.NewGuid();
            _BaseCommand = new RelayCommand(Action, i => true);
            DB = db;

            this.client = client;
            this.number = number;
        }
        public void Action(object parameter)
        {
            DB.UserSpentInternet(client.ID, int.Parse(_InternetAmount));
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
}
