using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Interfaces.CategorySpecifics
{
    public interface ICategory
    {
        Task<IEnumerable<Product>> GetProdcutByCategory(Guid categoryId);
    }
}
