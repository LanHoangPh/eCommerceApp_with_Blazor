using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.ProductDTO
{
    public class UpdateProduct : ProductBase 
    {
        [Required]
        public Guid Id { get; set; }
    }

}
