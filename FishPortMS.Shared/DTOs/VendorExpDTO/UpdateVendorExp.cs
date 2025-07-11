using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.VendorExpDTO
{
	public class UpdateVendorExp
	{
		public int ReceiptId { get; set; }
		public List<GetVendorExp> VendorExpenses { get; set; } = new List<GetVendorExp>();
	}
}
