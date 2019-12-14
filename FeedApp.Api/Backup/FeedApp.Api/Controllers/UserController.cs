using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
//using FeedApp.Bll.Entities;
using FeedApp.Api.Dtos;
using FeedApp.Bll.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<Bll.Entities.ApplicationUser> _userManager;

        public UserController(IUserService userService, IMapper mapper, UserManager<Bll.Entities.ApplicationUser> userManager)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<User>>(_userService.GetUsers()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<User>(_userService.GetUser(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User userDto)
        {
            Bll.Entities.ApplicationUser user = new Bll.Entities.ApplicationUser
            { UserName = userDto.UserName, Email = userDto.Email , UserID = userDto.ID };
            var result = await _userManager.CreateAsync(user, userDto.Password);

            var created = _userService.InsertUser(_mapper.Map<Bll.Entities.User>(userDto));//Itt leküldi az InsertUser metódusnak, amihez vissza kell mappelni a Bll beli user-nek      
            return CreatedAtAction(nameof(Get), new { created.ID }, _mapper.Map<User>(created));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]User User)
        {
            _userService.UpdateUser(id, _mapper.Map<Bll.Entities.User>(User));
            return NoContent();
        }
    }
}
