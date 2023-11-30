using HomeBudgetAutomation.Dtos.Article;
using HomeBudgetAutomation.ServiceResponder;

namespace HomeBudgetAutomation.Services.Contract
{
    public interface IArticlesService
    {
        ServiceResponse<ArticleDto> Add(CreateArticleDto article);
        ServiceResponse<ArticleDto> GetById(int id);
        ServiceResponse<List<ArticleDto>> GetAll();
        ServiceResponse<ArticleDto> Update(int id, UpdateArticleDto article);
        ServiceResponse<string> DeleteById(int id);
    }
}
