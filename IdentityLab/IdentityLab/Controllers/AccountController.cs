using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityLab.Dtos;
using IdentityLab.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityLab.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterUserDTO registerUserDTO)
        {
            var user = new ApplicationUser { UserName = registerUserDTO.UserName, Email = registerUserDTO.Email };
            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);
            return Ok();
        }
    }
}