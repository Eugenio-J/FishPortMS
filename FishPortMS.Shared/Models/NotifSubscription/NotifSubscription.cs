using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.NotifSubscription
{
    public class NotifSubscription
    {
        [Key]
        public int? SubsriptionId { get; set; }
        public Guid? UserId { get; set; }
        public string? Url { get; set; }
        public string? P256dh { get; set; }
        public string? Auth { get; set; }
    }
}
