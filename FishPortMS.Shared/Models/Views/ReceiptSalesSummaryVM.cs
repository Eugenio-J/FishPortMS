using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Views
{
    [Table("VReceiptSalesSummary")]
    public class ReceiptSalesSummaryVM
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid? BSId { get; set; }
        public DateTime DateOnly { get; set; }
        public int Hour { get; set; }
        public string DayOfWeek { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossSales { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetSales { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DeductedAmount { get; set; }
    }
}
