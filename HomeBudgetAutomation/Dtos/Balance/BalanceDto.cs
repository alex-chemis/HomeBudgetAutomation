namespace HomeBudgetAutomation.Dtos.Balance
{
    public class BalanceDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Amount { get; set; }
    }
}
