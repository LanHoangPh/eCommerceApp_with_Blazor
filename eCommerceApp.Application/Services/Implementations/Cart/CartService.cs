using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Implementations.Cart
{
    public class CartService(ICart cartinterface, IMapper mapper, IGeneric<Product> genericProduct, IPaymentMethodService paymentMethodService, IPaymentService paymentService) : ICartService
    {
        public async Task<ServicesResponse> Checkout(Checkout checkout)
        {
            var (prodcuts, totalAmount) = await GetCartTotalAmount(checkout.Carts);
            var paymentMethods = await paymentMethodService.GetPaymentMethod();
            if(checkout.PaymentMethodId == paymentMethods.FirstOrDefault()!.Id)
            {
                return await paymentService.Pay(totalAmount, prodcuts, checkout.Carts);
            }
            else
            {
                return new ServicesResponse(false, "Invalid payment method");
            } 
        }

        public async Task<ServicesResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves)
        {
            var mapperData = mapper.Map<IEnumerable<Achieve>>(achieves);
            var result = await cartinterface.SaveCheckoutHistory(mapperData);
            return result > 0 ? new ServicesResponse(true, "Checkout Achiveed") : new ServicesResponse(false, "Error occurred in saving");
        }

        private async Task<(IEnumerable<Product>, decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts)
        {
            if (!carts.Any()) return ([], 0);
            var products = await genericProduct.GetAllAsync();
            if (!products.Any()) return ([], 0);

            var cartProducts = carts
                .Select(cartitem => products.FirstOrDefault(x => x.Id == cartitem.ProductId))
                .Where(x => x != null)
                .ToList();
            var totalAmount = carts.Where(cartItems => cartProducts.Any(p => p!.Id == cartItems.ProductId))
                .Sum(cartItems => cartItems.Quantity * cartProducts.First(p=>p!.Id == cartItems.ProductId)!.Price);
            return (cartProducts!, totalAmount);

        }
    }
}
