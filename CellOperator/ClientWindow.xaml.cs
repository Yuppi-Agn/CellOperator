﻿using System;
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

using BLL.Services;
using BLL.Models;

using CellOperator.MVVM.ViewModels;

namespace CellOperator
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {        public ClientWindow(ClientDTO client)
        {
            InitializeComponent();
            DataContext = new ClientViewModel(client);
        }
    }
}