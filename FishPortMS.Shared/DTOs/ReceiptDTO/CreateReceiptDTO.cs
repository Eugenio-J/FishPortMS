using FishPortMS.Shared.DTOs.VendorExpDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ReceiptDTO
{
    public class CreateReceiptDTO
    {
        public string Notes { get; set; } = string.Empty;
        public Guid? BsId { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public string CashierName { get; set; } = string.Empty;
        public string BSname { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DeductedPercentage { get; set; }
        public List<GetReceiptItemDTO> GetReceiptItemDTO { get; set; } = new List<GetReceiptItemDTO>();
        public List<GetVendorExp>? VendorExpenses { get; set; }
    }
}
