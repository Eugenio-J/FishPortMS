using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FishPortMS.Shared.Models.FishPort;

namespace FishPortMS.Shared.Models.Sales
{
    public class ConsignacionSession
    {
        [Key]
        public Guid SessionId { get; set; }
        public DateTime StartSession { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal StartingCash { get; set; }

        public DateTime EndSession { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal EndCash { get; set; } = 0;

        public ConsignacionEmployee ConsignacionEmployee { get; set; }
        public int ConsignacionEmployeeId { get; set; }

        [JsonIgnore]
        public List<SalesOrder>? SalesOrder { get; set; }

        public Guid ConsignacionId { get; set; }
    }
}
