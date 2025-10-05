using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using SchoolAPI.Models.Auth;
using SchoolAPI.Repositories.AuthRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SchoolAPI.Services.AuthService
{
    public class AuthService(IAuthRepository authRepository, IConfiguration config) : IAuthService
    {
        public readonly IAuthRepository _authRepository = authRepository;
        private readonly IConfiguration _config = config;
        public async Task<LoginResponse> Login(string username, string password)
        {
            LoginResponse loginResponse = await _authRepository.Login(username, password).ConfigureAwait(false);
          
            if (loginResponse.Usertypeid == 2)
            {
                loginResponse.UserType = "Associate";
            }
            if (loginResponse.Usertypeid == 3)
            {
                loginResponse.UserType = "EnterpriseAdmin";
            }
            if (loginResponse.Usertypeid == 4 || loginResponse.Usertypeid == 11 || loginResponse.Usertypeid == 12 || loginResponse.Usertypeid == 13 || loginResponse.Usertypeid == 14)
            {
                loginResponse.UserType = "SchoolAdmin";
            }
            if (loginResponse.Usertypeid == 5)
            {
                loginResponse.UserType = "Staff";
            }
            if (loginResponse.Usertypeid == 6 || loginResponse.Usertypeid == 7)
            {
                loginResponse.UserType = "Parent";
            }
            if (loginResponse.Usertypeid == 8)
            {
                loginResponse.UserType = "Principal";
            }
            if (loginResponse.Usertypeid == 9)
            {
                loginResponse.UserType = "Account";
            }
            if (loginResponse.Usertypeid == 15)
            {
                loginResponse.UserType = "Driver";
            }

            List<MobileUserMenu> mobileUserMenus = await _authRepository.GetMobileUserMenuAsync(loginResponse.SchoolId, loginResponse.Usertypeid).ConfigureAwait(false);
            loginResponse.MobileMenu = mobileUserMenus;
            List<UserMenu> webMenu = await GetWebMenu(loginResponse.SchoolId, loginResponse.Usertypeid).ConfigureAwait(false);
            loginResponse.WebMenu = webMenu;
            return loginResponse;
        }
        public async Task<List<MobileUserMenu>> GetMobileUserMenuAsync(int schoolId, int roleId)
        {
            return await _authRepository.GetMobileUserMenuAsync(schoolId, roleId).ConfigureAwait(false);
        }

        public string GenerateJwtToken(LoginResponse user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);

            List<Claim> claims =
            [
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Role, user.Usertypeid.ToString()),  // role-based authorization
            ];

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64); // 512-bit token
            return Convert.ToBase64String(randomBytes);
        }
        public async Task<string> ChangePasswordAsync(string userName, string password)
        {
            return await _authRepository.ChangePasswordAsync(userName, password).ConfigureAwait(false);
        }
        private async Task<List<UserMenu>> GetWebMenu(int schoolId, int userTypeId)
        {
            try
            {
                List<UserMenu> userMenus = await _authRepository.GetUserMenuAsync(0, schoolId, userTypeId).ConfigureAwait(false);
                return GetMenuTree(userMenus, 0);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static List<UserMenu> GetMenuTree(List<UserMenu> userMenus, int? ParentId)
        {
            return [.. userMenus.Where(x => x.ParentMenuId == ParentId).Select(x => new UserMenu()
            {
                MenuId = x.MenuId,
                ParentMenuId = x.ParentMenuId,
                MenuDesc = x.MenuDesc,
                MenuUrl = x.MenuUrl,
                MenuOrder = x.MenuOrder,
                Isactive = x.Isactive,
                MenuIcon = x.MenuIcon,
                List = GetMenuTree(userMenus, x.MenuId)
            })];
        }
    }
}
