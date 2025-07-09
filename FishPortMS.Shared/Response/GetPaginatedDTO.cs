using FishPortMS.Shared.Enums;

namespace FishPortMS.Shared.Response
{
    public class GetPaginatedDTO
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public string? Status { get; set; }
        public string? SearchValue { get; set; }
        public Roles? Roles { get; set; }
        public string consignacion { get; set; } = string.Empty;
        public DateTime? AttendanceDateStart { get; set; }
        public DateTime? AttendanceDateEnd { get; set; }
        public string? EmployeeName { get; set; } = string.Empty;
    }
}
