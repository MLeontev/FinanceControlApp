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
    /// Логика взаимодействия для EditExpense.xaml
    /// </summary>
    public partial class EditExpense : Window
    {
        public EditExpense(Operation expenseToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
            DataManageViewModel.SelectedOperation = expenseToEdit;
            DataManageViewModel.ExpenseSum = expenseToEdit.Amount;
            DataManageViewModel.ExpenseCategory = expenseToEdit.Category;
            DataManageViewModel.ExpenseAccount = expenseToEdit.Account;
            DataManageViewModel.ExpenseDate = expenseToEdit.Date;
        }
    }
}
