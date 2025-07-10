using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.Response;

namespace FishPortMS.Services.ClientReceiptService
{
    public interface IClientReceiptService
    {
        Task<int> CreateReceipt(CreateReceiptDTO payload);
        Task<int> UpdateReceipt(int Id, CreateReceiptDTO payload);
        Task<PaginatedTableResponse<GetReceiptDTO>> GetReceiptPaginated(GetPaginatedDTO payload);
        Task<PaginatedTableResponse<GetReceiptDTO>> SearchReceipt(GetPaginatedDTO payload);
        Task<GetReceiptDTO> GetSingleReceipt(int receiptId);
    }
}
