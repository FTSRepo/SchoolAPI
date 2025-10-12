using SchoolAPI.Models.Auth;

namespace SchoolAPI.Services.AuthService
    {
    public interface IAuthService
        {
        Task<LoginResponse> Login(string username, string password);
        Task<List<MobileUserMenu>> GetMobileUserMenuAsync(int schoolId, int roleId);
        string GenerateJwtToken(LoginResponse user);
        string GenerateRefreshToken();
        Task<string> ChangePasswordAsync(string userName, string password);
        }
    }
