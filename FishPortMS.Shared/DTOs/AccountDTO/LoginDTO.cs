using System.ComponentModel.DataAnnotations;

namespace FishPortMS.Shared.DTOs.AccountDTO
{
    public class LoginDTO {

        [Required, EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
