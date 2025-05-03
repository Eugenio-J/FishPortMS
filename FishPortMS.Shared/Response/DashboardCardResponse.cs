using FishPortMS.Shared.Enums;

namespace FishPortMS.Shared.Response
{
    public class DashboardCardResponse
    {
        public decimal Data { get; set; }
        public CardStatus Status { get; set; }
        public decimal VaryTotal { get; set; }
    }
}
