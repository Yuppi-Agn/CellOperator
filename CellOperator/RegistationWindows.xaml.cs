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

using CellOperator.MVVM.ViewModels;

namespace CellOperator
{
    /// <summary>
    /// Логика взаимодействия для RegistationWindows.xaml
    /// </summary>
    public partial class RegistationWindows : Window
    {
        public RegistationWindows()
        {
            InitializeComponent();
            DataContext = new RegistationModel();
        }
    }
}
