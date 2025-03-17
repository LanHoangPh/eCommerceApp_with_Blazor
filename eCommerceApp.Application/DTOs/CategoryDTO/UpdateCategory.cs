using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.CategoryDTO
{
    public class UpdateCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }

}
