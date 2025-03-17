using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.ProductDTO;
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
    public class categoryServices(IGeneric<Product> productGeneric, IMapper mapper) : IcategoryServices
    {
        public async Task<ServicesResponse> AddAsync(CreateProduct product)
        {
            var mapperData = mapper.Map<Product>(product);
            int result = await productGeneric.AddAsync(mapperData);
            if (result < 0)
            {
                return new ServicesResponse(false, "Create Prodcut not found");
            }
            return new ServicesResponse(true, "Create Prodcut success");


        }

        public async Task<ServicesResponse> DeleteAsync(Guid id)
        {
            int  result = await productGeneric.DeleteAsync(id);
            return result > 0 ? new ServicesResponse(true, "Delete Product success") : new ServicesResponse(false, "Delete Product not found or failed to deletes ");
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var rawData = await productGeneric.GetAllAsync();
            if (!rawData.Any())
            {
                return [];
            }
            return mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var rawData = await productGeneric.GetByIdAsync(id);
            if (rawData == null)
            {
                return new GetProduct();
            }
            return mapper.Map<GetProduct>(rawData);
        }

        public async Task<ServicesResponse> UpdateAsync(UpdateProduct product)
        {
            var mapperData = mapper.Map<Product>(product);
            int result = await productGeneric.UpdateAsync(mapperData);
            return result > 0 ? new ServicesResponse(true, "Update Product success") : new ServicesResponse(false, "Update Product failed ");
        }
    }
}
