using FinApp.Model;
using FinApp.Model.Data;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace TestProject
{
    [TestClass]
    public class UnitTest
    {
        ApplicationContext? db;

        [TestInitialize]
        public void Init()
        {
            db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        #region Категории
        [TestMethod]
        public void GetAllCategoriesTest()
        {
            List<Category> expected = new List<Category>()
            {
                new Category() {Name = "TestCategory1"},
                new Category() {Name = "TestCategory2"},
                new Category() {Name = "TestCategory3"}
            };

            DataWorker.CreateCategory("TestCategory1");
            DataWorker.CreateCategory("TestCategory2");
            DataWorker.CreateCategory("TestCategory3");

            var result = DataWorker.GetAllCategories();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CreateCategoryTest()
        {
            string result = DataWorker.CreateCategory("TestCategory");

            Assert.AreEqual(db.Categories.FirstOrDefault().Name, "TestCategory");
            Assert.AreEqual(result, "Сделано");
        }

        [TestMethod]
        public void CreateExistingCategoryTest()
        {
            DataWorker.CreateCategory("TestCategory");
            string result = DataWorker.CreateCategory("TestCategory");

            Assert.AreEqual(result, "Такая категория уже существует");
        }

        [TestMethod]
        public void EditCategoryTest()
        {
            DataWorker.CreateCategory("TestCategory");
            Category c = db.Categories.FirstOrDefault();

            string result = DataWorker.EditCategory(c, "TestCategory1");

            db = new ApplicationContext();
            c = db.Categories.FirstOrDefault();

            Assert.AreEqual(result, "Категория изменена");
            Assert.AreEqual("TestCategory1", c.Name);
        }

        [TestMethod]
        public void DeleteCategoryTest()
        {
            DataWorker.CreateCategory("TestCategory");
            Category category = db.Categories.FirstOrDefault();

            string result = DataWorker.DeleteCategory(category);

            Assert.AreEqual(result, "Категория TestCategory удалена");
            Assert.IsFalse(db.Categories.Contains(category));
        }
        #endregion

        #region Счета
        [TestMethod]
        public void CreateAccountTest()
        {
            string result = DataWorker.CreateAccount("type1", "name1", 100);

            Assert.AreEqual(result, "Сделано");
        }

        [TestMethod]
        public void CreatExistingAccountTest()
        {
            DataWorker.CreateAccount("type1", "name1", 100);
            string result = DataWorker.CreateAccount("type1", "name1", 100);

            Assert.AreEqual(result, "Такой счет уже существует");
        }

        [TestMethod]
        public void EditAccountTest()
        {
            DataWorker.CreateAccount("type1", "name1", 100);
            Account a = db.Accounts.FirstOrDefault();

            string result = DataWorker.EditAccount(a, "type2", "name2", 200);

            db = new ApplicationContext();
            a = db.Accounts.FirstOrDefault();

            Assert.AreEqual(result, "Сделано! Счет name2 изменен");
            Assert.AreEqual("name2", a.Name);
            Assert.AreEqual("type2", a.Type);
            Assert.AreEqual(200, a.Balance);
        }

        [TestMethod]
        public void DeleteAccountTest()
        {
            DataWorker.CreateAccount("type1", "name1", 100);
            Account a = db.Accounts.FirstOrDefault();

            string result = DataWorker.DeleteAccount(a);

            Assert.AreEqual(result, "Счет name1 удален");
            Assert.IsFalse(db.Accounts.Contains(a));
        }

        [TestMethod]
        public void GetAllAccountsTest()
        {
            List<Account> expected = new List<Account>()
            {
                new Account() {Type = "type1", Name = "name1", Balance = 100},
                new Account() {Type = "type2", Name = "name2", Balance = 200},
                new Account() {Type = "type3", Name = "name3", Balance = 300}
            };

            DataWorker.CreateAccount("type1", "name1", 100);
            DataWorker.CreateAccount("type2", "name2", 200);
            DataWorker.CreateAccount("type3", "name3", 300);

            var result = DataWorker.GetAllAccounts();

            CollectionAssert.AreEqual(expected, result);
        }
        #endregion

        #region Операции
        [TestMethod]
        public void CreateOperationTest()
        {
            DataWorker.CreateCategory("TestCategory");
            DataWorker.CreateAccount("type1", "name1", 100);

            Account a = db.Accounts.FirstOrDefault();
            Category c = db.Categories.FirstOrDefault();

            string result = DataWorker.CreateOperation(a, 100, c, DateTime.Today, 1);

            Assert.AreEqual("Сделано", result);
        }
        #endregion

        [TestMethod]
        public void GetAllOperationsTest()
        {
            DataWorker.CreateCategory("TestCategory");
            DataWorker.CreateAccount("type1", "name1", 100);

            Account a = db.Accounts.FirstOrDefault();
            Category c = db.Categories.FirstOrDefault();

            DataWorker.CreateOperation(a, 100, c, DateTime.Today, 1);
            DataWorker.CreateOperation(a, 200, c, DateTime.Today, 0);
            
            var result = DataWorker.GetAllOperations();

            List<Operation> expected = new List<Operation>()
            {
                new Operation() {AccountId = 1, CategoryId = 1, Amount = 100, Date = DateTime.Today, IsIncome = 1},
                new Operation() {AccountId = 1, CategoryId = 1, Amount = 200, Date = DateTime.Today, IsIncome = 0}
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAllOperationsByAccountIdTest()
        {
            DataWorker.CreateCategory("TestCategory");
            DataWorker.CreateAccount("type1", "name1", 100);
            DataWorker.CreateAccount("type2", "name2", 100);

            Account a1 = db.Accounts.FirstOrDefault();
            Account a2 = db.Accounts.FirstOrDefault(a => a.Id == 2);
            Category c = db.Categories.FirstOrDefault();

            DataWorker.CreateOperation(a1, 100, c, DateTime.Today, 1);
            DataWorker.CreateOperation(a2, 200, c, DateTime.Today, 0);

            var result = DataWorker.GetAllOperationsByAccountId(1);

            List<Operation> expected = new List<Operation>()
            {
                new Operation() {AccountId = 1, CategoryId = 1, Amount = 100, Date = DateTime.Today, IsIncome = 1},
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAllOperationsByCategoryIdTest()
        {
            DataWorker.CreateCategory("TestCategory1");
            DataWorker.CreateCategory("TestCategory2");
            DataWorker.CreateAccount("type1", "name1", 100);

            Account a = db.Accounts.FirstOrDefault();
            Category c1 = db.Categories.FirstOrDefault();
            Category c2 = db.Categories.FirstOrDefault(c => c.Id == 2);

            DataWorker.CreateOperation(a, 100, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a, 200, c2, DateTime.Today, 0);

            var result = DataWorker.GetAllOperationsByCategoryId(1);

            List<Operation> expected = new List<Operation>()
            {
                new Operation() {AccountId = 1, CategoryId = 1, Amount = 100, Date = DateTime.Today, IsIncome = 1},
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAllIncomesByCategoryIdTest()
        {
            DataWorker.CreateCategory("TestCategory1");
            DataWorker.CreateCategory("TestCategory2");
            DataWorker.CreateAccount("type1", "name1", 100);

            Account a = db.Accounts.FirstOrDefault();
            Category c1 = db.Categories.FirstOrDefault();
            Category c2 = db.Categories.FirstOrDefault(c => c.Id == 2);

            DataWorker.CreateOperation(a, 100, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a, 200, c1, DateTime.Today, 0);

            var result = DataWorker.GetAllIncomesByCategoryId(1);

            List<Operation> expected = new List<Operation>()
            {
                new Operation() {AccountId = 1, CategoryId = 1, Amount = 100, Date = DateTime.Today, IsIncome = 1},
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAllExpensesByCategoryIdTest()
        {
            DataWorker.CreateCategory("TestCategory1");
            DataWorker.CreateCategory("TestCategory2");
            DataWorker.CreateAccount("type1", "name1", 100);

            Account a = db.Accounts.FirstOrDefault();
            Category c1 = db.Categories.FirstOrDefault();
            Category c2 = db.Categories.FirstOrDefault(c => c.Id == 2);

            DataWorker.CreateOperation(a, 100, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a, 200, c1, DateTime.Today, 0);

            var result = DataWorker.GetAllExpensesByCategoryId(1);

            List<Operation> expected = new List<Operation>()
            {
                new Operation() {AccountId = 1, CategoryId = 1, Amount = 200, Date = DateTime.Today, IsIncome = 0},
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DeleteOperationTest()
        {
            DataWorker.CreateCategory("TestCategory");
            DataWorker.CreateAccount("type1", "name1", 100);

            Account a = db.Accounts.FirstOrDefault();
            Category c = db.Categories.FirstOrDefault();

            DataWorker.CreateOperation(a, 100, c, DateTime.Today, 1);
            Operation o = db.Operations.FirstOrDefault();

            string result = DataWorker.DeleteOperation(o);

            Assert.AreEqual("Операция удалена", result);
            Assert.IsFalse(db.Operations.Contains(o));
        }

        [TestMethod]
        public void EditOperationTest()
        {
            DataWorker.CreateCategory("TestCategory");
            DataWorker.CreateAccount("type1", "name1", 100);

            Account a = db.Accounts.FirstOrDefault();
            Category c = db.Categories.FirstOrDefault();

            DataWorker.CreateOperation(a, 100, c, DateTime.Today, 1);
            Operation o = db.Operations.FirstOrDefault();

            string result = DataWorker.EditOperation(o, a, 200, c, DateTime.Today, 1);

            db = new ApplicationContext();
            o = db.Operations.FirstOrDefault();

            Assert.AreEqual("Операция за 18 июня 2023 г. изменена", result);
            Assert.AreEqual(200, o.Amount);
        }

        [TestMethod]
        public void GetMaxAmountTest()
        {
            DataWorker.CreateCategory("TestCategory1");
            DataWorker.CreateAccount("type1", "name1", 100);

            Account a = db.Accounts.FirstOrDefault();
            Category c = db.Categories.FirstOrDefault();

            DataWorker.CreateOperation(a, 100, c, DateTime.Today, 1);
            DataWorker.CreateOperation(a, 200, c, DateTime.Today, 0);

            int result = DataWorker.GetMaxAmount();

            Assert.AreEqual(200, result);
        }

        [TestMethod]
        public void GetBankOperationsInRangeTest()
        {
            DataWorker.CreateCategory("TestCategory1");
            DataWorker.CreateCategory("TestCategory2");
            DataWorker.CreateAccount("type1", "name1", 100);
            DataWorker.CreateAccount("type2", "name2", 200);

            Account a1 = db.Accounts.FirstOrDefault();
            Account a2 = db.Accounts.FirstOrDefault(a => a.Id == 2);

            Category c1 = db.Categories.FirstOrDefault();
            Category c2 = db.Categories.FirstOrDefault(c => c.Id == 2);

            DataWorker.CreateOperation(a1, 100, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a1, 200, c2, DateTime.Today.AddDays(-1), 1);
            DataWorker.CreateOperation(a2, 300, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a2, 400, c2, DateTime.Today.AddDays(-1), 0);

            var result = DataWorker.GetBankOperationsInRange(200, 500, DateTime.Today.AddDays(-1), DateTime.Today, 1, 2);
            Assert.AreEqual(new Operation() { AccountId = 2, CategoryId = 1, Amount = 300, Date = DateTime.Today, IsIncome = 1 }, result[0]);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetBankOperationsInRangeWithoutCategoryTest()
        {
            DataWorker.CreateCategory("TestCategory1");
            DataWorker.CreateCategory("TestCategory2");
            DataWorker.CreateAccount("type1", "name1", 100);
            DataWorker.CreateAccount("type2", "name2", 200);

            Account a1 = db.Accounts.FirstOrDefault();
            Account a2 = db.Accounts.FirstOrDefault(a => a.Id == 2);

            Category c1 = db.Categories.FirstOrDefault();
            Category c2 = db.Categories.FirstOrDefault(c => c.Id == 2);

            DataWorker.CreateOperation(a1, 100, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a1, 200, c2, DateTime.Today.AddDays(-1), 1);
            DataWorker.CreateOperation(a2, 300, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a2, 400, c2, DateTime.Today.AddDays(-1), 0);

            var result = DataWorker.GetBankOperationsInRangeWithoutCategory(200, 500, DateTime.Today.AddDays(-1), DateTime.Today, 2);

            Assert.AreEqual(new Operation() { AccountId = 2, CategoryId = 1, Amount = 300, Date = DateTime.Today, IsIncome = 1 }, result[0]);
            Assert.AreEqual(new Operation() { AccountId = 2, CategoryId = 2, Amount = 400, Date = DateTime.Today.AddDays(-1), IsIncome = 0 }, result[1]);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetBankOperationsInRangeWithoutAccountTest()
        {
            DataWorker.CreateCategory("TestCategory1");
            DataWorker.CreateCategory("TestCategory2");
            DataWorker.CreateAccount("type1", "name1", 100);
            DataWorker.CreateAccount("type2", "name2", 200);

            Account a1 = db.Accounts.FirstOrDefault();
            Account a2 = db.Accounts.FirstOrDefault(a => a.Id == 2);

            Category c1 = db.Categories.FirstOrDefault();
            Category c2 = db.Categories.FirstOrDefault(c => c.Id == 2);

            DataWorker.CreateOperation(a1, 100, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a1, 200, c2, DateTime.Today.AddDays(-1), 1);
            DataWorker.CreateOperation(a1, 300, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a2, 400, c2, DateTime.Today.AddDays(-1), 0);

            var result = DataWorker.GetBankOperationsInRangeWithoutAccount(200, 500, DateTime.Today.AddDays(-1), DateTime.Today, 2);

            Assert.AreEqual(new Operation() { AccountId = 1, CategoryId = 2, Amount = 200, Date = DateTime.Today.AddDays(-1), IsIncome = 1 }, result[0]);
            Assert.AreEqual(new Operation() { AccountId = 2, CategoryId = 2, Amount = 400, Date = DateTime.Today.AddDays(-1), IsIncome = 0 }, result[1]);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetBankOperationsInRangeWithoutCategoryAndAccountTest()
        {
            DataWorker.CreateCategory("TestCategory1");
            DataWorker.CreateCategory("TestCategory2");
            DataWorker.CreateAccount("type1", "name1", 100);
            DataWorker.CreateAccount("type2", "name2", 200);

            Account a1 = db.Accounts.FirstOrDefault();
            Account a2 = db.Accounts.FirstOrDefault(a => a.Id == 2);

            Category c1 = db.Categories.FirstOrDefault();
            Category c2 = db.Categories.FirstOrDefault(c => c.Id == 2);

            DataWorker.CreateOperation(a1, 100, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a1, 200, c2, DateTime.Today.AddDays(-1), 1);
            DataWorker.CreateOperation(a1, 300, c1, DateTime.Today, 1);
            DataWorker.CreateOperation(a2, 400, c2, DateTime.Today.AddDays(-1), 0);

            var result = DataWorker.GetBankOperationsInRangeWithoutCategoryAndAccount(300, 400, DateTime.Today, DateTime.Today);

            Assert.AreEqual(new Operation() { AccountId = 1, CategoryId = 1, Amount = 300, Date = DateTime.Today, IsIncome = 1 }, result[0]);
            Assert.AreEqual(1, result.Count);
        }
    }
}


