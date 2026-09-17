using FinSteady_API.Infrastructure;

namespace FinSteady_API.Repositories
{
    public static class RepositoryExtensions
    {
        public static void Map(this User dbUser, User user)
        {
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.PasswordHash = user.PasswordHash;
            dbUser.Email = user.Email;
            dbUser.PhoneNumber = user.PhoneNumber;
            dbUser.ProfileImage = user.ProfileImage;
            dbUser.Kycdoc = user.Kycdoc;
            dbUser.CreatedAt = user.CreatedAt;
            dbUser.UpdatedAt = user.UpdatedAt;
            dbUser.Status = user.Status;

        }

        public static void Map(this Card dbCard, Card card)
        {
            dbCard.CardId = card.CardId;
            dbCard.UserId = card.UserId;
            dbCard.TotalBalance = card.TotalBalance;
            dbCard.SavingBalance = card.SavingBalance;
            dbCard.AvailableBalance = card.AvailableBalance;
            dbCard.CreatedAt = card.CreatedAt;
            dbCard.UpdatedAt = card.UpdatedAt;

        }

        public static void Map(this SavingGoal dbSavingGoal, SavingGoal savingGoal)
        {
            dbSavingGoal.GoalId = savingGoal.GoalId;
            dbSavingGoal.UserId = savingGoal.UserId;
            dbSavingGoal.Name = savingGoal.Name;
            dbSavingGoal.Image = savingGoal.Image;
            dbSavingGoal.TargetAmount = savingGoal.TargetAmount;
            dbSavingGoal.TargetDate = savingGoal.TargetDate;
            dbSavingGoal.CategoryId = savingGoal.CategoryId;
            dbSavingGoal.SavingRuleId = savingGoal.SavingRuleId;
            dbSavingGoal.FundBalance = savingGoal.FundBalance;
            dbSavingGoal.CreatedAt = savingGoal.CreatedAt;
            dbSavingGoal.UpdatedAt = savingGoal.UpdatedAt;

        }

        public static void Map(this SavingRule dbSavingRule, SavingRule savingRule)
        {
            dbSavingRule.SavingRuleId = savingRule.SavingRuleId;
            dbSavingRule.Rulename = savingRule.Rulename;
            //   dbSavingRule.Description = savingRule.Description;
            dbSavingRule.CreatedAt = savingRule.CreatedAt;
            dbSavingRule.UpdatedAt = savingRule.UpdatedAt;

        }
        public static void Map(this Transaction dbTransaction, Transaction transaction)
        {
            dbTransaction.TransactionId = transaction.TransactionId;
            dbTransaction.Date = transaction.Date;
            dbTransaction.Amount = transaction.Amount;
            dbTransaction.FromUserId = transaction.FromUserId;
            dbTransaction.ToUserId = transaction.ToUserId;
            dbTransaction.TransactionType = transaction.TransactionType;
            dbTransaction.CreatedAt = transaction.CreatedAt;
            dbTransaction.UpdatedAt = transaction.UpdatedAt;

        }

        public static void Map(this Category dbCategory, Category category)
        {
            dbCategory.CategoryId = category.CategoryId;
            dbCategory.Name = category.Name;
            dbCategory.CreatedAt = category.CreatedAt;
            dbCategory.UpdatedAt = category.UpdatedAt;

        }
    }

}
