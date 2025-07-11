using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using FishPortMS.Server.Data;
using FishPortMS.Shared.DTOs.UserManagementDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Response;
using System.Security.Claims;
using FishPortMS.Shared.Models.Account;

namespace FishPortMS.Server.Services.UserManagementService
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public UserManagementService(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }   

        private string? GetUserId() => _httpContextAccessor.HttpContext?.User
           .FindFirstValue(ClaimTypes.NameIdentifier);

        private string? GetUserRole() => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.Role);

        private GetUsersDTO ConvertDTO(UserProfile userDetails)
        {
            return new GetUsersDTO
            {
                UserId = userDetails.UserId,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Address = userDetails.Address,
                AttendancePin = userDetails.AttendancePin,
                Phone = userDetails.PhoneNumber,
                Email = userDetails.User.Email,
                Roles = userDetails.User.Role,
                isActive  = userDetails.User.IsActive
            };
        }

        public async Task<PaginatedTableResponse<GetUsersDTO>?> GetAllUsersPaginated(GetPaginatedDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            PaginatedTableResponse<GetUsersDTO> response = new PaginatedTableResponse<GetUsersDTO>();
            List<GetUsersDTO> db_results = new List<GetUsersDTO>();
            List<UserProfile> userDetails = new List<UserProfile>();

            if(request.Roles != null) 
            {
                userDetails = await _context.UserProfiles
                 .Include(user => user.User)
                 .Where(user => user.User.Role == request.Roles)
                 .OrderByDescending(user => user.Id)
                 .Skip(request.Skip)
                 .Take(request.Take)
                 .ToListAsync();
            }
            else
            {
                userDetails = await _context.UserProfiles
                 .Include(user => user.User)
                 .OrderByDescending(user => user.Id)
                 .Skip(request.Skip)
                 .Take(request.Take)
                 .ToListAsync();
            }
            
            foreach (UserProfile? u in userDetails)
            {
                db_results.Add(ConvertDTO(u));
                response.ResponseData = db_results;
            }

            response.ResponseData = db_results;
            response.Count = userDetails.Count();

            return response;
        }

        public async Task<PaginatedTableResponse<GetUsersDTO>?> SearchUser(GetPaginatedDTO request)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return null;

            PaginatedTableResponse<GetUsersDTO> response = new PaginatedTableResponse<GetUsersDTO>();
            List<GetUsersDTO> db_results = new List<GetUsersDTO>();
            List<UserProfile> userDetails = new List<UserProfile>();

            if (request.Roles != null)
            {
                userDetails = await _context.UserProfiles
                 .Include(user => user.User)
                 .OrderByDescending(user => user.Id)
                  .Where(user => user.User.Role == request.Roles
                     && (user.Id.ToString().Contains(request.SearchValue)
                     || user.FirstName.Contains(request.SearchValue)
                     || user.LastName.Contains(request.SearchValue)
                     || user.Address.Contains(request.SearchValue)
                     || user.AttendancePin.Contains(request.SearchValue)))
                .ToListAsync();
            }
            else
            {
                userDetails = await _context.UserProfiles
                 .Include(user => user.User)
                 .OrderByDescending(user => user.Id)
                  .Where(user => user.Id.ToString().Contains(request.SearchValue)
                     || user.FirstName.Contains(request.SearchValue)
                     || user.LastName.Contains(request.SearchValue)
                     || user.Address.Contains(request.SearchValue)
                     || user.AttendancePin.Contains(request.SearchValue))
                .ToListAsync();
            }

            foreach (UserProfile? u in userDetails)
            {
                db_results.Add(ConvertDTO(u));
                response.ResponseData = db_results;
            }

            response.ResponseData = db_results;
            response.Count = userDetails.Count();

            return response;
        }

        public async Task<int> DisableAccount(Guid userId)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            User? users = await _context.Users
               .Where(u => u.Id == userId)
               .FirstOrDefaultAsync();

            if (users == null) return 0;

            users.IsActive = false;
            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }

        public async Task<int> EnableAccount(Guid userId)
        {
            if (!Enum.TryParse(GetUserRole(), out Roles acc_role)) return -1;

            User? users = await _context.Users
               .Where(u => u.Id == userId)
               .FirstOrDefaultAsync();

            if (users == null) return 0;

            users.IsActive = true;
            return await _context.SaveChangesAsync() == 0 ? 0 : 1;
        }

        public async Task<List<GetUsersDTO>> GetBSList()
        {
            List<GetUsersDTO> users = new List<GetUsersDTO>();

            var bsUserList = await _context.UserProfiles
                .Include(x => x.User)
                .Where(x => x.User.Role == Roles.BUY_AND_SELL)
                .ToListAsync();

            users = bsUserList.Select(ConvertDTO).ToList();

            return users;
        }
    }
}
