using FishPortMS.Shared.DTOs.ConsignacionDTO;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Services.ConsignacionService
{
    public interface IConsignacionService
    {
        Task<PaginatedTableResponse<GetConsignacionDTO>?> GetAllConsignacionesPaginated(GetPaginatedDTO request);
        Task<PaginatedTableResponse<GetConsignacionDTO>?> SearchConsignacion(GetPaginatedDTO request);
        Task<List<GetConsignacionDTO>?> GetAllConsignacion();
        Task<int> AddMoreFranchiseConsignacion(Guid userId, CreateConsignacionDTO request);
        Task<List<GetConsignacionPin>?> GetAllConsignacionPin();
        Task<int> EnableConsignacion(Guid id);
        Task<int> DisableConsignacion(Guid id);
        Task<int> EditConsignacionGeolocation(Guid id, UpdateConsignacionGeoDTO request);
        Task<List<GetConsignacionDTO>?> GetRenewalDetails(DateTime month);
        Task<List<GetConsignacionDTO>?> GetAllConsignacionDetails();
    }
}
