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
        public string OperationDate
        {
            get
            {
                return Date.ToString("D");
            }
        }

        [NotMapped]
        public string OperationType
        {
            get
            {
                return IsIncome == 1 ? "Пополнение" : "Расход";
            }
        }

        [NotMapped]
        public string ChangeInBalance
        {
            get
            {
                return IsIncome == 1 ? $"+{Amount}" : $"-{Amount}";
            }
        }
    }
}
