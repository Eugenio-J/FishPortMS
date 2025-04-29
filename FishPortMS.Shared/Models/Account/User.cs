using FishPortMS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Account
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] Password { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public Roles Role { get; set; }
        public string VerificationToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime RefreshTokenCreatedAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
        public UserProfile UserProfile { get; set; }

    }
}
