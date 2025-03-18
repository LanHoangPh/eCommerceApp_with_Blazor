using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infracstructure.Services
{
    public class SripePaymentService : IPaymentService
    {
        public async Task<ServicesResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts)
        {
            try
            {
                var lineTtems = new List<SessionLineItemOptions>();
                foreach (var item in cartProducts)
                {
                    var pQuantity = carts.FirstOrDefault(x => x.ProductId == item.Id);
                    lineTtems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Name,
                                Description = item.Description,
                            },
                            UnitAmount = (long)(item.Price * 100),
                        },
                        Quantity = pQuantity!.Quantity,
                    });
                }
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = ["usd"],
                    LineItems = lineTtems,
                    Mode = "payment",
                    SuccessUrl = "http:localhost:5021/payment-success",
                    CancelUrl = "http:localhost:5021/payment-cancle"
                };
                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                return new ServicesResponse { Success = true, Message = session.Url };
            }
            catch (Exception ex)
            {
                return new ServicesResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
