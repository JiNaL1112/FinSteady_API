namespace FinSteady_API.Infrastructure;

public partial class SavingRule
{
    public int SavingRuleId { get; set; }

    public string Rulename { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<SavingGoal> SavingGoals { get; set; } = new List<SavingGoal>();
}
