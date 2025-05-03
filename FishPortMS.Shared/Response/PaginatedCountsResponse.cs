using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Response
{
    public class PaginatedCountsResponse
    {
        public int AllCount { get; set; }
        public int PendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }

        //DELIVERY
        public int PrepShipmentCount { get; set; }
        public int InTransitCount { get; set; }
        public int ArrivedCount { get; set; }
    }
}
