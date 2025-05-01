using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.NotificationDTO
{
    public class GetNotificationDTO
    {
        public string PONotif { get; set; } = "0";
        public string PaymentNotif { get; set; } = "0"; 
        public string DeliveryNotif { get; set; } = "0";
        public string ExpenseNotif { get; set; } = "0";
        public string ShippingNotif { get; set; } = "0";
    }
}
