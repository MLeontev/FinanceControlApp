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
        
        private void SetDefaultBlockControl(Window wnd, string blockname)
        {
            Control block = wnd.FindName(blockname) as Control;
            block.BorderBrush = Brushes.Gray;
        }

        public static string AccountName { get; set; }

        public static int AccountBalance { get; set; }

        public static string AccountType { get; set; }

        public static string CategoryName { get; set; }

        public static int IncomeSum { get; set; }

        public static Category IncomeCategory { get; set; }

        public static Account IncomeAccount { get; set; }

        public static DateTime IncomeDate { get; set; } = DateTime.Now.Date;

        public static int ExpenseSum { get; set; }

        public static Category ExpenseCategory { get; set; }

        public static Account ExpenseAccount { get; set; }

        public static DateTime ExpenseDate { get; set; } = DateTime.Now.Date;

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
                        SetDefaultBlockControl(wnd, "CategoryName");
                        resultStr = DataWorker.CreateCategory(CategoryName);
                        ShowMessage(resultStr);
                        UpdateAll();
                        SetNullToProperties();
                        wnd.Close();
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
                        SetDefaultBlockControl(wnd, "AccountName");
                        SetDefaultBlockControl(wnd, "AccountTypes");
                        SetDefaultBlockControl(wnd, "AccountBalance");
                        resultStr = DataWorker.CreateAccount(AccountType, AccountName, AccountBalance);
                        ShowMessage(resultStr);
                        UpdateAll();
                        SetNullToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        private RelayCommand addNewIncome;
        public RelayCommand AddNewIncome
        {
            get
            {
                return addNewIncome ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (IncomeSum <= 0) 
                    {
                        SetRedBlockControl(wnd, "IncomeSum");
                    }
                    else if (IncomeCategory == null)
                    {
                        SetDefaultBlockControl(wnd, "IncomeSum");
                        SetRedBlockControl(wnd, "IncomeCategory");
                    }
                    else if (IncomeAccount == null)
                    {
                        SetDefaultBlockControl(wnd, "IncomeSum");
                        SetDefaultBlockControl(wnd, "IncomeCategory");
                        SetRedBlockControl(wnd, "IncomeAccount");
                    }
                    else
                    {
                        SetDefaultBlockControl(wnd, "IncomeSum");
                        SetDefaultBlockControl(wnd, "IncomeCategory");
                        SetDefaultBlockControl(wnd, "IncomeAccount");
                        resultStr = DataWorker.CreateOperation(IncomeAccount, IncomeSum, IncomeCategory, IncomeDate, 1);
                        ShowMessage(resultStr);
                        UpdateAll();
                        SetNullToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        private RelayCommand addNewExpense;
        public RelayCommand AddNewExpense
        {
            get
            {
                return addNewExpense ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (ExpenseSum <= 0)
                    {
                        SetRedBlockControl(wnd, "ExpenseSum");
                    }
                    else if (ExpenseCategory == null)
                    {
                        SetDefaultBlockControl(wnd, "ExpenseSum");
                        SetRedBlockControl(wnd, "ExpenseCategory");
                    }
                    else if (ExpenseAccount == null)
                    {
                        SetDefaultBlockControl(wnd, "ExpenseSum");
                        SetDefaultBlockControl(wnd, "ExpenseCategory");
                        SetRedBlockControl(wnd, "ExpenseAccount");
                    }
                    else
                    {
                        SetDefaultBlockControl(wnd, "ExpenseSum");
                        SetDefaultBlockControl(wnd, "ExpenseCategory");
                        SetDefaultBlockControl(wnd, "ExpenseAccount");
                        resultStr = DataWorker.CreateOperation(ExpenseAccount, ExpenseSum, ExpenseCategory, ExpenseDate, 0);
                        ShowMessage(resultStr);
                        UpdateAll();
                        SetNullToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        #endregion

        #region Обновить View
        private void SetNullToProperties()
        {
            AccountName = null;
            AccountBalance = 0;
            AccountType = null;
            CategoryName = null;
            IncomeSum = 0;
            IncomeCategory = null;
            IncomeAccount = null;
            IncomeDate = DateTime.Now;
            ExpenseSum = 0;
            ExpenseCategory = null;
            ExpenseAccount = null;
            ExpenseDate = DateTime.Now;
        }

        private void UpdateAll()
        {
            UpdateAllAccountsView();
            UpdateAllCategoriesView();
            UpdateAllOperationsView();
        }

        private void UpdateAllAccountsView()
        {
            AllAccounts = DataWorker.GetAllAccounts();
            MainWindow.AllAccountsView.ItemsSource = null;
            MainWindow.AllAccountsView.Items.Clear();
            MainWindow.AllAccountsView.ItemsSource = AllAccounts;
            MainWindow.AllAccountsView.Items.Refresh();
        }

        private void UpdateAllCategoriesView()
        {
            AllCategories = DataWorker.GetAllCategories();
            MainWindow.AllCategoriesView.ItemsSource = null;
            MainWindow.AllCategoriesView.Items.Clear();
            MainWindow.AllCategoriesView.ItemsSource = AllCategories;
            MainWindow.AllCategoriesView.Items.Refresh();
        }

        private void UpdateAllOperationsView()
        {
            AllOperations = DataWorker.GetAllOperations();
            MainWindow.AllOperationsView.ItemsSource = null;
            MainWindow.AllOperationsView.Items.Clear();
            MainWindow.AllOperationsView.ItemsSource = AllOperations;
            MainWindow.AllOperationsView.Items.Refresh();
        }
        #endregion


        private void ShowMessage(string message)
        {
            MessageWindow messageWindow = new MessageWindow(message);
            SetCenterPositionAndOpen(messageWindow);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
