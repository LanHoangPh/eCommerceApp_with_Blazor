using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentMethodService methodService) : ControllerBase
    {
        [HttpGet("payment-method")]
        public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPayments()
        {
            var methods = await methodService.GetPaymentMethod();
            if(!methods.Any()) return NotFound();
            else return Ok(methods);
        }
    }
}
