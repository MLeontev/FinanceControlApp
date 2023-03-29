using FinApp.Model.Data;
using FinApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FinApp.View;

namespace FinApp.ViewModel
{
    public class DataManageViewModel : INotifyPropertyChanged
    {
        //id пользователя
        private static int userId;

        //все счета пользователя
        private List<Account> allUserAccounts = DataWorker.GetAllAccountsByUserId(userId);
        public List<Account> AllUserAccounts
        {
            get { return allUserAccounts; }
            set
            {
                allUserAccounts = value;
                OnPropertyChanged("AllUserAccounts");
            }
        }

        //все операции пользователя
        private List<Operation> allUserOperations = DataWorker.GetAllOperationsByUserId(userId);
        public List<Operation> AllUserOperations
        {
            get
            {
                return allUserOperations;
            }
            set
            {
                allUserOperations = value;
                OnPropertyChanged("AllUserOperations");
            }
        }

        //все категории пользователя
        private List<Category> allUserCategories = DataWorker.GetAllCategoriesByUserId(userId);
        public List<Category> AllUserCategories
        {
            get
            {
                return allUserCategories;
            }
            set
            {
                allUserCategories = value;
                OnPropertyChanged("AllUserCategories");
            }
        }


        #region Методы открытия окон
        private void OpenAddAccountWindowMethod()
        {
            AddNewAccount newAccountWindow = new AddNewAccount();
            SetCenterPositionAndOpen(newAccountWindow);
        }

        private void OpenAddCategoryWindowMethod()
        {
            AddNewCategory newCategoryWindow = new AddNewCategory();
            SetCenterPositionAndOpen(newCategoryWindow);
        }

        private void OpenAddExpenseWindowMethod()
        {
            AddNewExpense newExpenseWindow = new AddNewExpense();
            SetCenterPositionAndOpen(newExpenseWindow);
        }

        private void OpenAddIncomeWindowMethod()
        {
            AddNewIncome newIncomeWindow = new AddNewIncome();
            SetCenterPositionAndOpen(newIncomeWindow);
        }

        private void OpenAuthenticationWindowMethod(Window window)
        {
            AuthenticationWindow authWindow = new AuthenticationWindow();
            SetCenterPositionAndOpen(authWindow);
            window.Close();
        }

        private void OpenRegistrationWindowMethod(Window window)
        {
            RegistrationWindow regWindow = new RegistrationWindow();
            SetCenterPositionAndOpen(regWindow);
            window.Close();
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
