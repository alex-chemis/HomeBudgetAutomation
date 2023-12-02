using AutoMapper;

namespace HomeBudgetAutomation.Mapper
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Models.Article, Dtos.Article.ArticleDto>().ReverseMap();
            CreateMap<Models.Article, Dtos.Article.CreateArticleDto>().ReverseMap();
            CreateMap<Models.Article, Dtos.Article.UpdateArticleDto>().ReverseMap();

            CreateMap<Models.Balance, Dtos.Balance.BalanceDto>().ReverseMap();
            CreateMap<Models.Balance, Dtos.Balance.FormBalanceDto>().ReverseMap();

            CreateMap<Models.Operation, Dtos.Operation.OperationDto>().ReverseMap();
            CreateMap<Models.Operation, Dtos.Operation.CreateOperationDto>().ReverseMap();
            CreateMap<Models.Operation, Dtos.Operation.UpdateOperationDto>().ReverseMap();
        }
    }
}
