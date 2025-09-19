
using Microsoft.AspNetCore.Mvc;
using SmartTeach.App.Dto.AuthDto;
using SmartTeach.App.Services;

namespace SmartTeach.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController:ControllerBase
    {
        private readonly IAuthenticationService authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var result = await authService.Register(registerDto);
            if (result.IsAuthenticated == false)
            {
                return BadRequest(result.Message);
            }
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var result = await authService.Login(loginDto);
            if (result.IsAuthenticated == false)
            {
                return BadRequest(result.Message);
            }
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }
        void SetRefreshTokenInCookie(string refreshToken, DateTime expiresOn)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expiresOn,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        [HttpPost("RefreshToken")]
        public async Task< IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("No refresh token found in cookies.");
            }
            var result = await authService.RefreshTokenAsync(refreshToken);
            if (result.IsAuthenticated == false)
            {
                return BadRequest(result.Message);
            }
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }
    }
}
