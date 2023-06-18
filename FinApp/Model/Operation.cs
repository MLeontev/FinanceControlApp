using FinApp.Model.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Operation o)
                return Amount == o.Amount
                    && Date == o.Date
                    && IsIncome == o.IsIncome
                    && AccountId == o.AccountId
                    && CategoryId == o.CategoryId;
            return false;
        }
    }
}
