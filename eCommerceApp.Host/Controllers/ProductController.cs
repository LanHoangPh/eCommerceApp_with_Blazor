using eCommerceApp.Application.DTOs.ProductDTO;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IcategoryServices categoryServices) : ControllerBase
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
            return product != null ? Ok(product) : NotFound(new {message = "Product Not found"});
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]CreateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryServices.AddAsync(product);
            return result.Success ? Ok(result) : NotFound(result);
        }
        [HttpPut("Upadate")]
        public async Task<IActionResult> Update([FromBody]UpdateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryServices.UpdateAsync(product);
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
