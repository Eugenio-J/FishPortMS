using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Services.ReceiptService
{
    public interface IReceiptService
    {
        Task<int> CreateReceipt(CreateReceiptDTO request);
        Task<int> UpdateReceipt(int Id, CreateReceiptDTO request);
        Task<PaginatedTableResponse<GetReceiptDTO>> GetReceiptPaginated(GetPaginatedDTO request);
        Task<PaginatedTableResponse<GetReceiptDTO>> SearchReceipt(GetPaginatedDTO request);
        Task<GetReceiptDTO?> GetSingleReceipt(int receiptId);
    }
}
