using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiSample.DataAccess.Models;
using WebApiSample.DataAccess.Repositories;

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsRepository _repository;

        public ProductsController(ProductsRepository repository)
        {
            _repository = repository;
        }

        #region snippet_Get
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _repository.GetProducts();
        }
        #endregion

        #region snippet_GetById
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Product> GetById(int id)
        {
            if (!_repository.TryGetProduct(id, out var product))
            {
                return NotFound();
            }

            return product;
        }
        #endregion

        #region snippet_CreateAsync
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Product>> CreateAsync(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddProductAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        #endregion
    }
}