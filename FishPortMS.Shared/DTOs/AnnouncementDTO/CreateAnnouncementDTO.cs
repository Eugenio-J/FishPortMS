using FishPortMS.Shared.Enums.Announcements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.AnnouncementDTO
{
    public class CreateAnnouncementDTO
    {
        public int Id { get; set; }
        public string? AnnouncementTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AnnouncementRole AnnouncementRole { get; set; }
        public AnnouncementType AnnouncementType { get; set; }
        public Guid? BranchId { get; set; }
    }
}
