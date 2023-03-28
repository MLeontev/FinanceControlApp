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
        //создать пользователя
        public static string CreatUser(string login, string password)
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
        public static string CreatAccount(User user, string type, string name, int balance)
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
        public static string CreatCategory(User user, string name)
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
        public static string CreatIncome(Account account, int amount, Category category, DateTime date)
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
        public static string CreatExpense(Account account, int amount, Category category, DateTime date)
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
        public static string DeleteIncome(Expense expense)
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
    }
}
