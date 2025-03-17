using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DTOs
{
    public class LoginResponse(bool success = false, string message = null!, string token = null!, string RefreshToken = null!)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public string Token { get; set; } = token;
        public string RefreshToken { get; set; } = RefreshToken;
    }
}
