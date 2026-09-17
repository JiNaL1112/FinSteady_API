namespace FinSteady_API.Infrastructure;

public partial class SavingGoal
{
    public int GoalId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public decimal TargetAmount { get; set; }

    public DateOnly TargetDate { get; set; }

    public int? CategoryId { get; set; }

    public int? SavingRuleId { get; set; }

    public decimal FundBalance { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual SavingRule? SavingRule { get; set; }

    public virtual User? User { get; set; }
}
