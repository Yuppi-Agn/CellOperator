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

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.OpenXml;

namespace CellOperator.MVVM.ViewModels.Administator
{
    public class AdministratorReportExpensesModel : INotifyPropertyChanged
    {
        IDialogService DialogService;
        IFileService FileService;
        IDialogService dialogServiceSVG;
        IFILESVGService FILESVGService;

        Methods_service Methods;

        /// 
        private bool _IsIndividual;
        public bool IsIndividual
        {
            get { return _IsIndividual; }
            set
            {
                _IsIndividual = value;
                NotifyPropertyChanged("IsIndividual");
                NotifyPropertyChanged("IsLegal");
            }
        }
        public bool IsLegal
        {
            get { return !_IsIndividual; }
            set
            {
                _IsIndividual = !value;
                NotifyPropertyChanged("IsIndividual");
                NotifyPropertyChanged("IsLegal");
            }
        }
        /// 

        List<int> _Years;
        public List<int> Years
        {
            get { return _Years; }
            set
            {
                _Years = value;
                NotifyPropertyChanged("Years");
            }
        }
        int _SelectedYear;
        public int SelectedYear
        {
            get { return _SelectedYear; }
            set
            {
                _SelectedYear = value;
                NotifyPropertyChanged("SelectedYear");
                CreateGraph();
            }
        }
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
        private PlotModel _GraphMain;
        public PlotModel GraphMain
        {
            get { return _GraphMain; }
            set
            {
                _GraphMain = value;
                NotifyPropertyChanged("GraphMain");
            }
        }

        private List<AdministratorReportExpences> MyTable;
        private RelayCommand _SaveCSVAction;
        public RelayCommand SaveCSVAction { get { return _SaveCSVAction; } }
        private RelayCommand _SaveSVGAction;
        public RelayCommand SaveSVGAction { get { return _SaveSVGAction; } }
        public ObservableCollection<AdministratorReportExpences> Table { get; set; }
        public AdministratorReportExpensesModel(ref Methods_service Methods, IDialogService DialogService, IFileService FileService, IDialogService dialogServiceSVG, IFILESVGService FILESVGService)
        {
            this.dialogServiceSVG = dialogServiceSVG;
            this.FILESVGService = FILESVGService;

            this.Methods = Methods;
            this.MyTable = Methods.Report_AllExpences();
            this.DialogService = DialogService;
            this.FileService = FileService;

            this.Table = new ObservableCollection<AdministratorReportExpences>();
            for (int i = 0; i < MyTable.Count; i++) this.Table.Add(MyTable[i]);
            _SaveCSVAction = new RelayCommand(SaveCSV, i => true);
            _SaveSVGAction = new RelayCommand(SaveSVG, i => true);
            FirstDate = FromDate = Methods.Report_Expenses_FirstDate();
            LastDate = ToDate = Methods.Report_Expenses_LastDate();

            _Years = new List<int>();
            for (int i= FirstDate.Year; i<= LastDate.Year; i++)
            {
                _Years.Add(i);
            }
            CreateGraph();
        }
        private void CreateGraph()
        {
            /*
             * 1-Звонки в тарифе | бесплатно
             * 2-СМС в тарифе | бесплатно
             * 3-Трафик в тарифе | бесплатно
             * 
             * 4-Звонки вне тарифа | платно
             * 5-СМС вне тарифа | платно
             * 6-Трафик вне тарифа | платно
             * 
             * 8-Подключение/смена тарифа | платно
             * 9-Подключение услуги | платно
             * 
             * 10-Плата за услугу в месяц | платно
             * 11-Плата за тариф в месяц | платно
             */
            PlotModel Thisgrap = new PlotModel();
            var SortedTable = MyTable.OrderBy(p => p.Date);
            if (Thisgrap == null) Thisgrap = new PlotModel();
            Thisgrap.Title = "Прибыль";
            Thisgrap.SubtitleFont = Thisgrap.TitleFont;
            Thisgrap.IsLegendVisible = true;

            for (int TypeID = 4; TypeID <= 11; TypeID++)
            {
                
                var series = new LineSeries();
                series.Color = OxyColor.FromRgb(0, 0, 0);
                switch (TypeID)
                {
                    default:
                    case 1:
                        //series.Title = "Звонки в тарифе";
                        TypeID++;
                        break;
                    case 2:
                        //series.Title = "СМС в тарифе";
                        TypeID++;
                        break;
                    case 3:
                        //series.Title = "Трафик в тарифе";
                        TypeID++;
                        break;
                    case 4:
                        series.Title = "Звонки вне тарифа";
                        //series.Color = OxyColor.FromRgb(255,255,255);
                        break;
                    case 5:
                        series.Title = "СМС вне тарифа";
                        //series.Color = OxyColor.FromRgb(255, 255, 255);
                        break;
                    case 6:
                        series.Title = "Трафик вне тарифа";
                        //series.Color = OxyColor.FromRgb(255, 255, 255);
                        break;
                    case 7:
                        TypeID++;
                        break;
                    case 8:
                        series.Title = "Подключение/смена тарифа";
                        //series.Color = OxyColor.FromRgb(255, 255, 255);
                        break;
                    case 9:
                        series.Title = "Подключение услуги";
                        //series.Color = OxyColor.FromRgb(255, 255, 255);
                        break;
                    case 10:
                        series.Title = "Плата за услугу в месяц";
                        //series.Color = OxyColor.FromRgb(255, 255, 255);
                        break;
                    case 11:
                        series.Title = "Плата за тариф в месяц";
                        //series.Color = OxyColor.FromRgb(255, 255, 255);
                        break;
                }

                var ThisType = MyTable.Where(p => p.type == TypeID && p.Date.Value.Year == Years[_SelectedYear]).ToList();
                for (int Mounth = FirstDate.Month; Mounth <= LastDate.Month; Mounth++)
                {
                    var ThisMounthType = ThisType.Where(p => p.Date.Value.Month == Mounth).ToList();
                    double sum = 0;
                    foreach (var item in ThisMounthType)
                    {
                        sum += (double)item.Expense;
                    }
                    series.Points.Add(new DataPoint(Mounth, sum));
                }

                Thisgrap.Series.Add(series);
                //XmlWriterBase xmlWriter = new XmlWriterBase;
                //XmlWriterBase(GraphMain)
                /*if (DialogService.SaveFileDialog())
                {
                    var Path = DialogService.FilePath;
                    FileService.Save(GraphMain, Path);
                }*/
            }
            GraphMain = Thisgrap;
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
        void SaveSVG(object parameter)
        {
            if (dialogServiceSVG.SaveFileDialog())
            {
                var Path = dialogServiceSVG.FilePath;
                FILESVGService.Save(GraphMain, Path);
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
