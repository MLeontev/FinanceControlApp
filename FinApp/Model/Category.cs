using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinApp.Model.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FinApp.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Operation> Operations { get; set; }

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Category c = (Category)obj;

            if (Name != c.Name)
            {
                return false;
            }

            if (Operations == null && c.Operations == null)
            {
                return true;
            }

            if (Operations == null || c.Operations == null || Operations.Count != c.Operations.Count)
            {
                return false;
            }

            for (int i = 0; i < Operations.Count; i++)
            {
                if (!Operations[i].Equals(c.Operations[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
