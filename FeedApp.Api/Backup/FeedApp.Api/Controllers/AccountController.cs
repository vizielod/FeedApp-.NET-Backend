using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedApp.Api.Dtos;
using FeedApp.Bll.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Dtos.User userDto)
        {
            var user = new ApplicationUser
            { UserName = userDto.UserName, Email = userDto.Email };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            return Ok();
        }

    }
}