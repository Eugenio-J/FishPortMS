using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.VendorExpDTO
{
	public class CreateVendorExp
	{
		public int VendorExpenseCategoryId { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }
	}
}
