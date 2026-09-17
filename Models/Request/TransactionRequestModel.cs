using FinSteady_API.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinSteady_API.Models.Request
{
    public class TransactionRequestModel
    {
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        public int? FromUserId { get; set; }

        public int? ToUserId { get; set; }

        [Required(ErrorMessage = "Transaction Type is required.")]
        [StringLength(50, ErrorMessage = "Transaction Type cannot be longer than 50 characters.")]
        public string TransactionType { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Transaction ToEntity()
        {
            return new Transaction
            {
                TransactionId = this.TransactionId,
                Date = this.Date,
                Amount = this.Amount,
                FromUserId = this.FromUserId,
                ToUserId = this.ToUserId,
                TransactionType = this.TransactionType,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }
    }
}
