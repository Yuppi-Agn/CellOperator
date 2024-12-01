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
    class AdministatorViewModel : INotifyPropertyChanged
    {
        DataBase_service Database;
        Methods_service methods;

        private RelayCommand _GenerateBaseCommand;
        public RelayCommand GenerateBaseCommand { get { return _GenerateBaseCommand; } }

        /*private string _Field1;
        public string Field1
        {
            get { return _Field1; }
            set { _Field1 = value; NotifyPropertyChanged(nameof(Field1)); }
        }

        private string _Field2;
        public string Field2
        {
            get { return _Field2; }
            set { _Field2 = value; NotifyPropertyChanged(nameof(Field2)); }
        }

        private string _Field3;
        public string Field3
        {
            get { return _Field3; }
            set { _Field3 = value; NotifyPropertyChanged(nameof(Field3)); }
        }

        private string _Field4;
        public string Field4
        {
            get { return _Field4; }
            set { _Field4 = value; NotifyPropertyChanged(nameof(Field4)); }
        }

        private string _Field5;
        public string Field5
        {
            get { return _Field5; }
            set { _Field5 = value; NotifyPropertyChanged(nameof(Field5)); }
        }

        private string _Field6;
        public string Field6
        {
            get { return _Field6; }
            set { _Field6 = value; NotifyPropertyChanged(nameof(Field6)); }
        }

        private string _Field7;
        public string Field7
        {
            get { return _Field7; }
            set { _Field7 = value; NotifyPropertyChanged(nameof(Field7)); }
        }

        private string _Field8;
        public string Field8
        {
            get { return _Field8; }
            set { _Field8 = value; NotifyPropertyChanged(nameof(Field8)); }
        }

        private string _Field9;
        public string Field9
        {
            get { return _Field9; }
            set { _Field9 = value; NotifyPropertyChanged(nameof(Field9)); }
        }

        private string _Field10;
        public string Field10
        {
            get { return _Field10; }
            set { _Field10 = value; NotifyPropertyChanged(nameof(Field10)); }
        }

        private string _Field11;
        public string Field11
        {
            get { return _Field11; }
            set { _Field11 = value; NotifyPropertyChanged(nameof(Field11)); }
        }

        private string _FieldName1;
        public string FieldName1
        {
            get { return _FieldName1; }
            set { _FieldName1 = value; NotifyPropertyChanged("FieldName1"); }
        }

        private string _FieldName2;
        public string FieldName2
        {
            get { return _FieldName2; }
            set { _FieldName2 = value; NotifyPropertyChanged("FieldName2"); }
        }

        private string _FieldName3;
        public string FieldName3
        {
            get { return _FieldName3; }
            set { _FieldName3 = value; NotifyPropertyChanged("FieldName3"); }
        }

        private string _FieldName4;
        public string FieldName4
        {
            get { return _FieldName4; }
            set { _FieldName4 = value; NotifyPropertyChanged("FieldName4"); }
        }

        private string _FieldName5;
        public string FieldName5
        {
            get { return _FieldName5; }
            set { _FieldName5 = value; NotifyPropertyChanged("FieldName5"); }
        }

        private string _FieldName6;
        public string FieldName6
        {
            get { return _FieldName6; }
            set { _FieldName6 = value; NotifyPropertyChanged("FieldName6"); }
        }

        private string _FieldName7;
        public string FieldName7
        {
            get { return _FieldName7; }
            set { _FieldName7 = value; NotifyPropertyChanged("FieldName7"); }
        }

        private string _FieldName8;
        public string FieldName8
        {
            get { return _FieldName8; }
            set { _FieldName8 = value; NotifyPropertyChanged(nameof(FieldName8)); }
        }

        private string _FieldName9;
        public string FieldName9
        {
            get { return _FieldName9; }
            set { _FieldName9 = value; NotifyPropertyChanged(nameof(FieldName9)); }
        }

        private string _FieldName10;
        public string FieldName10
        {
            get { return _FieldName10; }
            set { _FieldName10 = value; NotifyPropertyChanged(nameof(FieldName10)); }
        }

        private string _FieldName11;
        public string FieldName11
        {
            get { return _FieldName11; }
            set { _FieldName11 = value; NotifyPropertyChanged(nameof(FieldName11)); }
        }

        private bool _FieldVisible1;
        public bool FieldVisible1
        {
            get { return _FieldVisible1; }
            set { _FieldVisible1 = value; NotifyPropertyChanged(nameof(FieldVisible1)); }
        }

        private bool _FieldVisible2;
        public bool FieldVisible2
        {
            get { return _FieldVisible2; }
            set { _FieldVisible2 = value; NotifyPropertyChanged(nameof(FieldVisible2)); }
        }
        private bool _FieldVisible3;
        public bool FieldVisible3
        {
            get { return _FieldVisible3; }
            set { _FieldVisible3 = value; NotifyPropertyChanged(nameof(FieldVisible3)); }
        }
        private bool _FieldVisible4;
        public bool FieldVisible4
        {
            get { return _FieldVisible4; }
            set { _FieldVisible4 = value; NotifyPropertyChanged(nameof(FieldVisible4)); }
        }
        private bool _FieldVisible5;
        public bool FieldVisible5
        {
            get { return _FieldVisible5; }
            set { _FieldVisible5 = value; NotifyPropertyChanged(nameof(FieldVisible5)); }
        }
        private bool _FieldVisible6;
        public bool FieldVisible6
        {
            get { return _FieldVisible6; }
            set { _FieldVisible6 = value; NotifyPropertyChanged(nameof(FieldVisible6)); }
        }
        private bool _FieldVisible7;
        public bool FieldVisible7
        {
            get { return _FieldVisible7; }
            set { _FieldVisible7 = value; NotifyPropertyChanged(nameof(FieldVisible7)); }
        }

        private bool _FieldVisible8;
        public bool FieldVisible8
        {
            get { return _FieldVisible8; }
            set { _FieldVisible8 = value; NotifyPropertyChanged(nameof(FieldVisible8)); }
        }
        private bool _FieldVisible9;
        public bool FieldVisible9
        {
            get { return _FieldVisible9; }
            set { _FieldVisible9 = value; NotifyPropertyChanged(nameof(FieldVisible9)); }
        }
        private bool _FieldVisible10;
        public bool FieldVisible10
        {
            get { return _FieldVisible10; }
            set { _FieldVisible10 = value; NotifyPropertyChanged(nameof(FieldVisible10)); }
        }
        private bool _FieldVisible11;
        public bool FieldVisible11
        {
            get { return _FieldVisible11; }
            set { _FieldVisible11 = value; NotifyPropertyChanged(nameof(FieldVisible11)); }
        }


        private RelayCommand _AddCommand;
        public RelayCommand AddCommand { get { return _AddCommand; } }

        private RelayCommand _ChangeCommand;
        public RelayCommand ChangeCommand { get { return _ChangeCommand; } }

        private RelayCommand _DeleteCommand;
        public RelayCommand DeleteCommand { get { return _DeleteCommand; } }

        private RelayCommand _UpdateCommand;
        public RelayCommand UpdateCommand { get { return _UpdateCommand; } }

        private readonly RelayCommand _TabChangedCommand;
        public RelayCommand TabChangedCommand { get { return _TabChangedCommand; } }*/

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
            //Datagrid8.ItemsSource = methods.InDB();
            //Datagrid9.ItemsSource = methods.Other();
            //Datagrid1.row

            /*_ChangeCommand = new RelayCommand(TableChanged, i => true);
            _UpdateCommand = new RelayCommand(UpdateClass, i => true);*/
            _GenerateBaseCommand = new RelayCommand(GenerateBase, i => true);
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
        /*public void ChangeTable(int TabIndex)
        {
            SetChangedToNull();
            if (TabIndex == 0)
            {
                FieldName1 = "Имя";
                FieldName2 = "Фамилия";
                FieldName3 = "Отчество";
                FieldName4 = "Паспорт";
                FieldName5 = "Логин";
                FieldName6 = "Пароль";
            }
            else if (TabIndex == 1)
            {
                FieldName1 = "Название организации";
                FieldName2 = "Индивидуальный номер";
                FieldName3 = "Адрес";
                FieldName4 = "Логин";
                FieldName5 = "пароль";
            }
            HideChangedNull();
        }
        public void SetChangedToNull()
        {
            FieldName1 = "";
            FieldName2 = "";
            FieldName3 = "";
            FieldName4 = "";
            FieldName5 = "";
            FieldName6 = "";
            FieldName7 = "";
            FieldName8 = "";
            FieldName9 = "";
            FieldName10 = "";
            FieldName11= "";

            Field1 = "";
            Field2 = "";
            Field3 = "";
            Field4 = "";
            Field5 = "";
            Field6 = "";
            Field7 = "";
            Field8 = "";
            Field9 = "";
            Field10 = "";
            Field11 = "";
        }
        public void HideChangedNull()
        {
            if(FieldName1 == "") FieldVisible1=false; else FieldVisible1=true;
            if (FieldName2 == "") FieldVisible2 = false; else FieldVisible2 = true;
            if (FieldName3 == "") FieldVisible3 = false; else FieldVisible3 = true;
            if (FieldName4 == "") FieldVisible4 = false; else FieldVisible4 = true;
            if (FieldName5 == "") FieldVisible5 = false; else FieldVisible5 = true;
            if (FieldName6 == "") FieldVisible6 = false; else FieldVisible6 = true;
            if (FieldName7 == "") FieldVisible7 = false; else FieldVisible7 = true;
            if (FieldName8 == "") FieldVisible8 = false; else FieldVisible8 = true;
            if (FieldName9 == "") FieldVisible9 = false; else FieldVisible9 = true;
            if (FieldName10 == "") FieldVisible10 = false; else FieldVisible10 = true;
            if (FieldName11 == "") FieldVisible11 = false; else FieldVisible11 = true;
        }
        public void TableChanged(object parameter) {
            TabControl Tab = parameter as TabControl;
            if (Tab == null) return;
            ChangeTable(Tab.SelectedIndex);
        }

        public void UpdateClass(object parameter)
        {
            TabControl Tab = parameter as TabControl;
            if (Tab == null) return;
            int Index = Tab.SelectedIndex;

            if (Index == 0)
            {
                Client_IndividualDTO DTO = new Client_IndividualDTO();

                DTO.FirstName = Field1;
                DTO.LastName = Field2;
                DTO.Patronymic = Field3;
                DTO.Passport = Field4;
                DTO.Login = Field5;
                DTO.Password = Field6;

                Database.UpdateIndividualClient(DTO);
            }
            if (Index == 1)
            {
                Client_LegalEntityDTO DTO = new Client_LegalEntityDTO();

                DTO.OrganizationName = Field1;
                DTO.IdividualTaxpayerNumber = Field2;
                DTO.Adress = Field3;
                DTO.MSRN = "1234";
                DTO.Password = Field4;
                DTO.Login = Field5;
                Database.UpdateLegalEntityClient(DTO);
            }
            SetChangedToNull();
            HideChangedNull();
        }*/
        public void GenerateBase(object parameter)
        {
            Database.GenerateMembers(700);
            LoadMembers();
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
