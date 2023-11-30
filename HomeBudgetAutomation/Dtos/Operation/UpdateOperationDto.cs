namespace HomeBudgetAutomation.Dtos.Operation
{
    public class UpdateOperationDto
    {
        public int ArticleId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
