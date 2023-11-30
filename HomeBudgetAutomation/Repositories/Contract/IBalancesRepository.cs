using HomeBudgetAutomation.Models;

namespace HomeBudgetAutomation.Repositories.Contract
{
    public interface IBalancesRepository
    {
        ICollection<Balance> GetAll();
        Balance Get(int id);
        bool Create(Balance balance);
        bool SoftDelete(int id);
    }
}
