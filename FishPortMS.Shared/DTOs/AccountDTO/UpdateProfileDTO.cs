using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.AccountDTO
{
    public class UpdateProfileDTO
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Region { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(09|\\+639)\\d{9}$", ErrorMessage = "The number should start with +639 or 09 followed by 9 numbers.")]
        public string Phone { get; set; } = string.Empty;

        public string? Avatar { get; set; }
    }
}
