using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
//using FeedApp.Bll.Entities;
using FeedApp.Api.Dtos;
using FeedApp.Bll.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Food")]
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IMapper _mapper;

        public FoodController(IFoodService foodService, IMapper mapper)
        {
            _foodService = foodService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<Food>>(_foodService.GetFoods()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<Food>(_foodService.GetFood(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Food food)
        {
            var created = _foodService.InsertFood(_mapper.Map<Bll.Entities.Food>(food));
            return CreatedAtAction(nameof(Get), new { created.ID }, _mapper.Map<Food>(created));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _foodService.DeleteFood(id);
            return NoContent();
        }        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Food food)
        {
            _foodService.UpdateFood(id, _mapper.Map<Bll.Entities.Food>(food));
            return NoContent();
        }
    }
}