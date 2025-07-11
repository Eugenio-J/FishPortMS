using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.VendorExpDTO
{
	public class AddVendorExp
	{
		public int ReceiptId { get; set; }
		public List<CreateVendorExp> VendorExpenses { get; set; } = new List<CreateVendorExp>();
	}
}
