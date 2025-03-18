using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces.CategorySpecifics;
using eCommerceApp.Infracstructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infracstructure.Repositories.CategorySpecifics
{
    public class CategoryRepository(AppDbContext context) : ICategory
    {
        public async Task<IEnumerable<Product>> GetProdcutByCategory(Guid categoryId)
        {
            var prodcuts = await context.Products
                .Include(p => p.Category)
                .Where(p=>p.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();
            return prodcuts.Count > 0 ? prodcuts : [] ;
        }
    }
}
