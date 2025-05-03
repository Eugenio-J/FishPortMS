using FishPortMS.Shared.Enums.Announcements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.AnnouncementDTO
{
    public class GetRightSidebarAnnouncementDTO
    {
        public int Id { get; set; }
        public string? AnnouncementTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AnnouncementRole AnnouncementRole { get; set; }
        public AnnouncementType AnnouncementType { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime ParentDate { get; set; } // Added parent date
        public string TimeElapsed { get; set; } // Added time elapsed
        public string? Consignacion { get; set; } 
    }
}
