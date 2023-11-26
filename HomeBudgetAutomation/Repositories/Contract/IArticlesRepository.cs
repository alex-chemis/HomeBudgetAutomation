using HomeBudgetAutomation.Models;

namespace HomeBudgetAutomation.Repositories.Contract
{
    public interface IArticlesRepository
    {
        bool Create(Article article);

        bool Update(Article article);

        Article Get(int id);

        ICollection<Article> GetAll();

        bool Delete(int id);
    }
}
