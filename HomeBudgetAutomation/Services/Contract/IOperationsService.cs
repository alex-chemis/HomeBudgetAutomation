using HomeBudgetAutomation.Dtos;
using HomeBudgetAutomation.Dtos.Operation;
using HomeBudgetAutomation.ServiceResponder;

namespace HomeBudgetAutomation.Services.Contract
{
    public interface IOperationsService
    {
        ServiceResponse<OperationDto> Add(CreateOperationDto operation);
        ServiceResponse<OperationDto> GetById(int id);
        ServiceResponse<List<OperationDto>> GetAll();
        ServiceResponse<OperationDto> Update(int id, UpdateOperationDto operation);
        ServiceResponse<string> DeleteById(int id);
        ServiceResponse<List<OperationDto>> GetByDate(FunctionParamsDto functionParams);
    }
}
