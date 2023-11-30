namespace HomeBudgetAutomation.Dtos.Operation
{
    public class OperationDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public DateTime CreateDate { get; set; }
        public int? BalanceId { get; set; }
    }
}
