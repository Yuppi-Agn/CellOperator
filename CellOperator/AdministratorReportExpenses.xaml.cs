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
using BLL.Services;
using CellOperator.MVVM.ViewModels;
using CellOperator.MVVM.Services;
using CellOperator.MVVM.ViewModels.Administator;

namespace CellOperator
{
    /// <summary>
    /// Логика взаимодействия для AdministratorReportExpenses.xaml
    /// </summary>
    public partial class AdministratorReportExpenses : Window
    {
        public AdministratorReportExpenses( ref Methods_service Methods)
        {
            InitializeComponent();

            FileService_csv fileService = new FileService_csv();
            WindowsDilalogService dilalogService = new WindowsDilalogService("Введите название", ".csv", "Таблица формата csv");

            FileService_svg fileService_Svg = new FileService_svg();
            WindowsDilalogService dilalogService_Svg = new WindowsDilalogService("Введите название", ".svg", "Svg");

            DataContext = new AdministratorReportExpensesModel( ref Methods,
                dilalogService, fileService,
                dilalogService_Svg, fileService_Svg
                );

        }
    }
}
