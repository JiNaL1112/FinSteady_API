using FinSteady_API.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinSteady_API.Repositories.Interface
{
    /// <summary>
    /// Interface for Transaction repository.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns>A list of transactions.</returns>
        Task<IEnumerable<Transaction>> GetTransactions();

        /// <summary>
        /// Gets a transaction by its ID.
        /// </summary>
        /// <param name="id">The ID of the transaction.</param>
        /// <returns>The transaction with the specified ID.</returns>
        Task<Transaction> GetTransactionById(int id);

        /// <summary>
        /// Adds a new transaction.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        /// <returns>The added transaction.</returns>
        Task<Transaction> AddTransaction(Transaction transaction);

        /// <summary>
        /// Updates an existing transaction.
        /// </summary>
        /// <param name="dbTransaction">The existing transaction in the database.</param>
        /// <param name="transaction">The new transaction data.</param>
        /// <returns>The updated transaction.</returns>
        Task<Transaction> UpdateTransaction(Transaction dbTransaction, Transaction transaction);

        /// <summary>
        /// Deletes a transaction.
        /// </summary>
        /// <param name="transaction">The transaction to delete.</param>
        Task DeleteTransaction(Transaction transaction);
    }
}
