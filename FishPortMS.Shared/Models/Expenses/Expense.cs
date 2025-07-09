using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishPortMS.Shared.Enums.Status;

namespace FishPortMS.Shared.Models.Expenses
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public DateTime? ExpenseDate { get; set; } = DateTime.UtcNow;
        public ExpenseCategory ExpenseCategory { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string Description { get; set; } = string.Empty;

        [Required]
        public string PaidCompany { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; } = 0;

        [Required]
        public string ReceiptNo { get; set; } = string.Empty;
        public string ReceiptImage { get; set; } = string.Empty;

        public Guid CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
 