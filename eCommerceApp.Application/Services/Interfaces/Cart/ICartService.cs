using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Domain.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServicesResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves);
        Task<ServicesResponse> Checkout(Checkout checkout);
    }
}
