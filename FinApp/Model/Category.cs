using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinApp.Model.Data;

namespace FinApp.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public int CategoryExpensesSum
        {
            get
            {
                List<Operation> list = DataWorker.GetAllExpensesByCategoryId(Id);
                int sum = 0;
                foreach (Operation op in list)
                {
                    sum += op.Amount;
                }
                return sum;
            }
        }

        [NotMapped]
        public int CategoryIncomesSum
        {
            get
            {
                List<Operation> list = DataWorker.GetAllIncomesByCategoryId(Id);
                int sum = 0;
                foreach (Operation op in list)
                {
                    sum += op.Amount;
                }
                return sum;
            }
        }

        [NotMapped]
        public int CategoryIncomePercent
        {
            get
            {
                List<Operation> list = DataWorker.GetAllIncomesByCategoryId(Id);

                List<Operation> incomes = DataWorker.GetAllIncomes();
                int total = 0;
                foreach (Operation op in incomes)
                {
                    total += op.Amount;
                }

                int categoryIncomes = 0;
                foreach (Operation op in list)
                {
                    categoryIncomes += op.Amount;
                }

                if (total == 0) 
                    return 0;
                else
                    return categoryIncomes/total * 100;
            }
        }

        [NotMapped]
        public int CategoryExpensePercent
        {
            get
            {
                List<Operation> list = DataWorker.GetAllExpensesByCategoryId(Id);

                List<Operation> expenses = DataWorker.GetAllExpenses();
                int total = 0;
                foreach (Operation op in expenses)
                {
                    total += op.Amount;
                }

                int categryExpenses = 0;
                foreach (Operation op in list)
                {
                    categryExpenses += op.Amount;
                }

                if (total == 0) 
                    return 0;
                else
                    return categryExpenses / total * 100;
            }
        }
    }
}
