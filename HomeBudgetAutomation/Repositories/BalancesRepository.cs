using HomeBudgetAutomation.Data;
using HomeBudgetAutomation.Models;
using HomeBudgetAutomation.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Globalization;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HomeBudgetAutomation.Repositories
{
    public class BalancesRepository : IBalancesRepository
    {
        private readonly ApplicationDbContext _context;

        public BalancesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool SoftDelete(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlInterpolated($"UPDATE operations SET balance_id = {null} WHERE balance_id = {id}");
                    _context.Operations.OrderBy(p => p.Id);
                    var result = _context.Database.ExecuteSqlInterpolated($"DELETE FROM balance WHERE id = {id} ");
                    
                    if (Check(result))
                    {
                        transaction.Commit();
                        return true;
                    }

                    transaction.Rollback();
                    return false;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Create(Balance balance)
        {
            using (var transaction = _context.Database.BeginTransaction())
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                try
                {
                    command.CommandText = File.ReadAllText(string.Format(
                        CultureInfo.InvariantCulture,
                        "{1}{0}SqlScripts{0}{2}",
                        Path.DirectorySeparatorChar,
                        AppContext.BaseDirectory,
                        "form_balance.sql"));

                    NpgsqlParameter param = new NpgsqlParameter("@n_current_date", balance.CreateDate);
                    command.Parameters.Add(param);

                    var result = command.ExecuteNonQuery();

                    if (Check(result))
                    {
                        var last = _context.Balances.OrderBy(p => p.Id).Last();
                        balance.Id = last.Id;
                        balance.Debit = last.Debit;
                        balance.Credit = last.Credit;
                        balance.Amount = last.Amount;
                        balance.CreateDate = last.CreateDate;

                        transaction.Commit();

                        return true;
                    }

                    transaction.Rollback();
                    return false;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Balance Get(int id)
        {
            return _context.Balances.FromSqlInterpolated($"SELECT * FROM balance WHERE id = {id}").ToList().First();
        }

        public ICollection<Balance> GetAll()
        {
            return _context.Balances.FromSqlInterpolated($"SELECT * FROM balance").ToList();
        }
        private bool Check(int executedRows)
        {
            return executedRows > 0 ? true : false;
        }
    }
}
