using HomeBudgetAutomation.Data;
using HomeBudgetAutomation.Models;
using HomeBudgetAutomation.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

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

        private bool Check(int executedRows)
        {
            return executedRows > 0 ? true : false;
        }
    }
}
