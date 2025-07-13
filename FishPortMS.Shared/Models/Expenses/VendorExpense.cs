using FishPortMS.Shared.Models.Receipts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Expenses
{
	public class VendorExpense
	{
		[Key] 
		public int Id { get; set; }
		public VendorExpenseCategory VendorExpenseCategory { get; set; }
		public int VendorExpenseCategoryId { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }
		public Receipt Receipt { get; set; }
		public int ReceiptId { get; set; }
	}
}
