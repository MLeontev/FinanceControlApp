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
using System.Windows.Controls;
using System.Windows.Media;

namespace FinApp.ViewModel
{
    public class DataManageViewModel : INotifyPropertyChanged
    {

        //все счета пользователя
        private List<Account> allAccounts = DataWorker.GetAllAccounts();
        public List<Account> AllAccounts
        {
            get { return allAccounts; }
            set
            {
                allAccounts = value;
                OnPropertyChanged("AllAccounts");
            }
        }

        //все операции пользователя
        private List<Operation> allOperations = DataWorker.GetAllOperations();
        public List<Operation> AllOperations
        {
            get
            {
                return allOperations;
            }
            set
            {
                allOperations = value;
                OnPropertyChanged("AllOperations");
            }
        }

        //все категории пользователя
        private List<Category> allCategories = DataWorker.GetAllCategories();
        public List<Category> AllCategories
        {
            get
            {
                return allCategories;
            }
            set
            {
                allCategories = value;
                OnPropertyChanged("AllCategories");
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

        private void OpenEditAccountWindowMethod()
        {
            EditAccount editAccountWindow = new EditAccount();
            SetCenterPositionAndOpen(editAccountWindow);
        }

        private void OpenEditCategoryWindowMethod()
        {
            EditCategory editCategoryWindow = new EditCategory();
            SetCenterPositionAndOpen(editCategoryWindow);
        }

        private void OpenEditExpenseWindowMethod()
        {
            EditExpense editExpenseWindow = new EditExpense();
            SetCenterPositionAndOpen(editExpenseWindow);
        }

        private void OpenEditIncomeWindowMethod()
        {
            EditIncome editIncomeWindow = new EditIncome();
            SetCenterPositionAndOpen(editIncomeWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        #region Команды открытия окон
        private RelayCommand openAddAccountWindow;
        public RelayCommand OpenAddAccountWindow
        {
            get
            {
                return openAddAccountWindow ?? new RelayCommand(obj =>
                {
                    OpenAddAccountWindowMethod();
                }
                );
            }
        }

        private RelayCommand openAddCategoryWindow;
        public RelayCommand OpenAddCategoryWindow
        {
            get
            {
                return openAddCategoryWindow ?? new RelayCommand(obj =>
                {
                    OpenAddCategoryWindowMethod();
                }
                );
            }
        }

        private RelayCommand openAddExpenseWindow;
        public RelayCommand OpenAddExpenseWindow
        {
            get
            {
                return openAddExpenseWindow ?? new RelayCommand(obj =>
                {
                    OpenAddExpenseWindowMethod();
                }
                );
            }
        }

        private RelayCommand openAddIncomeWindow;
        public RelayCommand OpenAddIncomeWindow
        {
            get
            {
                return openAddIncomeWindow ?? new RelayCommand(obj =>
                {
                    OpenAddIncomeWindowMethod();
                }
                );
            }
        }

        #endregion

        private void SetRedBlockControl(Window wnd, string blockname)
        {
            Control block = wnd.FindName(blockname) as Control;
            block.BorderBrush = Brushes.Red;
        }

        public static string AccountName { get; set; }

        public static int AccountBalance { get; set; }

        public static string AccountType { get; set; }

        public static string CategoryName { get; set; }

        public static int IncomeSum { get; set; }

        public static string IncomeCategorie { get; set; }

        public static DateTime IncomeDate { get; set; }

        #region Команды для добавления
        private RelayCommand addNewCategory;
        public RelayCommand AddNewCategory
        {
            get
            {
                return addNewCategory ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (CategoryName == null || CategoryName.Length == 0 || CategoryName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "CategoryName");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateCategory(CategoryName);
                    }
                }
                );
            }
        }

        private RelayCommand addNewAccount;
        public RelayCommand AddNewAccount
        {
            get
            {
                return addNewAccount ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (AccountName == null || AccountName.Length == 0 || AccountName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "AccountName");
                    }
                    else if (AccountType == null)
                    {
                        SetRedBlockControl(wnd, "AccountTypes");
                    }
                    else if (AccountBalance < 0)
                    {
                        SetRedBlockControl(wnd, "AccountBalance");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateAccount(AccountType, AccountName, AccountBalance);
                    }
                }
                );
            }
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
