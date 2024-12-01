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
using BLL.Services;

namespace CellOperator
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow_ReportCalling.xaml
    /// </summary>
    public partial class ClientWindow_ReportCalling : Window
    {
        public ClientWindow_ReportCalling(List<Methods.Report_Calling> Table)
        {
            InitializeComponent();
            DataContext = new ClientWindow_ReportCallingModel(Table);
        }
    }
}
