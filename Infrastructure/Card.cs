namespace FinSteady_API.Infrastructure;

public partial class Card
{
    public int CardId { get; set; }

    public int? UserId { get; set; }

    public decimal TotalBalance { get; set; }

    public decimal SavingBalance { get; set; }

    public decimal AvailableBalance { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }
}
