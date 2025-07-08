using FishPortMS.Shared.Models.ConPettyCash;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Account
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string AttendancePin { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        public List<PettyCash>? PettyCash { get; set; }
    }
}
