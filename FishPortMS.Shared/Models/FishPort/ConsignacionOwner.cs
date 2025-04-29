using FishPortMS.Shared.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.FishPort
{
    public class ConsignacionOwner
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore] 
        public List<Consignacion> Consignacion { get; set; }

        public UserProfile UserProfile { get; set; }

    }
}
