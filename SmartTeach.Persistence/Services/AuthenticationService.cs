using SmartTeach.App.Dto.AuthDto;
using SmartTeach.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Persistence.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<AuthResponse> Login(LoginDto login)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> Register(RegisterDto register)
        {
            throw new NotImplementedException();
        }
    }
}
