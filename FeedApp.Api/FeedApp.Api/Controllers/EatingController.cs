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
    [Route("api/Eating")]
    public class EatingController : Controller
    {
        private readonly IEatingService _eatingService;
        private readonly IMapper _mapper;

        public EatingController(IEatingService eatingService, IMapper mapper)
        {
            _eatingService = eatingService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<Eating>>(_eatingService.GetEatings()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<Eating>(_eatingService.GetEating(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Eating eating)
        {
            var created = _eatingService.InsertEating(_mapper.Map<Bll.Entities.Eating>(eating));
            return CreatedAtAction(nameof(Get), new { created.ID }, _mapper.Map<Eating>(created));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _eatingService.DeleteEating(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Eating eating)
        {
            _eatingService.UpdateEating(id, _mapper.Map<Bll.Entities.Eating>(eating));
            return NoContent();
        }
    }
}