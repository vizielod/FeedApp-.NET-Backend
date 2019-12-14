using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FeedApp.Api.Dtos;
using FeedApp.Bll.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/UserInfo")]
    public class UserInfoController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IMapper _mapper;

        public UserInfoController(IUserInfoService userInfoService, IMapper mapper)
        {
            _userInfoService = userInfoService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<UserInfo>>(_userInfoService.GetUserInfos()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<UserInfo>(_userInfoService.GetUserInfo(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserInfo userInfo)
        {
            var created = _userInfoService.InsertUserInfo(_mapper.Map<Bll.Entities.UserInfo>(userInfo));
            return CreatedAtAction(nameof(Get), new { created.ID }, _mapper.Map<UserInfo>(created));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userInfoService.DeleteUserInfo(id);
            return NoContent();
        }        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserInfo userInfo)
        {
            _userInfoService.UpdateUserInfo(id, _mapper.Map<Bll.Entities.UserInfo>(userInfo));
            return NoContent();
        }
    }
}