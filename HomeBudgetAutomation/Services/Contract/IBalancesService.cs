using HomeBudgetAutomation.Dtos.Balance;
using HomeBudgetAutomation.Dtos.Operation;
using HomeBudgetAutomation.ServiceResponder;

namespace HomeBudgetAutomation.Services.Contract
{
    public interface IBalancesService
    {
        ServiceResponse<BalanceDto> Form(DateTime formDate);
        ServiceResponse<BalanceDto> GetById(int id);
        ServiceResponse<List<BalanceDto>> GetAll();
        ServiceResponse<string> SoftDelete(int id);
    }
}
