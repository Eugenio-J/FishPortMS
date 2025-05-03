using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FishPortMS.Shared.Response;
using FishPortMS.Server.Services.ConsignacionService;
using FishPortMS.Shared.DTOs.ConsignacionDTO;

namespace FishPortMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsignacionController : ControllerBase
    {
        private readonly IConsignacionService _consignacionService;

        public ConsignacionController(IConsignacionService ConsignacionService)
        {
            _consignacionService = ConsignacionService;
        }

        [HttpGet("all-consignacion-paginated")]
        public async Task<ActionResult<PaginatedTableResponse<GetConsignacionDTO>?>> GetAllConsignacionesPaginated([FromQuery] GetPaginatedDTO request)
        {
            var status = await _consignacionService.GetAllConsignacionesPaginated(request);
            if (status == null) return Unauthorized();
            return Ok(status);
        }

        [HttpGet("search-consignacion")]
        public async Task<ActionResult<PaginatedTableResponse<GetConsignacionDTO>?>> SearchConsignacion([FromQuery] GetPaginatedDTO request)
        {
            var status = await _consignacionService.SearchConsignacion(request);
            if (status == null) return Unauthorized();
            return status;
        }

        [HttpPost("add-more-consignacion/{userId}")]
        public async Task<ActionResult<int>> AddMoreFranchiseConsignacion(Guid userId, CreateConsignacionDTO request)
        {
            int status = await _consignacionService.AddMoreFranchiseConsignacion(userId, request);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest();
            return Ok(status);
        }

        [HttpPut("enable-consignacion/{id}")]
        public async Task<ActionResult<int>> EnableConsignacion(Guid id)
        {
            var status = await _consignacionService.EnableConsignacion(id);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest(status);
            return Ok(status);
        }

        [HttpPut("disable-consignacion/{id}")]
        public async Task<ActionResult<int>> DisableConsignacion(Guid id)
        {
            var status = await _consignacionService.DisableConsignacion(id);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest(status);
            return Ok(status);
        }

        [HttpPut("update-geolocation/{id}")]
        public async Task<ActionResult<int>> EditConsignacionGeolocation(Guid id, UpdateConsignacionGeoDTO request)
        {
            var status = await _consignacionService.EditConsignacionGeolocation(id, request);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest(status);
            return Ok(status);
        }

        [HttpGet("all-renewal-details")]
        public async Task<ActionResult<List<GetConsignacionDTO>?>> GetRenewalDetails([FromQuery] DateTime month)
        {
            var status = await _consignacionService.GetRenewalDetails(month);
            if (status == null) return Unauthorized();
            return Ok(status);
        }

        [HttpGet("all-consignacion-pin")]
        public async Task<ActionResult<List<GetConsignacionPin>?>> GetAllConsignacionPin()
        {
            var status = await _consignacionService.GetAllConsignacionPin();
            if (status == null) return Unauthorized();
            return Ok(status);
        }

        [HttpGet("all-consignacion-details")]
        public async Task<ActionResult<List<GetConsignacionDTO>?>> GetAllConsignacionDetails()
        {
            var status = await _consignacionService.GetAllConsignacionDetails();
            if (status == null) return Unauthorized();
            return Ok(status);
        }

    }
}
