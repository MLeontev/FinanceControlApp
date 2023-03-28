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
                bool checkIsExist = db.Users.Any(el => (el.Login == login && el.Password == password));
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

    }
}
