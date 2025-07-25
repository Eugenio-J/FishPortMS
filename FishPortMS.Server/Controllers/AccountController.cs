using FishPortMS.Server.Response;
using FishPortMS.Server.Services.AccountService;
using FishPortMS.Shared.DTOs.AccountDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FishPortMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(IAccountService accountService, IHttpContextAccessor contextAccessor)
        {
            _accountService = accountService;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("register")]
        public async Task<ActionResult<int>> Register(RegisterDTO request) 
        {
            int response = await _accountService.Register(request);
            if (response == 0) return BadRequest("Invalid Credentials");

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO request) 
        {
            LoginResponse response = await _accountService.Login(request);

            if (string.IsNullOrEmpty(response.AccessToken)) return BadRequest("Invalid Credential");

            Response.Cookies.Append("refreshToken", response.RefreshToken, response.CookieOptions);

            return Ok(response.AccessToken);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<string>> Logout() 
        {
            LoginResponse response = _accountService.Logout();
            Response.Cookies.Delete("refreshToken");
            return Ok(response.AccessToken);
        }

        [HttpGet("get-single-user")]
        public async Task<ActionResult<UpdateProfileDTO>> GetSingleUser() 
        {
            UpdateProfileDTO? response = await _accountService.GetSingleUser();

            if (response == null) return NotFound("User not found");

            return Ok(response);
        }

        [HttpGet("get-user-avatar")]
        public async Task<ActionResult<string>> GetSingleUserAvatar() 
        {
            string? result = await _accountService.GetSingleUserAvatar();

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("update-user")]
        public async Task<ActionResult<int>> UpdateUser(UpdateProfileDTO request) 
        {
            int result = await _accountService.UpdateUser(request);

            if (result == -1) return Unauthorized();
            if (result == 0) return BadRequest();
            return Ok(result);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> GenRefreshToken()
        {
            string? refToken = Request.Cookies["refreshToken"];

            LoginResponse? response = await _accountService.ReRefreshToken(refToken);
            if (response == null) return BadRequest("");
            if (string.IsNullOrEmpty(response.AccessToken)) return BadRequest("");

            Response.Cookies.Append("refreshToken", response.RefreshToken, response.CookieOptions);

            return Ok(response.AccessToken);
        }

        [HttpPut("change-pass")]
        public async Task<IActionResult> ChangePassword(ChangePassDTO request)
        {
            int response = await _accountService.ChangePassword(request);

            if (response == 0) return BadRequest(0);

            return Ok(1);
        }

        [HttpPost("forgot-pass")]
        public async Task<IActionResult> ForgotPass(ForgotPasswordDTO request)
        {
            int response = await _accountService.ForgotPass(request);
            if (response == 0) return BadRequest();
            return Ok(response);
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode(VerifyCodeDTO request)
        {
            int response = await _accountService.VerifyCode(request);
            if (response == 0) return BadRequest();
            return Ok(response);
        }

        [EnableRateLimiting("verify-email")]
        [HttpPost("send-verification/{userEmail}")]
        public async Task<IActionResult> SendVerification(EmailVerificationDTO request, string userEmail)
        {
            int response = await _accountService.SendVerification(request, userEmail);

            if (response == 0) return BadRequest();
            return Ok(response);
        }
    }
}
