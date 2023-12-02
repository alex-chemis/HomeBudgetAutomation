using HomeBudgetAutomation.Dtos;
using HomeBudgetAutomation.Models;

namespace HomeBudgetAutomation.Repositories.Contract
{
    public interface IArticlesRepository
    {
        bool Create(Article article);
        Article Get(int id);
        ICollection<Article> GetAll();
        bool Update(Article article);
        bool Delete(int id);
        ICollection<ArticlePercentageDto> Percentage(CursorParamsDto cursorParams);
    }
}
