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
            var result = _context.Database.ExecuteSqlRaw("INSERT INTO operations VALUES (default, @article_id, @debit, @credit, @create_date, @balance_id})",
                new NpgsqlParameter("@article_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = operation.ArticleId },
                new NpgsqlParameter("@debit", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = operation.Debit},
                new NpgsqlParameter("@credit", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = operation.Credit },
                new NpgsqlParameter("@create_date", NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = operation.CreateDate },
                new NpgsqlParameter("@balance_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = operation.BalanceId });

            return Check(result);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Operation Get(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Operation> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Operation operation)
        {
            throw new NotImplementedException();
        }

        private bool Check(int executedRows)
        {
            return executedRows >= 0 ? true : false;
        }
    }
}
