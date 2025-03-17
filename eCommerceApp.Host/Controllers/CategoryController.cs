using eCommerceApp.Application.DTOs.CategoryDTO;
using eCommerceApp.Application.DTOs.ProductDTO;
using eCommerceApp.Application.Services.Implementations;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryServices categoryServices) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var data = await categoryServices.GetAllAsync();
            return data.Count() > 0 ? Ok(data) : NotFound();
        }
        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await categoryServices.GetByIdAsync(id);
            return product != null ? Ok(product) : NotFound(new { message = "Product Not found" });
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateCategory category)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryServices.AddAsync(category);
            return result.Success ? Ok(result) : NotFound(result);
        }
        [HttpPut("Upadate")]
        public async Task<IActionResult> Update([FromBody] UpdateCategory category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryServices.UpdateAsync(category);
            return result.Success ? Ok(result) : NotFound(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await categoryServices.DeleteAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
