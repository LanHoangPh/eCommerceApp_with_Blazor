using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.CategoryDTO;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.DTOs.ProductDTO;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<CreateProduct, Product>();

            CreateMap<UpdateCategory, Category>();
            CreateMap<UpdateProduct, Product>();

            CreateMap<Category, GetCategory>();
            CreateMap<Product, GetProduct>();
            CreateMap<PaymentMethod, GetPaymentMethod>();

            CreateMap<CreateUser, AppUser>();
            CreateMap<LoginUser, AppUser>();

            CreateMap<CreateAchieve, Achieve>();

        }

    }
}
