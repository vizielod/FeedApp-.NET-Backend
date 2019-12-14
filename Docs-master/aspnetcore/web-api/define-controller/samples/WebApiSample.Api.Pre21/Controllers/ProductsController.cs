﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiSample.DataAccess.Models;
using WebApiSample.DataAccess.Repositories;

namespace WebApiSample.Api.Pre21.Controllers
{
    #region snippet_ControllerSignature
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    #endregion
    {
        private readonly ProductsRepository _repository;

        public ProductsController(ProductsRepository repository)
        {
            _repository = repository;
        }

        #region snippet_GetById
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            if (!_repository.TryGetProduct(id, out var product))
            {
                return NotFound();
            }

            return Ok(product);
        }
        #endregion

        #region snippet_BindingSourceAttributes
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), 200)]
        public IActionResult Get([FromQuery] bool discontinuedOnly = false)
        {
            List<Product> products = null;

            if (discontinuedOnly)
            {
                products = _repository.GetDiscontinuedProducts();
            }
            else
            {
                products = _repository.GetProducts();
            }

            return Ok(products);
        }
        #endregion

        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddProductAsync(product);

            return CreatedAtAction(nameof(GetById),
                new { id = product.Id }, product);
        }
    }
}