using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models.Auth;
using SchoolAPI.Repositories.AuthRepository;
using SchoolAPI.Services.AuthService;

namespace SchoolAPI.Controllers
{ 
    [ApiController]
    public class AuthController(IAuthService authService, IAuthRepository authRepository) : ControllerBase
    {
        public readonly IAuthService _authService = authService;
        public readonly IAuthRepository _authRepository = authRepository;

        [HttpGet("api/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Username and password are required" });
            }

            LoginResponse loginResponse = await _authService.Login(userName, password);
            if (loginResponse != null && loginResponse.UserId != 0)
            {
                // 2. Generate JWT token
                var accessToken = _authService.GenerateJwtToken(loginResponse);
                var refreshToken = _authService.GenerateRefreshToken();

                await _authRepository.SaveRefreshToken(loginResponse.UserId, refreshToken, DateTime.UtcNow.AddDays(30));

                return Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    User = loginResponse
                });
            }

            return Unauthorized(new { message = "Invalid username or password" });
        }
       
        [HttpPost("api/refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            RefreshToken savedToken = await _authRepository.GetRefreshToken(request.RefreshToken);

            if (savedToken == null || savedToken.IsRevoked || savedToken.Expires < DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token");

            LoginResponse user = await _authRepository.GetUserById(savedToken.UserId);

            string newAccessToken = _authService.GenerateJwtToken(user);
            string newRefreshToken = _authService.GenerateRefreshToken();

            await _authRepository.RevokeRefreshToken(savedToken.Token);
            await _authRepository.SaveRefreshToken(user.UserId, newRefreshToken, DateTime.UtcNow.AddDays(30));

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [Route("api/changepassword")]
        [HttpPost]
        public async Task<IActionResult> Changepassword(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Username and password are required" });
            }

            string response = await _authService.ChangePasswordAsync(userName, password);
            return Ok(response);
        }
    }
}
