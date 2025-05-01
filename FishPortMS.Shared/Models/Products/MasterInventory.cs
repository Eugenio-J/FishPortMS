using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Products
{
    public class MasterInventory
    {
        [Key]
        public int Id { get; set; }
        public int Qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SRP { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PreviousPrice { get; set; } = 0;


        [JsonIgnore]
        public MasterProduct MasterProduct { get; set; }
        public int MasterProductId { get; set; }
    }
}
