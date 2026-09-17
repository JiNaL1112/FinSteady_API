

using System;
using System.ComponentModel.DataAnnotations;
using FinSteady_API.Infrastructure;

namespace FinSteady_API.Models.Request
{
    public class CardRequestModel
    {
        public int CardId { get; set; }

        public int? UserId { get; set; }

        [Required(ErrorMessage = "Total Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Total Balance cannot be negative.")]
        public decimal TotalBalance { get; set; }

        [Required(ErrorMessage = "Saving Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Saving Balance cannot be negative.")]
        public decimal SavingBalance { get; set; }

        [Required(ErrorMessage = "Available Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Available Balance cannot be negative.")]
        public decimal AvailableBalance { get; set; }

        public Card ToEntity()
        {
            return new Card
            {
                CardId = this.CardId,
                UserId = this.UserId,
                TotalBalance = this.TotalBalance,
                SavingBalance = this.SavingBalance,
                AvailableBalance = this.AvailableBalance,
            };
        }
    }
}


