using FishPortMS.Shared.Models.Announcements;
using FishPortMS.Shared.Models.ConPettyCash;
using FishPortMS.Shared.Models.Expenses;
using FishPortMS.Shared.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.FishPort
{
    public class Consignacion
    {
        [Key]
        public Guid Id { get; set; }
        public string ConsignacionName { get; set; } = string.Empty;
        public string ConsignacionAddress { get; set; } = string.Empty;
        public string ConsignacionLocation { get; set; } = string.Empty;
        public DateTime? StartOfContract { get; set; } = DateTime.Now;
        public DateTime? EndOfContract { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public Guid? ActiveSessionId { get; set; }

        public ConsignacionOwner ConsignacionOwner { get; set; }
        public int ConsignacionOwnerId { get; set; }

        [JsonIgnore]
        public List<ConsignacionEmployee>? ConsignacionEmployees { get; set; }

        [JsonIgnore]
        public List<Expense>? Expenses { get; set; }

        [JsonIgnore]
        public List<ClientProduct>? ClientProduct { get; set; }

        [JsonIgnore]
        public List<PettyCash>? PettyCash { get; set; }

        [JsonIgnore]
        public List<Announcement>? Announcements { get; set; }

        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
