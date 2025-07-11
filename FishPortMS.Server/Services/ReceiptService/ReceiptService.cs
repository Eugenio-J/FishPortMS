using FishPortMS.Server.Data;
using FishPortMS.Shared.DTOs.ReceiptDTO;
using FishPortMS.Shared.DTOs.VendorExpDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Models.Products;
using FishPortMS.Shared.Models.Receipts;
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

        public async Task<int> CreateReceipt(CreateReceiptDTO request)
        {
            string userId = GetUserId() ?? string.Empty;

            Guid.TryParse(userId, out Guid UserId);

            Receipt? newReceipt = new Receipt
            {
                CashierName = request.CashierName,
                BSName = request.BSname,
                BSId = request.BsId,
                Notes = request.Notes,
                DateCreated = PHTime(),
                CreatedBy = UserId,
                DeductedPercentage = request.DeductedPercentage,
                ReceiptItems = new List<ReceiptItem>()
            };

            _context.Receipts.Add(newReceipt);

            foreach (GetReceiptItemDTO? item in request.GetReceiptItemDTO)
            {
                MasterProduct? masterProduct = await _context.MasterProducts
                    .Where(x => x.Id == item.MasterProductId).FirstOrDefaultAsync();

                if (masterProduct == null)
                {
                    return 0;
                }

                ReceiptItem new_item = new ReceiptItem
                {
                    MasterProductId = item.MasterProductId,
                    CurrentPrice = item.CurrentPrice,
                    IsOut = item.IsOut,
                    Weight = item.Weight,
                    Subtotal = item.Weight * item.CurrentPrice
                };

                newReceipt.ReceiptItems.Add(new_item);
                _context.ReceiptItems.Add(new_item);
            }

            newReceipt.GrossSales = newReceipt.ReceiptItems.Sum(x => x.Subtotal);

            decimal amountDeducted = (newReceipt.GrossSales * (request.DeductedPercentage / 100));

            newReceipt.NetSales = newReceipt.GrossSales - amountDeducted;

            int count = await _context.SaveChangesAsync();

            return newReceipt.Id;
        }

        public async Task<PaginatedTableResponse<GetReceiptDTO>> GetReceiptPaginated(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetReceiptDTO> response = new PaginatedTableResponse<GetReceiptDTO>();

            List<Receipt> receipt = new List<Receipt>();

            string userId = GetUserId() ?? string.Empty;
            string userRole = GetUserRole() ?? string.Empty;

            IQueryable<Receipt>? query = _context.Receipts
                    .Include(consignacion => consignacion.ReceiptItems)
                    .ThenInclude(consignacion => consignacion.MasterProduct);

            if (userRole == Roles.BUY_AND_SELL.ToString()) 
            {
                query = query.Where(x => x.BSId.ToString() == userId && x.BSId != null); 
            }

            receipt = await query.OrderByDescending(p => p.Id)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

            response.ResponseData = receipt.Select(ConvertReceipt).ToList();
            response.Count = await query.CountAsync();

            return response;
        }

        public async Task<GetReceiptDTO?> GetSingleReceipt(int receiptId)
        {
            var singleReceipt = await _context.Receipts
                .Include(x => x.ReceiptItems)
                  .ThenInclude(x => x.MasterProduct)
                .Include(x => x.VendorExpenses)
                .SingleOrDefaultAsync(x => x.Id == receiptId);

            if (singleReceipt == null) return null;

            return ConvertReceipt(singleReceipt);
        }

        public async Task<PaginatedTableResponse<GetReceiptDTO>> SearchReceipt(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetReceiptDTO> response = new PaginatedTableResponse<GetReceiptDTO>();

            List<Receipt> receipt = new List<Receipt>();

            IQueryable<Receipt>? query = _context.Receipts.Where(b => b.BSName.Contains(request.SearchValue)
                || (b.CashierName.Contains(request.SearchValue)));


            receipt = await _context.Receipts
                    .OrderByDescending(p => p.Id)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

            response.ResponseData = receipt.Select(ConvertReceipt).ToList();
            response.Count = await query.CountAsync();

            return response;
        }

        private GetReceiptDTO ConvertReceipt(Receipt receipt)
        {
            List<GetReceiptItemDTO> receiptItem = receipt.ReceiptItems.Select(x => new GetReceiptItemDTO
            {
                Id = x.Id,
                MasterProductId = x.MasterProductId,
                CurrentPrice = x.CurrentPrice,
                IsOut = x.IsOut,
                ProductName = x.MasterProduct.Name,
                ReceiptId = x.ReceiptId,
                Weight = x.Weight,
                Subtotal = x.Subtotal,
            }).ToList();

            List<GetVendorExp>? vendorExpenses = receipt.VendorExpenses?.Select(x => new GetVendorExp
            {
                Id = x.Id,
                Amount = x.Amount,
                ExpenseCategoryId = x.ExpenseCategoryId,
                ReceiptId = x.ReceiptId 
            }).ToList();


            return new GetReceiptDTO
            {
                Id = receipt.Id,
                BSName = receipt.BSName,
                BuyAndSellId = receipt.BSId,
                CashierName = receipt.CashierName,
                Notes = receipt.Notes,
                DateCreated = receipt.DateCreated,
                DeductedPercentage = receipt.DeductedPercentage,
                GrossSales = receipt.GrossSales,
                NetSales = receipt.NetSales,
                ReceiptItems = receiptItem,
                VendorExpenses = vendorExpenses
            };
        }

        public async Task<int> UpdateReceipt(int Id, CreateReceiptDTO request)
        {
            Receipt? receipt = await _context.Receipts
                 .Include(x => x.ReceiptItems)
                 .ThenInclude(x => x.MasterProduct)
                 .Where(product => product.Id == Id)
                 .FirstOrDefaultAsync();

            if (receipt == null) return 0;

            decimal grossSales = 0;
            decimal netSales = 0;

            receipt.Notes = request.Notes;
            receipt.BSName = request.BSname;
            receipt.BSId = request.BsId;
            receipt.CashierName = request.CashierName;
            receipt.DeductedPercentage = request.DeductedPercentage;

            foreach (var item in request.GetReceiptItemDTO)
            {
                var receiptItem = await _context.ReceiptItems.FirstOrDefaultAsync(x => x.Id == item.Id);

                if (receiptItem == null)
                {
                    return 0;
                }

                receiptItem.CurrentPrice = item.CurrentPrice;
                receiptItem.Weight = item.Weight;
                receiptItem.IsOut = item.IsOut;
                receiptItem.Subtotal = item.Weight * item.CurrentPrice;


                grossSales += item.CurrentPrice * item.Weight;

            }

            receipt.GrossSales = grossSales;

            decimal amountDeducted = (grossSales * (request.DeductedPercentage / 100));

            receipt.NetSales = grossSales - amountDeducted;

            await _context.SaveChangesAsync();

            return receipt.Id;
        }
    }
}
