using HomeBudgetAutomation.Data;
using HomeBudgetAutomation.Models;
using HomeBudgetAutomation.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace HomeBudgetAutomation.Repositories
{
    public class OperationsRepository : IOperationsRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Operation operation)
        {
            var articleId = new NpgsqlParameter("@article_id", operation.ArticleId);
            var debit = new NpgsqlParameter("@debit", operation.Debit);
            var credit = new NpgsqlParameter("@credit", operation.Credit);
            var createDate = new NpgsqlParameter("@create_date", operation.CreateDate);
            var balanceId = new NpgsqlParameter("@balance_id", DBNull.Value);

            var result = _context.Database.ExecuteSqlRaw("INSERT INTO operations VALUES (default, @article_id, @debit, @credit, @create_date, @balance_id)",
                articleId, debit, credit, createDate, balanceId);

            if (Check(result))
            {
                operation.Id = _context.Operations.Last().Id;
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            return Check(_context.Database.ExecuteSqlInterpolated($"DELETE FROM operations WHERE id = {id}"));
        }

        public Operation Get(int id)
        {
            return _context.Operations.FromSqlInterpolated($"SELECT * FROM operations WHERE id = {id}").ToList().First();
        }

        public ICollection<Operation> GetAll()
        {
            return _context.Operations.FromSqlInterpolated($"SELECT * FROM operations").ToList();
        }

        public bool Update(Operation operation)
        {
            var articleId = new NpgsqlParameter("@article_id", operation.ArticleId);
            var debit = new NpgsqlParameter("@debit", operation.Debit);
            var credit = new NpgsqlParameter("@credit", operation.Credit);
            var createDate = new NpgsqlParameter("@create_date", operation.CreateDate);

            var result = _context.Database.ExecuteSqlRaw("UPDATE operations SET debit=@debit credit=@credit create_date=@create_date WHERE id=@article_id",
                    articleId, debit, credit, createDate);
            
            return Check(result);
        }

        private bool Check(int executedRows)
        {
            return executedRows > 0 ? true : false;
        }
    }
}
