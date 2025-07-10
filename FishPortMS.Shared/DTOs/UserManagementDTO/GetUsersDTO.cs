using FishPortMS.Shared.Enums;

namespace FishPortMS.Shared.DTOs.UserManagementDTO
{
    public class GetUsersDTO
    {
        public Guid? UserId { get; set; }
        public string AttendancePin { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool isActive { get; set; }
        public Roles Roles { get; set; }
    }
}
