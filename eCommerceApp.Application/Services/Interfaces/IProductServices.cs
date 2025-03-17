using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface IcategoryServices
    {
        Task<IEnumerable<GetProduct>> GetAllAsync(); 
        Task<GetProduct> GetByIdAsync(Guid id);
        Task<ServicesResponse> AddAsync(CreateProduct entity); 
        Task<ServicesResponse> UpdateAsync(UpdateProduct entity);
        Task<ServicesResponse> DeleteAsync(Guid id);
    }
}
