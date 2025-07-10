using FishPortMS.Shared.DTOs.UserManagementDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Models.Account;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Services.UserManagementService
{
    public interface IUserManagementService
    {
        Task<PaginatedTableResponse<GetUsersDTO>?> GetAllUsersPaginated(GetPaginatedDTO request);
        Task<PaginatedTableResponse<GetUsersDTO>?> SearchUser(GetPaginatedDTO request);
        Task<int> EnableAccount(Guid userId);
        Task<int> DisableAccount(Guid userId);
        Task<List<GetUsersDTO>> GetBSList();

    }
}
