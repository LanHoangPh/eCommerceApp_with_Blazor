using eCommerceApp.Application.DTOs.ProductDTO;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.CategoryDTO
{
    public class GetCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; } = new Guid();
        public ICollection<GetProduct> Products { get; set; } = new List<GetProduct>();
    }

}
