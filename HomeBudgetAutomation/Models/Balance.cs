using System;
using System.Collections.Generic;

namespace HomeBudgetAutomation.Models
{
    public class Balance
    {
        public Balance()
        {
            Operations = new HashSet<Operation>();
        }

        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Amount { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
