﻿using FishPortMS.Shared.Models.FishPort;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Sales
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public string BSName { get; set; } = string.Empty;
        public string CashierName { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossSales { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetSales { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DeductedPercentage { get; set; }
        public Guid CreatedBy { get; set; } 
        public Guid? BSId { get; set; } 
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public Consignacion Consignacion { get; set; }
        public Guid ConsignacionId { get; set; }

        public List<ReceiptItem> ReceiptItems { get; set; } = new List<ReceiptItem>(); 
    }
}
