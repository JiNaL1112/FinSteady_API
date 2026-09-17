using FinSteady_API.Infrastructure;
using FinSteady_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSteady_API.Repositories
{
    public class SavingRuleRepository : RepositoryBase<SavingRule>, ISavingRuleRepository
    {
        private readonly SmartSaverDatabaseContext databaseContext;

        public SavingRuleRepository(SmartSaverDatabaseContext smartSaverDatabaseContext)
            : base(smartSaverDatabaseContext)
        {
            this.databaseContext = smartSaverDatabaseContext;
        }

        public async Task<IEnumerable<SavingRule>> GetSavingRules()
        {
            return await this.Find()
                .OrderByDescending(a => a.SavingRuleId)
                .ToListAsync();
        }

        public async Task<SavingRule> GetSavingRuleById(int id)
        {
            return await this.Find(d => d.SavingRuleId == id)
                .SingleOrDefaultAsync();
        }

        public async Task<SavingRule> AddSavingRule(SavingRule savingRule)
        {
            this.CreateEntity(savingRule);
            await this.SaveAsync();
            return savingRule;
        }

        public async Task<SavingRule> UpdateSavingRule(SavingRule dbSavingRule, SavingRule savingRule)
        {
            savingRule.SavingRuleId = dbSavingRule.SavingRuleId;

            dbSavingRule.Map(savingRule);
            this.UpdateEntity(dbSavingRule);

            await this.SaveAsync();
            return dbSavingRule;
        }

        public async Task DeleteSavingRule(SavingRule savingRule)
        {
            this.DeleteEntity(savingRule);
            await this.SaveAsync();
        }
    }
}
