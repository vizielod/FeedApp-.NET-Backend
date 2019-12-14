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
        private readonly ProductService _productService;

        public ProductController()
        {
            // TODO create product service
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
        public Product Post([FromBody]Product product)
        {
            return _productService.InsertProduct(product);
        }
    }
}
