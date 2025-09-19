using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartTeach.App.Dto.AuthDto;
using SmartTeach.Domain.Models;
using SmartTeach.Persistence.Dbcontext;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Persistence.Services
{
    public class TokenService
    {
        private
             readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager; 
        public TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            // Get claims & roles
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    }
            .Union(roleClaims)
            .Union(userClaims);

            // Get JWT config
            var key = _configuration["Jwt:SigningKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"] ?? "60");

            // Create signing credentials
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // Build the token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: signingCredentials
            );

            // Return token string
            return jwtSecurityToken;
        }
        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    ExpiresOn = DateTime.UtcNow.AddDays(7),
                    CreatedOn = DateTime.UtcNow
                };
            }
        }
        public AuthResponse RefreshTokenAsync(string token)
        {
           var authmodel=new AuthResponse();
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
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
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _userManager.UpdateAsync(user);
            // Generate new JWT
            var jwtToken = CreateToken(user).Result;
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

    }
}
