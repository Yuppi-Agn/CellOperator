using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using BLL.Services;
using BLL.Models;

namespace CellOperator.MVVM.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        DataBase_service Database;
        Methods_service methods;
        ClientDTO Client;
        List<NumberDTO> Numbers;

        string SNameBox, STarif;
        List<string> SNumbersToVisible;

        decimal SMoney;

        int SSelectedNumber;
        public int SelectedNumber
        {
            get { return SSelectedNumber; }
            set {
                SSelectedNumber = value;
                NotifyPropertyChanged("SelectedNumber");
                NumberChanged(SSelectedNumber);
                }
        }

        public string NameBox
        {
            get { return SNameBox; }
            set { SNameBox = value; NotifyPropertyChanged("NameBox"); }
        }
        public decimal Money
        {
            get { return SMoney; }
            set { SMoney = value; NotifyPropertyChanged("Money"); }
        }
        public string Tarif
        {
            get { return STarif; }
            set { STarif = value; NotifyPropertyChanged("Tarif"); }
        }
        public List<string> NumbersToVisible
        {
            get { return SNumbersToVisible; 
                    }
            set { SNumbersToVisible = value; NotifyPropertyChanged("NumbersToVisible"); }
        }

        private RelayCommand _SMSReport;
        public RelayCommand SMSReport { get { return _SMSReport; } }
        private RelayCommand _CallingReport;
        public RelayCommand CallingReport { get { return _CallingReport; } }
        private RelayCommand _TarifChangeAction;
        public RelayCommand TarifChangeAction { get { return _TarifChangeAction; } }
        private RelayCommand _BuyNumChangeAction;
        public RelayCommand BuyNumChangeAction { get { return _BuyNumChangeAction; } }
        private RelayCommand _AddMoneyAction;
        public RelayCommand AddMoneyAction { get { return _AddMoneyAction; } }

        public ClientViewModel(ClientDTO client) {
            Client = client;
            Database = new DataBase_service();
            methods = new Methods_service();

            UpdateNums();
            SNameBox = Database.FindName(Client);
                        
            SSelectedNumber = 0;
            NumberChanged(0);

            _viewId = Guid.NewGuid();
            _CallingReport = new RelayCommand(Show_Calling_Report, i => true);
            _SMSReport = new RelayCommand(Show_SMS_Report, i => true);
            _TarifChangeAction = new RelayCommand(Show_TarifChange, i => true);
            _BuyNumChangeAction = new RelayCommand(Show_NumberBuy, i => true);
            _AddMoneyAction = new RelayCommand(AddMoney, i => true);
        }

        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Show_Calling_Report(object parameter)
        {
            ClientWindow_ReportCalling taskWindow = new ClientWindow_ReportCalling(methods.Report_Calling(Client.ID));
            taskWindow.Show();
        }
        public void Show_SMS_Report(object parameter)
        {
            ClientWindow_ReportSMS taskWindow = new ClientWindow_ReportSMS(methods.Report_SMS(Client.ID));
            taskWindow.Show();
        }
        public void Show_TarifChange(object parameter)
        {
            ClientTarifChange taskWindow = new ClientTarifChange(ref Database, Client, Numbers[SelectedNumber]);
            taskWindow.ShowDialog();
            UpdateNums();
            NumberChanged(SSelectedNumber);
        }
        public void Show_NumberBuy(object parameter)
        {
            ClientWindow_BuyNumber taskWindow = new ClientWindow_BuyNumber(ref Database, Client);
            taskWindow.ShowDialog();
            UpdateNums();
            NumberChanged(SSelectedNumber);
        }
        private void NumberChanged(int NumID)
        {
            if (NumID == -1) NumID = SSelectedNumber = Numbers.Count - 1;
            if (Numbers.Count > 0)
            {
                Money = Numbers[NumID].Bill;
                Tarif = Numbers[NumID].Tarif;
            }
        }
        private void UpdateNums()
        {
            Numbers = Database.FindNumbers(Client);
            List<string> ThisNumbersToVisible = new List<string>();
            for (int i = 0; i < Numbers.Count(); i++) ThisNumbersToVisible.Add(Numbers[i].Number);

            NumbersToVisible = ThisNumbersToVisible;
        }
        private void AddMoney(object parameter)
        {
            Database.AddMoney(Numbers[SelectedNumber].ID, (decimal) 100);
            UpdateNums();
            NumberChanged(SelectedNumber);
        }
    }
}
