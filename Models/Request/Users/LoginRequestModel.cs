using System.ComponentModel.DataAnnotations;

namespace FinSteady_API.Models.Request.Users
{
    public class LoginRequestModel
    {
        [StringLength(10)]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
