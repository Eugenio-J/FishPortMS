using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.SalesDTO
{
    public class GetReceiptItemDTO
    {
        public int ReceiptId { get; set; }
        public bool IsOut { get; set; } = false;
        public string ProductName { get; set; }
        public string UOM { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }
    }
}
