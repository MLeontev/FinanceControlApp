using FinApp.Model;
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
    /// Логика взаимодействия для EditIncome.xaml
    /// </summary>
    public partial class EditIncome : Window
    {
        public EditIncome(Operation incomeToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
            DataManageViewModel.SelectedOperation = incomeToEdit;
            DataManageViewModel.IncomeSum = incomeToEdit.Amount;
            DataManageViewModel.IncomeCategory = incomeToEdit.Category;
            DataManageViewModel.IncomeAccount = incomeToEdit.Account;
            DataManageViewModel.IncomeDate = incomeToEdit.Date;
        }
    }
}
