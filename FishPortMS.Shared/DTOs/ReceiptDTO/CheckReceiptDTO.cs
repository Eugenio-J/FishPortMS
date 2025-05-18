using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ReceiptDTO
{
    public class CheckReceiptDTO
    {
        public int ProductId { get; set; }
        public int ReceiptItemId { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
