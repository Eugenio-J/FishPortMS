using FishPortMS.Shared.Models.Expenses;
using FishPortMS.Shared.Models.Receipts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.VendorExpDTO
{
	public class GetVendorExp
	{		
		public int Id { get; set; }
		public int VendorExpenseCategoryId { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }
		public int ReceiptId { get; set; }
	}
}
