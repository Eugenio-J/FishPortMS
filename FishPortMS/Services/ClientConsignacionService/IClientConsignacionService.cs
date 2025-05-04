using FishPortMS.Shared.DTOs.ConsignacionDTO;
using FishPortMS.Shared.Response;

namespace FishPortMS.Services.ClientConsignacionService
{
    public interface IClientConsignacionService
    {
        List<GetRenewalDetailsDTO> getRenewalDetails { get; set; }
        List<GetConsignacionPin> getConsignacionPin { get; set; }
        List<GetConsignacionDTO> getConsignacion { get; set; }
        Task<PaginatedTableResponse<GetConsignacionDTO>> GetAllConsignacionesPaginated(GetPaginatedDTO payload);
        Task<List<GetConsignacionDTO>> GetAllConsignacions();
        Task<PaginatedTableResponse<GetConsignacionDTO>> SearchConsignacion(GetPaginatedDTO payload);
        Task<int> AddMoreFranchiseConsignacion(Guid userId, CreateConsignacionDTO payload);
        Task<List<GetConsignacionPin>> GetAllConsignacionPin();
        Task<int> EnableConsignacion(Guid id);
        Task<int> DisableConsignacion(Guid id);
        Task<int> EditConsignacionGeolocation(Guid id, UpdateConsignacionGeoDTO request);
        Task<List<GetConsignacionDTO>> GetRenewalDetails(DateTime month);
        Task<List<GetConsignacionDTO>?> GetAllConsignacionDetails();
    }
}
