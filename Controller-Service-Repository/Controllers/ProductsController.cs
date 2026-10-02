using Controller_Service_Repository.Entities;
using Controller_Service_Repository.Models.DTOs.Requests;
using Controller_Service_Repository.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Controller_Service_Repository.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private ProductService _service = new ProductService();

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _service.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductForCreateDto dto)
        {
            var product = _service.CreateProduct(dto);
            if (product == null)
            {
                return Conflict("Ya existe un producto con ese nombre.");
            }

            return Created($"/api/products/{product.Id}", product);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var productExist = _service.GetProductById(id);
            if (productExist == null)
            {
                return NotFound();
            }
            _service.UpdateProduct(id, dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productExist = _service.GetProductById(id);
            if(productExist == null)
            {
                return NotFound();
            }
            _service.DeleteProduct(id);
            return NoContent();
        }
        [HttpGet("search")]
        public IActionResult SearchProductByName(string name)
        {
            var products = _service.SearchProductsByName(name);
            return Ok(products);
        }
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var stats = _service.GetStats();
            return Ok(stats);
        }
    }
}
