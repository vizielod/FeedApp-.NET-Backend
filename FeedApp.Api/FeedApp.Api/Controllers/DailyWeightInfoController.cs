using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FeedApp.Bll.Services;
using FeedApp.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/DailyWeightInfo")]
    public class DailyWeightInfoController : Controller
    {
        private readonly IDailyWeightInfoService _dailyWeightInfoService;
        private readonly IMapper _mapper;

        public DailyWeightInfoController(IDailyWeightInfoService dailyWeightInfoService, IMapper mapper)
        {
            _dailyWeightInfoService = dailyWeightInfoService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<DailyWeightInfo>>(_dailyWeightInfoService.GetDailyWeightInfos()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<DailyWeightInfo>(_dailyWeightInfoService.GetDailyWeightInfo(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody]DailyWeightInfo dailyWeightInfo)
        {
            var created = _dailyWeightInfoService.InsertDailyWeightInfo(_mapper.Map<Bll.Entities.DailyWeightInfo>(dailyWeightInfo));
            return CreatedAtAction(nameof(Get), new { created.ID }, _mapper.Map<DailyWeightInfo>(created));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dailyWeightInfoService.DeleteDailyWeightInfo(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]DailyWeightInfo dailyWeightInfo)
        {
            _dailyWeightInfoService.UpdateDailyWeightInfo(id, _mapper.Map<Bll.Entities.DailyWeightInfo>(dailyWeightInfo));
            return NoContent();
        }
    }
}