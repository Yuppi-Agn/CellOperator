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

using BLL.Models;
using BLL.Services;
using CellOperator.MVVM.ViewModels;

namespace CellOperator
{
    /// <summary>
    /// Логика взаимодействия для ClientServiceChange.xaml
    /// </summary>
    public partial class ClientServiceChange : Window
    {
        public ClientServiceChange(ref DataBase_service db, ClientDTO client, NumberDTO number)
        {
            InitializeComponent();
            DataContext = new ClientWindow_ServiceChangeModel(ref db, client, number);
        }
    }
}