using FinSteady_API.Infrastructure;
using FinSteady_API.Repositories.Interface;
using FinSteady_API.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinSteady_API.Services
{
    public class DummyDataService : IDummyDataService
    {
        private readonly SmartSaverDatabaseContext _dbContext;
        private readonly ICardRepository _cardRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISavingGoalRepository _savingGoalRepository;
        private readonly ISavingRuleRepository _savingRuleRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;

        public DummyDataService(
            SmartSaverDatabaseContext dbContext,
            ICardRepository cardRepository,
            ICategoryRepository categoryRepository,
            ISavingGoalRepository savingGoalRepository,
            ISavingRuleRepository savingRuleRepository,
            ITransactionRepository transactionRepository,
            IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _cardRepository = cardRepository;
            _categoryRepository = categoryRepository;
            _savingGoalRepository = savingGoalRepository;
            _savingRuleRepository = savingRuleRepository;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        public async Task EnsureAllDummyDataAsync()
        {
            await EnsureDummyDataForTable<Card>(() => AddDummyCards());
            await EnsureDummyDataForTable<Category>(() => AddDummyCategories());
            await EnsureDummyDataForTable<SavingGoal>(() => AddDummySavingGoals());
            await EnsureDummyDataForTable<SavingRule>(() => AddDummySavingRules());
            await EnsureDummyDataForTable<Transaction>(() => AddDummyTransactions());
            await EnsureDummyDataForTable<User>(() => AddDummyUsers());
        }

        private async Task EnsureDummyDataForTable<TEntity>(Func<Task> addDummyDataFunc) where TEntity : class
        {
            if (!_dbContext.Set<TEntity>().Any())
            {
                await addDummyDataFunc();
            }
        }

        private async Task AddDummyCards()
        {
            var cards = new[]
            {
                new Card { UserId = 1, TotalBalance = 1000, SavingBalance = 500, AvailableBalance = 500, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Card { UserId = 2, TotalBalance = 2000, SavingBalance = 1000, AvailableBalance = 1000, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                // Add more dummy data as needed
            };

            foreach (var card in cards)
            {
                await _cardRepository.AddCard(card);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task AddDummyCategories()
        {
            var categories = new[]
            {
                new Category { Name = "Category 1", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Category { Name = "Category 2", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                // Add more dummy data as needed
            };

            foreach (var category in categories)
            {
                await _categoryRepository.AddCategory(category);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task AddDummySavingGoals()
        {
            var savingGoals = new[]
            {
                new SavingGoal { UserId = 1, Name = "Goal 1", TargetAmount = 5000, TargetDate =DateOnly.FromDateTime(DateTime.Now) , FundBalance = 0, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new SavingGoal { UserId = 2, Name = "Goal 2", TargetAmount = 10000, TargetDate = DateOnly.FromDateTime(DateTime.Now), FundBalance = 0, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                // Add more dummy data as needed
            };

            foreach (var goal in savingGoals)
            {
                await _savingGoalRepository.AddSavingGoal(goal);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task AddDummySavingRules()
        {
            var savingRules = new[]
            {
                new SavingRule { Rulename = "Rule 1", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new SavingRule { Rulename = "Rule 2", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                // Add more dummy data as needed
            };

            foreach (var rule in savingRules)
            {
                await _savingRuleRepository.AddSavingRule(rule);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task AddDummyTransactions()
        {
            var transactions = new[]
            {
                new Transaction { Date = DateTime.UtcNow, Amount = 100, FromUserId = 1, ToUserId = 2, TransactionType = "Transfer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Transaction { Date = DateTime.UtcNow, Amount = 200, FromUserId = 2, ToUserId = 1, TransactionType = "Transfer", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                // Add more dummy data as needed
            };

            foreach (var transaction in transactions)
            {
                await _transactionRepository.AddTransaction(transaction);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task AddDummyUsers()
        {
            var users = new[]
            {
                new User { Email = "user1@example.com", PasswordHash = "hashed_password", FirstName = "User", LastName = "One", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Email = "user2@example.com", PasswordHash = "hashed_password", FirstName = "User", LastName = "Two", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                // Add more dummy data as needed
            };

            foreach (var user in users)
            {
                await _userRepository.AddUser(user);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
