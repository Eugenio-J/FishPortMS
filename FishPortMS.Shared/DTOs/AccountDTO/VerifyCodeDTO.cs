using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.AccountDTO
{
    public class VerifyCodeDTO
    {
        [Required, EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Verification Code field is required")]
        public string Code { get; set; } = string.Empty;
    }
}
