using FinApp.Model;
using FinApp.Model.Data;
using Microsoft.EntityFrameworkCore;

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
        //[TestMethod]
        //public void GetAllCategoriesTest()
        //{
        //    List<Category> expected = new List<Category>()
        //    {
        //        new Category() {Name = "TestCategory1"},
        //        new Category() {Name = "TestCategory2"},
        //        new Category() {Name = "TestCategory3"}
        //    };

        //    DataWorker.CreateCategory("TestCategory1");
        //    DataWorker.CreateCategory("TestCategory2");
        //    DataWorker.CreateCategory("TestCategory3");

        //    var result = DataWorker.GetAllCategories();

        //    CollectionAssert.AreEqual(expected, result);
        //}

        //[TestMethod]
        //public void CreateCategoryTest()
        //{
        //    string result = DataWorker.CreateCategory("TestCategory");

        //    Assert.AreEqual(result, "Сделано");
        //}

        //[TestMethod]
        //public void CreateExistingCategoryTest()
        //{
        //    DataWorker.CreateCategory("TestCategory");
        //    string result = DataWorker.CreateCategory("TestCategory");

        //    Assert.AreEqual(result, "Такая категория уже существует");
        //}

        [TestMethod]
        public void EditCategoryTest()
        {
            DataWorker.CreateCategory("TestCategory");
            Category c = db.Categories.FirstOrDefault(category => category.Id == 1);

            string result = DataWorker.EditCategory(c, "TestCategory1");

            Assert.AreEqual(result, "Категория изменена");
            db = new ApplicationContext();
            c = db.Categories.FirstOrDefault(category => category.Id == 1);
            Assert.AreEqual("TestCategory1", c.Name);
        }

        //[TestMethod]
        //public void DeleteCategoryTest()
        //{
        //    DataWorker.CreateCategory("TestCategory");
        //    Category category = db.Categories.FirstOrDefault(category => category.Id == 1);

        //    string result = DataWorker.DeleteCategory(category);

        //    Assert.AreEqual(result, "Категория TestCategory удалена");
        //    Assert.IsFalse(db.Categories.Contains(category));
        //}
        #endregion

        //#region Счета
        //[TestMethod]
        //public void CreatAccountTest()
        //{
        //    string result = DataWorker.CreateAccount("type1", "name1", 100);

        //    Assert.AreEqual(result, "Сделано");
        //}

        //[TestMethod]
        //public void CreatExistingAccountTest()
        //{
        //    DataWorker.CreateAccount("type1", "name1", 100);
        //    string result = DataWorker.CreateAccount("type1", "name1", 100);

        //    Assert.AreEqual(result, "Такой счет уже существует");
        //}

        //[TestMethod]
        //public void EditAccountTest()
        //{
        //    DataWorker.CreateAccount("type1", "name1", 100);
        //    string result = DataWorker.CreateAccount("type1", "name1", 100);

        //    Assert.AreEqual(result, "Такой счет уже существует");
        //}
        //#endregion

        //#region Операции

        //#endregion
    }
}


