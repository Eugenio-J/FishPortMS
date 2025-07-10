using FishPortMS.Shared.DTOs.ReceiptDTO;

namespace FishPortMS.Utilities
{
    public class Tabs
    {
        public string Title { get; set; }
        public List<GetReceiptItemDTO> POItemsList { get; set; } = new List<GetReceiptItemDTO>();
    }
}
