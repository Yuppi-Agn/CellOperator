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
using System.Windows.Navigation;
using System.Windows.Shapes;

using BLL.Services;
using BLL.Models;

using CellOperator.MVVM.ViewModels;
namespace CellOperator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new EnterViewModel();
        }

        /*private void Button__UserAuth(object sender, RoutedEventArgs e)
        {
            if (UserAuth_check(LoginBox.Text, passwordBox.Password))
            {
                ClientWindow taskWindow = new ClientWindow();
                taskWindow.Show();
                Close();
            }
        }

        private void Button_AdministratorAuth(object sender, RoutedEventArgs e)
        {
            if (AdministratorAuth_check(LoginBox.Text, passwordBox.Password))
            {
                AdministatorWindows taskWindow = new AdministatorWindows();
                taskWindow.Show();
                Close();
            }
        }*/        
    }
}
