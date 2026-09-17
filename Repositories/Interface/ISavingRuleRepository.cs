using FinSteady_API.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinSteady_API.Repositories.Interface
{
    /// <summary>
    /// Interface for SavingRule repository.
    /// </summary>
    public interface ISavingRuleRepository
    {
        /// <summary>
        /// Gets all saving rules.
        /// </summary>
        /// <returns>A list of saving rules.</returns>
        Task<IEnumerable<SavingRule>> GetSavingRules();

        /// <summary>
        /// Gets a saving rule by its ID.
        /// </summary>
        /// <param name="id">The ID of the saving rule.</param>
        /// <returns>The saving rule with the specified ID.</returns>
        Task<SavingRule> GetSavingRuleById(int id);

        /// <summary>
        /// Adds a new saving rule.
        /// </summary>
        /// <param name="savingRule">The saving rule to add.</param>
        /// <returns>The added saving rule.</returns>
        Task<SavingRule> AddSavingRule(SavingRule savingRule);

        /// <summary>
        /// Updates an existing saving rule.
        /// </summary>
        /// <param name="dbSavingRule">The existing saving rule in the database.</param>
        /// <param name="savingRule">The new saving rule data.</param>
        /// <returns>The updated saving rule.</returns>
        Task<SavingRule> UpdateSavingRule(SavingRule dbSavingRule, SavingRule savingRule);

        /// <summary>
        /// Deletes a saving rule.
        /// </summary>
        /// <param name="savingRule">The saving rule to delete.</param>
        Task DeleteSavingRule(SavingRule savingRule);
    }
}
