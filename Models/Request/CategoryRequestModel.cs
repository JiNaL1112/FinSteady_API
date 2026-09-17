using FinSteady_API.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinSteady_API.Models.Request
{
    public class CategoryRequestModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Category ToEntity()
        {
            return new Category
            {
                CategoryId = this.CategoryId,
                Name = this.Name,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }
    }
}
