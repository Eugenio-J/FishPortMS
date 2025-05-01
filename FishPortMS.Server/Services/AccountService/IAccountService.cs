using FishPortMS.Server.Response;
using FishPortMS.Shared.DTOs.AccountDTO;

namespace FishPortMS.Server.Services.AccountService
{
    public interface IAccountService
    {
        Task<int> Register(RegisterDTO request);
        Task<LoginResponse> Login(LoginDTO request);
        Task<int> ForgotPass(ForgotPasswordDTO request);
        Task<int> VerifyCode(VerifyCodeDTO request);
        Task<int> SendVerification(EmailVerificationDTO request, string userEmail);
        Task<LoginResponse?> ReRefreshToken(string? refToken);
        LoginResponse Logout();
        Task<int> ChangePassword(ChangePassDTO request);
        Task<int> UpdateUser(UpdateProfileDTO request);
        Task<UpdateProfileDTO?> GetSingleUser();
        Task<string> GetSingleUserAvatar();
    }
}
