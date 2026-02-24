using Microsoft.AspNetCore.Mvc;
using FirstAPI.Models;
using FirstAPI.Services;
using AutoMapper;
using FirstAPI.DTOs;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        { 
            var products = _service.GetAll();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos); 
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);

            if (product == null)
                return NotFound("Product Not Found");

            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Name))
                return BadRequest("Product name is required.");
            if (createDto.Price <= 0)
                return BadRequest("Price must be greater than zero.");

            var product = _mapper.Map<Product>(createDto);
            var createdProduct = _service.Create(product);

            var createdProductDto = _mapper.Map<ProductDto>(createdProduct);

            return CreatedAtAction(nameof(GetById), new { id = createdProductDto.Id }, createdProductDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateProductDto updateDto)
        {
            var existingProduct = _service.GetById(id);

            if (existingProduct == null)
                return NotFound("Product Not Found");

            _mapper.Map(updateDto, existingProduct);
            var sucess = _service.Update(id, existingProduct);

            if (!sucess)
                return NotFound("Product Not Found");

            var productDto = _mapper.Map<ProductDto>(existingProduct);

            return Ok(productDto);
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
