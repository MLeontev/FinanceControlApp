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

        [TestMethod]
        public void CreateCategotyTest()
        {
            string result = DataWorker.CreateCategory("TestCategory");

            Assert.AreEqual(result, "Сделано");
        }

        [TestMethod]
        public void CreateExistingCategotyTest()
        {
            DataWorker.CreateCategory("TestCategory");
            string result = DataWorker.CreateCategory("TestCategory");

            Assert.AreEqual(result, "Такая категория уже существует");
        }
    }
}


