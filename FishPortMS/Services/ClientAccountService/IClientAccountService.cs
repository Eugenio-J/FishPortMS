using FishPortMS.Client.Response;
using FishPortMS.Shared.DTOs.AccountDTO;

namespace FishPortMS.Services.ClientAccountService
{
    public interface IClientAccountService
    {
        Token Token { get; set; }
        Task<string> LoginAsync(LoginDTO request);
        Task<string> RefreshToken();
        Task<int> Register(RegisterDTO request);
        Task<string> Logout();
        Task<int> ChangePassword(ChangePassDTO payload);
        Task<int> ForgotPass(ForgotPasswordDTO payload);
        Task<int> VerifyCode(VerifyCodeDTO payload);
        Task<int> SendVerification(EmailVerificationDTO payload, string userEmail);
        Task<UpdateProfileDTO?> GetSingleUser();
        Task<int> UpdateUser(UpdateProfileDTO request);
        Task<string> GetSingleUserAvatar();
    }
}
