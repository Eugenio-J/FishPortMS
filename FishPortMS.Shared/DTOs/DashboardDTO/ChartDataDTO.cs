using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.DashboardDTO
{
    public class ChartDataDTO
    {
        public string Label { get; set; }         // Time label (e.g., "08:00", "Monday", "March", "2025")
        public decimal GrossSales { get; set; }  // Aggregated amount
        public decimal NetSales { get; set; }  // Aggregated amount
        public decimal Porsyento { get; set; }  // Aggregated amount
        public decimal NetSalesVendorView { get; set; }  // Aggregated amount
    }
}
