using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Products
{
    public class ClientInventory
    {
        [Key]
        public int Id { get; set; }
        public int Qty { get; set; } = 0;
        public int MaxQty { get; set; } = 0;

        [JsonIgnore]
        public ClientProduct ClientProduct { get; set; }
        public int ClientProductId { get; set; }
    }
}
