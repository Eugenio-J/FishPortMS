using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.DashboardDTO
{
    public class ProductChart
    {
        public string Label { get; set; }        
        public decimal TotalSales { get; set; }  
        public string ProductName { get; set; } 
    }
}
