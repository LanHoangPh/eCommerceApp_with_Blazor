using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DTOs.Cart
{
    public class Checkout
    {
        public required Guid PaymentMethodId { get; set; }
        public required IEnumerable<ProcessCart> Carts { get; set; }
    }
}
