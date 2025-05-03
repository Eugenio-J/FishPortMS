using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ConsignacionDTO
{
    public class GetConsignacionPin
    {
        public string ConsignacionName { get; set; } = string.Empty;
        public string? Latitude { get; set; }
        public string? longitude { get; set; }
        public Guid? ActiveSessionId { get; set; }
    }
}
