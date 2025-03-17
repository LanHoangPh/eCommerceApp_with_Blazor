using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService ) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser user)
        {
            var result = await authenticationService.CreateUserAsync(user);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            var result = await authenticationService.LoginUser(user);
            return result.Success ?  Ok(result) : BadRequest();
        }

        [HttpGet("RefreshToken/{refreshToken}")]
        public async Task<IActionResult> Login(string refreshToken)
        {
            var result = await authenticationService.ReviveToken(refreshToken);
            return result.Success ? Ok(result) : BadRequest();
        }
    }
}
