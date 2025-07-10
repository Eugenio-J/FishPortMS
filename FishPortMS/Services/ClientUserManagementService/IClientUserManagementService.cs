using FishPortMS.Shared.DTOs.UserManagementDTO;
using FishPortMS.Shared.Response;

namespace FishPortMS.Services.ClientUserManagementService
{
    public interface IClientUserManagementService
    {
        Task<PaginatedTableResponse<GetUsersDTO>> GetAllUsersPaginated(GetPaginatedDTO payload);
        Task<PaginatedTableResponse<GetUsersDTO>> SearchUser(GetPaginatedDTO payload);
        Task<int> EnableAccount(Guid userId);
        Task<int> DisableAccount(Guid userId);
        Task<List<GetUsersDTO>> GetBSList();

    }
}
