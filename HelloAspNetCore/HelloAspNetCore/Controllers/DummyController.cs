using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloAspNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Dummy")]
    public class DummyController : Controller
    {
        // GET: api/Dummy
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NullReferenceException();
            return new string[] { "value1", "value2" };
        }

        // GET: api/Dummy/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Dummy
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Dummy/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("/error")]
        public IActionResult Index()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Hibaa");
        }
    }
}
