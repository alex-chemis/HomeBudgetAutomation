using HomeBudgetAutomation.Data;
using HomeBudgetAutomation.Dtos;
using HomeBudgetAutomation.Models;
using HomeBudgetAutomation.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace HomeBudgetAutomation.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticlesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Article article)
        {
            var result = _context.Database.ExecuteSqlInterpolated($"INSERT INTO articles (name) VALUES ({article.Name})");

            if (Check(result))
            {
                article.Id = _context.Articles.OrderBy(p => p.Id).Last().Id;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            return Check(_context.Database.ExecuteSqlInterpolated($"DELETE FROM articles WHERE id = {id}"));
        }

        public Article Get(int id)
        {
            return _context.Articles.FromSqlInterpolated($"SELECT * FROM articles WHERE id = {id}").ToList().First();
        }

        public ICollection<Article> GetAll()
        {
            return _context.Articles.FromSqlInterpolated($"SELECT * FROM articles").ToList();
        }

        public bool Update(Article article)
        {
            return Check(_context.Database.ExecuteSqlInterpolated($"UPDATE articles SET name={article.Name} WHERE id={article.Id}"));
        }

        public ICollection<ArticlePercentageDto> Percentage(CursorParamsDto cursorParams)
        {
            List<ArticlePercentageDto> list = new();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM calculate_percentage_by_item(@start_date, @end_date, ARRAY[@articel_ids], @flow_type)";
                var startDate = new NpgsqlParameter("@start_date", cursorParams.StartDate);
                var endDate = new NpgsqlParameter("@end_date", cursorParams.EndDate);
                var articleIds = new NpgsqlParameter("@articel_ids", cursorParams.ArticleIds.ToArray());
                var flowType = new NpgsqlParameter("@flow_type", cursorParams.FlowType);
                command.Parameters.Add(startDate);
                command.Parameters.Add(endDate);
                command.Parameters.Add(articleIds);
                command.Parameters.Add(flowType);

                _context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            list.Add(new ArticlePercentageDto()
                            {
                                Id = result.GetInt32(0),
                                Percentage = result.GetDecimal(1),
                            });
                        }
                    }
                }
            }
            return list;
        }

        private bool Check(int executedRows)
        {
            return executedRows > 0 ? true : false;
        }
    }
}
