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
        private RelayCommand _ExpensesReport;
        public RelayCommand ExpensesReport { get { return _ExpensesReport; } }
        private RelayCommand _TarifChangeAction;
        public RelayCommand TarifChangeAction { get { return _TarifChangeAction; } }
        private RelayCommand _ServiceChangeAction;
        public RelayCommand ServiceChangeAction { get { return _ServiceChangeAction; } }
        private RelayCommand _BuyNumChangeAction;
        public RelayCommand BuyNumChangeAction { get { return _BuyNumChangeAction; } }
        private RelayCommand _AddMoneyAction;
        public RelayCommand AddMoneyAction { get { return _AddMoneyAction; } }
        private RelayCommand _SendSMSAction;
        public RelayCommand SendSMSAction { get { return _SendSMSAction; } }
        private RelayCommand _MakeCallAction;
        public RelayCommand MakeCallAction { get { return _MakeCallAction; } }
        private RelayCommand _SpentInternetAction;
        public RelayCommand SpentInternetAction { get { return _SpentInternetAction; } }
        private RelayCommand _ChangePasswordAction;
        public RelayCommand ChangePasswordAction { get { return _ChangePasswordAction; } }

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
            _ExpensesReport = new RelayCommand(Show_Expenses_Report, i => true);

            _TarifChangeAction = new RelayCommand(Show_TarifChange, i => true);
            _ServiceChangeAction = new RelayCommand(Show_ServiceChange, i => true);
            _BuyNumChangeAction = new RelayCommand(Show_NumberBuy, i => true);
            _AddMoneyAction = new RelayCommand(AddMoney, i => true);

            _SendSMSAction = new RelayCommand(SendSMS, i => true);
            _MakeCallAction = new RelayCommand(MakeCall, i => true);
            _SpentInternetAction = new RelayCommand(SpentInternet, i => true);

            _ChangePasswordAction = new RelayCommand(ChangePassword, i => true);
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
            if (Numbers.Count - 1 < SelectedNumber) return;
            ClientWindow_ReportCalling taskWindow = new ClientWindow_ReportCalling(methods.Report_Calling(Numbers[SelectedNumber].ID));
            taskWindow.Show();
        }
        public void Show_SMS_Report(object parameter)
        {
            if (Numbers.Count - 1 < SelectedNumber) return;
            ClientWindow_ReportSMS taskWindow = new ClientWindow_ReportSMS(methods.Report_SMS(Numbers[SelectedNumber].ID));
            taskWindow.Show();
        }
        public void Show_Expenses_Report(object parameter)
        {
            if (Numbers.Count - 1 < SelectedNumber) return;
            ClientWindow_ReportExpenses taskWindow = new ClientWindow_ReportExpenses(methods.Report_Expenses(Numbers[SelectedNumber].ID));
            taskWindow.Show();
        }
        public void Show_TarifChange(object parameter)
        {
            if (Numbers.Count - 1 < SelectedNumber) return;
            ClientTarifChange taskWindow = new ClientTarifChange(ref Database, Client, Numbers[SelectedNumber]);
            taskWindow.ShowDialog();
            UpdateNums();
            NumberChanged(SSelectedNumber);
        }
        public void Show_ServiceChange(object parameter)
        {
            if (Numbers.Count - 1 < SelectedNumber) return;
            ClientServiceChange taskWindow = new ClientServiceChange(ref Database, Client, Numbers[SelectedNumber]);
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
            if (Numbers.Count - 1 < SelectedNumber) return;
            Database.AddMoney(Numbers[SelectedNumber].ID, (decimal) 100);
            UpdateNums();
            NumberChanged(SelectedNumber);
        }
        public void SendSMS(object parameter)
        {
            if (Numbers.Count - 1 < SelectedNumber) return;
            var taskWindow = new ClientWindow_SendSMS(ref Database, Client, Numbers[SelectedNumber]);
            taskWindow.ShowDialog();
            UpdateNums();
            NumberChanged(SSelectedNumber);
        }
        public void MakeCall(object parameter)
        {
            if (Numbers.Count - 1 < SelectedNumber) return;
            var taskWindow = new ClientWindow_MakeCall(ref Database, Client, Numbers[SelectedNumber]);
            taskWindow.ShowDialog();
            UpdateNums();
            NumberChanged(SSelectedNumber);
        }
        public void SpentInternet(object parameter)
        {
            if (Numbers.Count - 1 < SelectedNumber) return;
            var taskWindow = new ClientWindow_SpentInternet(ref Database, Client, Numbers[SelectedNumber]);
            taskWindow.ShowDialog();
            UpdateNums();
            NumberChanged(SSelectedNumber);
        }
        public void ChangePassword(object parameter)
        {
            var taskWindow = new ClientChangePassword(ref Database, Client);
            taskWindow.ShowDialog();
        }
    }
}
