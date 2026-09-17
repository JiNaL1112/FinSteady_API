using FinSteady_API.Infrastructure;

namespace FinSteady_API.Repositories.Interface
{
    public interface ISavingGoalRepository
    {

        Task<IEnumerable<SavingGoal>> GetSavingGoals();

        Task<SavingGoal> GetSavingGoalById(int id);

        Task<SavingGoal> AddSavingGoal(SavingGoal savingGoal);

        Task<SavingGoal> UpdateSavingGoal(SavingGoal dbSavingGoal, SavingGoal savingGoal);

        Task DeleteSavingGoal(SavingGoal savingGoal);
    }
}
