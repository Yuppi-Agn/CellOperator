using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using BLL.Models;
using CellOperator.MVVM.ViewModels;

namespace CellOperator
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow_ReportSMS.xaml
    /// </summary>
    public partial class ClientWindow_ReportSMS : Window
    {
        public ClientWindow_ReportSMS(List<Methods.Report_SMS> Table)
        {
            InitializeComponent();
            DataContext = new ClientWindow_ReportSMSModel(Table);
        }
    }
}
