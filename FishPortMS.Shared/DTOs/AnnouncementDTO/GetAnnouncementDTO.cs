using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Enums.Announcements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.AnnouncementDTO
{
    public class GetAnnouncementDTO
    {
        public int Id { get; set; }
        public string? AnnouncementTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = false;
        public AnnouncementRole AnnouncementRole { get; set; }
        public AnnouncementType AnnouncementType { get; set; }
        public string? Consignacion { get; set; }
    }
}
