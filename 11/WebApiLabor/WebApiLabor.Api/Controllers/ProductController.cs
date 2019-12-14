using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLabor.Api.Dtos;
using WebApiLabor.Bll.Context;
using WebApiLabor.Bll.Services;

namespace WebApiLabor.Api.Controllers
{
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper )
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<Product>>(_productService.GetProducts()));
        }


        /// <summary>
        /// Get a specific product with the given identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>Returns a specific product with the given identifier.</returns>
        /// <response code="200">Returns a specific product with the given identifier.</response>
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<Product>(_productService.GetProduct(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            var created = _productService.InsertProduct(_mapper.Map<Bll.Entities.Product>(product));
            return CreatedAtAction(nameof(Get),new { created.Id }, created);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            _productService.UpdateProduct(id, _mapper.Map<Bll.Entities.Product>(product));
            return NoContent();
        }
    }
}
