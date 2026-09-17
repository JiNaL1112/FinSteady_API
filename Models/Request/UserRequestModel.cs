using FinSteady_API.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace FinSteady_API.Models.Request
{
    public class UserRequestModel
    {
        [Required]
        [StringLength(10)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(10)]
        public string? LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string? PasswordHash { get; set; }

        [Required]
        public string? ConfirmPassword { get; set; }


        public User ToEntity()
        {
            return new User
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                PasswordHash = this.PasswordHash,
                PhoneNumber = this.PhoneNumber

            };
        }
    }
}
