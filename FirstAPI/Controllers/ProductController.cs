using Microsoft.AspNetCore.Mvc;
using FirstAPI.Models;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> products = [];

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product Not Found");

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product == null)
                return BadRequest("Product cannot be null");
            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Product Name is required");
            if (product.Price <= 0)
                return BadRequest("Price must be greater than zero");

            product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;

            products.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest("Request body cannot be null");
            if (string.IsNullOrWhiteSpace(updatedProduct.Name))
                return BadRequest("Product name is required");
            if (updatedProduct.Price <= 0)
                return BadRequest("Price must be geater than zero");

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound("Product Not Found");

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => id == p.Id);

            if (product == null)
                return NotFound("Product not found");

            products.Remove(product);

            return NoContent();
        }
    }
}
