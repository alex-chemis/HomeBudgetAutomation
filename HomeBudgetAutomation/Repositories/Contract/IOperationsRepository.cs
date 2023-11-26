using HomeBudgetAutomation.Models;

namespace HomeBudgetAutomation.Repositories.Contract
{
    public interface IOperationsRepository
    {
        bool Create(Operation operation);
        bool Update(Operation operation);
        Operation Get(int id);
        ICollection<Operation> GetAll();
        bool Delete(int id);
    }
}
