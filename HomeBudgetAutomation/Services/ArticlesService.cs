using AutoMapper;
using HomeBudgetAutomation.Dtos;
using HomeBudgetAutomation.Dtos.Article;
using HomeBudgetAutomation.Models;
using HomeBudgetAutomation.Repositories.Contract;
using HomeBudgetAutomation.ServiceResponder;
using HomeBudgetAutomation.Services.Contract;

namespace HomeBudgetAutomation.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesRepository _repository;
        private readonly IMapper _mapper;

        public ArticlesService(IArticlesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ServiceResponse<ArticleDto> Add(CreateArticleDto article)
        {
            ServiceResponse<ArticleDto> response = new();
            try
            {
                var newArticle = _mapper.Map<Article>(article);
                
                if (!_repository.Create(newArticle))
                {
                    response.Data = null;
                    response.Message = ServiceMessageType.InternalServerError;
                    return response;
                }

                response.Data = _mapper.Map<ArticleDto>(newArticle);
                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.InternalServerError;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };   
            }
            return response;
        }

        public ServiceResponse<string> DeleteById(int id)
        {
            ServiceResponse<string> response = new();
            try
            {
                if (!_repository.Delete(id))
                {
                    response.Data = null;
                    response.Message = ServiceMessageType.NotFound;
                    return response;
                }

                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.InternalServerError;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public ServiceResponse<List<ArticleDto>> GetAll()
        {
            ServiceResponse<List<ArticleDto>> response = new();
            try
            {
                var articles = _repository.GetAll();
                response.Data = new List<ArticleDto>();

                foreach (var article in articles)
                {
                    response.Data.Add(_mapper.Map<ArticleDto>(article));
                }

                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.InternalServerError;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public ServiceResponse<ArticleDto> GetById(int id)
        {
            ServiceResponse<ArticleDto> response = new();
            try
            {
                response.Data = _mapper.Map<ArticleDto>(_repository.Get(id));
                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.NotFound;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public ServiceResponse<ArticleDto> Update(int id, UpdateArticleDto article)
        {
            ServiceResponse<ArticleDto> response = new();
            try
            {
                var updatedArticle = _mapper.Map<Article>(article);
                updatedArticle.Id = id;

                if (!_repository.Update(updatedArticle))
                {
                    response.Data = null;
                    response.Message = ServiceMessageType.NotFound;
                    return response;
                }

                response.Data = _mapper.Map<ArticleDto>(updatedArticle);
                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.InternalServerError;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public ServiceResponse<List<ArticlePercentageDto>> Percentage(CursorParamsDto cursorParams)
        {
            ServiceResponse<List<ArticlePercentageDto>> response = new();
            try
            {
                var articles = _repository.Percentage(cursorParams);
                response.Data = new List<ArticlePercentageDto>();

                foreach (var article in articles)
                {
                    response.Data.Add(article);
                }

                response.Message = ServiceMessageType.Ok;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ServiceMessageType.InternalServerError;
                response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return response;
        }
    }
}
