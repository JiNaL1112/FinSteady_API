using FinSteady_API.Infrastructure;
using FinSteady_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinSteady_API.Repositories
{

    public class SavingGoalRepository : RepositoryBase<SavingGoal>, ISavingGoalRepository
    {
        private readonly SmartSaverDatabaseContext calistaContext;

        public SavingGoalRepository(SmartSaverDatabaseContext smartSaverDatabaseContext)
        : base(smartSaverDatabaseContext)
        {
            this.calistaContext = calistaContext;
        }

        public async Task<IEnumerable<SavingGoal>> GetSavingGoals()
        {
            return await this.Find()
            .OrderByDescending(a => a.GoalId)
            .ToListAsync();
        }

        public async Task<SavingGoal> GetSavingGoalById(int id)
        {
            return await this.Find(d => d.GoalId == id)
            .SingleOrDefaultAsync();
        }

        public async Task<SavingGoal> AddSavingGoal(SavingGoal saving)
        {

            this.CreateEntity(saving);

            await this.SaveAsync();

            return saving;
        }

        public async Task<SavingGoal> UpdateSavingGoal(SavingGoal dbSavingGoal, SavingGoal savingGoal)
        {
            savingGoal.GoalId = dbSavingGoal.GoalId;


            dbSavingGoal.Map(savingGoal);
            this.UpdateEntity(dbSavingGoal);

            await this.SaveAsync();
            return dbSavingGoal;
        }

        public async Task DeleteSavingGoal(SavingGoal savingGoal)
        {
            this.DeleteEntity(savingGoal);

            await this.SaveAsync();
        }

    }

}


