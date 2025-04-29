using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishPortMS.Shared.Enums;

namespace FishPortMS.Shared.Models.Sales
{
    public class SalesOrder
    {
        [Key]
        public int Id { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentAmount { get; set; } = 0;

        public PaymentType PaymentType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        public string Ref_No { get; set; } = string.Empty;


        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; } = 0;

        public List<OrderItem>? OrderItems { get; set; }

        public ConsignacionSession Session { get; set; }
        public Guid SessionId { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
