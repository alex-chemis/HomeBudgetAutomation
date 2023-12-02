using HomeBudgetAutomation.Dtos;
using HomeBudgetAutomation.Models;
using System.Collections;

namespace HomeBudgetAutomation.Repositories.Contract
{
    public interface IOperationsRepository
    {
        bool Create(Operation operation);
        Operation Get(int id);
        ICollection<Operation> GetAll();
        bool Update(Operation operation);
        bool Delete(int id);
        ICollection<Operation> GetByDate(FunctionParamsDto functionParams);
    }
}
