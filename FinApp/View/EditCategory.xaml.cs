﻿using FinApp.Model;
using FinApp.ViewModel;
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
    /// Логика взаимодействия для EditCategory.xaml
    /// </summary>
    public partial class EditCategory : Window
    {
        public EditCategory(Category categoryToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
            DataManageViewModel.SelectedCategory = categoryToEdit;
            DataManageViewModel.CategoryName = categoryToEdit.Name;
        }
    }
}
