using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Attendance
{
    public class ConsignacionAttendance
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public AttendanceState AttendanceState { get; set; }
        public DateTime AttendanceDate { get; set; } = DateTime.Now;
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? LunchIn { get; set; }
        public DateTime? LunchOut { get; set; }
        public string TimeInImage { get; set; } = string.Empty;
        public string TimeOutImage { get; set; } = string.Empty;
        public UserProfile UserProfile { get; set; }
        public int UserDetailsId { get; set; }
        public Guid ConsignacionId { get; set; }
    }
}
