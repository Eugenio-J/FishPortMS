using FishPortMS.Server.Data;
using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Models.Sales;
using FishPortMS.Shared.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static MudBlazor.Icons.Custom;

namespace FishPortMS.Server.Services.ReceiptService
{
    public class ReceiptService : IReceiptService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReceiptService(DataContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _httpContextAccessor = contextAccessor;
        }

        private DateTime PHTime()
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");
            DateTime philippineTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            return philippineTime;
        }

        private string? GetUserId() => _httpContextAccessor.HttpContext?.User
         .FindFirstValue(ClaimTypes.NameIdentifier);

        private string? GetUserRole() => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.Role);

        private CheckReceiptDTO ReturnError()
        {
            return new CheckReceiptDTO
            { 
                IsSuccess = false,
                ProductId = 0,
                ReceiptItemId = 0,
            };
        }

        public async Task<CheckReceiptDTO> CreateReceipt(CreateReceiptDTO request, string consignacionId)
        {
            CheckReceiptDTO result = new CheckReceiptDTO(); 

            string userId = GetUserId() ?? string.Empty;

            string userRole = GetUserRole() ?? string.Empty;

            Guid.TryParse(consignacionId, out Guid consignacion);

            Guid.TryParse(userId, out Guid UserId);


            var cashierName = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId.ToString() == userId);

            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return ReturnError();

            if (string.IsNullOrEmpty(userId)) return ReturnError();

            Receipt? newReceipt = new Receipt 
            {
                BSId = request.BSId,
                CashierName = request.CashierName,
                BSName = request.BSname,
                ConsignacionId = consignacion,
                DateCreated = PHTime(),
                CreatedBy = UserId,
                DeductedPercentage = request.DeductedPercentage,
                ReceiptItems = new List<ReceiptItem>()
            };

            _context.Receipts.Add(newReceipt);  

            foreach (GetReceiptItemDTO? item in request.GetReceiptItemDTO) 
            {
                ClientProduct? clientProduct = await _context.ClientProducts
                    .Include(x => x.MasterProduct)
                    .Where(x => x.Id == item.ClientProductId && x.ConsignacionId == consignacion).FirstOrDefaultAsync();

                if (clientProduct == null) 
                {
                    return ReturnError();
                }

                ReceiptItem new_item = new ReceiptItem
                {
                    ClientProductId = item.ClientProductId,
                    CurrentPrice = item.CurrentPrice,
                    IsOut = item.IsOut,
                    UOM = item.UOM,
                    Weight = item.Weight,
                };

                newReceipt.ReceiptItems.Add(new_item);  
                _context.ReceiptItems.Add(new_item);
            }

            newReceipt.GrossSales = newReceipt.ReceiptItems.Sum(x => x.CurrentPrice);

            decimal amountDeducted = newReceipt.GrossSales-(newReceipt.GrossSales * (request.DeductedPercentage/100));

            newReceipt.NetSales = newReceipt.ReceiptItems.Sum(x => x.CurrentPrice) - amountDeducted;

            await _context.SaveChangesAsync();

            result.ReceiptItemId = newReceipt.Id;
            result.IsSuccess = true;

            return result;
        }

        public Task<PaginatedTableResponse<GetReceiptDTO>> GetReceiptPaginated(GetPaginatedDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<GetReceiptDTO> GetSingleReceipt(int receiptId)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedTableResponse<GetReceiptDTO>> SearchReceipt(GetPaginatedDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
