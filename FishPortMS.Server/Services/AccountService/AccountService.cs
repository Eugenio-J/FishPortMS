using FishPortMS.Server.Data;
using FishPortMS.Server.Response;
using FishPortMS.Shared.DTOs.AccountDTO;
using FishPortMS.Shared.Enums;
using FishPortMS.Shared.Models.Account;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using static MudBlazor.Icons.Custom;

namespace FishPortMS.Server.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context; 
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetUserId() => _httpContextAccessor.HttpContext?.User
          .FindFirstValue(ClaimTypes.NameIdentifier);

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private UpdateProfileDTO ConvertProfile(UserProfile request)
        {
            return new UpdateProfileDTO
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.PhoneNumber,
                City = request.City,
                Address = request.Address,
                Region = request.Region,
                Avatar = request.Avatar
            };
        }

        public async Task<int> ChangePassword(ChangePassDTO request)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userIdGuid = Guid.TryParse(userId, out Guid parsedGuid) ? parsedGuid : Guid.Empty;

            User? db_user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == userIdGuid);

            if (db_user == null) return 0;

            if (
                db_user != null
                && VerifyPasswordHash(request.CurrPassword, db_user.Password, db_user.PasswordSalt)
            )
            {
                CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                db_user.Password = passwordHash;
                db_user.PasswordSalt = passwordSalt;
                await _context.SaveChangesAsync();
                return 1;

            }
            return 0;
        }

        public async Task<int> ForgotPass(ForgotPasswordDTO request)
        {
            User? dbUser = await _context.Users.
              FirstOrDefaultAsync(user => user.Email == request.Email && user.VerificationToken == request.Code);

            if (dbUser == null) return 0;

            CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            dbUser.PasswordSalt = passwordSalt;
            dbUser.Password = passwordHash;
            dbUser.VerificationToken = new Random().Next(100000, 1000000).ToString();

            int status = await _context.SaveChangesAsync();
            return status;
        }

        public async Task<UpdateProfileDTO?> GetSingleUser()
        {
            var user = await _context.UserProfiles
               .Where(u => u.UserId.ToString() == GetUserId())
               .FirstOrDefaultAsync();
            if (user == null) { return null; }
            return ConvertProfile(user);
        }

        public async Task<string> GetSingleUserAvatar()
        {
            var users = await _context.UserProfiles
                   .Where(p => p.UserId.ToString() == GetUserId())
                    .Select(p => p.Avatar)
                   .FirstOrDefaultAsync();

            return users;
        }

        private async Task<string> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AccessToken").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private async Task<RefreshToken> GenerateRefreshToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("RefreshToken")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new RefreshToken
            {
                Token = jwt,
                ExpiresAt = DateTime.Now.AddDays(7)
            };
        }

        private LoginResponse DeleteRefreshToken()
        {
            LoginResponse response = new LoginResponse();

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now
            };

            response.CookieOptions = cookieOptions;
            response.AccessToken = string.Empty;
            response.RefreshToken = string.Empty;

            return response;
        }

        public async Task<LoginResponse> Login(LoginDTO request)
        {
            User? user = await _context.Users
                 .FirstOrDefaultAsync(user => user.Email == request.Email && user.IsActive == true);

            LoginResponse response = new LoginResponse();

            if (
                user != null
                && VerifyPasswordHash(request.Password, user.Password, user.PasswordSalt)
            )
            {
                if (user.Role == Roles.SUPERUSER
                    || user.Role == Roles.ADMIN
                    || user.Role == Roles.ADMIN_ASST
                    || user.Role == Roles.CONSIGNACION_OWNER
                    || user.Role == Roles.BATILYO
                    || user.Role == Roles.BUY_AND_SELL
                    || user.Role == Roles.CASHIER)
                {
                    string token = await CreateToken(user);
                    RefreshToken refreshToken = await GenerateRefreshToken(user);

                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = refreshToken.ExpiresAt,
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    };

                    user.RefreshToken = refreshToken.Token;
                    user.RefreshTokenCreatedAt = refreshToken.CreatedAt;
                    user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

                    await _context.SaveChangesAsync();

                    response.CookieOptions = cookieOptions;
                    response.AccessToken = token;
                    response.RefreshToken = refreshToken.Token;

                    return response;
                }
            }
            return response;
        }

        public LoginResponse Logout()
        {
            return DeleteRefreshToken();
        }

        public async Task<int> Register(RegisterDTO request)
        {
            if (await _context.Users.AnyAsync(user => user.Email == request.Email)) return 0;

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User new_user = new User
            {
                Email = request.Email,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = new Random().Next(100000, 1000000).ToString(),
                Role = request.Role
            };

            _context.Users.Add(new_user);
            int status = await _context.SaveChangesAsync();

            if (status == 0)
            {
                return 0;
            }
            else
            {
                if (request.Role == Roles.SUPERUSER
                    || request.Role == Roles.ADMIN
                    || request.Role == Roles.ADMIN_ASST
                    || request.Role == Roles.CONSIGNACION_OWNER
                    || request.Role == Roles.BATILYO
                    || request.Role == Roles.CASHIER
                    || request.Role == Roles.BUY_AND_SELL)
                {
                    var userDetails = new UserProfile
                    {
                        User = new_user,
                        UserId = new_user.Id,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Address = request.Address,
                        City = request.City,
                        Region = request.Region,
                        PhoneNumber = request.Phone,
                        AttendancePin = Guid.NewGuid().ToString("N").Substring(0, 4),
                    };
                    _context.UserProfiles.Add(userDetails);
                    await _context.SaveChangesAsync();
                }
            }

            return status;
        }

        private ClaimsPrincipal? ValidateRefreshToken(string refreshToken)
        {
            try
            {
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("RefreshToken")!)),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
                return new JwtSecurityTokenHandler().ValidateToken
                (
                    refreshToken,
                    validationParams,
                    out SecurityToken token
                );
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<LoginResponse?> ReRefreshToken(string? refToken)
        {
            if (refToken == null) return null;

            var claims = ValidateRefreshToken(refToken);
            if (claims == null) return null;

            string? userIdString = claims.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value;

            User? db_user = await _context.Users.FirstOrDefaultAsync(user => user.Id.ToString() == userIdString);
            if (db_user == null) return null;

            if (!db_user.RefreshToken.Equals(refToken))
            {
                return null;
            }
            else if (db_user.RefreshTokenExpiresAt < DateTime.Now)
            {
                var res = DeleteRefreshToken();

                return res;
            }

            LoginResponse response = new LoginResponse();

            string token = await CreateToken(db_user);
            RefreshToken refreshToken = await GenerateRefreshToken(db_user);

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiresAt
            };

            db_user.RefreshToken = refreshToken.Token;
            db_user.RefreshTokenCreatedAt = refreshToken.CreatedAt;
            db_user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

            await _context.SaveChangesAsync();

            response.CookieOptions = cookieOptions;
            response.AccessToken = token;
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<int> SendVerification(EmailVerificationDTO request, string userEmail)
        {
            User? db_user = await _context.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
            if (db_user == null) return 0;

            string emailBody = "<div style='text-align: center; font-weight: bold; font-size: 18px;'>";
            emailBody += "<span style='color: black!important;'> Hello!<br><br>Here is your verification code :</span><br><br>";
            emailBody += $"<span style='font-size:27px!important; letter-spacing: 1px; background: linear-gradient(112deg, rgba(244,222,81,1) 0%, rgba(242,180,30,1) 91%); color: white; padding: 7px; padding-left: 10px; padding-right: 10px; border-radius: 4px;'>{db_user.VerificationToken}</span></div><br>";

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailUserName")));
            email.To.Add(MailboxAddress.Parse(db_user.Email));
            email.Subject = "FishPortMS Forgot password";
            email.Body = new TextPart(TextFormat.Html) { Text = emailBody };

            string acc_host = _configuration.GetValue<string>("EmailHost")!;
            using var smtp = new SmtpClient();
            smtp.Connect(acc_host, 465, SecureSocketOptions.SslOnConnect);

            string acc_email = _configuration.GetValue<string>("EmailUserName")!;
            string acc_password = _configuration.GetValue<string>("EmailPassword")!;
            smtp.Authenticate(acc_email, acc_password);
            smtp.Send(email);
            smtp.Disconnect(true);

            return 1;
        }

        public async Task<int> UpdateUser(UpdateProfileDTO request)
        {
            var user = await _context.UserProfiles
                .Where(u => u.UserId.ToString() == GetUserId())
                .FirstOrDefaultAsync();

            if (user == null)
                return -1;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.Phone;
            user.City = request.City;
            user.Address = request.Address;
            user.Region = request.Region;
            user.City = request.City;
            user.Avatar = request.Avatar;

            int status = await _context.SaveChangesAsync();
            if (status == 0) return 0;

            return 1;
        }

        public async Task<int> VerifyCode(VerifyCodeDTO request)
        {
            var verificationToken = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.VerificationToken == request.Code);
            if (verificationToken == null) return 0;
            return 1;
        }
    }
}
