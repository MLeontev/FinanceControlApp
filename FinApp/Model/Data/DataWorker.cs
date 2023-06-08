using Microsoft.EntityFrameworkCore;
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

        //получить все операции
        public static List<Operation> GetAllOperations()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = db.Operations
                    .Include(o => o.Category)
                    .Include(o => o.Account)
                    .ToList();
                operations.Sort(new DateComparer());
                return operations;
            }
        }

        //получить все операции по счету
        public static List<Operation> GetAllOperationsByAccountId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from operation in GetAllOperations() where operation.AccountId == id select operation).ToList();
                return operations;
            }
        }

        //получить все операции по категории
        public static List<Operation> GetAllOperationsByCategoryId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from operation in GetAllOperations() where operation.CategoryId == id select operation).ToList();
                return operations;
            }
        }

        //получить все доходы по категории
        public static List<Operation> GetAllIncomesByCategoryId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> incomes = (from operation in GetAllOperationsByCategoryId(id) where operation.IsIncome == 1 select operation).ToList();
                return incomes;
            }
        }

        //получить все расходы по категории
        public static List<Operation> GetAllExpensesByCategoryId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> expenses = (from operation in GetAllOperationsByCategoryId(id) where operation.IsIncome == 0 select operation).ToList();
                return expenses;
            }
        }

        //создать счет
        public static string CreateAccount(string type, string name, int balance)
        {
            string result = "Такой счет уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Accounts.Any(el => (el.Type == type && el.Name == name));
                if (!checkIsExist)
                {
                    Account account = new Account
                    {
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
        public static string CreateCategory(string name)
        {
            string result = "Такая категория уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Categories.Any(el => (el.Name == name));
                if (!checkIsExist)
                {
                    Category category = new Category
                    {
                        Name = name
                    };
                    db.Categories.Add(category);
                    db.SaveChanges();
                    result = "Сделано";
                }
            }
            return result;
        }

        //создать операцию
        public static string CreateOperation(Account account, int amount, Category category, DateTime date, int isIncome)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                Operation operation = new Operation
                {
                    AccountId = account.Id,
                    Amount = amount,
                    CategoryId = category.Id,
                    Date = date,
                    IsIncome = isIncome
                };
                db.Operations.Add(operation);
                db.SaveChanges();
                result = "Сделано";
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

        //удалить операцию
        public static string DeleteOperation(Operation operation)
        {
            string result = "Такой операции не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Operations.Remove(operation);
                db.SaveChanges();
                result = "Операция удалена";
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

        //редактировать операцию
        public static string EditOperation(Operation oldOperation, Account newAccount, int newAmount, Category newCategory, DateTime newDate, int newIsIncome)
        {
            string result = "Такого дохода не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Operation operation = db.Operations.FirstOrDefault(income => income.Id == oldOperation.Id);
                operation.AccountId = newAccount.Id;
                operation.Amount = newAmount;
                operation.Category = newCategory;
                operation.Date = newDate;
                operation.IsIncome = newIsIncome;
                db.SaveChanges();
                result = $"Доход за {operation.Date:D} изменен";
            }
            return result;
        }

        //редактировать категорию
        public static string EditCategory(Category oldCategory, string newName)
        {
            string result = "Такой категории не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Category operation = db.Categories.FirstOrDefault(category => category.Id == oldCategory.Id);
                operation.Name = newName;
                db.SaveChanges();
                result = $"Категория изменена";
            }
            return result;
        }

        //получить операции по фильтрам
        public static List<Operation> GetBankOperationsInRange(int minAmount, int maxAmount, DateTime startDate, DateTime endDate, int categoryId, int accountId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var operations = db.Operations
                    .Where(o =>
                        o.Amount >= minAmount &&
                        o.Amount <= maxAmount &&
                        o.Date >= startDate &&
                        o.Date <= endDate &&
                        o.CategoryId == categoryId &&
                        o.AccountId == accountId)
                    .Include(o => o.Category)
                    .Include(o => o.Account)
                    .ToList();

                return operations;
            }
        }

        public static List<Operation> GetBankOperationsInRangeWithoutCategory(int minAmount, int maxAmount, DateTime startDate, DateTime endDate, int accountId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var operations = db.Operations
                    .Where(o =>
                        o.Amount >= minAmount &&
                        o.Amount <= maxAmount &&
                        o.Date >= startDate &&
                        o.Date <= endDate &&
                        o.AccountId == accountId)
                    .Include(o => o.Category)
                    .Include(o => o.Account)
                    .ToList();

                return operations;
            }
        }

        public static List<Operation> GetBankOperationsInRangeWithoutAccount(int minAmount, int maxAmount, DateTime startDate, DateTime endDate, int categoryId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var operations = db.Operations
                    .Where(o =>
                        o.Amount >= minAmount &&
                        o.Amount <= maxAmount &&
                        o.Date >= startDate &&
                        o.Date <= endDate &&
                        o.CategoryId == categoryId)
                    .Include(o => o.Category)
                    .Include(o => o.Account)
                    .ToList();

                return operations;
            }
        }

        public static List<Operation> GetBankOperationsInRangeWithoutCategoryAndAccount(int minAmount, int maxAmount, DateTime startDate, DateTime endDate)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var operations = db.Operations
                    .Where(o =>
                        o.Amount >= minAmount &&
                        o.Amount <= maxAmount &&
                        o.Date >= startDate &&
                        o.Date < endDate)
                    .Include(o => o.Category)
                    .Include(o => o.Account)
                    .ToList();

                return operations;
            }
        }

        //получить максимальную сумму оперции
        public static int GetMaxAmount()
        {
            List<Operation> operations = GetAllOperations();
            int max = 0;
            foreach (Operation operation in operations)
            {
                if (operation.Amount > max) max = operation.Amount;
            }
            return max;
        }
    }
}
