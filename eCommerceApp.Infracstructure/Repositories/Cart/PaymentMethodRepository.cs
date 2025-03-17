using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infracstructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infracstructure.Repositories.Cart
{
    public class PaymentMethodRepository(AppDbContext dbContext) : IPaymentMethod
    {
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
        {
            return await dbContext.PaymentMethods.AsNoTracking().ToListAsync();
        }
    }
}
