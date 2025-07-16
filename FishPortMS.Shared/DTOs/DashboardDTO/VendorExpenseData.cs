using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.DashboardDTO
{
    public class VendorExpenseData
    {
        public decimal Amount { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string? ExpenseCategoryName { get; set; }
    }
}
