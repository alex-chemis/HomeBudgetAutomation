namespace HomeBudgetAutomation.Dtos
{
    public class FunctionParamsDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> ArticleIds { get; set; } = null!; 
    }
}
