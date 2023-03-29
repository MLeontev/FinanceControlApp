using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinApp.Model.Data
{
    public static class DataWorker
    {
        //получить счет по id счета
        public static Account GetAccountById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Account account = db.Accounts.FirstOrDefault(a => a.Id == id);
                return account;
            }
        }

        //получить всех пользователей
        public static List<User> GetAllUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<User> users = db.Users.ToList();
                return users;
            }
        }

        //получить все категории
        public static List<Category> GetAllCategories()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Category> categories = db.Categories.ToList();
                return categories;
            }
        }

        //получить все счета
        public static List<Account> GetAllAccounts()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Account> accounts = db.Accounts.ToList();
                return accounts;
            }
        }

        //получить все доходы
        public static List<Income> GetAllIncomes()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Income> incomes = db.Incomes.ToList();
                return incomes;
            }
        }

        //получить все расходы
        public static List<Expense> GetAllExpenses()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Expense> expenses = db.Expenses.ToList();
                return expenses;
            }
        }

        //получить все категории пользователя
        public static List<Category> GetAllCategoriesByUserId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Category> categories = (from category in GetAllCategories() where category.UserId == id select category).ToList();
                return categories;
            }
        }

        //получить все счета пользователя
        public static List<Account> GetAllAccountsByUserId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Account> accounts = (from account in GetAllAccounts() where account.UserId == id select account).ToList();
                return accounts;
            }
        }

        //получить все расходы пользователя
        public static List<Expense> GetAllExpensesByUserId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Expense> expenses = (from expense in GetAllExpenses() where expense.ExpenseAccount.UserId == id select expense).ToList();
                return expenses;
            }
        }

        //получить все расходы по счету
        public static List<Expense> GetAllExpensesByAccountId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Expense> expenses = (from expense in GetAllExpenses() where expense.AccountId == id select expense).ToList();
                return expenses;
            }
        }

        //получить все доходы пользователя
        public static List<Income> GetAllIncomesByUserId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Income> incomes = (from income in GetAllIncomes() where income.IncomeAccount.UserId == id select income).ToList();
                return incomes;
            }
        }

        //получить все доходы по счету
        public static List<Income> GetAllIncomesByAccountId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Income> incomes = (from income in GetAllIncomes() where income.AccountId == id select income).ToList();
                return incomes;
            }
        }

        

        //создать пользователя
        public static string CreateUser(string login, string password)
        {
            string result = "Такой пользователь уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Users.Any(el => el.Login == login);
                if (!checkIsExist)
                {
                    User user = new User { Login = login, Password = password };
                    db.Users.Add(user);
                    db.SaveChanges();
                    result = "Сделано";
                }
            }
            return result;
        }

        //создать счет
        public static string CreateAccount(User user, string type, string name, int balance)
        {
            string result = "Такой счет уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Accounts.Any(el => (el.User == user && el.Type == type && el.Name == name));
                if (!checkIsExist)
                {
                    Account account = new Account
                    {
                        UserId = user.Id,
                        Type = type,
                        Name = name,
                        Balance = balance
                    };
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    result = "Сделано";
                }
            }
            return result;
        }

        //создать категорию
        public static string CreateCategory(User user, string name)
        {
            string result = "Такая категория уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Categories.Any(el => (el.User == user && el.Name == name));
                if (!checkIsExist)
                {
                    Category category = new Category
                    {
                        UserId = user.Id,
                        Name = name
                    };
                    db.Categories.Add(category);
                    db.SaveChanges();
                    result = "Сделано";
                }
            }
            return result;
        }

        //создать доход
        public static string CreateIncome(Account account, int amount, Category category, DateTime date)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                Income income = new Income
                {
                    AccountId = account.Id,
                    Amount = amount,
                    Category = category,
                    Date = date
                };
                db.Incomes.Add(income);
                db.SaveChanges();
                result = "Сделано";
            }
            return result;
        }

        //создать расход
        public static string CreateExpense(Account account, int amount, Category category, DateTime date)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                Expense expense = new Expense
                {
                    AccountId = account.Id,
                    Amount = amount,
                    Category = category,
                    Date = date
                };
                db.Expenses.Add(expense);
                db.SaveChanges();
                result = "Сделано";
            }
            return result;
        }

        //удалить пользователя
        public static string DeleteUser(User user)
        {
            string result = "Такого пользователя не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = "Пользователь " + user.Login + " удален";
            }
            return result;
        }

        //удалить счет
        public static string DeleteAccount(Account account)
        {
            string result = "Такого счета не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Accounts.Remove(account);
                db.SaveChanges();
                result = "Счет " + account.Name + " удален";
            }
            return result;
        }

        //удалить категорию
        public static string DeleteCategory(Category category)
        {
            string result = "Такой категории не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                result = "Категория " + category.Name + " удалена";
            }
            return result;
        }

        //удалить доход
        public static string DeleteIncome(Income income)
        {
            string result = "Такого дохода не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Incomes.Remove(income);
                db.SaveChanges();
                result = "Доход удален";
            }
            return result;
        }

        //удалить расход
        public static string DeleteExpense(Expense expense)
        {
            string result = "Такого расхода не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Expenses.Remove(expense);
                db.SaveChanges();
                result = "Расход удален";
            }
            return result;
        }

        //редактировать пользователя
        public static string EditUser(User oldUser, string newLogin, string newPassword)
        {
            string result = "Такого пользователя не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(user => user.Id == oldUser.Id);
                user.Login = newLogin;
                user.Password = newPassword;
                db.SaveChanges();
                result = $"Сделано! Пользователь {user.Login} изменен";
            }
            return result;
        }

        //редактировать счет
        public static string EditAccount(Account oldAccount, string newType, string newName, int newBalance)
        {
            string result = "Такого счета не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Account account = db.Accounts.FirstOrDefault(account => account.Id == oldAccount.Id);
                account.Type = newType;
                account.Name = newName;
                account.Balance = newBalance;
                db.SaveChanges();
                result = $"Сделано! Счет {account.Name} изменен";
            }
            return result;
        }

        //редактировать доход
        public static string EditIncome(Income oldIncome, Account newAccount, int newAmount, Category newCategory, DateTime newDate)
        {
            string result = "Такого дохода не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Income income = db.Incomes.FirstOrDefault(income => income.Id == oldIncome.Id);
                income.AccountId = newAccount.Id;
                income.Amount = newAmount;
                income.Category = newCategory;
                income.Date = newDate;
                db.SaveChanges();
                result = $"Доход за {income.Date} изменен";
            }
            return result;
        }

        //редактировать расход
        public static string EditExpense(Income oldExpense, Account newAccount, int newAmount, Category newCategory, DateTime newDate)
        {
            string result = "Такого дохода не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Expense expense = db.Expenses.FirstOrDefault(expense => expense.Id == oldExpense.Id);
                expense.AccountId = newAccount.Id;
                expense.Amount = newAmount;
                expense.Category = newCategory;
                expense.Date = newDate;
                db.SaveChanges();
                result = $"Расход за {expense.Date} изменен";
            }
            return result;
        }
    }
}
