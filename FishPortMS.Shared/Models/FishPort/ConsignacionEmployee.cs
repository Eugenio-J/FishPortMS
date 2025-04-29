using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FishPortMS.Shared.Models.Account;
using FishPortMS.Shared.Models.Sales;

namespace FishPortMS.Shared.Models.FishPort
{
    public class ConsignacionEmployee
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("ConsignacionId")]
        public Consignacion Consignacion { get; set; }
        public Guid ConsignacionId { get; set; }
        public Guid? CurrSessionId { get; set; }

        [JsonIgnore]
        public List<ConsignacionSession>? ConsignacionSessions { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
