using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DTOs
{
    public class ServicesResponse(bool success = false, string message = null!)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
    }
}
