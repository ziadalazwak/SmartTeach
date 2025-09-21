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




        
            if(User.RefreshTokens.Any(rt => rt.IsActive))
            {
                var activeRefreshToken = User.RefreshTokens.FirstOrDefault(rt => rt.IsActive);
                return new AuthResponse
                {
                    //Expiration=token.ValidTo,
                    Username = login.Username,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    IsAuthenticated = true,
                    Message = "Login Successfull",
                    Roles = (await _userManager.GetRolesAsync(User)).ToList(),
                    RefreshToken = activeRefreshToken.Token,
                    RefreshTokenExpiration = activeRefreshToken.ExpiresOn
                };
            } var refreshToken = _tokenService.GenerateRefreshToken();
            User.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(User);
            return new AuthResponse
            {
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
                //Expiration=token.ValidTo,
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

            var refreshToken = _tokenService.GenerateRefreshToken();    
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);
            return new AuthResponse{ 
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
                Roles = new List<string>{register.Role.ToString()},
                Username = user.UserName,

                Token=new JwtSecurityTokenHandler().WriteToken(token), IsAuthenticated = true, Message = "User created successfully" };   


        }
        public async Task<AuthResponse> RefreshTokenAsync(string token)
        {
            var authmodel = new AuthResponse();
            var user =  _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authmodel.IsAuthenticated = false;
                authmodel.Message = "Invalid token";
                return authmodel;
            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                authmodel.IsAuthenticated = false;
                authmodel.Message = "Inactive token";
                return authmodel;
            }
            // Revoke current refresh token
            refreshToken.RevokedOn = DateTime.UtcNow;
            // Generate new refresh token and add it to the user
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);
            // Generate new JWTc 
            var jwtToken =  _tokenService.CreateToken(user).Result;
            // Return authentication response
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authmodel.RefreshToken = newRefreshToken.Token;
            authmodel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
            authmodel.IsAuthenticated = true;
            authmodel.Username = user.UserName;
            authmodel.Message = "Token refreshed successfully";
            authmodel.Roles = _userManager.GetRolesAsync(user).Result.ToList();
            return authmodel;
        }
                public async Task<bool> RevokeRefreshTokenAsync(string token)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                return false;
            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                return false;
            }
            // Revoke current refresh token
            refreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return true;
        }
    } }

