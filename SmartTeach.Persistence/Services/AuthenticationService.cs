using Microsoft.AspNetCore.Identity;
using SmartTeach.App.Dto.AuthDto;
using SmartTeach.App.Services;
using SmartTeach.Persistence.Dbcontext;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Persistence.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;    
        public AuthenticationService(UserManager<ApplicationUser> userManager,TokenService tokenservice)
        {
            _userManager = userManager;
            _tokenService = tokenservice;
        }

        
        public async Task<AuthResponse> Login(LoginDto login)
        {
            var User = await _userManager.FindByNameAsync(login.Username);
            if (User is null || !await _userManager.CheckPasswordAsync(User, login.Password))
                return new AuthResponse { IsAuthenticated = false, Message = "User Not Registered" };
        

            var token = await _tokenService.CreateToken(User);   
            return new AuthResponse
            {
                Expiration=token.ValidTo,
                Username = login.Username,
                Token =new JwtSecurityTokenHandler().WriteToken( token),
                IsAuthenticated = true,
                Message = "Login Successfull",
                Roles = (await _userManager.GetRolesAsync(User)).ToList()
            };

        }

        public async Task<AuthResponse> Register(RegisterDto register)
        {
           
            if(await _userManager.FindByEmailAsync(register.Email) is not null)
                return new AuthResponse{  IsAuthenticated = false, Message = "User already exists"};
                  
            if(await _userManager.FindByNameAsync(register.Username) is not null)
                return new AuthResponse{  IsAuthenticated = false, Message = "Username already exists"};
            var user = new ApplicationUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.Username,


            };
            var add_user = await _userManager.CreateAsync(user, register.Password);
            if(!add_user.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in add_user.Errors)
                {
                   errors+= error.Description + ",";
                }
                return new AuthResponse{  IsAuthenticated = false, Message = errors};
            }
            await _userManager.AddToRoleAsync(user, register.Role.ToString());   

            var token = await _tokenService.CreateToken(user); 
            return new AuthResponse{ 
                Roles = new List<string>{register.Role.ToString()},
                Username = user.UserName,

                Token=new JwtSecurityTokenHandler().WriteToken(token), IsAuthenticated = true, Message = "User created successfully" ,Expiration=token.ValidTo};   


        }
    } }

