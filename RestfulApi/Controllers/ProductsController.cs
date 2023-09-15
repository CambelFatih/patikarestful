using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using MyWebApi.Repositories;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly JsonProductRepository _repository = new JsonProductRepository();

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        // GET: api/products/1
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            var createdProduct = _repository.Create(product);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/products/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _repository.Update(id, product);
            return NoContent();
        }

        // DELETE: api/products/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }

        // GET: api/products/list?name=something
        [HttpGet("list")]
        public ActionResult<IEnumerable<Product>> GetFiltered([FromQuery] string? name)
        {
            return GetFilteredProducts(name).ToList();
        }

        private IEnumerable<Product> GetFilteredProducts(string? name)
        {
            var products = _repository.GetAll();

            if (string.IsNullOrEmpty(name))
                return products;

            return products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
