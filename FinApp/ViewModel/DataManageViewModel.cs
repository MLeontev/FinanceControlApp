using FinApp.Model.Data;
using FinApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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






        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
