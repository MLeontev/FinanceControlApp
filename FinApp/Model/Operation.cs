using FinApp.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinApp.Model
{
    public class Operation
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public int IsIncome { get; set; }

        [NotMapped]
        public Account OperationAccount
        {
            get
            {
                return DataWorker.GetAccountById(AccountId);
            }
        }

        [NotMapped]
        public Category OperationCategory
        {
            get
            {
                return DataWorker.GetCategoryById(CategoryId);
            }
        }

        [NotMapped]
        public string OperationDate
        {
            get
            {
                return Date.ToString("D");
            }
        }
    }
}
