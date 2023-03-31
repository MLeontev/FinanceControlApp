using FinApp.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinApp.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Balance { get; set; }
        public List<Operation> Operations { get; set; }

        [NotMapped]
        public int CurrentBalance
        {
            get
            {
                List<Operation> list = DataWorker.GetAllOperationsByAccountId(Id);
                int balance = Balance;
                foreach (Operation op in list)
                {
                    if (op.IsIncome == 1)
                    {
                        balance += op.Amount;
                    }
                    else
                    {
                        balance -= op.Amount;
                    }
                }
                return balance;
            }
        }
    }
}
