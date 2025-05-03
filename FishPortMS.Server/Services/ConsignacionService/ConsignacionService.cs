using Microsoft.EntityFrameworkCore;
using FishPortMS.Server.Data;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Response;
using System.Security.Claims;
using FishPortMS.Shared.DTOs.ConsignacionDTO;
using FishPortMS.Shared.Models.FishPort;

namespace FishPortMS.Server.Services.ConsignacionService
{
    public class ConsignacionService : IConsignacionService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConsignacionService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private DateTime PHTime(DateTime date)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");
            DateTime utcDate = new DateTime(date.Year, date.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime philippineTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, tz);
            return philippineTime;
        }

        private string? GetUserId() => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        private string? GetUserRole() => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.Role);

        public async Task<int> AddMoreFranchiseConsignacion(Guid userId, CreateConsignacionDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            int franchiseeId = await _context.ConsignacionOwners
                .Include(f => f.UserProfile)
                .Where(f => f.UserProfile.UserId == userId)
                .Select(f => f.Id)
                .FirstOrDefaultAsync();

            if (franchiseeId == 0) return 0;

            if (await _context.Consignacions.AnyAsync(branch => branch.ConsignacionName == request.ConsignacionName)) return 0;

            Consignacion branch = new Consignacion
            {
                ConsignacionName = request.ConsignacionName,
                ConsignacionAddress = request.ConsignacionAddress,
                ConsignacionLocation = request.ConsignacionLocation,
                StartOfContract = request.StartOfContract,
                EndOfContract = request.StartOfContract!.Value.AddYears(1),
                ConsignacionOwnerId = franchiseeId,
                DateCreated = DateTime.Now.Date
            };

            _context.Consignacions.Add(branch);

            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }

        private async Task<GetConsignacionDTO> ConvertDTO(Consignacion branch)
        {
            return new GetConsignacionDTO
            {
                Id = branch.Id,
                ConsignacionOwnerUserId = branch.ConsignacionOwner.UserProfile.UserId,
                UserAvatar = branch.ConsignacionOwner.UserProfile?.Avatar,
                ConsignacionOwner = branch.ConsignacionOwner.UserProfile!.FirstName + " " + branch.ConsignacionOwner.UserProfile!.LastName,
                ConsignacionName = branch.ConsignacionName,
                ConsignacionAddress = branch.ConsignacionAddress,
                ConsignacionLocation = branch.ConsignacionLocation,
                IsActive = branch.IsActive,
                Latitude = branch.Latitude,
                Longitude = branch.Longitude,   
                StartOfContract = branch.StartOfContract,
                EndOfContract = branch.EndOfContract,
            };
        }


        public async Task<PaginatedTableResponse<GetConsignacionDTO>?> GetAllConsignacionesPaginated(GetPaginatedDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            PaginatedTableResponse<GetConsignacionDTO> response = new PaginatedTableResponse<GetConsignacionDTO>();
            List<GetConsignacionDTO> db_results = new List<GetConsignacionDTO>();

            List<Consignacion> branches = await _context.Consignacions
                 .Include(branch => branch.ConsignacionOwner)
                 .ThenInclude(branch => branch.UserProfile)
                 .ThenInclude(branch => branch.User)
                 .OrderByDescending(branch => branch.Id)
                 .Skip(request.Skip)
                 .Take(request.Take)
                 .ToListAsync();

            foreach (Consignacion? branch in branches)
            {
                db_results.Add(await ConvertDTO(branch));
                response.ResponseData = db_results;
            }

            response.ResponseData = db_results;
            response.Count = await _context.Consignacions.CountAsync();

            return response;
        }

        public async Task<PaginatedTableResponse<GetConsignacionDTO>?> SearchConsignacion(GetPaginatedDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            PaginatedTableResponse<GetConsignacionDTO> response = new PaginatedTableResponse<GetConsignacionDTO>();
            List<GetConsignacionDTO> db_results = new List<GetConsignacionDTO>();

            List<Consignacion> branches = await _context.Consignacions
                 .Include(branch => branch.ConsignacionOwner)
                 .ThenInclude(branch => branch.UserProfile)
                 .ThenInclude(branch => branch.User)
                 .OrderByDescending(branch => branch.Id)
                 .Where(branch => branch.Id.ToString().Contains(request.SearchValue)
                 || branch.ConsignacionName.Contains(request.SearchValue)
                 || branch.ConsignacionLocation.Contains(request.SearchValue)
                 || branch.ConsignacionAddress.Contains(request.SearchValue))
                 .ToListAsync();

            foreach (Consignacion? branch in branches)
            {
                db_results.Add(await ConvertDTO(branch));
                response.ResponseData = db_results;
            }

            response.ResponseData = db_results;
            response.Count = await _context.Consignacions.CountAsync();

            return response;
        }

        public async Task<List<GetConsignacionPin>?> GetAllConsignacionPin()
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            List<GetConsignacionPin> getConsignacionPins = new List<GetConsignacionPin>();

            List<Consignacion> branches = await _context.Consignacions
                .Where(b => b.Longitude != null && b.Latitude != null)
               .ToListAsync();

            foreach (var branch in branches)
            {
                var pin = new GetConsignacionPin
                {
                    ConsignacionName = branch.ConsignacionName,
                    Latitude = branch.Latitude,
                    longitude = branch.Longitude,
                    ActiveSessionId = branch.ActiveSessionId
                };
                getConsignacionPins.Add(pin);
            }

            return getConsignacionPins;
        }

        public async Task<int> EnableConsignacion(Guid id)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            Consignacion? branch = await _context.Consignacions
                .FirstOrDefaultAsync(b => b.Id == id);

            if (branch == null) return 0;

            branch.IsActive = true;

            int status = await _context.SaveChangesAsync();
            return status == 0 ? 0 : 1;
        }

        public async Task<int> DisableConsignacion(Guid id)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            Consignacion? branch = await _context.Consignacions
                .FirstOrDefaultAsync(b => b.Id == id);

            if (branch == null) return 0;

            branch.IsActive = false;

            int status = await _context.SaveChangesAsync();
            return status == 0 ? 0 : 1;
        }

        public async Task<int> EditConsignacionGeolocation(Guid id, UpdateConsignacionGeoDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            Consignacion? branch = await _context.Consignacions
                .FirstOrDefaultAsync(b => b.Id == id);

            if (branch == null) return 0;

            branch.Latitude = request.Latitude;
            branch.Longitude = request.longitude;

            int status = await _context.SaveChangesAsync();
            return status == 0 ? 0 : 1;
        }

        public async Task<List<GetConsignacionDTO>?> GetRenewalDetails(DateTime month)
        {
            DateTime startOfMonth = PHTime(new DateTime(month.Year, month.Month, 1));
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            List<GetConsignacionDTO> getRenewalDetails = await _context.Consignacions
                .Include(branch => branch.ConsignacionOwner)
                .ThenInclude(branch => branch.UserProfile)
                .Where(branch => branch.EndOfContract >= startOfMonth && branch.EndOfContract <= endOfMonth)
                .Select(branch => new GetConsignacionDTO
                {
                    Id = branch.Id,
                    ConsignacionOwnerUserId = branch.ConsignacionOwner.UserProfile.UserId,
                    UserAvatar = branch.ConsignacionOwner.UserProfile.Avatar,
                    ConsignacionOwner = branch.ConsignacionOwner.UserProfile!.FirstName + " " + branch.ConsignacionOwner.UserProfile!.LastName,
                    ConsignacionName = branch.ConsignacionName,
                    ConsignacionAddress = branch.ConsignacionAddress,
                    ConsignacionLocation = branch.ConsignacionLocation,
                    IsActive = branch.IsActive,
                    Latitude = branch.Latitude,
                    Longitude = branch.Longitude,
                    StartOfContract = branch.StartOfContract,
                    EndOfContract = branch.EndOfContract
                })
                .ToListAsync();

            return getRenewalDetails;
        }

        public async Task<List<GetConsignacionDTO>?> GetAllConsignacionDetails()
        {
            return await _context.Consignacions
            .Where(f => f.IsActive)
            .Select(f => new GetConsignacionDTO
            {
                Id = f.Id,
                ConsignacionName = f.ConsignacionName
            })
            .ToListAsync();
        }

    }
}
