using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FishPortMS.Server.Services.UserManagementService;
using FishPortMS.Shared.DTOs.UserManagementDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Response;

namespace FishPortMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService UserManagementService)
        {
            _userManagementService = UserManagementService;
        }

        [HttpGet("all-users-paginated")]
        public async Task<ActionResult<PaginatedTableResponse<GetUsersDTO>?>> GetAllUsersPaginated([FromQuery] GetPaginatedDTO request)
        {
            var status = await _userManagementService.GetAllUsersPaginated(request);
            if (status == null) return Unauthorized();
            return Ok(status);
        }

        [HttpGet("search-users")]
        public async Task<ActionResult<PaginatedTableResponse<GetUsersDTO>?>> SearchUser([FromQuery] GetPaginatedDTO request)
        {
            var status = await _userManagementService.SearchUser(request);
            if (status == null) return Unauthorized();
            return status;
        }

        [HttpPut("enable-acc/{userId}")]
        public async Task<ActionResult<int>> EnableAccount(Guid userId)
        {
            var status = await _userManagementService.EnableAccount(userId);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest();
            return Ok(status);
        }

        [HttpPut("disable-acc/{userId}")]
        public async Task<ActionResult<int>> DisableAccount(Guid userId)
        {
            var status = await _userManagementService.DisableAccount(userId);
            if (status == -1) return Unauthorized();
            if (status == 0) return BadRequest();
            return Ok(status);
        }

        [HttpGet("all-bs")]
        public async Task<ActionResult<List<GetUsersDTO>?>> GetBSList()
        {
            var status = await _userManagementService.GetBSList();
            if (status == null) return Unauthorized();
            return Ok(status);
        }
    }
}
