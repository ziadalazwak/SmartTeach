using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.AuthDto
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Message { get; set; } 
        public bool IsAuthenticated { get; set; }   
        public List<string> Roles { get; set; }
        public string Username{ get; set; }
        //public DateTime Expiration { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }    
        public DateTime RefreshTokenExpiration { get; set; }

    }
}
