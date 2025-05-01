using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ConsignacionDTO
{
    public class CreateConsignacionDTO
    {
        public string ConsignacionName { get; set; } = string.Empty;
        public string ConsignacionAddress { get; set; } = string.Empty;
        public string ConsignacionLocation { get; set; } = string.Empty;
        public DateTime? StartOfContract { get; set; } = DateTime.Now;
        public DateTime? EndOfContract { get; set; }
    }
}
