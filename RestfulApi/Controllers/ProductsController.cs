//Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using MyWebApi.Services;
namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IProductService _productService;

        public ProductsController(IProductRepository repository, IProductService productService)
        {
            _repository = repository;
            _productService = productService;
        }
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

        // PUT: api/products/1   UPDATE METHOD
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

       [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> Search([FromQuery] string? name, [FromQuery] string orderBy = "Id", [FromQuery] bool ascending = true, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var products = _productService.SearchProducts(name, orderBy, ascending, page, pageSize);
                return products.ToList();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Updates a product partially.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /Product
        ///     [
        ///       {
        ///         "op": "replace",
        ///         "path": "/Name",
        ///         "value": "New Product Name"
        ///       }
        ///     ]
        ///
        /// </remarks>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var product = _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _repository.ApplyPatch(id, patchDoc);
                return NoContent();
            }
            catch (Exception ex)  // Catch more specific exceptions if possible.
            {
                // Log the exception and return an error response.
                return BadRequest(ex.Message);
            }
        }


    }
}
