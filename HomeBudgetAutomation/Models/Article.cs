using System;
using System.Collections.Generic;

namespace HomeBudgetAutomation.Models
{
    public partial class Article
    {
        public Article()
        {
            Operations = new HashSet<Operation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
