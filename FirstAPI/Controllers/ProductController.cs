using Microsoft.AspNetCore.Mvc;
using FirstAPI.Models;
using FirstAPI.Services;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        { 
            return Ok(_service.GetAll()); 
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);

            if (product == null)
                return NotFound("Product Not Found");

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            var createdProduct = _service.Create(product);

            if (createdProduct == null)
                return BadRequest("Invalid product data");

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product updatedProduct)
        {
            var updated = _service.Update(id, updatedProduct);

            if (updated == null)
                return BadRequest("Invalid product data");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sucess = _service.Delete(id);

            if (!sucess)
                return NotFound("Product not found");

            return NoContent();
        }
    }
}
