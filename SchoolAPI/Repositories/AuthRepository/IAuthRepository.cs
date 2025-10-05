using SchoolAPI.Models.Auth;

namespace SchoolAPI.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<LoginResponse> Login(string username, string password);
        Task<List<MobileUserMenu>> GetMobileUserMenuAsync(int schoolId, int roleId);
        Task SaveRefreshToken(int userId, string token, DateTime expires);
        Task<RefreshToken> GetRefreshToken(string token);
        Task RevokeRefreshToken(string token);
        Task<LoginResponse> GetUserById(int userId);
        Task<string> ChangePasswordAsync(string userName, string password);
        Task<List<UserMenu>> GetUserMenuAsync(int userId, int schoolId, int roleId);
    }
}
