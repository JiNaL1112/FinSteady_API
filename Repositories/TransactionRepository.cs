using FinSteady_API.Infrastructure;
using FinSteady_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSteady_API.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        private readonly SmartSaverDatabaseContext databaseContext;

        public TransactionRepository(SmartSaverDatabaseContext smartSaverDatabaseContext)
            : base(smartSaverDatabaseContext)
        {
            this.databaseContext = smartSaverDatabaseContext;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await this.Find()
                .OrderByDescending(a => a.TransactionId)
                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await this.Find(d => d.TransactionId == id)
                .SingleOrDefaultAsync();
        }

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            this.CreateEntity(transaction);
            await this.SaveAsync();
            return transaction;
        }

        public async Task<Transaction> UpdateTransaction(Transaction dbTransaction, Transaction transaction)
        {
            transaction.TransactionId = dbTransaction.TransactionId;

            dbTransaction.Map(transaction);
            this.UpdateEntity(dbTransaction);

            await this.SaveAsync();
            return dbTransaction;
        }

        public async Task DeleteTransaction(Transaction transaction)
        {
            this.DeleteEntity(transaction);
            await this.SaveAsync();
        }
    }
}
