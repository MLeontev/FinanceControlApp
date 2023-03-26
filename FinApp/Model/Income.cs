using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinApp.Model
{
    public class Income
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
    }
}
