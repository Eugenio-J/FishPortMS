using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ConsignacionDTO
{
    public class GetRenewalDetailsDTO
    {
        public Guid UserId { get; set; }
        public string Consignacion { get; set; } = string.Empty;
        public DateTime? EndOfContract { get; set; } = DateTime.Now;
    }
}
