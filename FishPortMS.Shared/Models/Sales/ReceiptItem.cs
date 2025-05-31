using FishPortMS.Shared.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Sales
{
    public class ReceiptItem
    {
        [Key]
        public int Id { get; set; }
        public Receipt Receipt { get; set; }
        public int ReceiptId { get; set; }
        public bool IsOut { get; set; } = false;
        MasterProduct MasterProduct { get; set; }
        public int MasterProductId { get; set; }
        public string UOM { get; set; } = "KG";

        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
    }
}
