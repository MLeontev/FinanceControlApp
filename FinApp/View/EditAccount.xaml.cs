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
    /// Логика взаимодействия для EditAccount.xaml
    /// </summary>
    public partial class EditAccount : Window
    {
        public EditAccount(Account accountToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
            DataManageViewModel.SelectedAccount = accountToEdit;
            DataManageViewModel.AccountName = accountToEdit.Name;
            DataManageViewModel.AccountBalance = accountToEdit.Balance;
            DataManageViewModel.AccountType = accountToEdit.Type;
        }
    }
}
