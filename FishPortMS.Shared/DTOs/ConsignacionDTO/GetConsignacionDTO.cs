using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ConsignacionDTO
{
    public class GetConsignacionDTO
    {
        public Guid Id { get; set; }
        public Guid ConsignacionOwnerUserId { get; set; }
        public string? UserAvatar { get; set; }
        public string ConsignacionOwner { get; set; } = string.Empty;
        public string ConsignacionName { get; set; } = string.Empty;
        public string ConsignacionAddress { get; set; } = string.Empty;
        public string ConsignacionLocation { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime? StartOfContract { get; set; } = DateTime.Now;
        public DateTime? EndOfContract { get; set; }
        public DateTime? LastOrder { get; set; }
    }
}
