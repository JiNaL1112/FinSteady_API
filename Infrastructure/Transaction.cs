namespace FinSteady_API.Infrastructure;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public int? FromUserId { get; set; }

    public int? ToUserId { get; set; }

    public string TransactionType { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? FromUser { get; set; }

    public virtual User? ToUser { get; set; }
}
