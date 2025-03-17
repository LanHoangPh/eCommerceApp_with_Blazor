using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.CategoryDTO
{
    public class CategoryBase
    {
        [Required]
        public string? Name { get; set; }
    }

}
