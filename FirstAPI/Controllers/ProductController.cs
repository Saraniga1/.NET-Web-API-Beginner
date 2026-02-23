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
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            if (product == null)
                return NotFound("Product Not Found");

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Product Name is required");
            if (product.Price <= 0)
                return BadRequest("Price must be greater than zero");

            var createdProduct = _service.Create(product);

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updatedProduct)
        {

            if (string.IsNullOrWhiteSpace(updatedProduct.Name))
                return BadRequest("Product name is required");
            if (updatedProduct.Price <= 0)
                return BadRequest("Price must be geater than zero");

            var sucess = _service.Update(id, updatedProduct);

            if (!sucess)
                return NotFound("Product Not Found");

            return Ok(updatedProduct);
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
