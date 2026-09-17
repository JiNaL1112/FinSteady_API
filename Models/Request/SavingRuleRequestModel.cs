using FinSteady_API.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinSteady_API.Models.Request
{
    public class SavingRuleRequestModel
    {
        public int SavingRuleId { get; set; }

        [Required(ErrorMessage = "Rule name is required.")]
        [StringLength(100, ErrorMessage = "Rule name cannot be longer than 100 characters.")]
        public string Rulename { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public SavingRule ToEntity()
        {
            return new SavingRule
            {
                SavingRuleId = this.SavingRuleId,
                Rulename = this.Rulename,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }
    }
}
