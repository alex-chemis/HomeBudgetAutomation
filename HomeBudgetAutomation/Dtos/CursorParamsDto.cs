namespace HomeBudgetAutomation.Dtos
{
    public class CursorParamsDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> ArticleIds { get; set; } = null!;
        public string FlowType { get; set; } = null!;
    }
}
