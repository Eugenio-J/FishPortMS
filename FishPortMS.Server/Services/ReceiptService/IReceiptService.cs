using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Services.ReceiptService
{
    public interface IReceiptService
    {
        Task<CheckReceiptDTO> CreateReceipt(CreateReceiptDTO request, string consignacionId);
        Task<PaginatedTableResponse<GetReceiptDTO>> GetReceiptPaginated(GetPaginatedDTO request);
        Task<PaginatedTableResponse<GetReceiptDTO>> SearchReceipt(GetPaginatedDTO request);
        Task<GetReceiptDTO> GetSingleReceipt(int receiptId);
    }
}
