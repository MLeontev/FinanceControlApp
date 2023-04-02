using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinApp.Model
{
    class DateComparer : IComparer<Operation>
    {
        public int Compare(Operation? x, Operation? y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }
}
