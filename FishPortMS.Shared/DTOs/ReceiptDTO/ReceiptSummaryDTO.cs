using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ReceiptDTO
{
    public class ReceiptSummaryDTO
    {
        public decimal DeductedPercentage { get; set; } = 0;
        public decimal DeductedAmount { get; set; } = 0;
        public decimal GrossSales { get; set; } = 0;
        public decimal NetSales { get; set; } = 0;
    }
}
