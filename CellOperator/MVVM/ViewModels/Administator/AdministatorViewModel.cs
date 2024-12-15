using System;
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
    public class AdministatorViewModel : INotifyPropertyChanged
    {
        int _SelectedClient;
        public int SelectedClient
        {
            get { return _SelectedClient; }
            set
            {
                _SelectedClient = value;
                NotifyPropertyChanged("SelectedClient");
            }
        }
        int _SelectedPage;
        public int SelectedPage
        {
            get { return _SelectedPage; }
            set
            {
                _SelectedPage = value;
                NotifyPropertyChanged("SelectedPage");
            }
        }
        DataBase_service Database;
        Methods_service methods;

        /*private RelayCommand _GenerateBaseCommand;
        public RelayCommand GenerateBaseCommand { get { return _GenerateBaseCommand; } }*/
        private RelayCommand _ChangePasswordCommand;
        public RelayCommand ChangePasswordCommand { get { return _ChangePasswordCommand; } }
        private RelayCommand _GenerateExpencesReportCommand;
        public RelayCommand GenerateExpencesReportCommand { get { return _GenerateExpencesReportCommand; } }
        private RelayCommand _MountlySpentCommand;
        public RelayCommand MountlySpentCommand { get { return _MountlySpentCommand; } }
        public ObservableCollection<Client_IndividualDTO> Client_Individuals { get; set; }
        public ObservableCollection<Client_LegalEntityDTO> Client_LegalEntitys { get; set; }
        public ObservableCollection<NumberDTO> Numbers { get; set; }
        public ObservableCollection<TarifDTO> Tarifs { get; set; }
        public ObservableCollection<Tarif_HistoryDTO> Tarifs_history { get; set; }
        public ObservableCollection<SMSDTO> SMS { get; set; }
        public ObservableCollection<CallingDTO> Callings { get; set; }
        public ObservableCollection<ServiceDTO> Services { get; set; }
        public ObservableCollection<Service_ConnectionDTO> Service_Connection { get; set; }
        public AdministatorViewModel()
        {
            loadData();
        }
        private void loadData()
        {
            if (Database == null) Database = new DataBase_service();
            if (methods == null) methods = new Methods_service();

            Client_Individuals = new ObservableCollection<Client_IndividualDTO>();
            Client_LegalEntitys = new ObservableCollection<Client_LegalEntityDTO>();
            Numbers = new ObservableCollection<NumberDTO>();
            Tarifs = new ObservableCollection<TarifDTO>();
            Tarifs_history = new ObservableCollection<Tarif_HistoryDTO>();
            SMS = new ObservableCollection<SMSDTO>();
            Callings = new ObservableCollection<CallingDTO>();

            Services = new ObservableCollection<ServiceDTO>();
            Service_Connection = new ObservableCollection<Service_ConnectionDTO>();

            LoadMembers();

            _ChangePasswordCommand = new RelayCommand(ChangePassword, i => true);
            _MountlySpentCommand = new RelayCommand(MountlySpent, i => true);
            _GenerateExpencesReportCommand = new RelayCommand(GenerateExpences, i => true);
        }
        private void LoadMembers()
        {
            var S1 = Database.GetAllIndividualClients();
            var S2 = Database.GetAllLegalEntityClients();
            var S3 = Database.GetAllNumbers();
            var S4 = Database.GetAllTarifs();
            var S5 = Database.GetAllSMS();
            var S6 = Database.GetAllCallings();

            var S7 = Database.GetAllServices();
            var S8 = Database.GetAllService_Connection();
            var S9 = Database.GetAllTarif_history();

            Client_Individuals.Clear();
            Client_LegalEntitys.Clear();
            Numbers.Clear();
            Tarifs.Clear();
            Tarifs_history.Clear();

            SMS.Clear();
            Callings.Clear();

            Services.Clear();
            Service_Connection.Clear();

            for (int i = 0; i < S1.Count(); i++) Client_Individuals.Add(S1[i]);
            for (int i = 0; i < S2.Count(); i++) Client_LegalEntitys.Add(S2[i]);
            for (int i = 0; i < S3.Count(); i++) Numbers.Add(S3[i]);
            for (int i = 0; i < S4.Count(); i++) Tarifs.Add(S4[i]);
            for (int i = 0; i < S9.Count(); i++) Tarifs_history.Add(S9[i]);

            for (int i = 0; i < S5.Count(); i++) SMS.Add(S5[i]);
            for (int i = 0; i < S6.Count(); i++) Callings.Add(S6[i]);

            for (int i = 0; i < S7.Count(); i++) Services.Add(S7[i]);
            for (int i = 0; i < S8.Count(); i++) Service_Connection.Add(S8[i]);
        }
        public void GenerateBase(object parameter)
        {
            Database.GenerateMembers(700);
            LoadMembers();
        }
        public void ChangePassword(object parameter)
        {
            ClientDTO Client;
            if(SelectedPage==0) Client=Database.GetClient((int)Client_Individuals[SelectedClient].ClientID);
            else Client = Database.GetClient((int)Client_LegalEntitys[SelectedClient].ClientID);
            if (Client != null)
            {
                var taskWindow = new ClientChangePassword(ref Database, Client);
                taskWindow.ShowDialog();
                LoadMembers();
            }
        }
        public void MountlySpent(object parameter)
        {
            Database.MonthSpent();
            LoadMembers();
        }
        public void GenerateExpences(object parameter)
        {
            var taskWindow = new AdministratorReportExpenses(ref methods);
            taskWindow.ShowDialog();
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
