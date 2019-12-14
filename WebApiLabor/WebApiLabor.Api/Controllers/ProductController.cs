using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLabor.Bll.Context;
using WebApiLabor.Bll.Entities;
using WebApiLabor.Bll.Services;

namespace WebApiLabor.Api.Controllers
{
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)//Dependency injection
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_productService.GetProduct(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            var created = _productService.InsertProduct(product);
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
            _productService.UpdateProduct(id, product);
            return NoContent();
        }
    }
}
