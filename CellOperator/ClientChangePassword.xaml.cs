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

namespace CellOperator
{
    /// <summary>
    /// Логика взаимодействия для ClientChangePassword.xaml
    /// </summary>
    public partial class ClientChangePassword : Window
    {
        public ClientChangePassword(ClientDTO client)
        {
            InitializeComponent();
            DataContext = new ClientWindow_PasswordChangeModel(client);
        }
    }
}
