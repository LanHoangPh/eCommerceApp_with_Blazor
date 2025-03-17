using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.CategoryDTO;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
        Task<ServicesResponse> AddAsync(CreateCategory entity);
        Task<ServicesResponse> UpdateAsync(UpdateCategory entity);
        Task<ServicesResponse> DeleteAsync(Guid id);
    }
}
