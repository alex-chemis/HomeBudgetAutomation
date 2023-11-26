using System;
using System.Collections.Generic;

namespace HomeBudgetAutomation.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public int? ArticleId { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? BalanceId { get; set; }

        public virtual Article? Article { get; set; }
        public virtual Balance? Balance { get; set; }
    }
}
