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

using System.Windows.Controls;

using CellOperator.MVVM.Services;

namespace CellOperator.MVVM.ViewModels.Administator
{
    public class AdministratorReportExpensesModel
    {
        IDialogService DialogService;
        IFileService FileService;
        Methods_service Methods;
        private DateTime _FromDate;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set
            {
                _FromDate = value;
                NotifyPropertyChanged("FromDate");
                TableChanged();
            }
        }
        private DateTime _ToDate;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set
            {
                _ToDate = value;
                NotifyPropertyChanged("ToDate");
                TableChanged();
            }
        }
        private DateTime _FirstDate;
        public DateTime FirstDate
        {
            get { return _FirstDate; }
            set
            {
                _FirstDate = value;
                NotifyPropertyChanged("FirstDate");
            }
        }
        private DateTime _LastDate;
        public DateTime LastDate
        {
            get { return _LastDate; }
            set
            {
                _LastDate = value;
                NotifyPropertyChanged("LastDate");
            }
        }

        private List<AdministratorReportExpences> MyTable;
        private RelayCommand _SaveCSVAction;
        public RelayCommand SaveCSVAction { get { return _SaveCSVAction; } }
        public ObservableCollection<AdministratorReportExpences> Table { get; set; }
        public AdministratorReportExpensesModel(ref Methods_service Methods, IDialogService DialogService, IFileService FileService)
        {
            this.Methods = Methods;
            this.MyTable = Methods.Report_AllExpences();
            this.DialogService = DialogService;
            this.FileService = FileService;

            this.Table = new ObservableCollection<AdministratorReportExpences>();
            for (int i = 0; i < MyTable.Count; i++) this.Table.Add(MyTable[i]);
            _SaveCSVAction = new RelayCommand(SaveCSV, i => true);
            FirstDate = FromDate = Methods.Report_Expenses_FirstDate();
            LastDate = ToDate = Methods.Report_Expenses_LastDate();
        }
        private void TableChanged()
        {
            MyTable = Methods.Report_AllExpences(FromDate, ToDate);
            if (MyTable != null)
            {
                Table.Clear();
                for (int i = 0; i < MyTable.Count; i++) this.Table.Add(MyTable[i]);
            }
            else Table.Clear();
        }
        void SaveCSV(object parameter)
        {
            if (DialogService.SaveFileDialog())
            {
                var Path = DialogService.FilePath;
                FileService.Save(MyTable, Path);
            }
            else return;
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
