using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.CategoryDTO;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CategoryServices(IGeneric<Category> cateGeneric, IMapper mapper) : ICategoryServices
    {
        public async Task<ServicesResponse> AddAsync(CreateCategory category)
        {
            var mapperData = mapper.Map<Category>(category);
            var result = await cateGeneric.AddAsync(mapperData);
            return result > 0 ?  new ServicesResponse(true, "Add Category success") : new ServicesResponse(false, "Add Category failed "); 
        }

        public async Task<ServicesResponse> DeleteAsync(Guid id)
        {
            int result = await cateGeneric.DeleteAsync(id);
           
            return result > 0 ? new ServicesResponse(true, "Delete category success") : new ServicesResponse(false, "Delete Category not found or failed to delete");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rawData = await cateGeneric.GetAllAsync();
            if (!rawData.Any())
            {
                return [];
            }
            return mapper.Map<IEnumerable<GetCategory>>(rawData);
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var rawData = await cateGeneric.GetByIdAsync(id);
            if (rawData == null)
            {
                return new GetCategory();
            }
            return mapper.Map<GetCategory>(rawData);
        }

        public async Task<ServicesResponse> UpdateAsync(UpdateCategory category)
        {
            var mapperData = mapper.Map<Category>(category);
            var result = await cateGeneric.UpdateAsync(mapperData);
            return result > 0 ? new ServicesResponse(true, "Update Category success") : new ServicesResponse(false, "Update Category failed "); ;
        }

    }
}
