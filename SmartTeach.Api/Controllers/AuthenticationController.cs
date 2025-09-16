
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
            return Ok(result);
        }

    }
}
