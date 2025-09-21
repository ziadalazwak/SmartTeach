using SmartTeach.App.Dto.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public interface IAuthenticationService
    {
        public Task<AuthResponse> Login(LoginDto login);
        public Task<AuthResponse> Register(RegisterDto register);
        public Task<AuthResponse> RefreshTokenAsync(string token);
        public Task<bool> RevokeRefreshTokenAsync(string token);
    }
}
