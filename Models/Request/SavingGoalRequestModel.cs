using FinSteady_API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace FinSteady_API.Models.Request
{
    public class SavingGoalRequestModel
    {
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        public string? Image { get; set; }

        [Required(ErrorMessage = "Target Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Target Amount must be a positive value.")]
        public decimal TargetAmount { get; set; }

        [Required(ErrorMessage = "Target Date is required.")]
        [CustomValidation(typeof(SavingGoalRequestModel), nameof(ValidateTargetDate))]
        public DateOnly TargetDate { get; set; }

        public int? CategoryId { get; set; }

        public int? SavingRuleId { get; set; }

        [Required(ErrorMessage = "Fund Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Fund Balance cannot be negative.")]
        public decimal FundBalance { get; set; }

        public SavingGoal ToEntity()
        {
            return new SavingGoal
            {
                UserId = this.UserId,
                Name = this.Name,
                Image = this.Image,
                TargetAmount = this.TargetAmount,
                TargetDate = this.TargetDate,
                CategoryId = this.CategoryId,
                SavingRuleId = this.SavingRuleId,
                FundBalance = this.FundBalance
            };
        }

        public static ValidationResult? ValidateTargetDate(DateOnly targetDate, ValidationContext context)
        {
            if (targetDate < DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("Target Date cannot be in the past.");
            }
            return ValidationResult.Success;
        }
    }
}
