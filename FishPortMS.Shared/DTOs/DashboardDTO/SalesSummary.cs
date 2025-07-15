using FishPortMS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.DashboardDTO
{
	public class SalesSummary
	{
		public decimal GrossSales { get; set; } = 0;
		public decimal NetSales { get; set; } = 0;
		public decimal TotalExpense { get; set; } = 0;
		public decimal TotalPorsyento { get; set; } = 0;
	}
}
