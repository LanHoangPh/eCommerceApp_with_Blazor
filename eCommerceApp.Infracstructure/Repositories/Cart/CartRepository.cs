using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infracstructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infracstructure.Repositories.Cart
{
    public class CartRepository(AppDbContext dbContext) : ICart
    {
        public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts)
        {
            dbContext.CheckoutAchieves.AddRange(checkouts);
            return await dbContext.SaveChangesAsync();
        }

    }
}
