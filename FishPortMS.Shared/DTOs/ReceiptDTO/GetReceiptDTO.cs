using FishPortMS.Shared.DTOs.VendorExpDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ReceiptDTO
{
    public class GetReceiptDTO
    {
        public int Id { get; set; }
        public Guid? BuyAndSellId { get; set; }
        public string BSName { get; set; } = string.Empty;
        public string CashierName { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossSales { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetSales { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DeductedPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DeductedAmount { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public Guid? LastModifiedBy { get; set; }
        public List<GetReceiptItemDTO> ReceiptItems { get; set; } = new List<GetReceiptItemDTO>();
        public List<GetVendorExp>? VendorExpenses { get; set; } 
    }
}
