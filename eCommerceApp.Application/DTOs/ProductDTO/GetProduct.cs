using eCommerceApp.Application.DTOs.CategoryDTO;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.ProductDTO
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        public GetCategory? Category { get; set; } = new GetCategory();
    }
}
