﻿using FinApp.ViewModel;
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

namespace FinApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewIncome.xaml
    /// </summary>
    public partial class AddNewIncome : Window
    {
        public static ComboBox IncomeCategories;
        public static ComboBox IncomeAccounts;

        public AddNewIncome()
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();

            IncomeCategories = IncomeCategory;
            IncomeAccounts = IncomeAccount;
        }
    }
}
