namespace HomeBudgetAutomation.ServiceResponder
{
    public class ServiceResponse<T>
    {

        public T? Data { get; set; }
        public ServiceMessageType Message { get; set; }
        public List<string>? ErrorMessages { get; set; } = null;
    }
}
