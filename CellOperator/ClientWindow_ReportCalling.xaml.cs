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
using CellOperator.MVVM.Services;

namespace CellOperator
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow_ReportCalling.xaml
    /// </summary>
    public partial class ClientWindow_ReportCalling : Window
    {
        public ClientWindow_ReportCalling(int NumId, ref Methods_service Methods)
        {
            InitializeComponent();

            FileService_csv fileService = new FileService_csv();
            WindowsDilalogService dilalogService = new WindowsDilalogService("Введите название", ".csv", "Таблица формата csv");

            DataContext = new ClientWindow_ReportCallingModel(NumId, ref Methods, dilalogService, fileService);
        }
    }
}
