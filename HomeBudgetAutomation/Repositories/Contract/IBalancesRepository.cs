using HomeBudgetAutomation.Models;

namespace HomeBudgetAutomation.Repositories.Contract
{
    public interface IBalancesRepository
    {
        ICollection<Operation> GetAll();
        Balance Get(int id);
        bool Form(DateTime date);
        bool Disband(int id);
    }
}
